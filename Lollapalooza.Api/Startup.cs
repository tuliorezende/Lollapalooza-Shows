using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Lollapalooza.Services.Model;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Lollapalooza.Services.Service;
using Lollapalooza.Services.Interface;
using Lime.Protocol.Serialization.Newtonsoft;

namespace Lollapalooza.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                 .AddJsonOptions(options =>
                 {
                     foreach (var settingsConverter in JsonNetSerializer.Settings.Converters)
                     {
                         options.SerializerSettings.Converters.Add(settingsConverter);
                     }
                 }); ;
            services.AddDbContext<LollapaloozaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LollapaloozaContext")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Lollapalooza API", Version = "v1", Description = "API to return shows of Lollapalooza Event" });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Lollapalooza.Api.xml");
                c.IncludeXmlComments(xmlPath);

            }
            );

            services.AddSingleton<IShowService, ShowService>();
            services.AddSingleton<ICarouselService, CarouselService>();
            services.AddSingleton<LollapaloozaContext>();

        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lollapalooza API V1");
            });
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}
