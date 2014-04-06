using System;

namespace Dsktp_SecureHeartbeat
{
    public class FFTFunction
    {
        private RetrieveWav getWavInst;
        private FFTFrameAnalysis fftFrameAnalysisInst = new FFTFrameAnalysis();

        private short[] getSoundAsShortByteArray;
        private const int FrameLength = 320; //length in bits of each frame extracted from sound file
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

        private float [,] fftresults;




        public FFTFunction()
        {
            magnitudeSpectrum = fftFrameAnalysisInst.GetMagnitudeSpectrumSize();
            currentFrame = new short[PaddedFrameLength];
        }

        public float[,] PerformFftFunction(String soundFileName)
        {
            //With parse impl pass in File name
            getWavInst = new RetrieveWav(soundFileName);
            getSoundAsShortByteArray = getWavInst.getWavFile();

            numberOfFrames = ((int)Math.Floor((getSoundAsShortByteArray.Length - NumOfShortValuesInFrame) / (NumOfShortValuesInFrame * 0.5))) + 1;
            fftresults = new float[numberOfFrames, magnitudeSpectrum];

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
                var fftFrame = fftFrameAnalysisInst.fastFourierTransformAnalysis(currentFrame);
                for (var j = 0; j < magnitudeSpectrum; j++)
                {
                    fftresults[i, j] = fftFrame[j];
                }
            }

            return fftresults;
        }

        public void PerformWienerFilter(float [,] magnitudeArray)
        {


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
