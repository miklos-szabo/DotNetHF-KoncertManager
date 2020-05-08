using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KoncertManager.BLL;
using KoncertManager.BLL.Interfaces;
using KoncertManager.BLL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using KoncertManager.DAL;

namespace KoncertManager.API
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
            services.AddControllers();

            //A string az appsettings.Development.json f�jlban van
            services.AddDbContext<ConcertManagerContext>(o =>
                o.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            //A Startup oszt�lyunk szerelv�ny�ben keresi a profilt a mapperhez
            services.AddAutoMapper(typeof(Startup));

            //Megadjuk az elemeink kiszolg�l�inak az interface-�t, �s ezek az implement�l�s�t
            services.AddTransient<IBandService, BandService>();
            services.AddTransient<IVenueService, VenueService>();
            services.AddTransient<IConcertService, ConcertService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
