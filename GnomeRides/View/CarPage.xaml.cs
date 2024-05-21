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
        private readonly Car? car;
        public CarPage(string reg_nr)
        {
            InitializeComponent();
            (Car?, string?) res = CarController.GetCarByRegNr(reg_nr);
            Car? car = res.Item1;
            string? error = res.Item2;

            this.car = car;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://unifleet.se/wp-content/uploads/2020/10/Volvo-V60-Recharge-Vapour-Grey.png");
            bitmap.EndInit();

            ImgCar.Source = bitmap;
            this.Loaded += (s, e) =>
            {
                if (car == null)
                {
                    NavigationService.Navigate(new Error(error));
                }
            };
            if (car != null)
            {
                TxtBlkModel.Text = $"{car.Manufacturer} {car.Model}";
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Säten: {car.Seats}",
                    FontWeight = FontWeights.Medium,
                });
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Miltal: {car.Mileage} mil",
                    FontWeight = FontWeights.Medium,
                });
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Hjul: {car.Wheels}",
                    FontWeight = FontWeights.Medium,
                });
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Drivmedel: {car.FuelType}",
                    FontWeight = FontWeights.Medium,
                });
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Utsläpp: {car.Co2} g/km",
                    FontWeight = FontWeights.Medium,
                });
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (car == null)
            {
                return;
            }
            TxtBlkPrice.Text = $"Pris: {Calendar.SelectedDates.Count * car.DailyRate / 100} kr";
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            if (car == null)
            {
                TxtBlkBookinMessage.Visibility = Visibility.Collapsed;
                TxtBlkBookinError.Visibility = Visibility.Visible;
                TxtBlkBookinError.Text = "Ingen bil";
                return;
            }
            string? error = car.LoanCar(DateOnly.FromDateTime(Calendar.SelectedDates.First() > Calendar.SelectedDates.Last() ? Calendar.SelectedDates.Last() : Calendar.SelectedDates.First()), DateOnly.FromDateTime(Calendar.SelectedDates.First() < Calendar.SelectedDates.Last() ? Calendar.SelectedDates.Last() : Calendar.SelectedDates.First()));
            if (error != null)
            {
                TxtBlkBookinMessage.Visibility = Visibility.Collapsed;
                TxtBlkBookinError.Visibility = Visibility.Visible;
                TxtBlkBookinError.Text = error;
                return;
            }
            TxtBlkBookinMessage.Visibility = Visibility.Visible;
            TxtBlkBookinError.Visibility = Visibility.Collapsed;
            TxtBlkBookinMessage.Text = "Bil bokad";
        }
    }
}
