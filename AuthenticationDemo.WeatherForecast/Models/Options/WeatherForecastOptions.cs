using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationDemo.WeatherForecast.Models.Options
{
    public class WeatherForecastOptions
    {
        public string BaseUrl { get; set; } = "https://api.ipregistry.co/";

        public string[] Scopes { get; set; }
    }
}
