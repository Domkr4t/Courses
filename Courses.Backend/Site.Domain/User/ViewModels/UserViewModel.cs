using Site.Domain.Course.Entity;
using Site.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.User.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }
        
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Роль")]
        public string Role { get; set; }

        [Display(Name = "Курсы")]
        public string Courses { get; set; }
    }
}
