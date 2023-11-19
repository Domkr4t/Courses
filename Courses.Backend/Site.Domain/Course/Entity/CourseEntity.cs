using Site.Domain.User.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.Course.Entity
{
    public class CourseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }
        public DateTime Created { get; set; }
        public List<UserEntity> Students { get; set; }
    }
}
