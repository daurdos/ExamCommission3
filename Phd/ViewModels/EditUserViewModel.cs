using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phd.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Неверно указана фамилия")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Неверно указано имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        public string UName { get; set; }

    }
}
