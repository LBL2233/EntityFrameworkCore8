using Microsoft.EntityFrameworkCore;

namespace MyBoards.Entities
{
    public class MyBoardsContext:DbContext
    {
        public MyBoardsContext(DbContextOptions<MyBoardsContext> options) : base(options)
        {
            
        }

        //在此处将利用创建好的类来定义数据库中将存在的表
        //DbSet类接收一个泛型参数，这个泛型参数将代表特定表，给定DbSet的名称将代表数据库中对应表的名称
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }

        //现在，整个数据库上下文包含5个属性，这些属性将代表数据库中的5个表

        /// <summary>
        /// 用于配置数据库模型的方法
        /// </summary>
        /// <param name="modelBuilder">该参数允许使用者配置数据库中的特定实体</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

