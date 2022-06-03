using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Music.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // here we configure dependencies
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicApi", Version = "v1" });
            });

            services.AddDbContext<ApiDbContext>(options => options.UseSqlServer("Server=tcp:rnemeth-lab-sqlsvr-01.database.windows.net,1433;Initial Catalog=azrtnmusicapisqldb;Persist Security Info=False;User ID=azadmin;Password=Pa$$w0rd@01;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

            // add the database context
            //services.AddDbContext<ApiDbContext>(options =>
            //        options.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = musicapi; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"));
        
            // add support for returning XML content type
            services.AddMvc().AddXmlDataContractSerializerFormatters(); 


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // here we configure middleware
        // run, use, next

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();

            app.UseAuthorization();

            app.UseHttpLogging();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
