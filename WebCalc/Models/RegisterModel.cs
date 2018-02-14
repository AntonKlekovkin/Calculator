using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebCalc.Models
{
    public class RegisterModel
    {
        [Display(Name ="Login")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="Введите логин")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Без пароля - нельзя!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Имя")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [Display(Name = "Дата рождения")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите дату рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [Display(Name = "Пол")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите пол")]
        public bool Sex { get; set; }

        public IEnumerable<SelectListItem> Sexes { get; set; }
    }
}
