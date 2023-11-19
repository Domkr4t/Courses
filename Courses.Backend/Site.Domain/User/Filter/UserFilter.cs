using Site.Domain.Course.Entity;
using Site.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.User.Filter
{
    public class UserFilter
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Course { get; set; }
    }
}
