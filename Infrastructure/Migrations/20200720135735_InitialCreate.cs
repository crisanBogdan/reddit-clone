using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "comment_vote",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false),
                    comment_id = table.Column<int>(nullable: false),
                    vote = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__commentu__D7C7606774E3E164", x => new { x.user_id, x.comment_id });
                });

            migrationBuilder.CreateTable(
                name: "post_vote",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false),
                    post_id = table.Column<int>(nullable: false),
                    vote = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__postupvo__CA534F79F69D59F1", x => new { x.user_id, x.post_id });
                });

            migrationBuilder.CreateTable(
                name: "topic",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 32, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topic", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 24, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    hash = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    salt = table.Column<string>(unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 32, nullable: false),
                    content = table.Column<string>(maxLength: 255, nullable: true),
                    url = table.Column<string>(maxLength: 64, nullable: true),
                    user_id = table.Column<int>(nullable: false),
                    topic_id = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.id);
                    table.ForeignKey(
                        name: "FK__post__topic_id__21B6055D",
                        column: x => x.topic_id,
                        principalTable: "topic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__post__user_id__164452B1",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(maxLength: 255, nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    post_id = table.Column<int>(nullable: false),
                    comment_id = table.Column<int>(nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.id);
                    table.ForeignKey(
                        name: "FK__comment__comment__4222D4EF",
                        column: x => x.comment_id,
                        principalTable: "comment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__comment__post_id__4316F928",
                        column: x => x.post_id,
                        principalTable: "post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__comment__user_id__440B1D61",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comment_comment_id",
                table: "comment",
                column: "comment_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_post_id",
                table: "comment",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_user_id",
                table: "comment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_topic_id",
                table: "post",
                column: "topic_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_user_id",
                table: "post",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "TITLE_UNIQUE",
                table: "topic",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "u_name",
                table: "user",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "comment_vote");

            migrationBuilder.DropTable(
                name: "post_vote");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "topic");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
