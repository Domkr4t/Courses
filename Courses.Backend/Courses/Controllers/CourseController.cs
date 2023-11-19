using Microsoft.AspNetCore.Mvc;
using Site.Domain.Course.Filters;
using Site.Domain.Course.ViewModels;
using Site.Domain.User.Filter;
using Site.Domain.User.ViewModels;
using Site.Services.Users.Interfaces;
using Site.Servises.Courses.Interfaces;

namespace Courses.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService) =>
            (_courseService) = (courseService);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseViewModel model)
        {
            var response = await _courseService.Create(model);

            if (response.StatusCode == Site.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpGet]
        public async Task<IActionResult> CourseHandler(CourseFilter filter)
        {
            var response = await _courseService.GetAllCourses(filter);

            return Json(new { data = response.Data });
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]int id)
        {
            var response = await _courseService.Delete(id);

            if (response.StatusCode == Site.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(CourseViewModel model)
        {
            var response = await _courseService.Update(model);

            if (response.StatusCode == Site.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok();
        }
    }
}
