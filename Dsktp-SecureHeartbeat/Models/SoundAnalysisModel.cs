using System;
using Parse;

namespace Dsktp_SecureHeartbeat.Models
{
    public class SoundAnalysisModel
    {
        // Password not pulled from the Backend as it is not needed

        private int username;

        public int Username
        {
            get { return username; }
            set { username = value; }
        }

        private String email;
        private long mobileNo;
        private String forename;
        private String surname;
        private String department;
        private DateTime dob;
        private bool withinBoundary;
        private double NWLat;
        private double NWLong;
        private double SELat;
        private double SELong;
        private double currentLat;
        private double currentLong;

        private object soundFile;
        public object SoundFile
        {
            get { return soundFile; }
            set { soundFile = value; }
        }


        private String soundFileName;
        public string SoundFileName
        {
            get { return soundFileName; }
            set { soundFileName = value; }
        }


        private float[,] spectrumResults;
        public float[,] SpectrumResults
        {
            get { return spectrumResults; }
            set { spectrumResults = value; }
        }

        private float[,] filteredSpectrumResults;
        public float[,] FilteredSpectrumResults
        {
            get { return filteredSpectrumResults; }
            set { filteredSpectrumResults = value; }
        }

        public SoundAnalysisModel(ParseObject user)
        {
            // spectrumResults = new float[FFTFunction.NumberOfFrames, FFTFunction.MagnitudeSpectrum];
            // filteredSpectrumResults = new float[FFTFunction.NumberOfFrames, FFTFunction.MagnitudeSpectrum];

            //todo iterate through the parse object and assign to the user details
        }

        public SoundAnalysisModel()
        {
        }


        //Examplary values and format of what a sound Analysis User Model would have


        //DateTime user1dob = new DateTime(1992, 3, 26);
        //double user1lat = 54.581728;
        //double user1long = -5.937756;
        //ParseGeoPoint user1GeoPoint = new ParseGeoPoint(user1lat, user1long);

        //var user1 = new ParseUser()
        //{
        //    Username = "10001",
        //    Password = "password1",
        //    Email = "rmcegan01@qub.ac.uk"
        //};


        //long mobileNo = 07920401001;
        //String forename = "Ronan";
        //String surname = "McEgan";
        //String department = "IT";
        //DateTime dob = user1dob;
        //bool withinBoundary = true;
        //double NWLat = 54.582353;
        //double NWLong = -5.938432;
        //double SELat = 54.581122;
        //double SELong = -5.936571;
        //double currentLat = user1GeoPoint.Latitude;
        //double currentLong = user1GeoPoint.Longitude;

    }
}
