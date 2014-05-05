using System;
using System.Windows;
using System.Windows.Controls;

namespace Dsktp_SecureHeartbeat
{
    /// <summary>
    /// Interaction logic for LogoutPage.xaml
    /// </summary>
    public partial class LogoutPage : Page
    {
        public LogoutPage()
        {
            InitializeComponent();
        }

        private void LoginPageButton_OnClick(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            var parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }
    }
}
