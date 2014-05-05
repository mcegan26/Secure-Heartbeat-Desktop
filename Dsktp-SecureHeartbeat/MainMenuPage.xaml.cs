using System;
using System.Windows;
using System.Windows.Controls;

namespace Dsktp_SecureHeartbeat
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void AddUsersButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(source: new Uri("/AddUsersPage.xaml", UriKind.Relative));
            }
        }

        private void EditUsersButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(source: new Uri("/EditUsersPage.xaml", UriKind.Relative));
            }
        }

        private void FFTButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(source: new Uri("/SoundAnalysisPage.xaml", UriKind.Relative));
            }
        }

        private void LogOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (HelperClass.ConfirmUserInput())
            {
                if (NavigationService != null)
                {
                    NavigationService.Navigate(source: new Uri("/LogOutPage.xaml", UriKind.Relative));
                } 
            }
        }

    }
}
