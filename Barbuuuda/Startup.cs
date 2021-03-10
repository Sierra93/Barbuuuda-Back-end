using Barbuuuda.Core.Data;
using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Autofac;

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
                builder.WithOrigins(
                    "https://testdevi.site",
                    "https://testdevi.site/",
                    "https://barbuuuda.ru",
                    "https://barbuuuda.ru/",
                    "http://localhost:8080/",
                    "http://localhost:8080")
                .AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Barbuuuda").EnableRetryOnFailure()));

            services.AddEntityFrameworkNpgsql().AddDbContext<PostgreDbContext>(opt =>
        opt.UseNpgsql(Configuration.GetConnectionString("PostgreConnection"), b => b.MigrationsAssembly("Barbuuuda").EnableRetryOnFailure()));

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("PostgreConnection"), b => b.MigrationsAssembly("Barbuuuda").EnableRetryOnFailure()));

            services.AddIdentity<UserEntity, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 5;   // Минимальная длина
                opts.Password.RequireNonAlphanumeric = false;   // Требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = false; // Требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = false; // Требуются ли символы в верхнем регистре
                opts.Password.RequireDigit = false; // Требуются ли цифры
            })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            //services.AddDbContext<ApplicationDbContext>(options =>
            //  options.UseSqlServer(
            //      Configuration.GetConnectionString("TestConnection"), b => b.MigrationsAssembly("Barbuuuda").EnableRetryOnFailure()));

            services.AddSwaggerGen(options =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo {
                //    Version = "v1",
                //    Title = "ToDo API",
                //    Description = "A simple example ASP.NET Core Web API",
                //    TermsOfService = new Uri("https://example.com/terms"),
                //    Contact = new OpenApiContact {
                //        Name = "Shayne Boyer",
                //        Email = string.Empty,
                //        Url = new Uri("https://twitter.com/spboyer"),
                //    },
                //    License = new OpenApiLicense {
                //        Name = "Use under LICX",
                //        Url = new Uri("https://example.com/license"),
                //    }
                //});               

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Barbuuuda API");
            });
        }
    }
}
