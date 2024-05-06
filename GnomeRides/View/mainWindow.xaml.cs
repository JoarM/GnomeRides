using GnomeRides.Classes;
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
using System.Windows.Shapes;

namespace GnomeRides.View
{
    /// <summary>
    /// Interaction logic for mainWindow.xaml
    /// </summary>
    public partial class mainWindow : Window
    {
        public mainWindow()
        {
            InitializeComponent();
            _frame.Navigate(new Home());
        }

        private void BtnCar_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new Cars());
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new Home());
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new AddVehicle());
        }

        private void BtnMotorcycle_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new Cars());
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new Profile());
        }
    }
}
