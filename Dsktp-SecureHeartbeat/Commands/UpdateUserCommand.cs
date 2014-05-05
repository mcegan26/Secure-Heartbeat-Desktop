using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Dsktp_SecureHeartbeat.Models;
using Parse;

namespace Dsktp_SecureHeartbeat.ViewModels
{
    public class UpdateUserCommand : ICommand
    {
        private SoundAnalysisModel _currentUser;

        public UpdateUserCommand(SoundAnalysisModel currentUser)
        {
            _currentUser = currentUser;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public async void Execute(object parameter)
        {
            // Check all validation values first and return with message of any values that failed
            // Parse get user with current user ID
            // (Loop through to assign all values)
            // Try catch upload to parse
            // all a message whether it was successful

        }
    }
}
