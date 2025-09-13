namespace MyBoards.Entities
{
    /// <summary>
    /// 用户
    /// <para>一个用户对应一个地址</para>
    /// <para>一个地址对应一个用户</para>
    /// <para>一个工作项对应一个用户</para>
    /// <para>一个用户可对应多个工作项</para>
    /// </summary>
    public class User
    {
        /// <summary>
        /// 主键（Guid也是可以被EntityFramework识别为主键的命名约定）
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 姓氏
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        public Address Address { get; set; }

        public List<WorkItem> WorkItems { get; set; }
    }
}
