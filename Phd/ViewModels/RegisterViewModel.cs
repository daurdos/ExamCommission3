using System.ComponentModel.DataAnnotations;

namespace Phd.ViewModels
{
    public class RegisterViewModel
    {
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Неверно указана эл. почта")]
        [EmailAddress]
        [Display(Name = "Эл. почта")]


        public string Email { get; set; }



        [Required(ErrorMessage = "Неверно указано имя")]
        [Display(Name = "Фамилия")]


        public string LastName { get; set; }

        [Required(ErrorMessage = "Неверно указана фамилия")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]


        public string MiddleName { get; set; }



        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]

        public string Password { get; set; }



        [Required(ErrorMessage = "Повторно введите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]


        public string PasswordConfirm { get; set; }

        [Display(Name = "Кафедра")]
        public int BMajorId { get; set; }



        public string UName { get; set; }





    }
}