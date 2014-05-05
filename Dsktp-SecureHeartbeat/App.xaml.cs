using System.Collections.Generic;
using System.Windows;
using Dsktp_SecureHeartbeat.Models;
using Dsktp_SecureHeartbeat.ViewModels;
using Parse;


namespace Dsktp_SecureHeartbeat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static LoginViewModel _loginvm = null;
        private static UsersViewModel _usersvm = null;
        private static SoundAnalysisViewModel _soundAnalysisvm = null;
        public static List<SoundAnalysisModel> userList; 

        public static LoginViewModel Loginvm
        {
            get
            {
                // Delay creation of the view model until necessary
                return _loginvm ?? (_loginvm = new LoginViewModel());
            }
        }

        public static UsersViewModel Usersvm
        {
            get
            {
                // Delay creation of the view model until necessary
                return _usersvm ?? (_usersvm = new UsersViewModel());
            }
        }


        public static SoundAnalysisViewModel SoundAnalysisvm
        {
            get
            {
                // Delay creation of the view model until necessary
                return _soundAnalysisvm ?? (_soundAnalysisvm = new SoundAnalysisViewModel());
            }
        }

        public App()
        {
            ParseClient.Initialize("JO4tBIiydFtLJ8zjDFg10Km8YS84a2WqgC8hUiQ3", "y2dLvFgBeyzt89pv9gLtJBaZlsMn7jiZfIty5Ufb");
            
            userList = new List<SoundAnalysisModel>();
            
        }

        
    }
}
