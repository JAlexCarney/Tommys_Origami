
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Core.Interfaces;
using TripPlanner.DAL;
using TripPlanner.DAL.Repos;

namespace TripPlanner.Web
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
           {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "http://localhost:2000",
                    ValidAudience = "http://localhost:2000",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"))
                };
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            });
            services.AddControllersWithViews();
            services.AddTransient<ITripRepository, EFTripRepository>();
            services.AddTransient<IUserRepository, EFUserRepository>();
            services.AddTransient<IReviewRepository, EFReviewRepository>();
            services.AddTransient<IDestinationRepository, EFDestinationRepository>();
            services.AddTransient<IDestinationTripRepository, EFDestinationTripRepository>();
            services.AddTransient<IReportsRepository, ADOReportsRepository>();


            services.AddCors(options => options.AddPolicy("corspolicy", (builder) =>
            {
                builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("corspolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
