using GnomeRides.Classes;
using GnomeRides.Controlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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

            if (CbxManufacturer.SelectedItem == null)
            {
                return;
            }
            if (CbxFuelType.SelectedItem == null)
            {
                return;
            }

            uint manufacturer = Constants.VehicleManufacturers.Find(kvp => kvp.Value == CbxManufacturer.SelectedItem.ToString()).Key;
            uint fuelType = Constants.FuelTypes.Find(kvp => kvp.Value == CbxFuelType.SelectedItem.ToString()).Key;

            if (!uint.TryParse(TbxSeats.Text, out seats))
            {
                return;
            }
            if (!uint.TryParse(TbxMileage.Text, out mileage))
            {
                return;
            }
            if (!uint.TryParse(TbxWheels.Text, out wheels))
            {
                return;
            }
            if (!uint.TryParse(TbxDailyRate.Text, out dailyRate))
            {
                return;
            }
            if (!uint.TryParse(TbxCo2.Text, out co2))
            {
                return;
            }

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
            this.NavigationService.Navigate(new Cars());
        }
    }
}
