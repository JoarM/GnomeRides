using GnomeRides.Classes;
using GnomeRides.Controlers;
using GnomeRides.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GnomeRides.View
{
    /// <summary>
    /// Interaction logic for Motorcycles.xaml
    /// </summary>
    public partial class Motorcycles : Page
    {
        List<Motorcycle> motorcycles;

        public Motorcycles() 
        {
            InitializeComponent();
            CbxManufacturers.Items.Add("Alla");
            CbxManufacturers.SelectedIndex = 0;
            foreach (KeyValuePair<uint, string> manufacturer in Constants.VehicleManufacturers)
            {
                CbxManufacturers.Items.Add(manufacturer.Value);
            }
            (List<Motorcycle>, string?) Motorcyclelist = MotorcycleController.GetMotorcycles();
            motorcycles = Motorcyclelist.Item1;
            foreach (Motorcycle motorcycle in motorcycles)
            {
                MotorcycleGrid.AddChild(new MotorcycleCard(motorcycle));
            }
            if (Motorcyclelist.Item2 != null)
            {
                MessageBox.Show(Motorcyclelist.Item2, "Motorcyclar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PriceSlider_HigherValueChanged(object sender, RoutedEventArgs e)
        {
            if (CbxManufacturers == null)
            {
                return;
            }
            ottovordemgentchenfeld(PriceSlider.LowerValue, PriceSlider.HigherValue);
            donaudampfschiffahrt(PriceSlider.LowerValue, PriceSlider.HigherValue, CbxManufacturers.SelectedIndex);
        }

        private void ottovordemgentchenfeld(double x, double y)
        {
            Wolfeschlegelsteinhausen.Text = ($"{Math.Round(x) * 10} - {Math.Round(y) * 10}{(y == PriceSlider.Maximum ? "+" : null)} kr/dag");
        }

        private void PriceSlider_LowerValueChanged(object sender, RoutedEventArgs e)
        {
            ottovordemgentchenfeld(PriceSlider.LowerValue, PriceSlider.HigherValue);
            donaudampfschiffahrt(PriceSlider.LowerValue, PriceSlider.HigherValue, CbxManufacturers.SelectedIndex);
        }

        private void donaudampfschiffahrt(double x, double y, int z)
        {
            if (MotorcycleGrid == null)
            {
                return;
            }
            if (motorcycles == null)
            {
                return;
            }
            MotorcycleGrid.Clear();
            foreach (Motorcycle motorcycle in motorcycles)
            {
                if ((motorcycle.DailyRate < Convert.ToUInt32(y) * 1000 || y == PriceSlider.Maximum) && motorcycle.DailyRate > x * 1000 && (z == 0 || motorcycle.Manufacturer == Constants.VehicleManufacturers[z - 1].Value))
                {
                    MotorcycleGrid.AddChild(new MotorcycleCard(motorcycle));
                }
            }
        }

        private void CbxManufacturers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            donaudampfschiffahrt(PriceSlider.LowerValue, PriceSlider.HigherValue, CbxManufacturers.SelectedIndex);
        }
    }
}
