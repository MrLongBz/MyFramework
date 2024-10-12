using SqlSugar;
using MyF.Infrastructure.Data;

namespace MyF.Entities.BaseModels
{
    [SugarTable("P_Roles")] // 指定数据库表名为 Roles
    public class Role : BaseEntity
    {
        [SugarColumn(ColumnName = "RoleName", Length = 50, IsNullable = false)] // 角色名称，最大长度50，不可为空
        public string RoleName { get; set; }

        [SugarColumn(ColumnName = "Description", Length = 200, IsNullable = true)] // 角色描述，最大长度200，可为空
        public string Description { get; set; } // 角色描述
    }
}
