using Phd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phd.ViewModels
{
    public class StudentGradeViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public double AverageGrade { get; set; }
        public List<User> Users { get; set; }
        public List<BRStudentGrade> Grades { get; set; }

    }
}
