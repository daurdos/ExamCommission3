using Microsoft.AspNetCore.Identity;
using Phd.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Phd.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, PhdContext context)
        {

            // закомментировать после первого запуска

            //string adminEmail = "daurdos@gmail.com";
            //string password = "!QAZ1qaz";
            //if (await roleManager.FindByNameAsync("admin") == null)
            //{
            //    await roleManager.CreateAsync(new IdentityRole("admin"));
            //}


            // закомментировать после первого запуска

            //if (await roleManager.FindByNameAsync("user") == null)
            //{
            //    await roleManager.CreateAsync(new IdentityRole("user"));
            //}



            // добавляю инициализацию кафедр
            if (!context.Faculty.Any())
            {
                context.Faculty.Add(new Faculty { Name = "Initial Faculty" });
                context.SaveChanges();
            }

            // добавляю инициализацию кафедр
            if (!context.AcademicDepartment.Any())
            {
                context.AcademicDepartment.Add(new AcademicDepartment { Name = "Initial AcademicDepartment", FacultyId = 1 });
                context.SaveChanges();
            }

            //// добавляю инициализацию специальностей бакалавра
            if (!context.BMajor.Any())
            {
                context.BMajor.Add(new BMajor { Cypher = "BMajor Initial Cypher", NameRus = "BMajor Initial NameRus", NameKaz = "BMajor Initial NameKaz", NameEng = "BMajor Initial NameEng", AcademicDepartmentId = 1 });
                context.SaveChanges();
            }



            // закомментировать после первого запуска

            //if (await userManager.FindByNameAsync(adminEmail) == null)
            //{
            //    User admin = new User { Email = adminEmail, UserName = "SuperAdmin", BMajorId = 1 };
            //    IdentityResult result = await userManager.CreateAsync(admin, password);
            //    if (result.Succeeded)
            //    {
            //        await userManager.AddToRoleAsync(admin, "admin");
            //    }
            //}



        }



    }
}
