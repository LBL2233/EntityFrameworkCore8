namespace MyBoards.Entities
{
    /// <summary>
    /// 工作项
    /// </summary>
    public class WorkItem
    {
        /// <summary>
        /// 主键（EntityFramework使用一种约定：基于名为Id的属性创建主键），或者在使用[Key]特性标识属性同样可以被识别为主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 迭代路径
        /// </summary>
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
        public DateTime? EndDate { get; set; }

        /*Issue*/
        /// <summary>
        /// 工作量
        /// </summary>
        public decimal Efford { get; set; }

        /*Task*/
        /// <summary>
        /// 活动
        /// </summary>
        public string Activity { get; set; }

        /// <summary>
        /// 剩余工作量
        /// </summary>
        public decimal RemaningWork { get; set; }


        public string Type { get; set; }
    }
}
