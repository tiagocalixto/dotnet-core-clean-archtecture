using System;
using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyLittlePetShop.Api.ExceptionHandler;
using MyLittlePetShop.DataProvider.context;
using MyLittlePetShop.IoC;

namespace MyLittlePetShop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
            
            //db connect - PostgreSQL
            var connectionString = Configuration["DbContextSettings:ConnectionString"];

            services.AddDbContext<PostgreSqlContext>(options =>
                options.UseNpgsql(connectionString)
            );

            //enable all projects in solution read from appsettings.json
            services.AddSingleton(Configuration);

            //payloads validation activated
            services.AddMvc()
                          .AddFluentValidation(fvc =>
                                      fvc.RegisterValidatorsFromAssemblyContaining<Startup>());
            
            //enable JWT auth
            var secretKey = Configuration["JwtSecretKey"];

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            //auth
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
