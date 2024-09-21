using SqlSugar;
using MyF.Infrastructure.Data;

namespace MyF.Entities.BaseModels
{
   [SugarTable("Users")]
    public class User: IEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnName = "UserName", Length = 50, IsNullable = false)]
        public string UserName { get; set; }

        [SugarColumn(ColumnName = "Email", Length = 100, IsNullable = false)]
        public string Email { get; set; }

        [SugarColumn(ColumnName = "PasswordHash", Length = 100, IsNullable = false)]
        public string PasswordHash { get; set; }

        [SugarColumn(ColumnName = "FirstName", Length = 50, IsNullable = true)]
        public string FirstName { get; set; }

        [SugarColumn(ColumnName = "LastName", Length = 50, IsNullable = true)]
        public string LastName { get; set; }

        [SugarColumn(ColumnName = "PhoneNumber", Length = 20, IsNullable = true)]
        public string PhoneNumber { get; set; }

        [SugarColumn(ColumnName = "IsActive", IsNullable = false)]
        public bool IsActive { get; set; }

        [SugarColumn(ColumnName = "LastLoginDate", IsNullable = true)]
        public DateTime? LastLoginDate { get; set; }

        [SugarColumn(ColumnName = "CreatedAt", IsNullable = false)]
        public DateTime CreatedAt { get; set; }

        [SugarColumn(ColumnName = "UpdatedAt", IsNullable = true)]
        public DateTime? UpdatedAt { get; set; }

        [SugarColumn(ColumnName = "IsDeleted", IsNullable = false)]
        public bool IsDeleted { get; set; }
    }
}
