using Site.Domain.Course.Entity;
using Site.Domain.Enum;
using Site.Domain.User.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.Course.Response
{
    public class CourseResponse<T> : ICourseResponse<T>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public UserEntity Teacher { get; set; }
        public DateTime Created { get; set; }
        public List<UserEntity> Students { get; set; }
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }

    public interface ICourseResponse<T>
    {
        string Title { get; }
        string Description { get; }
        UserEntity Teacher { get; }
        DateTime Created { get; }
        List<UserEntity> Students { get; }
        StatusCode StatusCode { get; }
        T Data { get; }
    }
}
