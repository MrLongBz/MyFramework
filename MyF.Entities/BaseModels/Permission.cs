using SqlSugar;
using MyF.Infrastructure.Data;

namespace MyF.Entities.BaseModels
{
    [SugarTable("P_Permissions")] // 指定数据库表名为 Permissions
    public class Permission : BaseEntity
    {
        [SugarColumn(ColumnName = "PermissionName", Length = 50, IsNullable = false)] // 权限名称，最大长度50，不可为空
        public string PermissionName { get; set; }

        [SugarColumn(ColumnName = "Description", Length = 200, IsNullable = true)] // 权限描述，最大长度200，可为空
        public string Description { get; set; } // 权限描述
    }
}
