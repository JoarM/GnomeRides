using GnomeRides.Classes;
using GnomeRides.View;
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

namespace GnomeRides.Components
{
    /// <summary>
    /// Interaction logic for AutoGrid.xaml
    /// </summary>
    public partial class AutoGrid : UserControl
    {
        private int children = 0;
        public AutoGrid()
        {
            InitializeComponent();
        }

        public void AddChild(FrameworkElement child)
        {
            children++;
            if (children > 3)
            {
                child.Margin = new Thickness(0, 16, 0, 0);
            }
            if (children % 3 == 1)
            {
                Panel1.Children.Add(child);
            }
            else if (children % 3 == 2)
            {
                Panel2.Children.Add(child);
            }
            else
            {
                Panel3.Children.Add(child);
            }
        }

        public void Clear()
        {
            children = 0;
            Panel1.Children.Clear();
            Panel2.Children.Clear();
            Panel3.Children.Clear();
        }
    }
}
