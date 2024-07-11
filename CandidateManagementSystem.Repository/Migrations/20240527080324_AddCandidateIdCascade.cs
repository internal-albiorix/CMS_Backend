using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidateIdCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_CandidateHistory_Candidates_CandidateId",
               table: "CandidateHistory");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateHistory_Candidates_CandidateId",
                table: "CandidateHistory",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
              name: "FK_CandidateHistory_Candidates_CandidateId",
              table: "CandidateHistory");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateHistory_Candidates_CandidateId",
                table: "CandidateHistory",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
