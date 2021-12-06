using LanFlexAPI.Models.Helper;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LanFlexAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new AppSettingsCorsAttribute("AllowedCorsOrigins", "AllowedCorsHeaders", "AllowedCorsMethods");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
