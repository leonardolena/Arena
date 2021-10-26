using Arena.Models;
using Arena.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILogger Logger { get; }
                
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddAuthentication();
            services.AddDbContext<AppDbContext>(options => {
                options.UseNpgsql(Configuration["ConnectionString"]);
            });
            services.AddSingleton<Arena>();         
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            app.Use(async (ctx, next) => {
                try {
                    await next();
                }catch (QueryException qe) {
                    Logger.LogInformation(qe, "Invalid Argument");
                    ctx.Response.StatusCode =404;
                }catch (SubmitException se) {
                    Logger.LogInformation(se, "Invalid Object");
                    ctx.Response.StatusCode = 422;
                }
                });    
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
