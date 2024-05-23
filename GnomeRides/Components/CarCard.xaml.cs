using GnomeRides.Classes;
using GnomeRides.View;
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

namespace GnomeRides.Components
{
    /// <summary>
    /// Interaction logic for CarCard.xaml
    /// </summary>
    public partial class CarCard : UserControl
    {
        private Car car;
        public CarCard(Car car)
        {
            InitializeComponent();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(car.ImageUrl ?? "https://static.vecteezy.com/system/resources/previews/035/715/673/non_2x/head-of-sad-robot-404-not-found-page-png.png");
            bitmap.EndInit();

            this.car = car;
            ImgCover.Source = bitmap;
            TxtBlkName.Text = $"{car.Manufacturer} {car.Model}";
            TxtBlkPrice.Text = $"{car.DailyRate / 100} kr/dag";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService svc = NavigationService.GetNavigationService(this);
            svc.Navigate(new CarPage(car.RegNr));
        }
    }
}
