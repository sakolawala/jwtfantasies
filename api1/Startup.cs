using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace api1
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
            string aud = Configuration.GetSection("jwt:Audience").Value;
            string aut = Configuration.GetSection("jwt:Authority").Value;
            bool requireHttps = Configuration.GetValue<bool>("jwt:RequireHttps");

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                ConfigureWithOfflineValidation(o, aud);
            });
            services.AddLogging(c => c.AddConsole());
            services.AddMvc();
        }

        private void ConfigureWithOfflineValidation(JwtBearerOptions o, string aud)
        {
            string certificatePath = Configuration.GetSection("jwt:CertificatePath").Value;
            string certificatePassword = Configuration.GetSection("jwt:CertificatePassword").Value;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidIssuer = "http://10.4.1.232/kiwiservices/oauthservice",
                IssuerSigningKey = new X509SecurityKey(new X509Certificate2(certificatePath, certificatePassword)),
            };

            o.Audience = aud;
            o.TokenValidationParameters = tokenValidationParameters;
        }

        private void ConfigureWithOnlineValidation
            (JwtBearerOptions o, string aud, string aut, bool requireHttps)
        {
            o.Audience = aud;
            o.Authority = aut;
            o.RequireHttpsMetadata = requireHttps;
            o.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    Debug.WriteLine("OnAuthenticationFailed");
                    c.NoResult();
                    c.Response.StatusCode = 401;
                    return c.Response.WriteAsync("Invalid Token or No Token");
                }
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
