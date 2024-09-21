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

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _db.Queryable<User>().FirstAsync(u => u.UserName == username);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _db.Queryable<User>().FirstAsync(u => u.Email == email);
        }

        public async Task<User> LoginAsync(string account, string password)
        {
            var hashedPassword = HashPassword(password);
            return await _db.Queryable<User>().FirstAsync(u => u.UserName == account && u.PasswordHash == hashedPassword);
        }

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