using MyF.Entities.BaseModels;
using MyF.Entities.DtoModesl;

namespace MyF.Services
{
    public interface IRoleService : IService<Role>
    {
        Task<Role> CreateRoleAsync(Role role);
        Task<List<Role>> GetAllRolesAsync();
    }
}