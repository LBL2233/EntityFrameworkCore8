using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBoards.Entities
{
    /// <summary>
    /// 工作项
    /// <para>一个工作项可对应多条评论</para>
    /// <para>一条评论对应一个工作项</para>
    /// <para>一个工作项对应一个用户</para>
    /// <para>一个用户可对应多个工作项</para>
    /// <para>每个工作项可以对应多个标签</para>
    /// <para>每个标签可以对应多个工作项</para>
    /// </summary>
    public class WorkItem
    {
        /// <summary>
        /// 主键（EntityFramework使用一种约定：基于名为Id的属性创建主键），或者在使用[Key]特性标识属性同样可以被识别为主键
        /// </summary>
        public int Id { get; set; }

        public WorkItemState State { get; set; }

        public int StateId { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        //[Column(TypeName = "varchar(200)")]  // 指定数据库中的列类型为varchar(200)而不是C# String类型 对应SQL数据库中默认的nvarchar(max)
        public string Area { get; set; }

        /// <summary>
        /// 迭代路径
        /// </summary>
        //[Column("Iteration_Path")]  // 指定数据库中的列名为Iteration_Path而不是默认的IterationPath
        public string IterationPath { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priorty { get; set; }

        /*Epic*/
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        //[Precision(3)]  //Precision特性用于指定用于指定数据库中该属性的精度。这里指定DateTime的精度为3，即毫秒级别，如：如 2024-06-01 12:34:56.789。
        public DateTime? EndDate { get; set; }

        /*Issue*/
        /// <summary>
        /// 工作量
        /// </summary>
        //[Column(TypeName = "decimal(5,2)")]  // 指定数据库中的列类型为decimal,精度为5,小数位为2
        public decimal Efford { get; set; }

        /*Task*/
        /// <summary>
        /// 活动
        /// </summary>
        //[MaxLength(200)]  // 其实也就是指定为varchar(200)类型
        public string Activity { get; set; }

        /// <summary>
        /// 剩余工作量
        /// </summary>
        //[Precision(14,2)]  // 指定数据库中的列类型为decimal,精度为14,小数位为2
        public decimal RemaningWork { get; set; }

        public string Type { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        /// <summary>
        /// 工作项的作者
        /// </summary>
        public User Author { get; set; }

        public Guid AuthorId { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
