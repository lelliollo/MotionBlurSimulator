using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics; //This .NET framework library implements complex algebra 
using SdS = Microsoft.Research.Science.Data; //You should deposit the library handles in a dedicated variable since some class constructors have the same name as system library classes
using Microsoft.Research.Science.Data.Imperative; //This contains the commands for the definitions of SdS
using FFTW.NET; //Super power FFT engine for frequency domain calculations

namespace MotionBlurSimulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SelectParThNumb.Value = 3;
            parPool.MaxDegreeOfParallelism = 3;//Setup parallel pool
            NotificationTextBox.Text = "Program Started \n";
            MB_alpha_input.Text = "30";
            MB_W_Input.Text = "2";
        }

        #region Declaring shared variables
        private Bitmap LdImg; //Loaded Image
        private Bitmap GscImg; //Grayscale bitmap
        private Double[,] ImgMatrix; //Double matrix of the original image
        private Bitmap BlrdImage; //Blurred image
        private Double[,] BlrdImgMatrix;  //Blurred image double matrix
        private Double MB_Width; //Shared variables for the MB parameters
        private Double MB_Dir;
        private static String m1ex = "        "; 
        ParallelOptions parPool = new ParallelOptions();//Just a string constant to fill spaces                                               

        #endregion

        #region GUI program

        ///<summary>
        ///Caller for loading images and transform them into matrices
        /// </summary>
        private void LoadImageButton_Click(object sender, EventArgs e)
        {

            if (LoadImageDlg.ShowDialog()==DialogResult.OK)
            {
                NotificationTextBox.Text = "Program Started \n";
                NotificationTextBox.Text = NotificationTextBox.Text + "==================\nLoad image called \n";
                //Load Image
                LdImg = new Bitmap(LoadImageDlg.FileName);
                NotificationTextBox.Text = NotificationTextBox.Text + m1ex + "Image " + LoadImageDlg.FileName + " has been loaded \n";
                int Tpix = LdImg.Width * LdImg.Height;
                NotificationTextBox.Text = NotificationTextBox.Text + m1ex + "Image size: " + LdImg.Size.ToString() + " the total pixel count is " + Tpix.ToString() + "\n";
                //RGB 2 Gray
                GscImg = Im_RGB2Grayscale(LdImg);
                NotificationTextBox.Text = NotificationTextBox.Text + m1ex + "Image succesfully converted to grayscale \n";
                //Convert image to double matrix
                ImgMatrix = Mat_Gray2Double(GscImg);
                NotificationTextBox.Text = NotificationTextBox.Text + m1ex + "Image succesfully converted to double matrix \n";
                //Display Image
                OrigImPicBox.Image = GscImg;
                OrigImPicBox.SizeMode = PictureBoxSizeMode.Zoom;
                //Save memory
                LdImg.Dispose();

                //Choose best wisdom for FFTW
                if (Tpix<2e6)
                {
                    DFT.Wisdom.Import("1MPX_wisdom.txt");
                }
                if (Tpix>2e6 && Tpix<6e6)
                {
                    DFT.Wisdom.Import("2MPX_wisdom.txt");
                }
                if (Tpix > 6e6)
                {
                    DFT.Wisdom.Import("6MPX_wisdom.txt");
                }

            }
        }

        ///<summary>
        ///Caller to export the image matrix to a netcdf binary
        /// </summary>
        private void SaveImMatrixButton_Click(object sender, EventArgs e)
        {
            NotificationTextBox.Text = NotificationTextBox.Text + "==================\nSave image matrix called \n";
            //Open CDF dump
            SdS.DataSet SaveMatNETCDFHandle = SdS.DataSet.Open("saved_matrix.nc?openMode=create");
            //Create CDF dataset
            SaveMatNETCDFHandle.Add<double[,]>("Image matrix double");
            //Write data and close
            SaveMatNETCDFHandle.PutData<double[,]>("Image matrix double", ImgMatrix);
            SaveMatNETCDFHandle.Dispose();
            NotificationTextBox.Text = NotificationTextBox.Text + m1ex + "Matrix succesfully exported to netCDF file saved_matrix.nc \n";
        }

        ///<summary>
        ///Caller to start the motion blur simulation routine
        /// </summary>
        private void ComputeMBButton_Click(object sender, EventArgs e)
        {
            NotificationTextBox.Text = NotificationTextBox.Text + "==================\nMotion blur simulation started \n";
            //Parse the input parameter boxes
            Double.TryParse(MB_W_Input.Text, out MB_Width);
            Double.TryParse(MB_alpha_input.Text, out MB_Dir);
            NotificationTextBox.Text = NotificationTextBox.Text + m1ex + "Input motion blur W = " + MB_Width.ToString() + " px. Input motion blur alpha = " + MB_Dir.ToString() + " deg. \n";
            //Start simulation and count time
            var watch = System.Diagnostics.Stopwatch.StartNew();
            BlrdImgMatrix = SimulateMotionBlurRoutine(ImgMatrix, MB_Width, MB_Dir,ParallelSelect.Checked);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            NotificationTextBox.Text = NotificationTextBox.Text + m1ex +  "MB succesfully computed in " + elapsedMs.ToString() + " ms \n";
            //Cast output of motion blur simulation to a bitmap
            BlrdImage = Double2Gray(BlrdImgMatrix);
            BlurdImgPicBox.Image = BlrdImage;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        ///<summary>
        ///Caller to save the blurred image into an image file format 
        /// </summary>
        private void SaveBlrdImgButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                BlrdImage.Save(saveFileDialog1.FileName);
            }
        }

        private void SelectParThNumb_ValueChanged(object sender, EventArgs e)
        {
            parPool.MaxDegreeOfParallelism = (int)SelectParThNumb.Value;
        }

        #endregion

        #region Subroutines to convert images into double matrices (back and forth)

        ///<summary>
        ///Converts the pixel color to luminance by averaging hue
        /// </summary>
        /// <param name="color">The RGB pixel value</param>
        /// <returns>A grayscale pixel value</returns>
        private static Color ExtractPixelLum(Color color) 
        {
            var level = (byte)((color.R + color.G + color.B) / 3);
            var result = Color.FromArgb(level, level, level);
            return result;
        }

        ///<summary>
        ///Function to convert a RGB bitmap into a grayscale bitmap (but still 3 channels image)
        /// </summary>
        /// <param name="bitmap">The image to be converted</param>
        /// <returns>The grayscale bitmap</returns>
        private static Bitmap Im_RGB2Grayscale(Bitmap bitmap) 
        {
            var result = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var grayColor = ExtractPixelLum(bitmap.GetPixel(x, y));
                    result.SetPixel(x, y, grayColor);
                }
            return result;
        }

        ///<summary>
        ///Converts a grayscale bitmap into a matrix of double 0->0.0 255->1.0
        /// </summary>
        /// <param name="bitmap">The grayscale bitmap</param>
        /// <returns>The double matrix</returns>
        private static double[,] Mat_Gray2Double(Bitmap bitmap)  
        {
            var result = new double[bitmap.Width, bitmap.Height];
            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                    result[x, y] = (double)bitmap.GetPixel(x, y).R / 255.0;
            return result;
        }

        ///<summary>
        ///Converts a matrix double image into a grayscale bitmap
        /// </summary>
        /// <param name="imMat">Image double matrix</param>
        /// <returns>The grayscale bitmap</returns>
        private static Bitmap Double2Gray(double[,] imMat)
        {
            int m = imMat.GetLength(0);
            int n = imMat.GetLength(1);
            Bitmap result = new Bitmap(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double lval = 255 * imMat[i, j];
                    if (lval>=255.0)
                    {
                        lval = 255;
                    };
                    if (lval<=0)
                    {
                        lval = 0;
                    };
                    var loc = (byte)lval;
                    var cpix = Color.FromArgb(loc, loc, loc);
                    result.SetPixel(i, j, cpix);
                }
            }
            return result;
        }

        #endregion

        #region FFTW workaround functions

        ///<summary>
        ///This routine casts the image to a format suitable for the FFTW engine
        /// </summary>
        /// <param name="im">Image double matrix to be cast to FTTW format</param>
        /// <returns>A pointer to an AlignedArrayComplex suitable for FFTW engine</returns>
        private static AlignedArrayComplex CastImageToFFTW(double[,] im)
        {
            int m = im.GetLength(0);
            int n = im.GetLength(1);
            var ImFFTW = new AlignedArrayComplex(16, m, n);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ImFFTW[i, j] = im[i, j];
                }
            }
            return ImFFTW;
        }

        ///<summary>
        ///This routine casts the output of FFTW engine to a Complex matrix, so the spectrum can be saved
        ///and manipulated
        /// </summary>
        /// <param name="FFTOutput">The FFT spectrum output in FFTW format</param>
        /// <returns>Spectrum as matrix of double</returns>
        private static Complex[,] CastFFTWToComplexSpectrum(AlignedArrayComplex FFTOutput)
        {
            int m = FFTOutput.GetLength(0);
            int n = FFTOutput.GetLength(1);
            double N_spe = (double)m * n;
            Complex[,] spectrum = new Complex[m, n]; //Initialize spectrum
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    spectrum[i, j] = new Complex(FFTOutput[i, j].Real / N_spe, FFTOutput[i, j].Imaginary / N_spe);
                }
            }

            return spectrum;
        }

        ///<summary>
        ///This routine casts the Fourier transform complex array to a format suitable for the FFTW engine
        /// </summary>
        /// <param name="Y">Spectrum to be sent to FFTW</param>
        /// <returns>A pointer to an AlignedArrayComplex suitable for FFTW engine</returns>
        private static AlignedArrayComplex CastSpectrumToFFTW(Complex[,] Y)
        {
            int m = Y.GetLength(0);
            int n = Y.GetLength(1);
            double S = (double)m * n;
            var FFTWHandle = new AlignedArrayComplex(16, m, n);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    FFTWHandle[i, j] = Y[i, j] * S;
                }
            }
            return FFTWHandle;

        }

        /// <summary>
        /// Function to extract data from Spectrum. You input the complex spectrum and then you can extract
        /// Magnitude, Phase, Real and Imag 
        /// </summary>
        /// <param name="X">Complex spectrum matrix</param>
        /// <param name="command">integer to select what you are interested in: 1 - Mag, 2 - Phase, 3 - Re, 4- Imag</param>
        private static double[,] ExtractSpectrumData(Complex[,] X, int command)
        {
            //Initialize variables
            int m;
            int n;
            m = X.GetLength(0);
            n = X.GetLength(1);
            double[,] Y = new double[m, n];

            //Do extrapolation
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    switch (command)
                    {
                        case 1:
                            //Magnitude calculation
                            Y[i, j] = X[i, j].Magnitude;
                            break;
                        case 2:
                            //Phase calculation
                            Y[i, j] = X[i, j].Phase;
                            break;
                        case 3:
                            //Real part
                            Y[i, j] = X[i, j].Real;
                            break;
                        case 4:
                            //Imag part
                            Y[i, j] = X[i, j].Real;
                            break;
                        default:
                            Y[i, j] = X[i, j].Magnitude;
                            break;
                    }
                }
            }
            return Y;
        }
        
        /// <summary>
        /// Very useful routine to export the 2D spectrum to a NetCDF binary (yes I hate csv text file!)
        /// </summary>
        /// <param name="FourierTransform">The complex matrix containing the spectrum</param>
        /// <param name="filename">String containing the file name</param>
        private static void ExportSpectrum2NetCDF(Complex [,] FourierTransform, string filename)
        {
            double[,] RealPart;
            double[,] ImagPart;
            double[,] Mag;
            double[,] Phase;

            RealPart = ExtractSpectrumData(FourierTransform, 3);
            ImagPart = ExtractSpectrumData(FourierTransform, 4);
            Mag = ExtractSpectrumData(FourierTransform, 1);
            Phase = ExtractSpectrumData(FourierTransform, 2);
            //Now save to netcdf
            SdS.DataSet SaveFTNETCDFHandle = SdS.DataSet.Open(filename+"?openMode=create");
            //Create database
            SaveFTNETCDFHandle.Add<double[,]>("Real");
            SaveFTNETCDFHandle.Add<double[,]>("Imag");
            SaveFTNETCDFHandle.Add<double[,]>("Mag");
            SaveFTNETCDFHandle.Add<double[,]>("Phase");
            //Dump Data
            SaveFTNETCDFHandle.PutData<double[,]>("Real", RealPart);
            SaveFTNETCDFHandle.PutData<double[,]>("Imag", ImagPart);
            SaveFTNETCDFHandle.PutData<double[,]>("Mag", Mag);
            SaveFTNETCDFHandle.PutData<double[,]>("Phase", Phase);
            //Close database
            SaveFTNETCDFHandle.Dispose();
        }

        #endregion

        #region Motion blur simulation routines

        /// <summary>
        /// Routine to simulate motion blur, given the amount of motion blur (it can be any real number!)
        /// and the direction of motion blur.
        /// </summary>
        /// <param name="im">Input double matrix image</param>
        /// <param name="W">Amount of motion blur i.e. 1.3</param>
        /// <param name="alpha">Motion blur direction angle (in degrees) - clockwise</param>
        /// <param name="Parallelize">Boolean to state if you want to parallelize spectral convolution</param>
        /// <returns>The blurred image as double matrix</returns>
        private static double [,] SimulateMotionBlurRoutine(double [,] im,double W, double alpha, bool Parallelize)
        {
            int m = im.GetLength(0);
            int n = im.GetLength(1);
            double[,] outputMat = new double[m, n];
            //Initialize FFTW arrays
            var ImgFFTWInput = CastImageToFFTW(im);
            AlignedArrayComplex FFTWOutputArray = new AlignedArrayComplex(16, ImgFFTWInput.GetSize());
            AlignedArrayComplex FFTWConvOutputArray = new AlignedArrayComplex(16, ImgFFTWInput.GetSize());
            //Step 1 - Compute image spectrum
            DFT.FFT(ImgFFTWInput, FFTWOutputArray);
            var ImSpectrum = CastFFTWToComplexSpectrum(FFTWOutputArray);
            //Step 2 - Generate Optical Transfer function (OTF)
            var GridU = GenerateFFTMeshGrid(m, n, 1);
            var GridV = GenerateFFTMeshGrid(m, n, 2);
            Complex[,] MB_OTF = new Complex[m, n]; //Initialize OTF
            Double f_indx = new double(); //kernel effective frequency
            for (int u = 0; u < m; u++)
            {
                for (int v = 0; v < n; v++)
                {
                    double f_u = GridU[u, v] / m;
                    double f_v= GridV[u, v] / n;
                    f_indx = Math.Cos(alpha / 180 * Math.PI) *f_u + Math.Sin(alpha / 180 * Math.PI) * f_v;
                    MB_OTF[u, v] = new Complex(Sinc(W * f_indx), 0);
                }
            }
            //Step 3 - Spectral convolution
            Complex[,] SpecConvolution = new Complex[m, n];
            if (Parallelize)
            {
                Parallel.For(0, m, u =>
                {
                    for (int v = 0; v < n; v++)
                    {
                        SpecConvolution[u, v] = Complex.Multiply(MB_OTF[u, v], ImSpectrum[u, v]);
                    }
                });
            }
            else
            {
                for (int u = 0; u < m; u++)
                {
                    for (int v = 0; v < n; v++)
                    {
                        SpecConvolution[u, v] = Complex.Multiply(MB_OTF[u, v], ImSpectrum[u, v]);
                    }
                }
            }
            //Step 4 - Inverse fourier transform to get the blurred image
            var FFTWSpecConvolution = CastSpectrumToFFTW(SpecConvolution);
            DFT.IFFT(FFTWSpecConvolution, FFTWConvOutputArray);
            var ConvOutputArray = CastFFTWToComplexSpectrum(FFTWConvOutputArray); //Just doing this to get the real part of IFFT
            if (Parallelize)
            {
                Parallel.For(0, m, u =>
                {
                    for (int v = 0; v < n; v++)
                    {
                        outputMat[u, v] = ConvOutputArray[u, v].Magnitude;
                    }
                });
            }
            else
            {
                for (int u = 0; u < m; u++)
                {
                    for (int v = 0; v < n; v++)
                    {
                        outputMat[u, v] = ConvOutputArray[u, v].Magnitude;
                    }
                }
            }
            //Print wisdom
            DFT.Wisdom.Export("exp_wis.txt");
            //Try to clear out as much memory as I can
            FFTWOutputArray.Dispose();
            ImgFFTWInput.Dispose();
            FFTWConvOutputArray.Dispose();
            FFTWOutputArray = null;
            ImgFFTWInput = null;
            FFTWConvOutputArray = null;
            return outputMat;
        }

        ///<summary>
        ///Function that evaluates the sinc(x) function ---> sin(pi*x)/(pi*x)
        /// </summary>
        /// <param name="x">The value at which you want to compute sinc</param>
        private static double Sinc(double x)
        {
            double result = new double();
            if (x==0)
            {
                result = 1;
            }
            else
            {
               result = Math.Sin(Math.PI * x) / (Math.PI * x);
            }
            
            return result;
        }

        /// <summary>
        /// Computes the harmonic meshgrid as they are used in FFT routine
        /// </summary>
        /// <param name="m">number of rows</param>
        /// <param name="n">number of columns</param>
        /// <param name="command">Integer to select if you want meshgrid for (1) rows armonic or (2) column harmonics</param>
        /// <returns>The meshgrid as matrix of double</returns>
        private static double[,] GenerateFFTMeshGrid(int m, int n,int command)
        {
            double [,] Grid = new double[m, n];
            var RowScan = new int[m];
            var ColScan = new int[n];

            switch (command)
            {
                //Here you ask for ROW meshgrid
                case 1:
                    //Generate Row Scanning
                    if (IsOdd(m))
                    {
                        for (int i = 0; i < (m - 1) / 2; i++)
                        {
                            RowScan[i] = i+1;
                        }
                        for (int j = ((m - 1) / 2); j < m; j++)
                        {
                            RowScan[j] = -(m-j)+1;
                        }
                    }
                    else
                    {
                        for (int i = 0; i <  m / 2; i++)
                        {
                            RowScan[i] = i+1;
                        }
                        for (int j = ( m / 2); j < m; j++)
                        {
                            RowScan[j] = -(m  - j)+1;
                        }
                    }
                    //Now cycle to get grid
                    for (int i = 0; i < m; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Grid[i, j] = RowScan[i];
                        }
                    }
                    break;
                //Here you ask for COLUMN meshgrid
                case 2:
                    //Generate Column Scanning
                    if (IsOdd(n))
                    {
                        for (int i = 0; i < (n - 1) / 2; i++)
                        {
                            ColScan[i] = i + 1;
                        }
                        for (int j = ((n - 1) / 2); j < n; j++)
                        {
                            ColScan[j] = -((n - j))+1;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < n / 2; i++)
                        {
                            ColScan[i] = i+1;
                        }
                        for (int j = (n / 2); j < n; j++)
                        {
                            ColScan[j] = -(n - j)+1;
                        }
                    }
                    //Now cycle to get grid
                    for (int i = 0; i < m; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Grid[i, j] = ColScan[j];
                        }
                    }
                    break;
                default:
                    throw new System.ArgumentException("Command value can be only 1 or 2", "command");
            }
            return Grid;
        }

        ///<summary>
        ///Check if a number is odd or even
        /// </summary>
        /// <param name="value">Number to be checked</param>
        /// <returns>A boolean true/false in case the number is odd/even</returns>
        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        #endregion

    }
}

