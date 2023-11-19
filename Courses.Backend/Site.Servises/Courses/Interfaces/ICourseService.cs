using Site.Domain.Course.Entity;
using Site.Domain.Course.Filters;
using Site.Domain.Course.Response;
using Site.Domain.Course.ViewModels;
using Site.Domain.User.Entity;
using Site.Domain.User.Filter;
using Site.Domain.User.Response;
using Site.Domain.User.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Servises.Courses.Interfaces
{
    public interface ICourseService
    {
        Task<ICourseResponse<CourseEntity>> Create(CreateCourseViewModel model);
        Task<ICourseResponse<CourseEntity>> Delete(int id);
        Task<ICourseResponse<CourseEntity>> Update(CourseViewModel model);
        Task<ICourseResponse<IEnumerable<CourseViewModel>>> GetAllCourses(CourseFilter filter);
    }
}
