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
    /// Interaction logic for AddVan.xaml
    /// </summary>
    public partial class AddVan : Page
    {
        public AddVan()
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
            uint outerHeight;
            uint outerWidth;
            uint outerLength;
            uint innerHeight;
            uint innerWidth;
            uint innerLength;
            uint seats;
            uint mileage;
            uint wheels;
            uint dailyRate;
            uint maxWeight;
            uint volume;

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

            uint manufacturer = Constants.VehicleManufacturers.Find(kvp => kvp.Value == CbxManufacturer.SelectedItem.ToString()).Key;
            uint fuelType = Constants.FuelTypes.Find(kvp => kvp.Value == CbxFuelType.SelectedItem.ToString()).Key;

            if (!uint.TryParse(TbxOuterHeight.Text, out outerHeight))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj yttre höjd";
                return;
            }
            if (!uint.TryParse(TbxOuterWidth.Text, out outerWidth))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj yttre bredd";
                return;
            }
            if (!uint.TryParse(TbxOuterLength.Text, out outerLength))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj yttre längd";
                return;
            }
            if (!uint.TryParse(TbxInnerHeight.Text, out innerHeight))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj inre längd ";
                return;
            }
            if (!uint.TryParse(TbxInnerWidth.Text, out innerWidth))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj inre bredd";
                return;
            }
            if (!uint.TryParse(TbxInnerLength.Text, out innerLength))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj inre längd";
                return;
            }
            if (!uint.TryParse(TbxMaxWeight.Text, out maxWeight))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj inre längd";
                return;
            }
            if (!uint.TryParse(TbxInnerLength.Text, out volume))
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = "Välj inre längd";
                return;
            }



            string? error = VanController.AddVan(TbxReg.Text,
            seats,
            manufacturer,
            mileage,
            wheels,
            TbxModel.Text,
            fuelType,
            dailyRate * 100,
            outerWidth,
            outerHeight,
            outerLength,
            innerWidth,
            innerHeight,
            innerLength,
            maxWeight,
            volume
            );
            if (error != null)
            {
                TxtBlkError.Visibility = Visibility.Visible;
                TxtBlkError.Text = error;
                return;
            }
            this.NavigationService.Navigate(new Vans());
        }
    }
}
