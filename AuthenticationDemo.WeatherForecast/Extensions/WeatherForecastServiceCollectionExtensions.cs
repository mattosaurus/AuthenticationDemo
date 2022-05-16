using AuthenticationDemo.WeatherForecast.Authenticators;
using AuthenticationDemo.WeatherForecast.Models.Options;
using AuthenticationDemo.WeatherForecast.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationDemo.WeatherForecast.Extensions
{
    public static class WeatherForecastServiceCollectionExtensions
    {
        public static IServiceCollection AddWeatherForecast(this IServiceCollection collection, Action<WeatherForecastOptions> setupAction)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            collection.Configure(setupAction);

            collection.AddTransient<IWeatherForecastService, WeatherForecastService>();

            return collection;
        }
    }
}
