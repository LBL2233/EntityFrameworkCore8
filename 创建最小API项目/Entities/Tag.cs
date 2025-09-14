namespace MyBoards.Entities
{
    /// <summary>
    /// 标签
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// 主键
        /// <para>每个标签可以对应多个工作项</para>
        /// <para>每个工作项可以对应多个标签</para>
        /// </summary>
        public int Id { get; set; }

        public string Value { get; set; }

        public string Category { get; set; }

        public List<WorkItem> WorkItems { get; set; }
    }
}
