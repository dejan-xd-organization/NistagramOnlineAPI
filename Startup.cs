using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NistagramOnlineAPI.Service;
using NistagramOnlineAPI.Service.Implementation;
using NistagramSQLConnection.Data;
using NistagramSQLConnection.Service;
using NistagramSQLConnection.Service.Interface;
using NistagramUtils.Mapper;

namespace NistagramOnlineAPI
{
    public class Startup
    {

        readonly private string _myAllow = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IOnlineService, OnlineServiceImpl>();
            services.AddScoped<IUserService, UserServiceImpl>();
            services.AddScoped<IPostService, PostServiceImpl>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SimpleMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<DataContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 11)));
            });

            services.AddCors(option =>
            {
                option.AddPolicy(name: _myAllow, builder =>
                {
                    builder.WithOrigins("http://localhost:4200/", "http://localhost:57793")
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NistagramOnlineAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NistagramOnlineAPI v1"));
            }

            app.UseRouting();

            app.UseCors(_myAllow);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
