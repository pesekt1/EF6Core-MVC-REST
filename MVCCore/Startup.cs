using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MVCCore.DbContext;
using MVCCore.MongoDB.Services;
using MVCCore.Services;

namespace MVCCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllersWithViews();

            //SQL Server CRUD app services:
            services.AddScoped<SchoolContext>(_ => 
                new SchoolContext(Environment.GetEnvironmentVariable("DB_URL")));
            
            services.AddScoped<CoursesService>();
            services.AddScoped<StudentsService>();
            services.AddScoped<RolesService>();
            services.AddScoped<UsersService>();
            services.AddScoped<AuthService>();

            //MongoDB CRUD app services:
            services.AddScoped<IMongoClient>(s => 
                new MongoClient(Environment.GetEnvironmentVariable("MONGO_URL"))
            );
            services.AddScoped<BookService>();

            // services.AddControllers()
            //     .AddNewtonsoftJson(options => options.UseMemberCasing()); // for MongoDB but it deactivates JsonIgnore
            
            services.AddHttpClient(); //for http requests
            
            services.AddSwaggerGen(); // Register the Swagger generator, defining 1 or more Swagger documents

            //using JWT - Json Web Token
            services.AddAuthentication(configureOptions: x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"))),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
            app.UseSwagger(); // Enable middleware to serve generated Swagger as a JSON endpoint.

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
            });
        }
    }
}
