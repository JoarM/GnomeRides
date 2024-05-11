using GnomeRides.Classes;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace GnomeRides.Components
{
    /// <summary>
    /// Interaction logic for MotorcycleCard.xaml
    /// </summary>
    public partial class MotorcycleCard : UserControl
    {
        public MotorcycleCard(Motorcycle motorcycle)
        {
            InitializeComponent();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://images5.1000ps.net/images_bikekat/2024/7-BMW/9949-F_900_XR/013-638239841766558822-bmw-f-900-xr.jpg?width=520&height=380&mode=crop");
            bitmap.EndInit();

            ImgCover.Source = bitmap;
            TxtBlkName.Text = $"{motorcycle.Manufacturer} {motorcycle.Model}";
            TxtBlkPrice.Text = $"{motorcycle.DailyRate / 100} kr/dag";
        }
    }
}
