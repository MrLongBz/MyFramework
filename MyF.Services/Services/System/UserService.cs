using MyF.Entities.BaseModels;
using MyF.Entities.DtoModesl;
using MyF.Infrastructure.Data;
using MyF.Infrastructure.Mapping;
using System.Security.Cryptography;
using System.Text;

namespace MyF.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IMapper _mapper;

        public UserService(SqlSugarDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }
        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _db.Queryable<User>().FirstAsync(u => u.UserName == username);
        }
        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _db.Queryable<User>().FirstAsync(u => u.Email == email);
        }
        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<(User user, List<Role> roles, List<Permission> permissions)> LoginAsync(string account, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = await _db.Queryable<User>().FirstAsync(u => u.Account == account && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                // 获取用户角色
                var userRoles = await _db.Queryable<UserRole>()
                    .Where(ur => ur.UserId == user.Id)
                    .ToListAsync();

                // 获取角色ID列表
                var roleIds = userRoles.Select(ur => ur.RoleId).ToList();

                // 获取角色详细信息
                var roles = await _db.Queryable<Role>()
                    .Where(rp => roleIds.Contains(rp.Id)) 
                    .ToListAsync();

                // 获取角色对应的权限
                var permissions = await _db.Queryable<RolePermission>()
                    .Where(rp => roleIds.Contains(rp.RoleId))
                    .Select(rp => rp.PermissionId)
                    .ToListAsync();

                // 获取权限详细信息
                var permissionDetails = await _db.Queryable<Permission>()
                    .Where(p => permissions.Contains(p.Id))
                    .ToListAsync();

                return (user, roles,permissionDetails);
            } 
            return (new User(),new List<Role>(), new List<Permission>());
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> CreateUserAsync(UserRegistrationModel userDto, string password)
        {
            var user = _mapper.Map<UserRegistrationModel, User>(userDto);
            user.PasswordHash = HashPassword(password);
            user.CreatedAt = DateTime.UtcNow;
            user.IsActive = true;
            user.IsDeleted = false;

            await _db.Insertable(user).ExecuteCommandAsync();
            return user;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}