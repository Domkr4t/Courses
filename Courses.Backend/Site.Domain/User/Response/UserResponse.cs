using Site.Domain.Course.Entity;
using Site.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.User.Response
{
    public class UserResponse<T> : IUserResponse<T>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<CourseEntity> Courses { get; set; }
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }

    public interface IUserResponse<T>
    {
        string Name { get; }
        string Password { get; }
        string Email { get; }
        string Role { get; }
        List<CourseEntity> Courses { get; }
        string Description { get; }
        StatusCode StatusCode { get; }
        T Data { get; }
    }
}
