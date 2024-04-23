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
        };

        public static readonly List<KeyValuePair<uint, string>> FuelTypes = new List<KeyValuePair<uint, string>>()
        {
            new KeyValuePair<uint, string>(1, "Bensin"),
        };
    }
}
