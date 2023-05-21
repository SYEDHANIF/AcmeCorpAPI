using AcmeCorpAPI.Authentication;
using AcmeCorpAPI.Entities;
using AcmeCorpAPI.EntitiesData;
using AcmeCorpAPI.Interface;
using AspNetCore.Authentication.ApiKey;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace AcmeCorpAPI
{
    public class Startup
    {
        

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<CustomerDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<CustomerDbContext>(options =>
            {
                string connectionString = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IApiKeyService, ApiKeyService>();

            services.AddAuthentication(ApiKeyDefaults.AuthenticationScheme)
                .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyDefaults.AuthenticationScheme, options => { });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiKeyPolicy", policy =>
                {
                    policy.AuthenticationSchemes.Add(ApiKeyDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Other app configurations

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization("ApiKeyPolicy");
            });
        }

    }
}
