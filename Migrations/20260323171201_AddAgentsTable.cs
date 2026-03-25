using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    /// <inheritdoc />
    public partial class AddAgentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RunDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TargetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Performance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            // Insert seed data for Agents
            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "Name", "Company", "Status", "RunDate", "TargetAmount", "Performance" },
                values: new object[,]
                {
                    { "John Doe", "Tech Solutions", "Không Hoạt Động", new DateTime(2024, 12, 5), 85m, 15m },
                    { "Sarah Miller", "Innovate Inc", "Hoạt Động", new DateTime(2024, 12, 12), 92m, 64m },
                    { "Interjohn Phelps", "Hardware", "Đang Chờ", new DateTime(2024, 12, 12), 68m, 54m },
                    { "David Clark", "Financial", "Hoạt Động", new DateTime(2024, 12, 5), 95m, 97m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agents");
        }
    }
}
