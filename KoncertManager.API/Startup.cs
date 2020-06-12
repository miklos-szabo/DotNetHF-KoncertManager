using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hellang.Middleware.ProblemDetails;
using KoncertManager.BLL;
using KoncertManager.BLL.DTOs;
using KoncertManager.BLL.Interfaces;
using KoncertManager.BLL.Services;
using KoncertManager.BLL.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using KoncertManager.DAL;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Http;

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
            services.AddControllers(mvcOptions => mvcOptions.EnableEndpointRouting = false);

            //A string az appsettings.Development.json fájlban van
            services.AddDbContextPool<ConcertManagerContext>(o =>
                //o.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));    //LocalDB
                o.UseMySql(Configuration["ConnectionStrings:MySqlConnString"]));            //MySQL

            //A Startup osztályunk szerelvényében keresi a profilt a mapperhez
            services.AddAutoMapper(typeof(Startup));

            //Megadjuk az elemeink kiszolgálóinak az interface-ét, és ezek az implementálását
            services.AddTransient<IBandService, BandService>();
            services.AddTransient<IVenueService, VenueService>();
            services.AddTransient<IConcertService, ConcertService>();

            //Hiba kezelése
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (context, exception) => false;
                options.Map<EntityNotFoundException>((context, exception) =>
                {
                    var pd = StatusCodeProblemDetails.Create(StatusCodes.Status404NotFound);
                    pd.Title = exception.Message;
                    return pd;
                });
            });

            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();

            app.UseRouting();

            app.UseAuthorization();

            var builder = new ODataConventionModelBuilder(app.ApplicationServices);
            builder.EntitySet<Band>("Bands");
            builder.EntitySet<Venue>("Venues");
            builder.EntitySet<Concert>("Concerts");
            app.UseOData("ODataRoute", null, builder.GetEdmModel());


            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });*/
        }


    }
}
