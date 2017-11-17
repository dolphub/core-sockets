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
using Swashbuckle.AspNetCore.Swagger;
using Users.Models;
using Microsoft.EntityFrameworkCore;
using Users.Middlewares;
using Libs.Ipc;
using Users.Configuration;

namespace Users
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            appConfig = new AppConfiguration(builder.Build());
            
        }

        public IAppConfiguration appConfig { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(options =>
                options.UseNpgsql(appConfig.getDatabaseConfig()));
            services.AddMvc();
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new Info { Title = "SocketChat Api", Version = "v1"});
            });
            services.AddSingleton<IAppConfiguration>(appConfig);
            var amqpConfig = appConfig.getAmqpConfiguration();
            Console.WriteLine(">>>> " + amqpConfig);
            IEventBus eventBus = new EventBus(
                amqpConfig.host,
                amqpConfig.user,
                amqpConfig.password,
                amqpConfig.exchange
            );
            services.AddSingleton<IEventBus, EventBus>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseMiddleware<ResponseTimeMiddleware>();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
