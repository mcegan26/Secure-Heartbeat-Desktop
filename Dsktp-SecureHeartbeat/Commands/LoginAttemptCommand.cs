using System;
using System.Windows;
using System.Windows.Input;
using Dsktp_SecureHeartbeat.Models;
using Parse;

namespace Dsktp_SecureHeartbeat.Commands
{
    public class LoginAttemptCommand : ICommand
    {
        private readonly LoginModel _loginModel;

        public LoginAttemptCommand(LoginModel loginModel)
        {
            _loginModel = loginModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public async void Execute(object parameter)
        {
            try
            {
                await ParseUser.LogInAsync(_loginModel.Username, _loginModel.Password);
                // Login was successful.
            }
            catch (Exception e)
            {
                // The login failed. Check the error to see why.
                MessageBoxResult result =
                    MessageBox.Show("Incorrect login credentials, Please try Agian",
                    "Login Failed",
                    MessageBoxButton.OK);
            }
        }


    }
}
