using System;
using Parse;

namespace Dsktp_SecureHeartbeat.Models
{
    public class SoundAnalysisModel
    {
        // Password not pulled from the Backend as it is not needed and would cause unecessary security risks

        private int _username;
        public int Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private long _mobileNo;
        public long MobileNo
        {
            get { return _mobileNo; }
            set { _mobileNo = value; }
        }

        private string _forename;
        public string Forename
        {
            get { return _forename; }
            set { _forename = value; }
        }
        
        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        private string _department;
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }

        private bool _admin;
        public bool Admin
        {
            get { return _admin; }
            set { _admin = value; }
        }

        private bool _withinBoundary;
        public bool WithinBoundary
        {
            get { return _withinBoundary; }
            set { _withinBoundary = value; }
        }

        private double _NWLat;
        public double NWLat
        {
            get { return _NWLat; }
            set { _NWLat = value; }
        }

        private double _NWLong;
        public double NWLong
        {
            get { return _NWLong; }
            set { _NWLong = value; }
        }

        private double _SELat;
        public double SELat
        {
            get { return _SELat; }
            set { _SELat = value; }
        }

        private double _SELong;
        public double SELong
        {
            get { return _SELong; }
            set { _SELong = value; }
        }

        private double _currentLat;
        public double CurrentLat
        {
            get { return _currentLat; }
            set { _currentLat = value; }
        }

        private double _currentLong;
        public double CurrentLong
        {
            get { return _currentLong; }
            set { _currentLong = value; }
        }

        private byte[] soundFile;
        public byte[] SoundFile
        {
            get { return soundFile; }
            set { soundFile = value; }
        }

        private byte[] imageFile;
        public byte[] ImageFile
        {
            get { return imageFile; }
            set { imageFile = value; }
        }


        private string soundFileName;
        public string SoundFileName
        {
            get { return soundFileName; }
            set { soundFileName = value; }
        }


        private double[,] spectrumResults;
        public double[,] SpectrumResults
        {
            get { return spectrumResults; }
            set { spectrumResults = value; }
        }

        private double[,] filteredSpectrumResults;
        public double[,] FilteredSpectrumResults
        {
            get { return filteredSpectrumResults; }
            set { filteredSpectrumResults = value; }
        }

        public SoundAnalysisModel(ParseObject user)
        {
            Username = user.Get<int>("username");
            Forename = user.Get<string>("forename");
            Surname = user.Get<string>("surname");
            Forename = user.Get<string>("forename");
            Department = user.Get<string>("department");
            Email = user.Get<string>("email");
            MobileNo = user.Get<long>("forename");
            NWLat = user.Get<double>("NWLat");
            NWLong = user.Get<double>("NWLong");
            SELat = user.Get<double>("SELat");
            SELong = user.Get<double>("SELong");
            Admin = user.Get<bool>("admin");
            WithinBoundary = user.Get<bool>("withinBoundary");

            ParseGeoPoint aux = user.Get<ParseGeoPoint>();
            CurrentLat = aux.Latitude;
            CurrentLong = aux.Longitude;
        }

        public SoundAnalysisModel()
        {
        }


        //Examplary values and format of what a sound Analysis User Model would have

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
