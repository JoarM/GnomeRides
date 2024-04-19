﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnomeRides.Classes
{
    class Vehicle
    {
        protected readonly string _reg_nr;
        protected readonly int _seats;
        protected readonly string _manufacturer;
        protected readonly int _mileage;
        protected readonly int _wheels;
        protected readonly string _model;
        protected readonly string _fuel_type;
        protected readonly int _daily_rate;
        protected readonly string _owner_id;

        public Vehicle(string reg_nr,
            int seats, 
            string manufacturer,
            int mileage,
            int wheels,
            string model,
            string fuel_type,
            int daily_rate,
            string owner_id) 
        { 
            _reg_nr = reg_nr;
            _seats = seats;
            _manufacturer = manufacturer;
            _mileage = mileage;
            _wheels = wheels;
            _model = model;
            _fuel_type = fuel_type;
            _daily_rate = daily_rate;
            _owner_id = owner_id;
        }

        public string RegNr { get { return _reg_nr; } }
        public int Seats { get { return _seats; } }
        public string Manufacturer { get { return _manufacturer; } }
        public int Mileage { get { return _mileage; } }
        public int Wheels {  get { return _wheels; } }
        public string Model { get { return _model; } }
        public string FuelType { get { return _fuel_type; } }
        public int DailyRate { get { return _daily_rate; } }
    }
}
