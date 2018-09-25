using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Annies.Common
{
   public class ConfigurationUtilities
    {
        public static string GetConnectionString()
        {
            var value = ConfigurationManager.ConnectionStrings[Constants.ConfigurationKeys.ConnectionString];

            if (value != null)
            {
                return value.ConnectionString;
            }
            return default(string);
        }
        public static string GetAppSettings(string key)
        {
            var value = ConfigurationManager.AppSettings[key];

            if (value != null)
            {
                return value.ToString();
            }
            return default(string);
        }
    }
}
