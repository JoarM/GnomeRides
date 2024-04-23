using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnomeRides.Classes
{
    internal class Constans
    {
        public static readonly List<KeyValuePair<uint, string>> VehicleManufacturers = new List<KeyValuePair<uint, string>>() 
        {
            new KeyValuePair<uint, string>(1, "Volvo"),
            new KeyValuePair<uint, string>(2, "Saab"),
            new KeyValuePair<uint, string>(3, "BMW"),
            new KeyValuePair<uint, string>(4, "Audi"),
            new KeyValuePair<uint, string>(5, "Volkswagen"),
            new KeyValuePair<uint, string>(6, "Ford"),
            new KeyValuePair<uint, string>(7, "Kia"),
            new KeyValuePair<uint, string>(8, "Mitsubishi"),
            new KeyValuePair<uint, string>(9, "Nissan"),
            new KeyValuePair<uint, string>(10, "Mercedes")
        };

        public static readonly List<KeyValuePair<uint, string>> FuelTypes = new List<KeyValuePair<uint, string>>()
        {
            new KeyValuePair<uint, string>(1, "Bensin"),
            new KeyValuePair<uint, string>(2, "Disel"),
            new KeyValuePair<uint, string>(3, "El"),
        };
    }
}
