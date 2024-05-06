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
    /// Interaction logic for AddVehicle.xaml
    /// </summary>
    public partial class AddVehicle : Page
    {
        public AddVehicle()
        {
            InitializeComponent();
            InnerFrame.Navigate(new AddCar());
        }

        private void BtnCar_Click(object sender, RoutedEventArgs e)
        {
            InnerFrame.Navigate(new AddCar());
        }

        private void BtnBike_Click(object sender, RoutedEventArgs e)
        {
            InnerFrame.Navigate(new AddMotorcycle());
        }

        private void BtnVan_Click(object sender, RoutedEventArgs e)
        {
            InnerFrame.Navigate(new AddVan());
        }
    }
}
