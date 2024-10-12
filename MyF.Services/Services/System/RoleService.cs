using MyF.Entities.BaseModels;
using MyF.Infrastructure.Data;
using MyF.Infrastructure.Mapping;

namespace MyF.Services
{
    public class RoleService :  Service<Role>, IRoleService
    {
        private readonly IMapper _mapper;

        public RoleService(SqlSugarDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            await _db.Insertable(role).ExecuteCommandAsync();
            return role;
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _db.Queryable<Role>().ToListAsync();
        }
    }
}