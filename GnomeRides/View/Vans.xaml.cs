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
    /// Interaction logic for Vans.xaml
    /// </summary>
    public partial class Vans : Page
    {
        List<Van> vans;
        public Vans()
        {
            InitializeComponent();
            CbxManufacturers.Items.Add("Alla");
            CbxManufacturers.SelectedIndex = 0;
            foreach (KeyValuePair<uint, string> manufacturer in Constants.VehicleManufacturers)
            {
                CbxManufacturers.Items.Add(manufacturer.Value);
            }
            (List<Van>, string?) Vanlist = VanController.GetVans();
            vans = Vanlist.Item1;
            foreach (Van van in vans)
            {
                VanGrid.AddChild(new VanCard(van));
            }
            if (Vanlist.Item2 != null)
            {
                MessageBox.Show(Vanlist.Item2, "Motorcyclar", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (VanGrid == null)
            {
                return;
            }
            if (vans == null)
            {
                return;
            }
            VanGrid.Clear();
            foreach (Van van in vans)
            {
                if ((van.DailyRate < Convert.ToUInt32(y) * 1000 || y == PriceSlider.Maximum) && van.DailyRate > x * 1000 && (z == 0 || van.Manufacturer == Constants.VehicleManufacturers[z - 1].Value))
                {
                    VanGrid.AddChild(new VanCard(van));
                }
            }
        }

        private void CbxManufacturers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            donaudampfschiffahrt(PriceSlider.LowerValue, PriceSlider.HigherValue, CbxManufacturers.SelectedIndex);
        }
    }
}
