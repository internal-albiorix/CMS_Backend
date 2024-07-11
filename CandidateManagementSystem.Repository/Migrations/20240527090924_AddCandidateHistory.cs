using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidateHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
     name: "CandidateHistory",
     columns: table => new
     {
         Id = table.Column<int>(type: "int", nullable: false)
             .Annotation("SqlServer:Identity", "1, 1"),
         CandidateId = table.Column<int>(type: "int", nullable: false),
         StatusId = table.Column<int>(type: "int", nullable: true),
         InterviewRoundId = table.Column<int>(type: "int", nullable: true),
         InterviewerId = table.Column<int>(type: "int", nullable: true),
         Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
         InterviewStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
         TimeLineDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
         IsActive = table.Column<bool>(type: "bit", nullable: false),
         UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
         UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
         InsertedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
         InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_CandidateHistory", x => x.Id);
         table.ForeignKey(
             name: "FK_CandidateHistory_Candidates_CandidateId",
             column: x => x.CandidateId,
             principalTable: "Candidates",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);
         table.ForeignKey(
             name: "FK_CandidateHistory_InterviewRound_InterviewRoundId",
             column: x => x.InterviewRoundId,
             principalTable: "InterviewRound",
             principalColumn: "Id");
         table.ForeignKey(
             name: "FK_CandidateHistory_Status_StatusId",
             column: x => x.StatusId,
             principalTable: "Status",
             principalColumn: "Id");
         table.ForeignKey(
             name: "FK_CandidateHistory_Users_InterviewerId",
             column: x => x.InterviewerId,
             principalTable: "Users",
             principalColumn: "Id");
     });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
