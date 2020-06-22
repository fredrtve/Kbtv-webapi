using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using MediatR;
using System.Reflection;
using BjBygg.Application.Queries.MissionQueries;
using CleanArchitecture.Infrastructure.Api.FileStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BjBygg.Application.Shared;
using CleanArchitecture.Infrastructure.Api.SendGridMailService;
using Microsoft.AspNetCore.Http.Features;
using System;
using CleanArchitecture.Infrastructure.Auth;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace BjBygg.WebApi
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
            services.AddCors();

            services.AddIdentityDbContext();

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 7;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            var authSettings = Configuration.GetSection(nameof(AuthSettings));
            services.Configure<AuthSettings>(authSettings);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings[nameof(AuthSettings.SecretKey)]));

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                  options.TokenValidationParameters = tokenValidationParameters;
                  options.SaveToken = true;

                  options.Events = new JwtBearerEvents
                  {
                      OnAuthenticationFailed = context =>
                      {
                          if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                          {
                              context.Response.Headers.Add("Token-Expired", "true");   
                          }
                          return Task.CompletedTask;
                      }
                  };
              });

            services.AddAppDbContext();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kbtv API", Version = "v1" });
            });

            services.Configure<FormOptions>(options => 
            {
                options.MemoryBufferThreshold = Int32.MaxValue;
            });

            services.AddAutoMapper(Assembly.GetAssembly(typeof(MissionDtoProfile)));

            services.AddMediatR(Assembly.GetAssembly(typeof(MissionByIdQuery)));

            services.AddTransient<IMailService, SendGridMailService>();

            services.AddSingleton<IBlobStorageService, AzureBlobStorageService>();

            services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();
            services.AddTransient<ITokenFactory, TokenFactory>();
            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddTransient<IJwtTokenValidator, JwtTokenValidator>();

            //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IActionContextAccessor, ActionContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseCors(options => options.WithOrigins(Configuration.GetValue<string>("CorsOrigin"))
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials()
            );

            app.UseHttpsRedirection();
          
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KBTV WebAPI V1");
            });


            app.UseRouting();

            app.UseAuthentication();

            //app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}
