using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ConsoleApp5
{
    class Program
    {


        private static void prepareDatabase()
        {

        }

        static void Main(string[] args)
        {
            Car car = new Car();
            #region SQLConnection

            var connectionString = ConfigurationManager.ConnectionStrings["Store"].ConnectionString;
            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            

            #endregion



            List<Car> cars = new List<Car>() {
            new Car{
            IsNew=true,
            Model="Santafe",
            Vendor="Hundai",
            Year=2020
            },
            new Car{
            IsNew=false,
            Vendor="Mitsubishi",
            Model="Pajero",
            Year=2009
            },
              new Car{
            IsNew=false,
            Vendor="Mazda",
            Model="CX5",
            Year=2015
            }
            };

            var carsType = car.GetType();

            command.CommandText = "USE Store";
            command.CommandText = "CREATE TABLE Cars( Id INT)";
            command.ExecuteNonQuery();

            foreach (var item in carsType.GetProperties())
            {
                if (item.PropertyType == typeof(string))
                {
                    command.CommandText = $"ALTER TABLE Cars ADD {item.Name} NVARCHAR(255)";
                    command.ExecuteNonQuery();
                }
                else if(item.PropertyType == typeof(int))
                {
                    command.CommandText = $"ALTER TABLE Cars ADD {item.Name} INT";
                    command.ExecuteNonQuery();
                }
                else if (item.PropertyType == typeof(bool))
                {
                    command.CommandText = $"ALTER TABLE Cars ADD {item.Name} BIT";
                    command.ExecuteNonQuery();
                }
                Console.WriteLine(item.Name);
            }

            foreach (var item in cars)
            {
                
                command.CommandText=$"INSERT INTO Cars(IsNew,Vendor,Model,Year) values('{item.IsNew}','{item.Vendor}','{item.Model}','{item.Year}')";
                command.ExecuteNonQuery();
            }
            command.Dispose();
            connection.Close();
        }
    }
}