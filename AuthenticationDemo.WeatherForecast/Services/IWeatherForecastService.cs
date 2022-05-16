using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationDemo.WeatherForecast.Services
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<Models.WeatherForecast>> GetWeatherForecastsAsync();
    }
}
