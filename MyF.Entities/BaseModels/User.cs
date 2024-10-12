using SqlSugar;
using MyF.Infrastructure.Data;
using MyF.Infrastructure.Enum;

namespace MyF.Entities.BaseModels
{
    [SugarTable("P_Users")] // 指定数据库表名为 Users
    public class User : BaseEntity
    { 
        [SugarColumn(ColumnName = "Account", Length = 50, IsNullable = false)] // 账号，最大长度50，不可为空
        public string Account { get; set; }

        [SugarColumn(ColumnName = "UserName", Length = 50, IsNullable = false)] // 用户名，最大长度50，不可为空
        public string UserName { get; set; }

        [SugarColumn(ColumnName = "WeChat", Length = 100, IsNullable = false)] // 微信号，最大长度100，不可为空
        public string WeChat { get; set; }

        [SugarColumn(ColumnName = "Gender", Length = 1, IsNullable = false)] //  性别
        public Gender? Gender { get; set; }

        [SugarColumn(ColumnName = "Email", Length = 100, IsNullable = false)] // 邮箱，最大长度100，不可为空
        public string Email { get; set; }

        [SugarColumn(ColumnName = "PasswordHash", Length = 100, IsNullable = false)] // 密码哈希，最大长度100，不可为空
        public string PasswordHash { get; set; }

        [SugarColumn(ColumnName = "PhoneNumber", Length = 20, IsNullable = true)] // 电话号码，最大长度20，可为空
        public string PhoneNumber { get; set; }

        [SugarColumn(ColumnName = "IsActive", IsNullable = false)] // 是否激活，不可为空
        public bool IsActive { get; set; }

        [SugarColumn(ColumnName = "LastLoginDate", IsNullable = true)] // 最后登录时间，可为空
        public DateTime? LastLoginDate { get; set; }

        [SugarColumn(ColumnName = "CreatedAt", IsNullable = false)] // 创建时间，不可为空
        public DateTime CreatedAt { get; set; }

        [SugarColumn(ColumnName = "UpdatedAt", IsNullable = true)] // 更新时间，可为空
        public DateTime? UpdatedAt { get; set; }

        [SugarColumn(ColumnName = "IsDeleted", IsNullable = false)] // 是否已删除，不可为空
        public bool IsDeleted { get; set; }

        [SugarColumn(ColumnName = "Company", Length = 100, IsNullable = true)] // 公司名称，最大长度100，可为空
        public string Company { get; set; } // 新增的公司属性

        [SugarColumn(ColumnName = "Department", Length = 100, IsNullable = true)] // 部门名称，最大长度100，可为空
        public string Department { get; set; } // 新增的部门属性

        [SugarColumn(ColumnName = "TenantId", Length = 100, IsNullable = true)] // 租户ID，预留
        public string? TenantId { get; set; } // 新增的部门属性
    }
}
