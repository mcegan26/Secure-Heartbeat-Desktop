using System;
using System.ComponentModel;
using System.Windows.Input;
using Dsktp_SecureHeartbeat.Models;
using SecureHeartbeat.Commands;
using SecureHeartbeat.Core.Impl;

namespace Dsktp_SecureHeartbeat.ViewModels
{
    public class LoginViewModel : ViewModel
    {


        public string VmUsername
        {
            get
            {
                return loginModel.Username;
            }
            set
            {
                if (value != loginModel.Username)
                {
                    loginModel.Username = value;
                    NotifyPropertyChanged("VmUsername");
                }
            }
        }


        public string VmPassword
        {
            get
            {
                return loginModel.Password;
            }
            set
            {
                if (value != loginModel.Password)
                {
                    loginModel.Password = (string)value;
                    NotifyPropertyChanged("VmPassword");
                }
            }
        }

        private LoginModel loginModel;
        private ICommand _loginCommand;

        public ICommand LoginAttemptCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new LoginAttemptCommand(loginModel);
                }
                return _loginCommand;
            }

        }

        public LoginViewModel()
        {
            loginModel = new LoginModel();
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
