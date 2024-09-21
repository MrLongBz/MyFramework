using SqlSugar;
using System.Collections.Concurrent;
using MyF.Infrastructure.Configuration;
using System;
using System.Reflection;
using System.Linq.Expressions;

namespace MyF.Infrastructure.Data
{
    public class SqlSugarDbContext
    {
        private readonly ConcurrentDictionary<string, SqlSugarClient> _tenantDbClients = new();
        private readonly ConcurrentDictionary<Type, string> _tableNameCache = new();

        public SqlSugarClient GetDbClient(string tenantId = null)
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                return _tenantDbClients.GetOrAdd("default", _ => CreateDbClient(ConfigurationManager.GetConnectionString("DefaultConnection")));
            }

            return _tenantDbClients.GetOrAdd(tenantId, _ => CreateDbClient(GetConnectionString(tenantId)));
        }

        private SqlSugarClient CreateDbClient(string connectionString)
        {
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            ConfigureGlobalFilter(db);
            ConfigureSqlLog(db);
           // ConfigureEntityTableNameMapping(db);

            return db;
        }

        private string GetConnectionString(string tenantId)
        {
            return ConfigurationManager.GetConnectionString($"TenantConnections:{tenantId}")
                ?? ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        private void ConfigureGlobalFilter(SqlSugarClient db)
        {
            db.QueryFilter.Add(new SqlFilterItem()
            {
                FilterValue = filterDb =>
                {
                    return new SqlFilterResult() { Sql = " IsDeleted = 0 " };
                },
                IsJoinQuery = true
            });
        }

        private void ConfigureSqlLog(SqlSugarClient db)
        {
            db.Aop.OnLogExecuting = (sql, parameters) =>
            {
                // 这里可以添加日志记录逻辑，例如使用 ILogger
                Console.WriteLine(sql);
            };
        }

        private void ConfigureEntityTableNameMapping(SqlSugarClient db)
        {
            db.CurrentConnectionConfig.ConfigureExternalServices = new ConfigureExternalServices()
            {
                EntityNameService = (type,  entity) =>
                {
                     _tableNameCache.GetOrAdd(type, _ =>
                    {
                        var tableName = string.Concat(entity.EntityName.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
                        return tableName;
                    });
                }
            };
        }

        public void InitDatabase(params Assembly[] assemblies)
        {
            var db = GetDbClient();
            db.DbMaintenance.CreateDatabase();

            var entityTypes = GetEntityTypes(assemblies);
            foreach (var entityType in entityTypes)
            {
                db.CodeFirst.SetStringDefaultLength(200)
                            .SplitTables()
                            .InitTables(entityType);
            }
        }

        public void MigrateDatabase(params Assembly[] assemblies)
        {
            var db = GetDbClient();
            var entityTypes = GetEntityTypes(assemblies);

            foreach (var entityType in entityTypes)
            {
                db.CodeFirst.SetStringDefaultLength(200)
                            .SplitTables()
                            .InitTables(entityType);
            }
        }

        private List<Type> GetEntityTypes(Assembly[] assemblies)
        {
            var entityTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                entityTypes.AddRange(assembly.GetTypes()
                    .Where(type => !type.IsAbstract && type.GetCustomAttributes(typeof(SugarTable), false).Any()));
            }
            return entityTypes;
        }

        // 辅助方法
        public ISugarQueryable<T> Query<T>() where T : class, new()
        {
            return GetDbClient().Queryable<T>();
        }

        public IInsertable<T> Insert<T>(T entity) where T : class, new()
        {
            return GetDbClient().Insertable(entity);
        }

        public IUpdateable<T> Update<T>() where T : class, new()
        {
            return GetDbClient().Updateable<T>();
        }

        public IDeleteable<T> Delete<T>() where T : class, new()
        {
            return GetDbClient().Deleteable<T>();
        }

        public void BeginTran()
        {
            GetDbClient().Ado.BeginTran();
        }

        public void CommitTran()
        {
            GetDbClient().Ado.CommitTran();
        }

        public void RollbackTran()
        {
            GetDbClient().Ado.RollbackTran();
        }

        public void ExecuteTransaction(Action action)
        {
            var db = GetDbClient();
            try
            {
                db.Ado.BeginTran();
                action();
                db.Ado.CommitTran();
            }
            catch
            {
                db.Ado.RollbackTran();
                throw;
            }
        }

        public T ExecuteTransaction<T>(Func<T> func)
        {
            var db = GetDbClient();
            try
            {
                db.Ado.BeginTran();
                var result = func();
                db.Ado.CommitTran();
                return result;
            }
            catch
            {
                db.Ado.RollbackTran();
                throw;
            }
        }

        public void BulkInsert<T>(List<T> entities) where T : class, new()
        {
            GetDbClient().Fastest<T>().BulkCopy(entities);
        }

        public void BulkUpdate<T>(List<T> entities) where T : class, new()
        {
            GetDbClient().Fastest<T>().BulkUpdate(entities);
        }

        public void TruncateTable<T>() where T : class, new()
        {
            GetDbClient().DbMaintenance.TruncateTable<T>();
        }

        public bool TableExists<T>() where T : class, new()
        {
            return GetDbClient().DbMaintenance.IsAnyTable(typeof(T).Name);
        }
    }
}