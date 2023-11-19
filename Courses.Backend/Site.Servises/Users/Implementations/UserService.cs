using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Site.Backend.DAL.Interfaces;
using Site.Domain.Course.Entity;
using Site.Domain.Enum;
using Site.Domain.User.Entity;
using Site.Domain.User.Filter;
using Site.Domain.User.Response;
using Site.Domain.User.ViewModels;
using Site.Services.Users.Interfaces;
using Site.Domain.Extentsions;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Site.Services.Users.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<UserEntity> _userRepository;
        private readonly IBaseRepository<CourseEntity> _courseRepository;
        private ILogger<UserService> _logger;

        public UserService(IBaseRepository<UserEntity> userRepository,
            IBaseRepository<CourseEntity> courseRepository, ILogger<UserService> logger) =>
                (_userRepository, _courseRepository, _logger) = (userRepository, courseRepository, logger);

        public async Task<IUserResponse<UserEntity>> Create(CreateUserViewModel model)
        {
            try
            {
                model.Validate();

                _logger.LogInformation($"Запрос на создание игрока - {model.Name}");

                var user = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Name == model.Name);

                if (user  != null)
                {
                    return new UserResponse<UserEntity>()
                    {
                        Description = "Такой игрок уже есть",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }

                string role;

                if (model.Role.Equals("Администратор"))
                {
                    role = RoleCode.Admin;
                }
                else if (model.Role.Equals("Учитель")) 
                {
                    role = RoleCode.Teacher;
                }
                else
                {
                    role = RoleCode.Student;
                }

                user = new UserEntity()
                {
                    Name = model.Name,
                    Password = model.Password,
                    Email = model.Email,
                    Role = role,
                    Courses = new List<CourseEntity>(),
                };


                await _userRepository.Create(user);

                _logger.LogInformation($"Пользователь создался: {user.Name} {DateTime.Now}");
                return new UserResponse<UserEntity>()
                {
                    Description = "Пользователь создан",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[UserService.Create]: {exception.Message}");
                return new UserResponse<UserEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IUserResponse<UserEntity>> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Запрос на удаление пользователя - {id}");

                var user = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    return new UserResponse<UserEntity>()
                    {
                        Description = "Такого пользователя нет",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                await _userRepository.Delete(user);

                _logger.LogInformation($"Пользователь удалился: {user.Name} {DateTime.Now}");
                return new UserResponse<UserEntity>()
                {
                    Description = "Пользователь удален",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[UserService.Delete]: {exception.Message}");
                return new UserResponse<UserEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IUserResponse<IEnumerable<UserViewModel>>> GetAllUsers(UserFilter filter)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(filter.Name),
                        x => x.Name.Contains(filter.Name))
                    .WhereIf(!string.IsNullOrWhiteSpace(filter.Role),
                        x => x.Role.Contains(filter.Role))
                    .WhereIf(!string.IsNullOrWhiteSpace(filter.Course),
                        x => string.Join(", ", x.Courses.Select(t => t.Title)).Contains(filter.Course))
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Password = x.Password,
                        Email = x.Email,
                        Role = x.Role,
                        Courses = string.Join(", ", x.Courses.Select(t => t.Title)),
                    })
                    .ToListAsync();

                return new UserResponse<IEnumerable<UserViewModel>>()
                {
                    Data = user,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[UserService.GetAllPlayers]: {exception.Message}");
                return new UserResponse<IEnumerable<UserViewModel>>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IUserResponse<UserEntity>> Update(UserViewModel model)
        {
            try
            {
                _logger.LogInformation($"Запрос на изменение пользователя - {model.Name}");

                var user = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == model.Id);

                if (user == null)
                {
                    return new UserResponse<UserEntity>()
                    {
                        Description = "Такой пользователь не найдена",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var courses = new List<CourseEntity>();
                foreach (var course in model.Courses.Split(", "))
                {
                    var tmpCourse = await _courseRepository.GetAll().FirstOrDefaultAsync(x => x.Title == course);
                    if (tmpCourse != null)
                    {
                        courses.Add(tmpCourse);
                    }
                    else
                    {
                        return new UserResponse<UserEntity>()
                        {
                            Description = "Одного из указанных курсов не существует",
                            StatusCode = StatusCode.UserNotFound
                        };
                    }
                }

                user = new UserEntity()
                {
                    Name = model.Name,
                    Password = model.Password,
                    Email = model.Email,
                    Role = model.Role,
                    Courses = courses,
                };

                await _userRepository.Update(user);

                _logger.LogInformation($"Пользователь {user.Name} изменена");
                return new UserResponse<UserEntity>()
                {
                    Description = $"Пользователь {user.Name} изменена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[TeamService.Update]: {exception.Message}");
                return new UserResponse<UserEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
