﻿using GnomeRides.Classes;
using GnomeRides.Utils;
using MySql.Data.MySqlClient;

namespace GnomeRides.Controlers
{
    internal class MotorcycleController
    {
        /// <summary>
        /// Gives you a list of all motorcycles
        /// </summary>
        /// <returns>A tuple containg a list of motorcycles and an error message or null</returns>
        public static (List<Motorcycle>, string?) GetMotorcycles()
        {
            List<Motorcycle> MotorcycleList = new();
            try
            {
                using MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "SELECT vehicle.reg_nr, " +
                    "vehicle.seats, " +
                    "vehicle.manufacturer, " +
                    "vehicle.mileage, vehicle.wheels, " +
                    "vehicle.model, " +
                    "vehicle.fuel_type, " +
                    "vehicle.daily_rate, " +
                    "vehicle.owner_id, " +
                    "vehicle.image_url, " +
                    "motorcycle.cc " +
                    "FROM vehicle " +
                    "INNER JOIN motorcycle " +
                    "ON vehicle.reg_nr = motorcycle.reg_nr;";
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Motorcycle motorcycle = new(
                        reader.GetString(0),
                        reader.GetUInt16(1),
                        Constants.VehicleManufacturers.Find(kvp => kvp.Key == reader.GetUInt16(2)).Value,
                        reader.GetUInt32(3),
                        reader.GetUInt16(4),
                        reader.GetString(5),
                        Constants.FuelTypes.Find(kvp => kvp.Key == reader.GetUInt16(6)).Value,
                        reader.GetUInt32(7),
                        reader.GetString(8),
                        (reader.IsDBNull(9) ? null : reader.GetString(9)),
                        reader.GetUInt16(10)
                    );
                    MotorcycleList.Add(motorcycle);
                }
            }
            catch 
            {
                return (MotorcycleList, "Ett oväntat fel uppstod");
            }
            return (MotorcycleList, null);
        }

        /// <summary>
        /// Returns a given motorcycle by the registratinon nummber
        /// </summary>
        /// <param name="regNr"></param>
        /// <returns>A tuple containing a motorcycle or null and an error message or null</returns>
        public static (Motorcycle?, string?) GetMotorcycleByRegNr(string regNr)
        {
            Motorcycle? motorcycle = null;
            try
            {
                using MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "SELECT vehicle.reg_nr, " +
                    "vehicle.seats, " +
                    "vehicle.manufacturer, " +
                    "vehicle.mileage, vehicle.wheels, " +
                    "vehicle.model, " +
                    "vehicle.fuel_type, " +
                    "vehicle.daily_rate, " +
                    "vehicle.owner_id, " +
                    "vehicle.image_url, " +
                    "motorcycle.cc " +
                    "FROM vehicle " +
                    "INNER JOIN motorcycle " +
                    "ON vehicle.reg_nr = motorcycle.reg_nr " +
                    "WHERE vehicle.reg_nr = @reg_nr;";
                cmd.Parameters.AddWithValue("@reg_nr", regNr);
                using MySqlDataReader reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    return (motorcycle, "Ett oväntat fel uppstod");
                }
                motorcycle = new(
                    reader.GetString(0),
                    reader.GetUInt16(1),
                    Constants.VehicleManufacturers.Find(kvp => kvp.Key == reader.GetUInt16(2)).Value,
                    reader.GetUInt32(3),
                    reader.GetUInt16(4),
                    reader.GetString(5),
                    Constants.FuelTypes.Find(kvp => kvp.Key == reader.GetUInt16(6)).Value,
                    reader.GetUInt32(7),
                    reader.GetString(8),
                    (reader.IsDBNull(9) ? null : reader.GetString(9)),
                    reader.GetUInt16(10)
                );
            }
            catch
            {
                return (motorcycle, "Ett oväntat fel uppstod");
            }
            return (motorcycle, null);
        }

        /// <summary>
        /// Adds a motorcycle to the database
        /// </summary>
        /// <param name="regNr"></param>
        /// <param name="seats"></param>
        /// <param name="manufacturer"></param>
        /// <param name="mileage"></param>
        /// <param name="wheels"></param>
        /// <param name="model"></param>
        /// <param name="fuelType"></param>
        /// <param name="dailyRate"></param>
        /// <param name="cc"></param>
        /// <returns>An error message on error and null on success</returns>
        public static string? AddMotorcycle(string regNr, uint seats, uint manufacturer, uint mileage, uint wheels, string model, uint fuelType, uint dailyRate, uint cc)
        {
            if (User.CurrentUser == null)
            {
                return "Logga in för att lägga upp ett fordon för uthyrning";
            }

            try
            {
                using MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "INSERT INTO vehicle (reg_nr, seats, manufacturer, mileage, wheels, model, fuel_type, daily_rate, owner_id, image_url) " +
                    "VALUES (@reg_nr, @seats, @manufacturer, @mileage, @wheels, @model, @fuel_type, @daily_rate, @owner_id, @image_url);" +
                    "INSERT INTO motorcycle (reg_nr, cc) VALUES (@reg_nr, @cc);";
                cmd.Parameters.AddWithValue("@reg_nr", regNr);
                cmd.Parameters.AddWithValue("@seats", seats);
                cmd.Parameters.AddWithValue("@manufacturer", manufacturer);
                cmd.Parameters.AddWithValue("@mileage", mileage);
                cmd.Parameters.AddWithValue("@wheels", wheels);
                cmd.Parameters.AddWithValue("model", model);
                cmd.Parameters.AddWithValue("@fuel_type", fuelType);
                cmd.Parameters.AddWithValue("@daily_rate", dailyRate);
                cmd.Parameters.AddWithValue("@owner_id", User.CurrentUser.Id);
                cmd.Parameters.AddWithValue("@image_url", "https://images5.1000ps.net/images_bikekat/2024/7-BMW/9949-F_900_XR/013-638239841766558822-bmw-f-900-xr.jpg?width=520&height=380&mode=crop");
                cmd.Parameters.AddWithValue("@cc", cc);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if ((int)ex.GetType().GetProperty("Number").GetValue(ex, null) == 1062)
                {
                    return "Ett fordon med detta regestrerings nummer finns redan utlagt";
                }
                return "Ett oväntat fel uppstod";
            }
            return null;
        }
    }
}
