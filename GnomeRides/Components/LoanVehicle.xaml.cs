using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GnomeRides.Classes;

namespace GnomeRides.Components
{
    /// <summary>
    /// Interaction logic for LoanVehicle.xaml
    /// </summary>
    public partial class LoanVehicle : UserControl
    {
        public LoanVehicle(Vehicle vehicle, Loan loan)
        {
            InitializeComponent();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://unifleet.se/wp-content/uploads/2020/10/Volvo-V60-Recharge-Vapour-Grey.png");
            bitmap.EndInit();

            ImgCover.Source = bitmap;
            TxtBlkName.Text = $"{vehicle.Manufacturer} {vehicle.Model}";
            TxtBlkPrice.Text = (loan.Price / 100).ToString();
            TxtBlkDates.Text = $"From {loan.StartDate} To {loan.EndDate}";
        }
    }
}
