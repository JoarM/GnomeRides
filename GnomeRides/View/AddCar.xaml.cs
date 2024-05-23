using GnomeRides.Classes;
using GnomeRides.Controlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddCar.xaml
    /// </summary>
    public partial class AddCar : Page
    {
        public AddCar()
        {
            InitializeComponent();
            foreach (KeyValuePair<uint, string> manufacturer in Constants.VehicleManufacturers)
            {
                CbxManufacturer.Items.Add(manufacturer.Value);
            }
            foreach (KeyValuePair<uint, string> fuelType in Constants.FuelTypes)
            {
                CbxFuelType.Items.Add(fuelType.Value);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            uint seats;
            uint mileage;
            uint wheels;
            uint dailyRate;
            uint co2;

            if (!new Regex("[A-Ö][A-Ö][A-Ö][0-9][0-9][A-Ö0-9]", RegexOptions.IgnoreCase).IsMatch(TbxReg.Text))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Fyll i ett giltigt regnummer";
                return;
            }
            if (!uint.TryParse(TbxSeats.Text, out seats))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj bil säten";
                return;
            }
            if (!uint.TryParse(TbxMileage.Text, out mileage))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj miltal";
                return;
            }
            if (!uint.TryParse(TbxWheels.Text, out wheels))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj hjul antal";
                return;
            }
            if (TbxModel.Text == "")
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Fyll i model";
                return;
            }
            if (CbxManufacturer.SelectedItem == null)
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj bil tilverkare";
                return;
            }
            if (CbxFuelType.SelectedItem == null)
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj drivmedels typ";
                return;
            }
            if (!uint.TryParse(TbxDailyRate.Text, out dailyRate))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj ett positivt tal som pris";
                return;
            }
            if (!uint.TryParse(TbxCo2.Text, out co2))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Fyll i koldioxid uttsläps mängd";
                return;
            }

            uint manufacturer = Constants.VehicleManufacturers.Find(kvp => kvp.Value == CbxManufacturer.SelectedItem.ToString()).Key;
            uint fuelType = Constants.FuelTypes.Find(kvp => kvp.Value == CbxFuelType.SelectedItem.ToString()).Key;

            string? error = CarController.AddCar(TbxReg.Text,
            seats,
            manufacturer,
            mileage,
            wheels,
            TbxModel.Text,
            fuelType,
            dailyRate * 100,
            co2
            );
            if (error != null)
            {
                TxtBlkError.Visibility = Visibility.Visible; 
                TxtBlkError.Text = error;
                return;
            }
            this.NavigationService.Navigate(new Cars());
        }
    }
}
