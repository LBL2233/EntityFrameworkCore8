using Microsoft.EntityFrameworkCore;
using MyBoards.Entities;

namespace MyBoards
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //在依赖注入容器中注册数据库上下文
            builder.Services.AddDbContext<MyBoardsContext>(
                option => option.UseSqlServer(
                    builder.Configuration.GetConnectionString("MyBoardsConnectionString"))
                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<MyBoardsContext>();

            var pendingMigrations = dbContext.Database.GetPendingMigrations(); //获取是否有任何迁移尚未应用到连接字符串中数据库的信息
            if (pendingMigrations.Any()) //如果有待执行的迁移
            {
                dbContext.Database.Migrate(); //执行所有待执行的迁移
            }

            app.Run();
        }
    }
}
