using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace MyF.Infrastructure.Data
{
    public class DatabaseSeeder
    {
        private readonly SqlSugarDbContext _dbContext;

        public DatabaseSeeder(SqlSugarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedData<T>(string seedFileName) where T : class, IEntity, new()
        {
            var entities = LoadEntitiesFromJson<T>(seedFileName);
            if (entities != null && entities.Any())
            {
                var db = _dbContext.GetDbClient();
                if (!db.Queryable<T>().Any())
                {
                    db.Insertable(entities).ExecuteCommand();
                }
            }
        }

        private List<T> LoadEntitiesFromJson<T>(string seedFileName) where T : class, new()
        {
            string seedFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Seed", seedFileName);
            if (!File.Exists(seedFilePath))
            {
                Console.WriteLine($"Seed file not found: {seedFilePath}");
                return null;
            }

            string jsonContent = File.ReadAllText(seedFilePath);
            var entities = JsonSerializer.Deserialize<List<T>>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return entities;
        }
    }
}