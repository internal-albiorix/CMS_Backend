using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreateInquiriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InquiriesId",
                table: "TechnologyAssociation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Inquiries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnologyAssociation_InquiriesId",
                table: "TechnologyAssociation",
                column: "InquiriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyAssociation_Inquiries_InquiriesId",
                table: "TechnologyAssociation",
                column: "InquiriesId",
                principalTable: "Inquiries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnologyAssociation_Inquiries_InquiriesId",
                table: "TechnologyAssociation");

            migrationBuilder.DropTable(
                name: "Inquiries");

            migrationBuilder.DropIndex(
                name: "IX_TechnologyAssociation_InquiriesId",
                table: "TechnologyAssociation");

            migrationBuilder.DropColumn(
                name: "InquiriesId",
                table: "TechnologyAssociation");
        }
    }
}
