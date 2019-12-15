using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenHealth.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GreenHealth.Controllers;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GreenHealth.Repositories;
using GreenHealth.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace GreenHealth
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IConfiguration Configuration { get; }
     

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(_config.GetConnectionString("GreenDbConnection")));

            //services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<ISpecializationRepository, SpecializationRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();


            services.AddControllersWithViews();
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddAuthentication()
                .AddFacebook(options =>
                {
                    options.AppId = "772499973213954";
                    options.AppSecret = "e925b1f292fa1693d1522f18ea741a6e";
                });
            
        }

        ////Role Creation Setup
        //private async Task CreateUserRoles(IServiceProvider serviceProvider)
        //{
        //    var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //    var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        //    IdentityResult roleResult;
        //    //Adding Admin Role
        //    var roleCheck = await RoleManager.RoleExistsAsync("Admin");
        //    if (!roleCheck)
        //    {
        //        //create the roles and seed them to the database
        //        roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
        //    }
        //    //Assign Admin role to the main User here we have given our newly registered 
        //    //login id for Admin management
        //    ApplicationUser user = await UserManager.FindByEmailAsync("tanvidhupkar0728@gmail.com");
        //    var User = new ApplicationUser();
        //    await UserManager.AddToRoleAsync(user, "Admin");
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context, 
            RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //SeedData.EnsurePopulated(app);
            AdminController.Initialize(context, userManager, roleManager).Wait();
        }
    }
}
