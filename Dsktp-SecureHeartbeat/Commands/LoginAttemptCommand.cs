using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Dsktp_SecureHeartbeat;
using Dsktp_SecureHeartbeat.Models;
using Parse;
using Dsktp_SecureHeartbeat.Models;

namespace SecureHeartbeat.Commands
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
                var passBox = parameter as PasswordBox;
                var pass = passBox.Password;
                await ParseUser.LogInAsync(_loginModel.Username, pass);
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
