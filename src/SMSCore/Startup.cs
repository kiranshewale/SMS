using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SMSCore.Data.Context;
using SMSCore.Infrastructure;
using SMSCore.Services.VehiclesMasterService;
using System;

namespace SMSCore
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
            services.AddDbContext<SmsDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("SmsCore")));
            //cookies
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            //hangfire
            services.AddHangfire(conf =>
               conf.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseDefaultTypeSerializer()
               .UseMemoryStorage());

            services.AddHangfireServer();



            // Add repository services
            services.AddRepositoryServices();

            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddSingleton(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,            
            IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseHangfireDashboard(options: new DashboardOptions()
            {
                Authorization = new IDashboardAuthorizationFilter[]
                {
                new MyAuthorizationFilter()
                }
            });

            app.UseHangfireServer();
            ////backgroundJobClient.Schedule(()=>serviceProvider.GetService<IVehiclesMasterService>().CalculateAgingDaysTaskAsync(),TimeSpan.FromMinutes(5));

            recurringJobManager.AddOrUpdate("Run every day", () => serviceProvider.GetService<IVehiclesMasterService>().CalculateAgingDaysTaskAsync(), Cron.Daily);
            //route configurations
            app.ConfigureRoutes();
        }
    }
}
