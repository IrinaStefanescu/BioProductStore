using System.Collections.Generic;
using BioProductStore.Data;
using BioProductStore.DataAccess;
using BioProductStore.Models;
using BioProductStore.Repositories;
using BioProductStore.Services;
using BioProductStore.Services.AuthenticationService;
using BioProductStore.Services.CategoryService;
using BioProductStore.Services.ExpeditionAddressService;
using BioProductStore.Services.OrderService;
using BioProductStore.Services.ProductService;
using BioProductStore.Utilities;
using BioProductStore.Utilities.JWTUtils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BioProductStore
{
    public class Startup
    {
        public string CorsAllowSpecificOrigin = "frontendAllowOrigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BioProductsStore", Version = "v1" });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
    
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "User",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.AddDbContext<BioProductStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Repositories:
            //it's created everytime a request it's been made
            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
            services.AddTransient<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddTransient<IGenericRepository<Product>, GenericRepository<Product>>();
            services.AddTransient<IGenericRepository<Order>, GenericRepository<Order>>();
            services.AddTransient<IGenericRepository<ExpeditionAddress>, GenericRepository<ExpeditionAddress>>();

            services.AddTransient<UnitOfWork>();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
           
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IJWTUtils, JWTUtils>();

            //Services:
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IExpeditionAddressService, ExpeditionAddressService>();

            //services.AddCors(c =>
            // {
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
            // .AllowAnyHeader());
            //});
            services.AddCors(option =>
            {
                option.AddPolicy(name: CorsAllowSpecificOrigin,
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:4200", "https://localhost:4201", "http://localhost:4200") //aici punem ce origine avem noi
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BioProductsStore v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader());
            app.UseCors(CorsAllowSpecificOrigin);

            //others
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseJwtAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
