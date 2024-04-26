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
            AppendCar(new Car("LGY 677", 5, "Ford", 123, 4, "Fiesta", "Bensin", 10000, "200509227872", 500));
            AppendCar(new Car("LGY 677", 5, "Ford", 123, 4, "Fiesta", "Bensin", 10000, "200509227872", 500));
            AppendCar(new Car("LGY 677", 5, "Ford", 123, 4, "Fiesta", "Bensin", 10000, "200509227872", 500));
            AppendCar(new Car("LGY 677", 5, "Ford", 123, 4, "Fiesta", "Bensin", 10000, "200509227872", 500));
        }

        private void AppendCar(Car car)
        {
            CarCard card = new CarCard(car);
            if (cars > 2)
            {
                card.Margin = new Thickness(0,16,0,0);
            }
            cars++;
            if (cars % 3 == 1)
            {
                Panel1.Children.Add(card);
            } else if (cars % 3 == 2)
            {
                Panel2.Children.Add(card);
            } else
            {
                Panel3.Children.Add(card);
            }
        }

        private void BtnCar_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Cars());
        }
    }
}
