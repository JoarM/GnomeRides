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
    /// Interaction logic for MotorcyclePage.xaml
    /// </summary>
    public partial class MotorcyclePage : Page
    {
        private readonly Motorcycle? motorcycle;
        public MotorcyclePage(string reg_nr)
        {
            InitializeComponent();
            (Motorcycle?, string?) res = MotorcycleController.GetMotorcycleByRegNr(reg_nr);
            Motorcycle? motorcycle = res.Item1;
            string? error = res.Item2;

            this.motorcycle = motorcycle;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://unifleet.se/wp-content/uploads/2020/10/Volvo-V60-Recharge-Vapour-Grey.png");
            bitmap.EndInit();

            ImgMotorcycle.Source = bitmap;
            this.Loaded += (s, e) =>
            {
                if (motorcycle == null)
                {
                    NavigationService.Navigate(new Error(error));
                }
            };
            if (motorcycle != null)
            {
                TxtBlkModel.Text = $"{motorcycle.Manufacturer} {motorcycle.Model}";
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Säten: {motorcycle.Seats}",
                    FontWeight = FontWeights.Medium,
                });
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Miltal: {motorcycle.Mileage} mil",
                    FontWeight = FontWeights.Medium,
                });
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Hjul: {motorcycle.Wheels}",
                    FontWeight = FontWeights.Medium,
                });
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Drivmedel: {motorcycle.FuelType}",
                    FontWeight = FontWeights.Medium,
                });
                InfoGrid.AddChild(new TextBlock()
                {
                    Text = $"Cylinder: {motorcycle.CC} CC",
                    FontWeight = FontWeights.Medium,
                });
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (motorcycle == null)
            {
                return;
            }
            TxtBlkPrice.Text = $"Pris: {Calendar.SelectedDates.Count * motorcycle.DailyRate / 100} kr";
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            if (motorcycle == null)
            {
                return;
            }
            string? error = motorcycle.LoanMotorcycle(DateOnly.FromDateTime(Calendar.SelectedDates.First() > Calendar.SelectedDates.Last() ? Calendar.SelectedDates.Last() : Calendar.SelectedDates.First()), DateOnly.FromDateTime(Calendar.SelectedDates.First() < Calendar.SelectedDates.Last() ? Calendar.SelectedDates.Last() : Calendar.SelectedDates.First()));
            if (error != null)
            {

            }
        }
    }
}
