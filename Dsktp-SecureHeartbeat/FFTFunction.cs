using System;

namespace Dsktp_SecureHeartbeat
{
    public class FFTFunction
    {
        private RetrieveWav getWavInst;
        private FFTFrameAnalysis fftFrameAnalysisInst = new FFTFrameAnalysis();

        private short[] getSoundAsShortByteArray;
        private const int FrameLength = 320; //length in bytes of each frame extracted from sound file
        private const int PaddedFrameLength = 512;
        private const int NumberOfBytesPerShortIndex = 2; // number of bits represented by each index in the short array holding the sound wave
        private int frameHead;

        private static int magnitudeSpectrum;
        public static int MagnitudeSpectrum
        {
            get
            {
                return magnitudeSpectrum;
            }
        }

        // [size of each frame/window] 320 bytes  /2 [] = 160 short values array size
        private const int NumOfShortValuesInFrame = FrameLength/NumberOfBytesPerShortIndex;

        //256 short values array size
        private const int PaddedNumOfValues = PaddedFrameLength/NumberOfBytesPerShortIndex;
        


        private short[] currentFrame;

        // (Total Length - framelength) / (0.5*framelength) + 1
        private static int numberOfFrames;

        public static int NumberOfFrames
        {
            get
            {
                return numberOfFrames;
            }
        }

        private double [,] fftresults;

        // Size of the running window to provide smoothness of the noise removal
        private int windowSpan = 5;
        // Commonly used over subtraction factor from theory in the field of noise removal
        private double alpha = 1.1;
        // float beta = 0.079432823 / 1.079432823 = Maximum  attenuation corresponding to a priori SNR (Sound to Noise Ratio) of -11db                   
        private double beta = 0.3; 

        public FFTFunction()
        {
            magnitudeSpectrum = fftFrameAnalysisInst.GetMagnitudeSpectrumSize();
            currentFrame = new short[PaddedNumOfValues];
        }

        public double[,] PerformFftFunction(String soundFileName)
        {
            //With parse impl pass in File name
            getWavInst = new RetrieveWav(soundFileName);
            getSoundAsShortByteArray = getWavInst.getWavFile();

            numberOfFrames = ((int)Math.Floor((getSoundAsShortByteArray.Length - NumOfShortValuesInFrame) / (NumOfShortValuesInFrame * 0.5))) + 1;
            fftresults = new double[numberOfFrames, magnitudeSpectrum];

            for (var i = 0; i < numberOfFrames; i++)
            {
                // Frame windowing moves up 80 short values or 160 bytes at a time
                frameHead = i * (NumOfShortValuesInFrame / 2);

                // retrieves 320 bytes from the current frame highligthed within the sound file array
                for (var k = 0; k < NumOfShortValuesInFrame; k++)
                {
                    currentFrame[k] = getSoundAsShortByteArray[frameHead + k];
                }

                // pads the rest of the frame with zeros to make 512 bytes - base 2 size needed to perform FFT effeciently
                for (var k = NumOfShortValuesInFrame; k < PaddedNumOfValues; k++)
                {
                    currentFrame[k] = 0;
                }

                // windowing applied within the FFT Frame anaylsis function  
                var fftFrame = fftFrameAnalysisInst.FastFourierTransformAnalysis(currentFrame);
                for (var j = 0; j < magnitudeSpectrum; j++)
                {
                    fftresults[i, j] = fftFrame[j];
                }
            }

            return fftresults;
        }

        /// <summary>
        /// Obtain a noise estimate by averaging the sound of the intial 20 frames/ 0.22 seconds using the assumtion it will be 
        /// composed pruely of background noise. Then using a running average over the sound to attempt to obtain an estimate of 
        /// the voice signal PSD (Power spectral density) by 'removing' the noise psd from the sound. Running average used to try 
        /// and smooth out the results when working in the PSD domain which tend to give unreadable results.
        /// </summary>
        /// <param name="magnitudeArray">The FFT'd results that need to be filtered</param>
        /// <returns></returns>
        public double[,] PerformWienerFilter(double[,] magnitudeArray)
        {
            var initalNoiseLength = 20;
            var filteredArray = new double[magnitudeArray.GetLength(0), magnitudeArray.GetLength(1)];
            var noiseEstimate = new double[magnitudeArray.GetLength(1)];

            for (var t = 0; t < initalNoiseLength; t++)
            {
                for (var k = 0; k < magnitudeArray.GetLength(1); k++)
                {
                    noiseEstimate[k] += magnitudeArray[t, k];
                }
            }   


            for (var k = 0; k < magnitudeArray.GetLength(1); k++)
            {
                noiseEstimate[k] /= initalNoiseLength;
            }

            for (var k = 0; k < magnitudeArray.GetLength(1); k++)
            {
                for (var t = 0; t < magnitudeArray.GetLength(0); t++)
                {
                    // running window
                    int lowerBoundWindow = t - windowSpan, upperBoundWindow = t + windowSpan;
                    while (lowerBoundWindow < 0)
                    {
                        lowerBoundWindow++;
                        upperBoundWindow++;
                    }
                    while (upperBoundWindow >= magnitudeArray.GetLength(0))
                    {
                        upperBoundWindow--;
                        lowerBoundWindow--;
                    }
                    var actualWindowSpan = upperBoundWindow - lowerBoundWindow + 1;

                    // running average input periodgram
                    var averageMagnitude = 0.0;
                    for (var q = lowerBoundWindow; q <= upperBoundWindow; q++)
                    {
                        averageMagnitude += magnitudeArray[q,k]*magnitudeArray[q,k];
                    }
                    averageMagnitude /= actualWindowSpan;

                    // noise psd
                    var noisePsd = noiseEstimate[k]*noiseEstimate[k];

                    // signal psd via spectral subtraction
                    var signalPsd = averageMagnitude - alpha*noisePsd;

                    // set the strength of the filter weight to apply
                    var filterStrength = 1.0;
                    if (averageMagnitude > 0.0)
                    {
                        filterStrength = signalPsd / averageMagnitude;
                    }
                    if (filterStrength < beta)
                    {
                        filterStrength = beta;
                    }


                    // filtering applied to the magnitude sound file represenation input
                    filteredArray[t, k] = filterStrength * magnitudeArray[t, k];
                }
            }

            return filteredArray;
        }

        public double[,] PerformLogOfResults(double [,] spectrumArray)
        {
            var logSpectrumArray = new double[spectrumArray.GetLength(0), spectrumArray.GetLength(1)];
            
            for (var t = 0; t < spectrumArray.GetLength(0); t++)
            {
                for (var k = 0; k < spectrumArray.GetLength(1); k++)
                {
                    logSpectrumArray[t, k] = Math.Log(spectrumArray[t, k]);
                    if (logSpectrumArray[t, k] < 1)
                    {
                        logSpectrumArray[t, k] = 1;
                    }
                }   
            }

            return logSpectrumArray;
        }



        /// take 320 bit frame from sample                                
        /// pad with zeros
        /// pass into fft anlysis
        /// create array the size of the sound file
        /// loop windowing of frames until whole sound file complete
        /// TO DO retrieve sound wav                                      ***
        /// verify float/short/double types                               ***
        /// verify what the zero point something means                    ***

    }
}
