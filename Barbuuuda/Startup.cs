using Barbuuuda.Core.Data;
using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Autofac;
using Barbuuuda.Core.Extensions;

namespace Barbuuuda
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public ContainerBuilder containerBuilder { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            containerBuilder = new ContainerBuilder();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                //builder.WithOrigins(
                //    "https://barbuuuda.ru/",
                //    "https://barbuuuda.ru",
                //    "https://testdevi.site",
                //    "https://testdevi.site/",
                //    "http://localhost:8080/",
                //    "http://localhost:8080");
                //.AllowAnyMethod().AllowAnyHeader();
                builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins("*")
                        .WithMethods("*")
                        .WithHeaders("*")
                        .DisallowCredentials();
            }));

            #region ПРОД.
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //      options.UseSqlServer(
            //          Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Barbuuuda").EnableRetryOnFailure()));

            //    services.AddEntityFrameworkNpgsql().AddDbContext<PostgreDbContext>(opt =>
            //opt.UseNpgsql(Configuration.GetConnectionString("PostgreConnection"), b => b.MigrationsAssembly("Barbuuuda").EnableRetryOnFailure()));

            //    services.AddDbContext<IdentityDbContext>(options =>
            //        options.UseNpgsql(Configuration.GetConnectionString("PostgreConnection"), b => b.MigrationsAssembly("Barbuuuda").EnableRetryOnFailure()));
            #endregion

            #region ТЕСТ.
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(
                 Configuration.GetConnectionString("TestMsSqlConnection"), b => b.MigrationsAssembly("Barbuuuda").EnableRetryOnFailure()));

            services.AddEntityFrameworkNpgsql().AddDbContext<PostgreDbContext>(opt =>
        opt.UseNpgsql(Configuration.GetConnectionString("TestNpgSqlConnection"), b => b.MigrationsAssembly("Barbuuuda").EnableRetryOnFailure()));

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("TestNpgSqlConnection"), b => b.MigrationsAssembly("Barbuuuda").EnableRetryOnFailure()));
            #endregion

            services.AddIdentity<UserEntity, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 5;   
                opts.Password.RequireNonAlphanumeric = false;   
                opts.Password.RequireLowercase = false; 
                opts.Password.RequireUppercase = false; 
                opts.Password.RequireDigit = false; 
            })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddSwaggerGen(options =>
            {             
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(Directory.GetCurrentDirectory(), xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,                          
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            app.UseCors("ApiCorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Запишет путь для xml-файла документации API.
            applicationLifetime.ApplicationStarted.Register(DocumentationFileExtension.OnApplicationStarted);

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Barbuuuda API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapDefaultControllerRoute();
            });
        }        
    }
}
