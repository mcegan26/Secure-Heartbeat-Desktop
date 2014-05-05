using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Dsktp_SecureHeartbeat.Models;
using SecureHeartbeat.Commands;
using SecureHeartbeat.Core.Impl;

namespace Dsktp_SecureHeartbeat.ViewModels
{
    public class UsersViewModel : ViewModel
    {
        public int Username
        {
            get
            {
                return currentUser.Username;
            }
            set
            {
                if (value != currentUser.Username)
                {
                    currentUser.Username = value;
                    NotifyPropertyChanged("Username");
                }
            }
        }

        public string Forename
        {
            get
            {
                return currentUser.Forename;
            }
            set
            {
                if (!currentUser.Forename.Equals(value))
                {
                    currentUser.Forename = value;
                    NotifyPropertyChanged("Forename");
                }
            }
        }

        public string Surname
        {
            get
            {
                return currentUser.Surname;
            }
            set
            {
                if (!currentUser.Surname.Equals(value))
                {
                    currentUser.Surname = value;
                    NotifyPropertyChanged("Surname");
                }
            }
        }

        public string Department
        {
            get
            {
                return currentUser.Department;
            }
            set
            {
                if (!currentUser.Department.Equals(value))
                {
                    currentUser.Department = value;
                    NotifyPropertyChanged("Department");
                }
            }
        }

        public long MobileNo
        {
            get
            {
                return currentUser.MobileNo;
            }
            set
            {
                if (!currentUser.MobileNo.Equals(value))
                {
                    currentUser.MobileNo = value;
                    NotifyPropertyChanged("MobileNo");
                }
            }
        }


        public string Admin
        {
            get
            {
                var isAdmin = currentUser.Admin;
                if (isAdmin)
                {
                    return "Yes";
                }
                return "No";
            }
        }

        public string WithinBoundary
        {
            get
            {
                var isWithinBoundary = currentUser.WithinBoundary;
                if (isWithinBoundary)
                {
                    return "Yes";
                }
                return "No";
            }
        }

        public string NWLat
        {
            get
            {
                return currentUser.NWLat.ToString();
            }
            set
            {
                if (!currentUser.NWLat.Equals(value))
                {
                    double dblVersion;
                    try
                    {
                        dblVersion = Convert.ToDouble(value);
                        if ((dblVersion > -90 && dblVersion < 90))
                        {
                            currentUser.NWLat = dblVersion;
                            NotifyPropertyChanged("NWLat");
                        }
                        else
                        {
                            HelperClass.ValueNotLatitude("North West Latitude");
                        }
                    }
                    catch (Exception)
                    {
                        HelperClass.ValueNotDouble("North West Latitude");
                    }
                    

                }
            }
        }

        public string NWLong
        {
            get
            {
                return currentUser.NWLong.ToString();
            }
            set
            {
                if (!currentUser.NWLong.Equals(value))
                {
                    double dblVersion;
                    try
                    {
                        dblVersion = Convert.ToDouble(value);
                        if ((dblVersion > -180 && dblVersion < 180))
                        {
                            currentUser.NWLong = dblVersion;
                            NotifyPropertyChanged("SELong");
                        }
                        else
                        {
                            HelperClass.ValueNotLatitude("North West Longitude");
                        }
                    }
                    catch (Exception)
                    {
                        HelperClass.ValueNotDouble("North West Longitude");
                    }
                }
            }
        }

        public string SELat
        {
            get
            {
                return currentUser.SELat.ToString();
            }
            set
            {
                if (!currentUser.SELat.Equals(value))
                {
                    double dblVersion;
                    try
                    {
                        dblVersion = Convert.ToDouble(value);
                        if ((dblVersion > -90 && dblVersion < 90))
                        {
                            currentUser.SELat = dblVersion;
                            NotifyPropertyChanged("SELat");
                        }
                        else
                        {
                            HelperClass.ValueNotLatitude("South East Latitude");
                        }
                    }
                    catch (Exception)
                    {
                        HelperClass.ValueNotDouble("South East Latitude");
                    }
                }
            }
        }

        public string SELong
        {
            get
            {
                return currentUser.SELong.ToString();
            }
            set
            {
                if (!currentUser.SELong.Equals(value))
                {
                    double dblVersion;
                    try
                    {
                        dblVersion = Convert.ToDouble(value);
                        if ((dblVersion > -180 && dblVersion < 180))
                        {
                            currentUser.SELong = dblVersion;
                            NotifyPropertyChanged("SELong");
                        }
                        else
                        {
                            HelperClass.ValueNotLatitude("South East Longitude");
                        }
                    }
                    catch (Exception)
                    {
                        HelperClass.ValueNotDouble("South East Longitude");
                    }
                }
            }
        }


        private SoundAnalysisModel currentUser;
        private ICommand _updateUserCommand;

        public ICommand UpdateUserCommand
        {
            get { return _updateUserCommand ?? (_updateUserCommand = new UpdateUserCommand(currentUser)); }
        }

        public UsersViewModel()
        {
            // todo GET USER FROM THE TOP OF THE LIST OF SOUNDANLYSIS MODELS
            currentUser = new SoundAnalysisModel();
        }


        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }


        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {

            this.IsDataLoaded = true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
