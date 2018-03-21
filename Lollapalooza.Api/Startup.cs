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
using Lollapalooza.Api.Middleware;
using Lollapalooza.Services.Sender;
using Take.Blip.Client;
using Take.Blip.Client.Extensions.Broadcast;

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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc()
                 .AddJsonOptions(options =>
                 {
                     foreach (var settingsConverter in JsonNetSerializer.Settings.Converters)
                     {
                         options.SerializerSettings.Converters.Add(settingsConverter);
                     }
                 });
            services.AddDbContext<LollapaloozaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LollapaloozaContext")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Lollapalooza API", Version = "v1", Description = "API to return shows of Lollapalooza Event" });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Lollapalooza.Api.xml");
                c.IncludeXmlComments(xmlPath);

            }
            );

            services.AddSingleton<ICarouselService, CarouselService>();

            //Based on Asp.net recommendation, the context injection needs to be scoped 
            //because caching problems (when you delete information on database, the change isn't reflected on entity)
            //All services that consuming context also needs to be scoped
            services.AddScoped<IShowService, ShowService>();
            services.AddScoped<IUserScheduleService, UserScheduleService>();
            services.AddScoped<IScheduleExtensionService, ScheduleExtensionService>();
            services.AddSingleton<ISender, CustomSender>();
            services.AddSingleton<IBroadcastExtension, BroadcastExtension>();
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

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
}
