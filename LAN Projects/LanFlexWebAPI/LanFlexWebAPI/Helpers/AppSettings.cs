using Microsoft.Extensions.Configuration;

namespace LanFlexWebAPI.Helpers
{
    public class AppSettings
    {
        public string connectionString = string.Empty;

        public AppSettings(IConfiguration config)
        {
            connectionString = config.GetValue<string>("ConnectionString");
        }
    }
}