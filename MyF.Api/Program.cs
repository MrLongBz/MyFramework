using MyF.Entities.BaseModels;
using MyF.Infrastructure.Configuration;
using MyF.Infrastructure.Data;
using MyF.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 初始化配置管理器
MyF.Infrastructure.Configuration.ConfigurationManager.Initialize(builder.Configuration);

// 注册 SqlSugarDbContext
builder.Services.AddSingleton<SqlSugarDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// 初始化数据库
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SqlSugarDbContext>();
    
    // 获取当前应用程序的基础路径
    string basePath = AppDomain.CurrentDomain.BaseDirectory;
    
    // 构建 MyF.Entities.dll 的完整路径
    string entitiesAssemblyPath = Path.Combine(basePath, "MyF.Entities.dll");
    
    // 加载程序集
    Assembly entitiesAssembly = Assembly.LoadFrom(entitiesAssemblyPath);
    
    // 初始化数据库
    dbContext.InitDatabase(entitiesAssembly);

        // 添加种子数据
    var seeder = new DatabaseSeeder(dbContext);
    seeder.SeedData<User>("users.json");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
