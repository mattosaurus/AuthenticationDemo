using AuthenticationDemo.WeatherForecast.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace AuthenticationDemo.Ui
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // https://www.josephguadagno.net/2020/06/26/connecting-to-an-api-protected-by-microsoft-identity-platform
            // https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-web-app-call-api-app-configuration?tabs=aspnetcore
            // https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-web-app-call-api-acquire-token?tabs=aspnetcore
            var builder = WebApplication.CreateBuilder(args);

            var initialScopes = builder.Configuration["DownstreamApi:Scopes"]?.Split(' ') ?? builder.Configuration["MicrosoftGraph:Scopes"]?.Split(' ');

            // Add services to the container.
            builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
                .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
                .AddDownstreamWebApi("DownstreamApi", builder.Configuration.GetSection("DownstreamApi"))
                .AddInMemoryTokenCaches();

            builder.Services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            builder.Services.AddRazorPages()
                .AddMicrosoftIdentityUI();
            
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = options.DefaultPolicy;
            });

            builder.Services.AddWeatherForecast(options =>
            {
                options.BaseUrl = "https://localhost:7219/";
                options.Scopes = new[] { "api://2f8b9358-35ad-461c-bae4-73d09b883808/Api.Access" };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
