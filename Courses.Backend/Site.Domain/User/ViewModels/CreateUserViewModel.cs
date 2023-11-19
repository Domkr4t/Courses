using Site.Domain.Course.Entity;
using Site.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.User.ViewModels
{
    public class CreateUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException(Name, "Укажите имя");
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                throw new ArgumentNullException(Email, "Укажите e-mail");
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                throw new ArgumentNullException(Password, "Укажите пароль");
            }

        }
    }
}
