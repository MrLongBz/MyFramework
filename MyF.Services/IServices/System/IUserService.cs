using MyF.Entities.BaseModels;
using MyF.Entities.DtoModesl;

namespace MyF.Services
{
    public interface IUserService : IService<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<(User user,List<Role> roles, List<Permission> permissions)> LoginAsync(string account, string password);
        Task<User> CreateUserAsync(UserRegistrationModel userDto, string password);
    }
}