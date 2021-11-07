using System;
using Barbuuuda.Core.Data;
using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Barbuuuda.Core.Utils;
using Microsoft.OpenApi.Models;

namespace Barbuuuda
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public ContainerBuilder ContainerBuilder { get; }

        public IContainer ApplicationContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ContainerBuilder = new ContainerBuilder();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200", "https://barbuuuda.ru")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            }));

            #region ПРОД.
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //      options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Barbuuuda.Core").EnableRetryOnFailure()));

            //    services.AddEntityFrameworkNpgsql().AddDbContext<PostgreDbContext>(opt =>
            //opt.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnection"), b => b.MigrationsAssembly("Barbuuuda.Core").EnableRetryOnFailure()));

            //    services.AddDbContext<IdentityDbContext>(options =>
            //        options.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnection"), b => b.MigrationsAssembly("Barbuuuda.Core").EnableRetryOnFailure()));
            #endregion

            #region ТЕСТ.
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("TestMsSqlConnection"), b => b.MigrationsAssembly("Barbuuuda.Core").EnableRetryOnFailure()));

            services.AddEntityFrameworkNpgsql().AddDbContext<PostgreDbContext>(opt =>
            opt.UseNpgsql(Configuration.GetConnectionString("TestNpgSqlConnection"), b => b.MigrationsAssembly("Barbuuuda.Core").EnableRetryOnFailure()));

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("TestNpgSqlConnection"), b => b.MigrationsAssembly("Barbuuuda.Core").EnableRetryOnFailure()));
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Barbuuuda", Version = "v1" });
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

            ApplicationContainer = AutoFac.Init(cb =>
            {
                cb.Populate(services);
            });

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("ApiCorsPolicy");
            app.UseDeveloperExceptionPage();
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
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
