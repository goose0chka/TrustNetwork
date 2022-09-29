using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrustNetwork.DAL.Migrations
{
    public partial class topic_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonTopic_Persons_PersonId",
                table: "PersonTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonTopic_Topic_TopicId",
                table: "PersonTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topic",
                table: "Topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonTopic",
                table: "PersonTopic");

            migrationBuilder.RenameTable(
                name: "Topic",
                newName: "Topics");

            migrationBuilder.RenameTable(
                name: "PersonTopic",
                newName: "PersonTopics");

            migrationBuilder.RenameIndex(
                name: "IX_PersonTopic_TopicId",
                table: "PersonTopics",
                newName: "IX_PersonTopics_TopicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "TopicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonTopics",
                table: "PersonTopics",
                columns: new[] { "PersonId", "TopicId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTopics_Persons_PersonId",
                table: "PersonTopics",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTopics_Topics_TopicId",
                table: "PersonTopics",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonTopics_Persons_PersonId",
                table: "PersonTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonTopics_Topics_TopicId",
                table: "PersonTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonTopics",
                table: "PersonTopics");

            migrationBuilder.RenameTable(
                name: "Topics",
                newName: "Topic");

            migrationBuilder.RenameTable(
                name: "PersonTopics",
                newName: "PersonTopic");

            migrationBuilder.RenameIndex(
                name: "IX_PersonTopics_TopicId",
                table: "PersonTopic",
                newName: "IX_PersonTopic_TopicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topic",
                table: "Topic",
                column: "TopicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonTopic",
                table: "PersonTopic",
                columns: new[] { "PersonId", "TopicId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTopic_Persons_PersonId",
                table: "PersonTopic",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTopic_Topic_TopicId",
                table: "PersonTopic",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
