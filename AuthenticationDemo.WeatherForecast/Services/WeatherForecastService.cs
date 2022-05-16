using AuthenticationDemo.WeatherForecast.Authenticators;
using AuthenticationDemo.WeatherForecast.Models.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationDemo.WeatherForecast.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly RestClient _restClient;
        private readonly ILogger<WeatherForecastService> _logger;
        private static readonly string[] Summaries = new[]
{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastService(IOptions<WeatherForecastOptions> options, ITokenAcquisition tokenAcquisition)
        {
            RestClient restClient = new RestClient(options.Value.BaseUrl);
            restClient.Authenticator = new TokenAuthenticator(tokenAcquisition, options.Value.Scopes);

            _restClient = restClient;
            _logger = new LoggerFactory().CreateLogger<WeatherForecastService>();
        }

        public async Task<IEnumerable<Models.WeatherForecast>> GetWeatherForecastsAsync()
        {
            RestRequest request = new RestRequest("WeatherForecast", Method.Get);
            return await _restClient.GetAsync<IEnumerable<Models.WeatherForecast>>(request);
        }
    }
}
