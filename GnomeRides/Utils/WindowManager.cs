using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GnomeRides.Utils
{
    class WindowManager
    {
        public static void OpenNewStandaloneWindow(Window window)
        {
            for (int i = App.Current.Windows.Count - 1; i >= 0; i--)
            {
                if (App.Current.Windows[i] != window)
                {
                    App.Current.Windows[i].Close();
                }
            }
            window.Show();
        }
    }
}
