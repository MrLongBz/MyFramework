using SqlSugar;

namespace MyF.Infrastructure.Data
{
    public abstract class BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 主键，自动递增
        public long Id { get; set; }
    }
}