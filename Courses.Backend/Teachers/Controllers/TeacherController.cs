using Microsoft.AspNetCore.Mvc;
using Site.Teacher.Domain.Filter;
using Site.Teacher.Domain.ViewModels;
using Site.Teacher.Service.Interfaces;

namespace Teachers.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService) =>
            (_teacherService) = (teacherService);

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeacherViewModel model)
        {
            var response = await _teacherService.Create(model);

            if (response.StatusCode == Site.Domain.Enum.StatusCode.Ok)
            {
            return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpGet]
        public async Task<IActionResult> TeacherHandler(TeacherFilter filter)
        {
            var response = await _teacherService.GetAllTeachers(filter);

            return Json(new { data = response.Data });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TeacherViewModel model)
        {
            var response = await _teacherService.Delete(model);

            if (response.StatusCode == Site.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateComputer(TeacherViewModel model)
        {
            var response = await _teacherService.Update(model);

            if (response.StatusCode == Site.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }
    }
}
