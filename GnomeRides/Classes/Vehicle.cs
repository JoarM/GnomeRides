using GnomeRides.Utils;
using MySql.Data.MySqlClient;

namespace GnomeRides.Classes
{
    public abstract class Vehicle
    {
        protected string _reg_nr;
        protected uint _seats;
        protected string _manufacturer;
        protected uint _mileage;
        protected uint _wheels;
        protected string _model;
        protected string _fuel_type;
        protected uint _daily_rate;
        protected string _owner_id;

        public string RegNr { get { return _reg_nr; } }
        public uint Seats { get { return _seats; } }
        public string Manufacturer { get { return _manufacturer; } }
        public uint Mileage { get { return _mileage; } }
        public uint Wheels {  get { return _wheels; } }
        public string Model { get { return _model; } }
        public string FuelType { get { return _fuel_type; } }
        public uint DailyRate { get { return _daily_rate; } }
        /// <summary>
        /// Deletes vehicle
        /// </summary>
        /// <returns>An error message on error and null on success</returns>
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
        /// <summary>
        /// Updates the vehicles mileage
        /// </summary>
        /// <param name="mileage"></param>
        /// <returns>An error message on error and null on success</returns>
        public string? UpdateMileage(uint mileage)
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
        /// <summary>
        /// Loan the vehicle to the currentUser
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns>An error code on error and null on success</returns>
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
                    cmd.CommandText = "SELECT reg_nr FROM loan WHERE reg_nr = @reg_nr " +
                        "AND start_date BETWEEN @start_date AND @end_date " +
                        "OR end_date BETWEEN @start_date AND @end_date;";
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
                    cmd.CommandText = "INSERT INTO loan (start_date, end_date, price, loan_owner_id, reg_nr) " +
                        "VALUES (@start_date, @end_date, @price, @loan_owner_id, @reg_nr);";
                    cmd.Parameters.AddWithValue("@start_date", StartDate);
                    cmd.Parameters.AddWithValue("@end_date", EndDate);
                    cmd.Parameters.AddWithValue("@price", (rentDays.TotalDays + 1) * DailyRate);
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

    public class Car : Vehicle
    {
        private readonly uint _co2;

        public Car(string reg_nr,
            uint seats,
            string manufacturer,
            uint mileage,
            uint wheels,
            string model,
            string fuel_type,
            uint daily_rate,
            string owner_id,
            uint co2
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

        public uint Co2 { get { return _co2; } }
        /// <summary>
        /// Loan the car to the currentUser
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns>An error message on error and null on success</returns>
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

    public class Motorcycle : Vehicle
    {
        private readonly uint _cc;

        public Motorcycle(string reg_nr,
            uint seats,
            string manufacturer,
            uint mileage,
            uint wheels,
            string model,
            string fuel_type,
            uint daily_rate,
            string owner_id, 
            uint cc
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

        public uint CC { get { return _cc; } }
        /// <summary>
        /// Loan the motorcycle to the currentUser
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns>An error message on error and null on success</returns>
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

    public class Van : Vehicle
    {
        private readonly uint _outer_width;
        private readonly uint _outer_height;
        private readonly uint _outer_length;
        private readonly uint _inner_width;
        private readonly uint _inner_height;
        private readonly uint _inner_length;
        private readonly uint _max_weight;
        private readonly uint _volume;

        public Van(string reg_nr,
            uint seats,
            string manufacturer,
            uint mileage,
            uint wheels,
            string model,
            string fuel_type,
            uint daily_rate,
            string owner_id, 
            uint outer_width, 
            uint outer_height, 
            uint outer_length, 
            uint inner_width,
            uint inner_height, 
            uint inner_length, 
            uint max_weight, 
            uint volume
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

        public uint OuterWidth { get { return _outer_width; } }
        public uint OuterHeight { get { return _outer_height; } }
        public uint OuterLength { get { return _outer_length; } }
        public uint InnerWidth { get { return _inner_width; } }
        public uint InnerHeight { get { return _inner_height; } }
        public uint InnerLength { get { return _inner_length; } }
        public uint MaxWeight { get { return _max_weight; } }
        public uint Volume { get { return _volume; } }
        /// <summary>
        /// Loan the van to the currentUser
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns>An error message on error and null on success</returns>
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
