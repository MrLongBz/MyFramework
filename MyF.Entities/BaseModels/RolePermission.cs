using SqlSugar;
using MyF.Infrastructure.Data;

namespace MyF.Entities.BaseModels
{
    [SugarTable("P_RolePermissions")] // 指定数据库表名为 RolePermissions
    public class RolePermission : BaseEntity
    {
        [SugarColumn(ColumnName = "RoleId", IsNullable = false)] // 角色ID，不可为空
        public long RoleId { get; set; }

        [SugarColumn(ColumnName = "PermissionId", IsNullable = false)] // 权限ID，不可为空
        public long PermissionId { get; set; }
    }
}
