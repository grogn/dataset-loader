using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatasetLoader.DataAccess.Mssql.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatasetModules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCyrillic = table.Column<bool>(type: "bit", nullable: false),
                    IsLatin = table.Column<bool>(type: "bit", nullable: false),
                    IsNumeric = table.Column<bool>(type: "bit", nullable: false),
                    IsSpecialSymbols = table.Column<bool>(type: "bit", nullable: false),
                    IsRegisterSpecific = table.Column<bool>(type: "bit", nullable: false),
                    AnswersLocation = table.Column<int>(type: "int", nullable: false),
                    DatasetPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatasetModules", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatasetModules");
        }
    }
}
