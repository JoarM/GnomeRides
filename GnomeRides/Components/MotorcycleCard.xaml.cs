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
            bitmap.UriSource = new Uri("https://unifleet.se/wp-content/uploads/2020/10/Volvo-V60-Recharge-Vapour-Grey.png");
            bitmap.EndInit();

            ImgCover.Source = bitmap;
            TxtBlkName.Text = $"{motorcycle.Manufacturer} {motorcycle.Model}";
            TxtBlkPrice.Text = $"{motorcycle.DailyRate / 100} kr/dag";
        }
    }
}
