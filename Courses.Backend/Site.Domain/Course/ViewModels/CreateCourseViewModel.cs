using Site.Domain.Course.Entity;
using Site.Domain.Enum;
using Site.Domain.User.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.Course.ViewModels
{
    public class CreateCourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }
        public List<string> Students { get; set; }
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                throw new ArgumentNullException(Title, "Укажите название курса");
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                throw new ArgumentNullException(Description, "Укажите описание");
            }

            if (string.IsNullOrWhiteSpace(Teacher))
            {
                throw new ArgumentNullException(Teacher, "Укажите автора курса");
            }
        }
    }
}
