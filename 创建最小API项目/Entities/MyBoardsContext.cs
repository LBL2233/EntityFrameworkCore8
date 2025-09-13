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
            //配置 WorkItem 实体的 State 属性，要求它在数据库中是“必填”的（即不能为 NULL）。
            modelBuilder.Entity<WorkItem>()
                .Property(x => x.State)
                .IsRequired();

            //配置 WorkItem 实体的 Area 属性，指定它在数据库中的列类型为 varchar(200)。
            modelBuilder.Entity<WorkItem>()
                .Property(x => x.Area)
                .HasColumnType("varchar(200)");

            //与上面的写法功能相同，都是配置实体中的属性，只是使用这种方法配置多个属性时更简洁方便
            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
                eb.Property(wi => wi.Efford).HasColumnType("decimal(5,2)");
                eb.Property(wi => wi.EndDate).HasPrecision(3);
                eb.Property(wi => wi.Activity).HasMaxLength(200);
                eb.Property(wi => wi.RemaningWork).HasPrecision(14,2);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

