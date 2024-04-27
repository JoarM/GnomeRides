using GnomeRides.Components;
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
using GnomeRides.Classes;
using GnomeRides.Controlers;

namespace GnomeRides.View
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private int cars = 0;
        public Home()
        {
            InitializeComponent();
            autoGrid.AddChild(new CarCard(CarController.GetCars().Item1[0]));
            autoGrid.AddChild(new CarCard(new Car("LGY 677", 5, "Ford", 123, 4, "Fiesta", "Bensin", 10000, "200509227872", 500)));
            autoGrid.AddChild(new CarCard(new Car("LGY 677", 5, "Ford", 123, 4, "Fiesta", "Bensin", 10000, "200509227872", 500)));
            autoGrid.AddChild(new CarCard(new Car("LGY 677", 5, "Ford", 123, 4, "Fiesta", "Bensin", 10000, "200509227872", 500)));
        }

        private void BtnCar_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Cars());
        }
    }
}
