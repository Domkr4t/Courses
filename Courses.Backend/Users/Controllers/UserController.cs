using Microsoft.AspNetCore.Mvc;
using Site.Domain.User.Filter;
using Site.Domain.User.ViewModels;
using Site.Services.Users.Interfaces;

namespace Users.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) =>
            (_userService) = (userService);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserViewModel model)
        {
            var response = await _userService.Create(model);

            if (response.StatusCode == Site.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpGet]
        public async Task<IActionResult> UserHandler(UserFilter filter)
        {
            var response = await _userService.GetAllUsers(filter);

            return Json(new { data = response.Data });
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]int id)
        {
            var response = await _userService.Delete(id);

            if (response.StatusCode == Site.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserViewModel model)
        {
            var response = await _userService.Update(model);

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
