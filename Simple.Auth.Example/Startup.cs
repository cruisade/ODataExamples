using AutoMapper;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.OData.Edm;
using Simple.Auth.Example.Db;

namespace Simple.Auth.Example
{
    public class Startup
    {
        public static readonly LoggerFactory LoggerFactory =
            new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOData();

            services.AddDbContext<UserContext>(opt => { opt.UseLoggerFactory(LoggerFactory); });
            services.AddAutoMapper(opt => opt.AddProfile(new MapperProfile()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc(opt =>
            {
                opt.Count().Filter().OrderBy().Expand().Select();
                opt.EnableDependencyInjection();
                opt.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<User>("Users");
            builder.EntitySet<UserApiModel>("UsersDto");
            return builder.GetEdmModel();
        }
    }
}