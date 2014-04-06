using System;
using System.Windows.Input;

namespace Dsktp_SecureHeartbeat.Commands
{
    class EnableButtonCommand : ICommand
    {
        /// <summary>
        /// The event used to trigger a refrehs on the UI when the status of the CanExecute command has changed
        /// Note - Currently this is never called by us, ie the state never changes
        /// </summary>
        public event EventHandler CanExecuteChanged = delegate { };


        public bool CanExecute(object parameter)
        {
            // Return true as the button can always be executed
            return true;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Successfully executed the command");
        }
    }
}
