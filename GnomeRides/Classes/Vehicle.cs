﻿using GnomeRides.Utils;
using MySql.Data.MySqlClient;

namespace GnomeRides.Classes
{
    abstract class Vehicle
    {
        protected string _reg_nr;
        protected int _seats;
        protected string _manufacturer;
        protected int _mileage;
        protected int _wheels;
        protected string _model;
        protected string _fuel_type;
        protected int _daily_rate;
        protected string _owner_id;

        public string RegNr { get { return _reg_nr; } }
        public int Seats { get { return _seats; } }
        public string Manufacturer { get { return _manufacturer; } }
        public int Mileage { get { return _mileage; } }
        public int Wheels {  get { return _wheels; } }
        public string Model { get { return _model; } }
        public string FuelType { get { return _fuel_type; } }
        public int DailyRate { get { return _daily_rate; } }

        public string? DeleteVehicle()
        {
            try
            {
                using MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "DELETE vehicle WHERE reg_nr = @reg_nr;";
                cmd.Parameters.AddWithValue("@reg_nr", _reg_nr);
                cmd.ExecuteNonQuery();
            } catch
            {
                return "An unexpected error occured";
            }
            return null;
        }

        public string? UpdateMileage(int mileage)
        {
            try
            {
                using MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "UPDATE vehicle SET mileage = @mileage WHERE reg_nr = @reg_nr;";
                cmd.Parameters.AddWithValue("@mileage", mileage);
                cmd.Parameters.AddWithValue("@reg_nr", _reg_nr);
                cmd.ExecuteNonQuery();
            } catch
            {
                return "An unexpected error occured";
            }
            _mileage = mileage;
            return null;
        }

        protected virtual int? LoanVehicle(DateOnly StartDate, DateOnly EndDate)
        {
            if (User.CurrentUser ==  null)
            {
                return 1;
            }

            try
            {
                using (MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand())
                {
                    cmd.CommandText = "GET reg_nr FROM vehicle WHERE reg_nr = @reg_nr AND @start_date BETWEEN start_date AND end_date OR @end_date BETWEEN start_date AND end_date;";
                    cmd.Parameters.AddWithValue("@reg_nr", RegNr);
                    cmd.Parameters.AddWithValue("@start_date", StartDate);
                    cmd.Parameters.AddWithValue("@end_date", EndDate);
                    using MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return 2;
                    }
                }

                using (MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand())
                {
                    TimeSpan rentDays = EndDate.ToDateTime(new TimeOnly(0,0,0,0,0)) - StartDate.ToDateTime(new TimeOnly(0, 0, 0, 0, 0));
                    cmd.CommandText = "INSERT INTO loan (start_date, end_date, price, loan_owner_id, reg_nr) VALUES (@start_date, @end_date, @price, @loan_owner_id, @reg_nr);";
                    cmd.Parameters.AddWithValue("@start_date", StartDate);
                    cmd.Parameters.AddWithValue("@end_date", EndDate);
                    cmd.Parameters.AddWithValue("@price", rentDays.TotalDays * DailyRate);
                    cmd.Parameters.AddWithValue("@loan_owner_id", User.CurrentUser.Id);
                    cmd.Parameters.AddWithValue("@reg_nr", RegNr);
                    cmd.ExecuteNonQuery();
                }
            } catch 
            {
                return -1;
            }
            return null;
        }
    }

    class Car : Vehicle
    {
        private readonly int _co2;

        public Car(string reg_nr,
            int seats,
            string manufacturer,
            int mileage,
            int wheels,
            string model,
            string fuel_type,
            int daily_rate,
            string owner_id,
            int co2
        )
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
            _co2 = co2;
        }

        public int Co2 { get { return _co2; } }

        public string? LoanCar(DateOnly StartDate, DateOnly EndDate)
        {
            int? error = LoanVehicle(StartDate, EndDate);
            return error switch
            {
                null => null,
                1 => "Vänligen logga in för att hyra en bil.",
                2 => "Bilen är redan bokad under denna period.",
                _ => "Ett oväntat fel uppstod",
            };
        }
    }

    class MotorCycle : Vehicle
    {
        private readonly int _cc;

        public MotorCycle(string reg_nr,
            int seats,
            string manufacturer,
            int mileage,
            int wheels,
            string model,
            string fuel_type,
            int daily_rate,
            string owner_id, 
            int cc
        )
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
            _cc = cc;
        }

        public int CC { get { return _cc; } }

        public string? LoanMotorCycle(DateOnly StartDate, DateOnly EndDate)
        {
            int? error = LoanVehicle(StartDate, EndDate);
            return error switch
            {
                null => null,
                1 => "Vänligen logga in för att hyra en motorcyckel.",
                2 => "Motorcycklen är redan bokad under denna period.",
                _ => "Ett oväntat fel uppstod",
            };
        }
    }

    class Van : Vehicle
    {
        private readonly int _outer_width;
        private readonly int _outer_height;
        private readonly int _outer_length;
        private readonly int _inner_width;
        private readonly int _inner_height;
        private readonly int _inner_length;
        private readonly int _max_weight;
        private readonly int _volume;

        public Van(string reg_nr,
            int seats,
            string manufacturer,
            int mileage,
            int wheels,
            string model,
            string fuel_type,
            int daily_rate,
            string owner_id, 
            int outer_width, 
            int outer_height, 
            int outer_length, 
            int inner_width,
            int inner_height, 
            int inner_length, 
            int max_weight, 
            int volume
        )
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
            _outer_width = outer_width;
            _outer_height = outer_height;
            _outer_length = outer_length;
            _inner_width = inner_width;
            _inner_height = inner_height;
            _inner_length = inner_length;
            _max_weight = max_weight;
            _volume = volume;
        }

        public int OuterWidth { get { return _outer_width; } }
        public int OuterHeight { get { return _outer_height; } }
        public int OuterLength { get { return _outer_length; } }
        public int InnerWidth { get { return _inner_width; } }
        public int InnerHeight { get { return _inner_height; } }
        public int InnerLength { get { return _inner_length; } }
        public int MaxWeight { get { return _max_weight; } }
        public int Volume { get { return _volume; } }

        public string? LoanVan(DateOnly StartDate, DateOnly EndDate)
        {
            int? error = LoanVehicle(StartDate, EndDate);
            return error switch
            {
                null => null,
                1 => "Vänligen logga in för att hyra en skåpbil.",
                2 => "Skåpbilen är redan bokad under denna period.",
                _ => "Ett oväntat fel uppstod",
            };
        }
    }
}