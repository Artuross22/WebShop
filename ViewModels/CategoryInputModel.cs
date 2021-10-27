using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.ViewModels
{
    public class CategoryInputModel
    {
        [Required(ErrorMessage = "Поле {0} є обов'язковим. Будь-ласка, введіть це поле!")]
        [Display(Name = "Назва")]
        public string Name { get; set; }
    }
}