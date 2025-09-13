namespace MyBoards.Entities
{
    /// <summary>
    /// 评论
    /// <para>一条评论对应一个工作项</para>
    /// <para>一条工作项可对应一个多条评论</para>
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 评论的消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 评论的作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 评论的创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 评论的更新日期
        /// </summary>
        public DateTime UpdateDate { get; set; }

        public WorkItem WorkItem { get; set; }

        public int WorkItemId { get; set; }
    }
}
