using GnomeRides.Classes;
using GnomeRides.Controlers;
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
    /// Interaction logic for AddMotorcycle.xaml
    /// </summary>
    public partial class AddMotorcycle : Page
    {
        public AddMotorcycle()
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
            //Todo add checks
            string? error = MotorcycleController.AddMotorcycle(TbxReg.Text,
            uint.Parse(TbxSeats.Text),
            Constants.VehicleManufacturers.Find(kvp => kvp.Value == CbxManufacturer.SelectedItem.ToString()).Key,
            uint.Parse(TbxMileage.Text),
            uint.Parse(TbxWheels.Text),
            TbxModel.Text,
            Constants.FuelTypes.Find(kvp => kvp.Value == CbxFuelType.SelectedItem.ToString()).Key,
            uint.Parse(TbxDailyRate.Text) * 100,
            uint.Parse(TbxCC.Text)
            );
            this.NavigationService.Navigate(new Motorcycles());
        }
    }
}
