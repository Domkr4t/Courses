using Site.Domain.User.Entity;
using Site.Domain.User.Filter;
using Site.Domain.User.Response;
using Site.Domain.User.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Site.Services.Users.Interfaces
{
    public interface IUserService
    {
        Task<IUserResponse<UserEntity>> Create(CreateUserViewModel model);
        Task<IUserResponse<UserEntity>> Delete(int id);
        Task<IUserResponse<UserEntity>> Update(UserViewModel model);
        Task<IUserResponse<IEnumerable<UserViewModel>>> GetAllUsers(UserFilter filter);
    }
}
