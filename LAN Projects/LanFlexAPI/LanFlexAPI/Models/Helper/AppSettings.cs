using System.Configuration;

namespace LanFlexAPI.Models.Helper
{
    public class AppSettings
    {
        public string connectionString = string.Empty;

        public AppSettings()
        {            
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
    }
}