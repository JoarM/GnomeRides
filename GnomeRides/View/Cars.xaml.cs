﻿using GnomeRides.Classes;
using GnomeRides.Controlers;
using GnomeRides.Components;
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
    /// Interaction logic for Cars.xaml
    /// </summary>
    public partial class Cars : Page
    {
        List<Car> cars;
        public Cars()
        {
            InitializeComponent();
            (List<Car>, string?) Carlist = CarController.GetCars();
            cars = Carlist.Item1;
            foreach (Car car in cars)
            {
                CarGrid.AddChild(new CarCard(car));
            }
            if (Carlist.Item2 != null)
            {
                MessageBox.Show(Carlist.Item2, "Bilar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PriceSlider_HigherValueChanged(object sender, RoutedEventArgs e)
        {
            ottovordemgentchenfeld(PriceSlider.LowerValue, PriceSlider.HigherValue);
            donaudampfschiffahrt(PriceSlider.LowerValue, PriceSlider.HigherValue);
        }

        private void ottovordemgentchenfeld(double x, double y) {
            Wolfeschlegelsteinhausen.Text = ($"{Math.Round(x) * 10} - {Math.Round(y) * 10}{(y == PriceSlider.Maximum ? "+": null)} kr/dag");
        }

        private void PriceSlider_LowerValueChanged(object sender, RoutedEventArgs e)
        {
            ottovordemgentchenfeld(PriceSlider.LowerValue, PriceSlider.HigherValue);
            donaudampfschiffahrt(PriceSlider.LowerValue, PriceSlider.HigherValue);
        }

        private void donaudampfschiffahrt(double x, double y)
        {
            if (CarGrid == null)
            {
                return;
            }
            CarGrid.Clear();
            foreach (Car car in cars)
            {
                if ((car.DailyRate < Convert.ToUInt32(y) * 1000 || y == PriceSlider.Maximum) && car.DailyRate > x * 1000) {
                    CarGrid.AddChild(new CarCard(car));
                }
            }
        }
    }
}
