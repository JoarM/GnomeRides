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
    /// Interaction logic for CarPage.xaml
    /// </summary>
    public partial class CarPage : Page
    {
        public CarPage(string reg_nr)
        {
            InitializeComponent();
            (Car?, string?) res = CarController.GetCarByRegNr(reg_nr);
            Car? car = res.Item1;
            string? error = res.Item2;
            TxtBlkModel.Text = $"{car.Manufacturer} {car.Model}";
        }
    }
}
