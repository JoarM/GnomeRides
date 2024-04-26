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
    /// Interaction logic for Cars.xaml
    /// </summary>
    public partial class Cars : Page
    {
        public Cars()
        {
            InitializeComponent();
        }

        private void PriceSlider_HigherValueChanged(object sender, RoutedEventArgs e)
        {
            ottovordemgentchenfeld(PriceSlider.LowerValue, PriceSlider.HigherValue);
        }

        private void ottovordemgentchenfeld(double x, double y) {
            Wolfeschlegelsteinhausen.Text = ($"{Math.Round(x) * 10} - {Math.Round(y) * 10}{(y == PriceSlider.Maximum ? "+": null)} kr/timme");
        }
    }
}
