namespace MyBoards.Entities
{
    /// <summary>
    /// tags和workitems的多对多关系表，也可称为连接表
    /// <para>连接表是可选的,如果要存储两个表之间给定关系的额外信息(比如该类的PublicationDate)，那么连接表必不可少</para>
    /// </summary>
    public class WorkItemTag
    {
        public WorkItem WorkItem { get; set; }
        public int WorkItemId { get; set; }

        public Tag Tag { get; set; }
        public int TagId { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
