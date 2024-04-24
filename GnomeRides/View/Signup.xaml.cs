using GnomeRides.Classes;
using GnomeRides.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GnomeRides.View
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Page
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void SignupBtn_Click(object sender, RoutedEventArgs e)
        {
            string? error = User.CreateAccount(IdBox.Text, PasswordBox.Text, EmailBox.Text, NameBox.Text);
            if (error != null) 
            {
                ErrorBox.Text = error;
                ErrorBox.Visibility = Visibility.Visible;
                return;
            }

            ErrorBox.Visibility = Visibility.Collapsed;
            ErrorBox.Text = "";
            WindowManager.OpenNewStandaloneWindow(new mainWindow());
        }
    }
}
