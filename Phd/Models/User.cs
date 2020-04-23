using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phd.Models
{
    public class User : IdentityUser
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string UName { get; set; }



        public int BMajorId { get; set; } // модернизации логики, чтобы была связь со специальностями
        public BMajor BMajor { get; set; }

        public ICollection<BRStudentGrade> BRStudentGrades {get; set;}

    }
}