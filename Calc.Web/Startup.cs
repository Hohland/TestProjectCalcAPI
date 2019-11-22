using Calc.Web.Services;
using Calc.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Calc.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<ICalcService, CalcService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Calculator API", Version = "v1" });

                var reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = CustomAuthExtensions.AuthenticationScheme };
                c.AddSecurityDefinition(CustomAuthExtensions.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "Token",
                    Reference = reference
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = CustomAuthExtensions.AuthenticationScheme,
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = reference
                        },
                        new List<string>()
                    }
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CustomAuthExtensions.AuthenticationScheme;
                options.DefaultChallengeScheme = CustomAuthExtensions.AuthenticationScheme;
            }).AddCustomAuth(o => { });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Add", policy => policy.Requirements.Add(new AddPermissionRequirement()));
                options.AddPolicy("Multiplicate", policy => policy.Requirements.Add(new MultiplicatePermissionRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionMiddleware();
            }
            else
            {
                app.UseExceptionMiddleware();
                app.UseExceptionHandler();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calculator API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
