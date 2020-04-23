using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phd.Models
{
    public class BRStudentGroup
    {
        public int Id { get; set; }

        [Display(Name = "Поток по защите ГАК")]
        public string Name { get; set; }
        public ICollection<BRStudent> BRStudent { get; set; }

        [Display(Name = "Комиссия")]
        public int BMajorId { get; set; }
        [Display(Name = "Комиссия")]
        public BMajor BMajor { get; set; }

    }
}
