using System.Windows;
using Dsktp_SecureHeartbeat.ViewModels;


namespace Dsktp_SecureHeartbeat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static LoginViewModel _loginvm = null;
        private static SoundAnalysisViewModel _soundAnalysisvm = null;

        public static LoginViewModel Loginvm
        {
            get
            {
                // Delay creation of the view model until necessary
                if(_loginvm == null)
                {
                    _loginvm = new LoginViewModel();
                }


                return _loginvm;
            }
        }


        /// <summary>
        /// A static ViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The MainViewModel object.</returns>
        public static SoundAnalysisViewModel SoundAnalysisvm
        {
            get
            {
                // Delay creation of the view model until necessary
                if(_soundAnalysisvm == null)
                    _soundAnalysisvm = new SoundAnalysisViewModel();

                return _soundAnalysisvm;
            }
            
        }

        public App()
        {
            //ParseClient.Initialize("JO4tBIiydFtLJ8zjDFg10Km8YS84a2WqgC8hUiQ3", "y2dLvFgBeyzt89pv9gLtJBaZlsMn7jiZfIty5Ufb");
        }

        
    }
}
