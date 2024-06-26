﻿using GnomeRides.Classes;
using GnomeRides.Utils;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace GnomeRides.Controlers
{
    internal class CarController
    {
        /// <summary>
        /// Gives you a list of all cars
        /// </summary>
        /// <returns>A tuple containg a list of cars and an error message or null</returns>
        public static (List<Car>, string?) GetCars() 
        { 
            List<Car> CarList = new();
            try
            {
                using MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "SELECT vehicle.reg_nr, " +
                    "vehicle.seats, " +
                    "vehicle.manufacturer, " +
                    "vehicle.mileage, vehicle.wheels, " +
                    "vehicle.model, " +
                    "vehicle.fuel_type, " +
                    "vehicle.daily_rate," +
                    " vehicle.owner_id, " +
                    "vehicle.image_url, " +
                    "car.co2 " +
                    "FROM vehicle " +
                    "INNER JOIN car " +
                    "ON vehicle.reg_nr = car.reg_nr;";
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Car car = new(
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
                    CarList.Add(car);
                }
            } catch
            {
                return (CarList, "Ett oväntat fel uppstod");
            }
            return (CarList, null);
        }

        /// <summary>
        /// Returns a given car by the registratinon nummber
        /// </summary>
        /// <param name="regNr"></param>
        /// <returns>A tuple containing a car or null and an error message or null</returns>
        public static (Car?, string?) GetCarByRegNr(string regNr)
        {
            Car? car = null;
            try
            {
                using MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "SELECT vehicle.reg_nr, " +
                    "vehicle.seats, " +
                    "vehicle.manufacturer, " +
                    "vehicle.mileage, vehicle.wheels, " +
                    "vehicle.model, " +
                    "vehicle.fuel_type, " +
                    "vehicle.daily_rate," +
                    " vehicle.owner_id, " +
                    "vehicle.image_url, " +
                    "car.co2 " +
                    "FROM vehicle " +
                    "INNER JOIN car " +
                    "ON vehicle.reg_nr = car.reg_nr " +
                    "WHERE vehicle.reg_nr = @reg_nr;";
                cmd.Parameters.AddWithValue("@reg_nr", regNr);
                using MySqlDataReader reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    return (car, "Ett oväntat fel uppstod");
                }
                car = new(
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
                return (car, "Ett oväntat fel uppstod");
            }
            return (car, null);
        }

        /// <summary>
        /// Adds a car to the database
        /// </summary>
        /// <param name="regNr"></param>
        /// <param name="seats"></param>
        /// <param name="manufacturer"></param>
        /// <param name="mileage"></param>
        /// <param name="wheels"></param>
        /// <param name="model"></param>
        /// <param name="fuelType"></param>
        /// <param name="dailyRate"></param>
        /// <param name="co2"></param>
        /// <returns>An error message on error and null on success</returns>
        public static string? AddCar(string regNr, uint seats, uint manufacturer, uint mileage, uint wheels, string model, uint fuelType, uint dailyRate,  uint co2)
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
                    "INSERT INTO car (reg_nr, co2) VALUES (@reg_nr, @co2);";
                cmd.Parameters.AddWithValue("@reg_nr", regNr);
                cmd.Parameters.AddWithValue("@seats", seats);
                cmd.Parameters.AddWithValue("@manufacturer", manufacturer);
                cmd.Parameters.AddWithValue("@mileage", mileage);
                cmd.Parameters.AddWithValue("@wheels", wheels);
                cmd.Parameters.AddWithValue("model", model);
                cmd.Parameters.AddWithValue("@fuel_type", fuelType);
                cmd.Parameters.AddWithValue("@daily_rate", dailyRate);
                cmd.Parameters.AddWithValue("@owner_id", User.CurrentUser.Id);
                cmd.Parameters.AddWithValue("@image_url", "https://unifleet.se/wp-content/uploads/2020/10/Volvo-V60-Recharge-Vapour-Grey.png");
                cmd.Parameters.AddWithValue("@co2", co2);
                cmd.ExecuteNonQuery();
            } catch (Exception ex)
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
