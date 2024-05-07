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
        private Car car;
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
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            TxtBlkPrice.Text = $"Pris: {Calendar.SelectedDates.Count * car.DailyRate / 100} kr";
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            string? error = car.LoanCar(DateOnly.FromDateTime(Calendar.SelectedDates.First()), DateOnly.FromDateTime(Calendar.SelectedDates.Last()));
            if (error != null)
            {

            }
        }
    }
}
