using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Saitynai.Data.Repositories;
using Saitynai.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoRestSimonas.Auth;
using Saitynai.Data.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Saitynai.Auth;
using Saitynai.Auth.Model;
using Microsoft.EntityFrameworkCore;

namespace Saitynai
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters.ValidAudience = _configuration["JWT:ValidAudience"];
                    options.TokenValidationParameters.ValidIssuer = _configuration["JWT:ValidIssuer"];
                    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.SameUser, policy => policy.Requirements.Add(new SameUserRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, SameUserAuthorizationHandler>();
            services.AddDbContext<Context>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<IPostsRepository, PostsRepository>();
            services.AddTransient<ICommentsRepository, CommentsRepository>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<DatabaseSeeder, DatabaseSeeder>();
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000",
                            "https://cheerful-froyo-3ebe83.netlify.app");
                        builder.WithHeaders("http://localhost:3000",
                            "https://cheerful-froyo-3ebe83.netlify.app");
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            // Shows UseCors with CorsPolicyBuilder
            app.UseCors(MyAllowSpecificOrigins);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
