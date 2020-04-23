using System.ComponentModel.DataAnnotations;

namespace Phd.ViewModels
{
    public class LoginViewModel
    {

        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}
