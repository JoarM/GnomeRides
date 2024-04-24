using GnomeRides.Utils;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace GnomeRides
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            
            var root = Path.GetFullPath($"C:\\Users\\{Environment.UserName}\\source\\repos\\GnomeRides\\GnomeRides");
            DotEnv.Load(Path.Combine(root, ".env.local"));
        }

        protected override void OnExit(ExitEventArgs e)
        {
            MySqlAdapter.Connection.Close();
        }
    }

}
