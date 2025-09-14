using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.Migrations
{
    public partial class UserFullName : Migration
    {
        /// <summary>
        /// 当执行迁移时，EntityFramework将从上到下执行Up方法中的命令
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            //Sql方法用于执行自定义的SQL代码,Sql方法接受一个参数，该参数是一个字符串，表示要执行的SQL脚本
            //该脚本确保在移除FirstName和LastName列之前，将FullName列正确地填充为FirstName和LastName的组合
            migrationBuilder.Sql(@"
                UPDATE Users 
                SET FullName=FirstName+' '+LastName"
            );

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            //将FullName列拆分回FirstName和LastName列
            migrationBuilder.Sql(@"
                UPDATE Users
                SET 
                    FirstName = 
                        CASE 
                            WHEN CHARINDEX(' ', FullName) > 0 
                                THEN LEFT(FullName, CHARINDEX(' ', FullName) - 1)
                            ELSE FullName
                        END,
                    LastName = 
                        CASE 
                            WHEN CHARINDEX(' ', FullName) > 0 
                                THEN SUBSTRING(FullName, CHARINDEX(' ', FullName) + 1, LEN(FullName))
                            ELSE ''
                        END
            ");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");
        }
    }
}
