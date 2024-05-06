using GnomeRides.Classes;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GnomeRides.Components
{
    /// <summary>
    /// Interaction logic for VanCard.xaml
    /// </summary>
    public partial class VanCard : UserControl
    {
        public VanCard(Van van)
        {
            InitializeComponent();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://unifleet.se/wp-content/uploads/2020/10/Volvo-V60-Recharge-Vapour-Grey.png");
            bitmap.EndInit();

            ImgCover.Source = bitmap;
            TxtBlkName.Text = $"{van.Manufacturer} {van.Model}";
            TxtBlkPrice.Text = $"{van.DailyRate / 100} kr/dag";
        }
    }
}
