using GnomeRides.Classes;
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
using GnomeRides.Components;

namespace GnomeRides.View
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public Profile()
        {
            InitializeComponent();
            TxtBlkName.Text = User.CurrentUser.Name;
            TxtBlkEmail.Text = User.CurrentUser.Email;

            (List<(Vehicle, Loan)>, string?) loansRes = User.CurrentUser.GetLoans();
            (List<Vehicle>, string?) vehicleRes = User.CurrentUser.GetVehicles();
            foreach (var loan in loansRes.Item1) 
            {
                BookingGrid.AddChild(new LoanVehicle(loan.Item1, loan.Item2));
            }

            foreach (var vehicle in vehicleRes.Item1)
            {
                VehicleGrid.AddChild(new OwnedVehicle(vehicle));
            }
        }
    }
}
