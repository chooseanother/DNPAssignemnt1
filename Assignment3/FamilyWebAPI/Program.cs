using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using FamilyWebAPI.Data;
using FamilyWebAPI.Models;
using FamilyWebAPI.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FamilyWebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using (UserContext userContext = new UserContext())
            {
                if (!userContext.Users.Any())
                {
                    SeedUsers(userContext);
                }
            }
            using (FamilyContext familyContext = new FamilyContext())
            {
                if (!familyContext.Families.Any())
                {
                    await SeedFamilies(familyContext);
                }
                
                // some families had conflicts so i tried to add them one at a time

                // IFamilyDataService familyDataService = new FamilyDataPersistence();
                // var families = await familyDataService.GetFamiliesAsync();
                // Console.WriteLine($"Fam count ============> {families.Count}");
                // var fam = families[9]; //failed fam 1, 2,
                // await familyContext.Families.AddAsync(fam);
                // await familyContext.SaveChangesAsync();
            }
            
            CreateHostBuilder(args).Build().Run();
        }

        private static async Task SeedFamilies(FamilyContext familyContext)
        {
            IFamilyDataService familyDataService = new FamilyDataPersistence();
            var families = await familyDataService.GetFamiliesAsync();
            
            foreach (var f in families)
            {
                // Console.WriteLine(f);
                await familyContext.Families.AddAsync(f);
            }
            
            await familyContext.SaveChangesAsync();
        }

        private static void SeedUsers(UserContext userContext)
        {
            var user = new User
            {
                Password = "123456",
                UserName = "Admin"
            };
            userContext.Add(user);
            user = new User
            {
                Password = "123456",
                UserName = "Troels"
            };
            userContext.Add(user);
            user = new User
            {
                Password = "123456",
                UserName = "Kim"
            };
            userContext.Add(user);
            userContext.SaveChanges();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}