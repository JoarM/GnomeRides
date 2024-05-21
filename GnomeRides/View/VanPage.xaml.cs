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
    /// Interaction logic for VanPage.xaml
    /// </summary>
    public partial class VanPage : Page
    {
        private readonly Van? van;
        public VanPage(string reg_nr)
        {
            InitializeComponent();
            (Van?, string?) res = VanController.GetVanByRegNr(reg_nr);
            Van? van = res.Item1;
            string? error = res.Item2;

            this.van = van;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://unifleet.se/wp-content/uploads/2020/10/Volvo-V60-Recharge-Vapour-Grey.png");
            bitmap.EndInit();

            ImgVan.Source = bitmap;
            this.Loaded += (s, e) =>
            {
                if (van == null)
                {
                    NavigationService.Navigate(new Error(error));
                }
            };
            if (van != null)
            {
                TxtBlkModel.Text = $"{van.Manufacturer} {van.Model}";
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Säten: {van.Seats}",
                    FontWeight = FontWeights.Medium,
                });
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Miltal: {  van.Mileage} mil",
                    FontWeight = FontWeights.Medium,
                });
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Hjul: {van.Wheels}",
                    FontWeight = FontWeights.Medium,
                });
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Drivmedel: {van.FuelType}",
                    FontWeight = FontWeights.Medium,
                });
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"MaxVikt: { van.MaxWeight} CC",
                    FontWeight = FontWeights.Medium,
                });
            }

        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (van == null)
            {
                return;
            }
            TxtBlkPrice.Text = $"Pris: {Calendar.SelectedDates.Count * van.DailyRate / 100} kr";
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            if (van == null)
            {
                return;
            }
            string? error = van.LoanVan(DateOnly.FromDateTime(Calendar.SelectedDates.First() > Calendar.SelectedDates.Last() ? Calendar.SelectedDates.Last() : Calendar.SelectedDates.First()), DateOnly.FromDateTime(Calendar.SelectedDates.First() < Calendar.SelectedDates.Last() ? Calendar.SelectedDates.Last() : Calendar.SelectedDates.First()));
            if (error != null)
            {

            }
        }
    }
}
