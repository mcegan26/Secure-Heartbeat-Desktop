using System;

namespace Dsktp_SecureHeartbeat
{
    public class FFTFrameAnalysis
    {
        // FFT of frame x, return real & image parts
        // nFFT = 512
        // nFFT2 = nFFT / 2
        // re, im are two arrays of size nFFT
 
        //nFFT
        private const int frameLength = 256;
        private const int halfFrameLength = frameLength / 2;
        //nFTT2
 
        private double[,] cosWave;
        private double[,] sinWave;

        private double[] realCoefficient;
        private double[] imaginaryCoefficient;
        private double[] hammWindow;

        private double[] frameMagnitudeInFreqSpectrum;

        public FFTFrameAnalysis()
        { 
            cosWave = new double[halfFrameLength, frameLength];
            sinWave = new double[halfFrameLength, frameLength];

            realCoefficient = new double[halfFrameLength];
            imaginaryCoefficient = new double[halfFrameLength];
            hammWindow = new double[frameLength];

            frameMagnitudeInFreqSpectrum = new double[halfFrameLength + 1];

            // create the waves to be able to anasyle the frame imput as a normal sinisodual wave
            generateSinusoidalWaves();
            
            //generate a hamming Window to be able apply to each frame of the sound file so that FFT results appear smooth and more accurate
            GenerateHammingWindow();
        }

 

        private void generateSinusoidalWaves()
        {
            for(int k = 0; k < halfFrameLength; k++) 
		    {                  
			    for(int n = 0; n < frameLength; n++) 
			    {
                	cosWave[k, n] = Math.Cos(2 * Math.PI * k * n / frameLength);
              		sinWave[k, n] = Math.Sin(2 * Math.PI * k * n / frameLength);
         		}	
    		}
        }


        /// <summary>
        /// A Hamming Window is created once and applied to each frame before the FFT function is applied 
        /// to avoid spectral leakage that may arise due to the FFT works under the assumption that the input 
        /// frame is an infitely repeating sound wave whereas this is extremley unlikely to happen with sounds in a real
        /// life environment. So the window will be 'added' over the frame to provide the affect that is is almost
        /// (not 100%) a reapteing sound wave.
        /// </summary>
        private void GenerateHammingWindow()
        {
            for (int i = 0; i < frameLength; i++)
            {
                hammWindow[i] = (0.54 - 0.46 * Math.Cos(2 * i * Math.PI / (frameLength - 1)));
            }
        }





        public double[] FastFourierTransformAnalysis(short[] frame)
        {
            //Before any of the FFT function is calculate apply the hamming window to the sound frame input
            for (int n = 0; n < frameLength; n++)
            {
                frame[n] = (short) (frame[n] * hammWindow[n]);
                // frame[n] = Convert.ToInt16(frame[n] * hammWindow[n]);
            }

            // Real values from the frame
            realCoefficient[0] = 0;

    		for(var n = 0; n < frameLength; n++)
            {
                realCoefficient[0] += frame[n];
            }


    		realCoefficient[halfFrameLength - 1] = 0;
    		for(var n = 0; n < frameLength; n++)
    		{
    		    float a = (n%2 == 1) ? -frame[n] : frame[n];
         		realCoefficient[halfFrameLength - 1] += a;
    		}



            for(var k = 1; k < halfFrameLength; k++)
			{
                realCoefficient[k] = 0;
         			
                for(var n = 0; n < frameLength; n++)
                {
                	realCoefficient[k] += frame[n] * cosWave[k, n];
                }
    		}
 
            // Imaginary values for the frame
    		imaginaryCoefficient[0] = imaginaryCoefficient[halfFrameLength - 1] = 0;
            for(var k = 1; k < halfFrameLength; k++) 
			{
                imaginaryCoefficient[k] = 0;
         		for(var n = 0; n < frameLength; n++)
                {
                	imaginaryCoefficient[k] += frame[n] * sinWave[k, n]; 
                }    		
            }


            /// This returns an array of the magnitude across the frequency spectrum betwween 0-8KHz 
            /// (as specified within Nyquist theorm; as the sampling rate of 16KHz is being used in the 
            /// recording of wav files from the mobile app and therefore the frequecy that is represent 
            /// is upto exactly half) 

		    for (var k = 0; k < halfFrameLength; k++)
		    {
		        var realPart = realCoefficient[k];
		        var imagnaryPart = imaginaryCoefficient[k];
		        frameMagnitudeInFreqSpectrum[k] = Math.Sqrt(realPart*realPart + imagnaryPart*imagnaryPart);
                //if (frameMagnitudeInFreqSpectrum[k] < 1)
                //{
                //    frameMagnitudeInFreqSpectrum[k] = 1; 
                //}
                
                // Only include this line if noise filtering isnt being used or is complete?
                //frameMagnitudeInFreqSpectrum[k] = (short) Math.Log(frameMagnitudeInFreqSpectrum[k]);
		    }   

		    return frameMagnitudeInFreqSpectrum;
		}

        public int GetMagnitudeSpectrumSize()
        {
            return halfFrameLength + 1;
        }

    }
}

