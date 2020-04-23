using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phd.Models
{
    public class BMajor
    {
        public int Id { get; set; }

        [Display(Name = "Шифр специальности")]
        public string Cypher { get; set; }

        [Display(Name = "Название на русс.яз.")]
        public string NameRus { get; set; }

        [Display(Name = "Название на каз.яз.")]
        public string NameKaz { get; set; }

        [Display(Name = "Название на англ.яз.")]
        public string NameEng { get; set; }

        [Display(Name = "Кафедра")]
        public int AcademicDepartmentId { get; set; }
        [Display(Name = "Кафедра")]
        public AcademicDepartment AcademicDepartment { get; set; }








       


    }
}
