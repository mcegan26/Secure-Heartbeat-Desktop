using System;
using NAudio.Wave;

namespace Dsktp_SecureHeartbeat
{
    public class RetrieveWav
    {
        private String soundFileName;

        public RetrieveWav(String soundFileName)
        {
            this.soundFileName = soundFileName;
        }


        /// <summary>
        /// Change to an async method when implementing Parse call
        /// </summary>
        /// <returns></returns> 
        public short[] getWavFile()
        {
            /// To do 
            /// obtain user name as a parameter from user interface text field 
            /// and use it within a parse query and pull dow attached wav file.
            /// Then convert to Byte Array.

            // int chunkID = reader.ReadInt32();
            // int fileSize = reader.ReadInt32();
            // int riffType = reader.ReadInt32();
            // int fmtID = reader.ReadInt32();
            // int fmtSize = reader.ReadInt32();
            // int fmtCode = reader.ReadInt16();
            // int channels = reader.ReadInt16();
            // int sampleRate = reader.ReadInt32();
            // int fmtAvgBPS = reader.ReadInt32();
            // int fmtBlockAlign = reader.ReadInt16();
            // int bitDepth = reader.ReadInt16();

            // Stream waveFileStream = (@"SampleFYPAudio.wav");

            // String soundFileAddress = "SampleFYPAudio.wav";
            // var wavFileAsByets = File.ReadAllBytes(soundFileAddress);

            // TODO Retrieve sound file from folder

            

            var wavFileReader = new WaveFileReader(@"C:\SampleFYPAudio.wav");
            var buffer = new byte[wavFileReader.Length];
            int read = wavFileReader.Read(buffer, 0, buffer.Length);
            var soundFileAsShortByteArray = new short[read / 2];
            Buffer.BlockCopy(buffer, 0, soundFileAsShortByteArray, 0, read);

            return soundFileAsShortByteArray;

        }
    }
}
