using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TripPlanner.DAL
{
    class SettingsManager
    {
        private static string _connectionString;

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                var builder = new ConfigurationBuilder();

                builder.AddUserSecrets<Program>();

                var config = builder.Build();

                _connectionString = config["ConnectionStrings:TripPlanner"];
            }

            return _connectionString;
        }
    }
}
