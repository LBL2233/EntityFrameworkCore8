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
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<WorkItemState> WorkItemStates { get; set; }

        //现在，整个数据库上下文包含9个属性，这些属性将代表数据库中的9个表

        /// <summary>
        /// 用于配置数据库模型的方法
        /// </summary>
        /// <param name="modelBuilder">该参数允许使用者配置数据库中的特定实体</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Epic>()
                .Property(x => x.EndDate)
                .HasPrecision(3); //配置 EndDate 属性的精度为3，即毫秒级别

            modelBuilder.Entity<Issue>()
                .Property(x => x.Efford)
                .HasColumnType("decimal(5,2)"); //配置 Efford 属性的数据库列类型为 decimal,精度为5,小数位为2

            modelBuilder.Entity<Task>(eb =>
            {
                eb.Property(x => x.Activity).HasMaxLength(200); //配置 Activity 属性的最大长度为200
                eb.Property(x => x.RemaningWork).HasPrecision(14, 2); //配置 RemaningWork 属性的数据库列类型为 decimal,精度为14,小数位为2
            });

            //配置 WorkItem 实体的 Area 属性，指定它在数据库中的列类型为 varchar(200)。
            modelBuilder.Entity<WorkItem>()
                .Property(x => x.Area)
                .HasColumnType("varchar(200)");

            //与上面的写法功能相同，都是配置实体中的属性，只是使用这种方法配置多个属性时更简洁方便
            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path"); //配置 IterationPath 属性在数据库中的列名为 Iteration_Path
                eb.Property(wi=>wi.Priorty).HasDefaultValue(1); //配置 Priorty 属性的默认值为 1

                eb.HasOne(w => w.State) //配置 WorkItem 实体与 WorkItemState 实体之间的多对一关系
                .WithMany()
                .HasForeignKey(w => w.StateId); //指定 WorkItem 实体中的 StateId 属性作为外键

                eb.HasMany(w => w.Comments) //配置 WorkItem 实体与 Comment 实体之间的一对多关系
                .WithOne(c => c.WorkItem) //配置 Comment 实体与 WorkItem 实体之间的多对一关系
                .HasForeignKey(c => c.WorkItemId); //指定 Comment 实体中的 WorkItemId 属性作为外键

                #region 外键解释
                //这段代码配置了 WorkItem（工作项）和 User（用户）之间的关系：
                //•	WorkItem 表有一个 AuthorId 字段，这个字段就是外键。
                //•	这个外键指向 User 表的主键 Id 字段。
                //•	这样，每个 WorkItem 都关联到一个 User（作者），而一个 User 可以有多个 WorkItem。
                //总结：
                //•	外键是 WorkItem 表中的 AuthorId 字段。
                //•	它引用的是 User 表的主键 Id 字段。
                //作用：
                //•	保证 WorkItem.AuthorId 的值必须在 User.Id 中存在，否则无法插入或更新数据。
                //•	实现了“一个用户有多个工作项，一个工作项只属于一个用户”的关系。
                //小提示：外键总是在“多”的一方（这里是 WorkItem）的表中
                #endregion
                eb.HasOne<User>(w => w.Author) //配置 WorkItem 实体与 User 实体之间的多对一关系
                .WithMany(u => u.WorkItems) //配置 User 实体与 WorkItem 实体之间的一对多关系
                .HasForeignKey(w => w.AuthorId); //指定 WorkItem 实体中的 AuthorId 属性作为外键

                eb.HasMany(w => w.Tags) //配置 WorkItem 实体与 Tag 实体之间的多对多关系
                    .WithMany(t => t.WorkItems) //配置 Tag 实体与 WorkItem 实体之间的多对多关系
                    .UsingEntity<WorkItemTag>( //配置连接实体 WorkItemTag
                    w => w.HasOne(wit => wit.Tag) //配置 WorkItemTag 实体与 Tag 实体之间的多对一关系
                    .WithMany()
                    .HasForeignKey(wit => wit.TagId),

                    w => w.HasOne(wit => wit.WorkItem) //配置 WorkItemTag 实体与 WorkItem 实体之间的多对一关系
                    .WithMany()
                    .HasForeignKey(wit => wit.WorkItemId),

                    wit =>
                    {
                        wit.HasKey(t => new { t.TagId, t.WorkItemId }); //配置联合主键，由 TagId 和 WorkItemId 组成
                        wit.Property(t => t.PublicationDate).HasDefaultValueSql("getutcdate()"); //配置 PublicationDate 属性的默认值为当前UTC时间
                    });
            });

            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(x=>x.CreatedDate).HasDefaultValueSql("getutcdate()"); //配置 CreatedDate 属性的默认值为当前UTC时间
                eb.Property(x => x.UpdateDate).ValueGeneratedOnUpdate(); //配置 UpdateDate 属性在每次更新时自动生成值

                eb.HasOne<User>(c => c.Author) //配置 Comment 实体与 User 实体之间的多对一关系
                .WithMany(a=>a.Comments) //配置 User 实体与 Comment 实体之间的一对多关系
                .HasForeignKey(c => c.AuthorId) //指定 Comment 实体中的 AuthorId 属性作为外键
                .OnDelete(DeleteBehavior.NoAction); //配置删除行为为 NoAction，防止级联删除
            });

            modelBuilder.Entity<User>()
                .HasOne(u => u.Address) // 配置 User 实体与 Address 实体之间的一对一关系
                .WithOne(a => a.User) // 配置 Address 实体与 User 实体之间的一对一关系
                .HasForeignKey<Address>(a => a.UserId); // 指定 Address 实体中的 UserId 属性作为外键

            modelBuilder.Entity<WorkItemState>()
                .Property(x=>x.Value)
                .IsRequired() // 配置 Value 属性为必填
                .HasMaxLength(50); // 配置 Value 属性的最大长度为 50

            modelBuilder.Entity<WorkItemState>()
                .HasData( // 使用 HasData 方法为 WorkItemState 实体预填充数据
                new WorkItemState { Id = 1, Value = "To Do" },
                new WorkItemState { Id = 2, Value = "Doing" },
                new WorkItemState { Id = 3, Value = "Done" }
                );

            modelBuilder.Entity<Tag>()
                .HasData( // 使用 HasData 方法为 Tag 实体预填充数据
                new Tag { Id = 1, Value = "Web" },
                new Tag { Id = 2, Value = "UI" },
                new Tag { Id = 3, Value = "Desktop" },
                new Tag { Id = 4, Value = "API" },
                new Tag { Id = 5, Value = "Service" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}

