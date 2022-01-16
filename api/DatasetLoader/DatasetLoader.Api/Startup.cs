using DatasetLoader.Api.Abstractions;
using DatasetLoader.Api.Implementation;
using DatasetLoader.Api.Implementation.Configurations;
using DatasetLoader.DataAccess.Abstractions;
using DatasetLoader.DataAccess.Mssql;
using DatasetLoader.UseCases;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DatasetLoader.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatasetSaverConfiguration>(_configuration.GetSection(DatasetSaverConfiguration.SectionName));
            services.AddDbContext<DatasetLoaderDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly(typeof(MssqlDataAccessMarker).Assembly.FullName)));
            services.AddScoped<IDatasetLoaderDbContext>(x => x.GetRequiredService<DatasetLoaderDbContext>());
            services.AddSingleton<IDatasetSaver, DatasetSaver>();
            services.AddSingleton<IDatasetValidator, DatasetValidator>();
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddAutoMapper(typeof(DatasetLoader.UseCases.DatasetModule.Utils.MapperProfile));
            services.AddMediatR(typeof(UseCasesMarker));
            services.AddControllers();
            services.AddHealthChecks();
            services.AddCors(o => o.AddPolicy("Policy", builder =>
            {
                builder.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatasetLoaderDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Policy");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
            
            dbContext.Database.Migrate();
        }
    }
}