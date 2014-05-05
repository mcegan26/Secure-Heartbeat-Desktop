using System;
using System.Windows;
using System.Windows.Controls;
using Parse;

namespace Dsktp_SecureHeartbeat
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }


        private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            var userQuery = ParseUser.Query.Where(user => user.Get<string>("username") == usernameBox.Text);
            var currentUser1 = await userQuery.FirstOrDefaultAsync();

            ParseObject currentUser = currentUser1;
            if (currentUser != null)
            {
                var UserIsAnAdmin = currentUser.Get<bool>("Admin");
                if (UserIsAnAdmin)
                {
                    try
                    {
                        var pass = passBox.Password;
                        await ParseUser.LogInAsync(usernameBox.Text, pass);
                        // Login was successful.
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                        var parentWindow = Window.GetWindow(this);
                        if (parentWindow != null)
                        {
                            parentWindow.Close();
                        }
                    }
                    catch (Exception)
                    {
                        // The login failed. Check the error to see why.
                        MessageBoxResult result =
                            MessageBox.Show("Incorrect password, please try Agian",
                            "Login Failed",
                            MessageBoxButton.OK);
                    }

                }
                else
                {
                    MessageBoxResult result =
                            MessageBox.Show("User entered is not an admin user, please enter admin credentials",
                            "Login Failed",
                            MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBoxResult result =
                            MessageBox.Show("User does not exsit, please enter an exsiting admin user's credentials",
                            "Login Failed",
                            MessageBoxButton.OK);
            }
        }


    }
}
