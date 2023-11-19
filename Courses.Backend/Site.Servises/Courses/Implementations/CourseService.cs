using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Site.Backend.DAL.Interfaces;
using Site.Backend.DAL.Repositories;
using Site.Domain.Course.Entity;
using Site.Domain.Course.Filters;
using Site.Domain.Course.Response;
using Site.Domain.Course.ViewModels;
using Site.Domain.Enum;
using Site.Domain.User.Entity;
using Site.Domain.User.Filter;
using Site.Domain.User.Response;
using Site.Domain.User.ViewModels;
using Site.Services.Users.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Domain.Extentsions;
using System.ComponentModel.DataAnnotations;
using Site.Servises.Courses.Interfaces;

namespace Site.Servises.Courses.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly IBaseRepository<UserEntity> _userRepository;
        private readonly IBaseRepository<CourseEntity> _courseRepository;
        private ILogger<UserService> _logger;

        public CourseService(IBaseRepository<UserEntity> userRepository,
            IBaseRepository<CourseEntity> courseRepository, ILogger<UserService> logger) =>
                (_userRepository, _courseRepository, _logger) = (userRepository, courseRepository, logger);

        public async Task<ICourseResponse<CourseEntity>> Create(CreateCourseViewModel model)
        {
            try
            {
                model.Validate();

                _logger.LogInformation($"Запрос на создание курса - {model.Title}");

                var course = await _courseRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Title == model.Title);

                if (course != null)
                {
                    return new CourseResponse<CourseEntity>()
                    {
                        Description = "Такой курс уже есть",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }

                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Teacher);

                if (user == null)
                {
                    return new CourseResponse<CourseEntity>()
                    {
                        Description = "Такого преподавателя нет",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }

                var existingUsers = _userRepository.GetAll()
                                            .Where(x => model.Students.Contains(x.Name))
                                            .ToList();

                foreach (var team in existingUsers)
                {
                    await _userRepository.Attach(team);
                }

                course = new CourseEntity()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Teacher = user.Name,
                    Created = DateTime.Today,
                    Students = existingUsers,
                };


                await _courseRepository.Create(course);

                _logger.LogInformation($"Курс создан: {course.Title} {DateTime.Now}");
                return new CourseResponse<CourseEntity>()
                {
                    Description = "Курс создан",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[CourseService.Create]: {exception.Message}");
                return new CourseResponse<CourseEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<ICourseResponse<CourseEntity>> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Запрос на удаление курса - {id}");

                var course = await _courseRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (course == null)
                {
                    return new CourseResponse<CourseEntity>()
                    {
                        Description = "Такого курса нет",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                await _courseRepository.Delete(course);

                _logger.LogInformation($"Курс удалился: {course.Title} {DateTime.Now}");
                return new CourseResponse<CourseEntity>()
                {
                    Description = "Курс удален",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[CourseService.Delete]: {exception.Message}");
                return new CourseResponse<CourseEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<ICourseResponse<IEnumerable<CourseViewModel>>> GetAllCourses(CourseFilter filter)
        {
            try
            {
                var course = await _courseRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(filter.Title),
                        x => x.Title.Contains(filter.Title))
                    .WhereIf(!string.IsNullOrWhiteSpace(filter.Teacher),
                        x => x.Teacher.Contains(filter.Teacher))
                    .Select(x => new CourseViewModel()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        Teacher = x.Teacher,
                        Created = DateTime.Now.ToString("dd/MM/yy"),
                        Students = string.Join(", ", x.Students.Select(t => t.Name))
                    })
                    .ToListAsync();

                return new CourseResponse<IEnumerable<CourseViewModel>>()
                {
                    Data = course,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[CourseService.GetAllPlayers]: {exception.Message}");
                return new CourseResponse<IEnumerable<CourseViewModel>>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<ICourseResponse<CourseEntity>> Update(CourseViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
