using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.Migrations
{
    /// <summary>
    /// 创建AdditionWorkItemStateSeed迁移的时候，因为模型和上下文没变，所以Up和Down方法都是空的
    /// <para>这种方式就能为特定表手动写数据初始化</para>
    /// </summary>
    public partial class AdditionWorkItemStateSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //用迁移构建器的InsertData方法就能指定往哪个表加什么数据
            //InsertData方法有3个参数，第1个是表名，第2个是列名数组/列名，第3个是值数组/值

            //这是为WorkItemStates表中的2个列插入一条新数据
            /*migrationBuilder.InsertData(
                table: "WorkItemStates",
                columns: new[] { "Id", "Value" },
                values: new object[] { 4, "Removed" });*/

            //这是为WorkItemStates表中的Value列插入一条新数据
            migrationBuilder.InsertData(
                table: "WorkItemStates",
                column: "Value",
                value: "On Hold");

            migrationBuilder.InsertData(
                table: "WorkItemStates",
                column: "Value",
                value: "Rejected");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //为了确保迁移回滚时，能撤销这些改动
            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "Value",
                keyValue: "On Hold");

            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "Value",
                keyValue: "Rejected");
        }
    }
}
