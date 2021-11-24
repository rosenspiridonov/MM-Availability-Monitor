namespace AvailabilityMonitor
{
    using AvailabilityMonitor.Data;
    using AvailabilityMonitor.Services.Comparer;
    using AvailabilityMonitor.Services.Csv;
    using AvailabilityMonitor.Services.Excel;
    using AvailabilityMonitor.Services.Xml;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.Configure<FormOptions>(options =>
            {
                // Set the limit to 256 MB
                options.MultipartBodyLengthLimit = 268435456;
            });

            services.AddTransient<ICsvProcesor, CsvProcesor>();
            services.AddTransient<IXmlProcesor, XmlProcesor>();
            services.AddTransient<ICompareService, CompareService>();
            services.AddTransient<IExcelService, ExcelService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
            });
        }
    }
}
