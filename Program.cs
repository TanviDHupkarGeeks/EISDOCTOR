using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GreenHealth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
                
            //    using (var scope = host.Services.CreateScope())
            //{
            //    var userManger = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            //    var user = new IdentityUser { UserName = "tanvidhupkar0728@gmail.com", Email = "tanvidhupkar0728@gmail.com" };
            //    userManger.CreateAsync(user, "Password").GetAwaiter().GetResult();
                
            //}
            //    host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
