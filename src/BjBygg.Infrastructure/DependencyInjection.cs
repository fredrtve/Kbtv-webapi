using BjBygg.Application.Application.Common;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using BjBygg.Core;
using BjBygg.Infrastructure.Api;
using BjBygg.Infrastructure.Api.FileStorage;
using BjBygg.Infrastructure.Api.SendGridMailService;
using BjBygg.Infrastructure.Auth;
using BjBygg.Infrastructure.Data;
using BjBygg.Infrastructure.Identity;
using BjBygg.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BjBygg.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlite(configuration.GetValue<string>("IdentityDbConnectionString"))); // will be created in web project root

            services.AddScoped<IAppIdentityDbContext>(provider => provider.GetService<AppIdentityDbContext>());

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = ValidationRules.UserPasswordMinLength;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<AppIdentityErrorDescriber>();

            services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();
            services.AddTransient<TokenFactory>();
            services.AddTransient<JwtFactory>();
            services.AddTransient<JwtTokenValidator>(); 

            services.AddTransient<ITokenManager, TokenManager>();

            var authSettings = configuration.GetSection(nameof(AuthSettings));

            services.AddOptions<AuthSettings>()
                .Bind(authSettings)
                .ValidateDataAnnotations();

            services.Configure<AuthSettings>(authSettings);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings[nameof(AuthSettings.SecretKey)]));

            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

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
                //ClockSkew = TimeSpan.Zero,
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

            return services;
        }
        public static IServiceCollection AddApplicationInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration.GetValue<string>("DbConnectionString"))); 

            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());

            services.AddSingleton<IIdGenerator, IdGenerator>();
            services.AddSingleton<IFileZipper, AzureBlobStorageZipper>();
            services.AddSingleton<ISyncTimestamps, SyncTimestamps>();

            services.AddTransient<IBlobStorageService, AzureBlobStorageService>();
            services.AddTransient<IMailService, SendGridMailService>(); 
            services.AddTransient<IGeocodeService, GoogleGeocodeService>();
            services.AddTransient<PdfMissionExtractor>();
            services.AddTransient<IImageResizer, ImageResizer>();

            return services;
        }
    }
}
