using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Shop.API.Models;
using Microsoft.AspNetCore.Identity;
using Shop.API.Services.IdentityPolicy;

namespace Shop.API
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
            services.AddTransient<IPasswordValidator<AppUser>, CustomPasswordPolicy>();
            services.AddTransient<IUserValidator<AppUser>, CustomerUserEmialPolicy>();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<AppUser, IdentityRole>( opts =>
                {
                    opts.Password.RequiredLength = 8;
                    opts.Password.RequireDigit = true;
                    opts.Password.RequireNonAlphanumeric = true;
                    opts.Password.RequireUppercase = true;
                    opts.Password.RequireLowercase = true;
                    opts.User.RequireUniqueEmail = true;
                    opts.User.AllowedUserNameCharacters = "aAbBcCdDeEfFgGhHiIj"
                    +"JkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ1234567890";                    
                }
                ).AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Authenticate/Login");
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddTransient<IProductRepository, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });
        }
    }
}
