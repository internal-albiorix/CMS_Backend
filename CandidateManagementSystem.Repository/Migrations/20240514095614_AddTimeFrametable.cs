using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CandidateManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeFrametable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeFrame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeFrame", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TimeFrame",
                columns: new[] { "Id", "Name", "value" },
                values: new object[,]
                {
                    { 1, "Last 30 days", 30 },
                    { 2, "Last 60 days", 60 },
                    { 3, "Last 180 days", 180 },
                    { 4, "Last 365 days", 365 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeFrame");
        }
    }
}
