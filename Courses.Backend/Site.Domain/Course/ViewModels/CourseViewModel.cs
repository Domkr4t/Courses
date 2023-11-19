using Site.Domain.User.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.Course.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Преподаватель")]
        public string Teacher { get; set; }

        [Display(Name = "Дата создания")]
        public string Created { get; set; }

        [Display(Name = "Студенты")]
        public string Students { get; set; }
    }
}
