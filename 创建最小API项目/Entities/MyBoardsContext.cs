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
                eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path"); //配置 IterationPath 属性在数据库中的列名为 Iteration_Path
                eb.Property(wi => wi.Efford).HasColumnType("decimal(5,2)"); //配置 Efford 属性的数据库列类型为 decimal,精度为5,小数位为2
                eb.Property(wi => wi.EndDate).HasPrecision(3); //配置 EndDate 属性的精度为3，即毫秒级别
                eb.Property(wi => wi.Activity).HasMaxLength(200); //配置 Activity 属性的最大长度为200
                eb.Property(wi => wi.RemaningWork).HasPrecision(14,2); //配置 RemaningWork 属性的数据库列类型为 decimal,精度为14,小数位为2
                eb.Property(wi=>wi.Priorty).HasDefaultValue(1); //配置 Priorty 属性的默认值为 1

                eb.HasMany(w => w.Comments) //配置 WorkItem 实体与 Comment 实体之间的一对多关系
                .WithOne(c => c.WorkItem) //配置 Comment 实体与 WorkItem 实体之间的多对一关系
                .HasForeignKey(c => c.WorkItemId); //指定 Comment 实体中的 WorkItemId 属性作为外键

                eb.HasOne<User>(w => w.Author) //配置 WorkItem 实体与 User 实体之间的多对一关系
                .WithMany(u => u.WorkItems) //配置 User 实体与 WorkItem 实体之间的一对多关系
                .HasForeignKey(w => w.AuthorId); //指定 WorkItem 实体中的 AuthorId 属性作为外键
            });

            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(x=>x.CreatedDate).HasDefaultValue("getutcdate()"); //配置 CreatedDate 属性的默认值为当前UTC时间
                eb.Property(x => x.UpdateDate).ValueGeneratedOnUpdate(); //配置 UpdateDate 属性在每次更新时自动生成值
            });

            modelBuilder.Entity<User>()
                .HasOne(u => u.Address) // 配置 User 实体与 Address 实体之间的一对一关系
                .WithOne(a => a.User) // 配置 Address 实体与 User 实体之间的一对一关系
                .HasForeignKey<Address>(a => a.UserId); // 指定 Address 实体中的 UserId 属性作为外键

            base.OnModelCreating(modelBuilder);
        }
    }
}

