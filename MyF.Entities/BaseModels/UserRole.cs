using SqlSugar;
using MyF.Infrastructure.Data;

namespace MyF.Entities.BaseModels
{
    [SugarTable("P_UserRoles")] // 指定数据库表名为 UserRoles
    public class UserRole : BaseEntity
    {
        [SugarColumn(ColumnName = "UserId", IsNullable = false)] // 用户ID，不可为空
        public long UserId { get; set; }

        [SugarColumn(ColumnName = "RoleId", IsNullable = false)] // 角色ID，不可为空
        public long RoleId { get; set; }
    }
}
