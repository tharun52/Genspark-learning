using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Twitter.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hastags",
                columns: table => new
                {
                    HastagId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tag = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hastags", x => x.HastagId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "tweets",
                columns: table => new
                {
                    TweetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tweets", x => x.TweetId);
                    table.ForeignKey(
                        name: "FK_tweets_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userfollows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FollowingId = table.Column<int>(type: "integer", nullable: false),
                    FollowerId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userfollows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userfollows_users_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userfollows_users_FollowingId",
                        column: x => x.FollowingId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    FollowersUserId = table.Column<int>(type: "integer", nullable: false),
                    FollowingUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.FollowersUserId, x.FollowingUserId });
                    table.ForeignKey(
                        name: "FK_UserUser_users_FollowersUserId",
                        column: x => x.FollowersUserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_users_FollowingUserId",
                        column: x => x.FollowingUserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TweetId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comments_tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "tweets",
                        principalColumn: "TweetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TweetId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_likes_tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "tweets",
                        principalColumn: "TweetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_likes_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tweethastags",
                columns: table => new
                {
                    TweetHastagsId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TweetId = table.Column<int>(type: "integer", nullable: false),
                    HastagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tweethastags", x => x.TweetHastagsId);
                    table.ForeignKey(
                        name: "FK_tweethastags_hastags_HastagId",
                        column: x => x.HastagId,
                        principalTable: "hastags",
                        principalColumn: "HastagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tweethastags_tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "tweets",
                        principalColumn: "TweetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_TweetId",
                table: "comments",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_UserId",
                table: "comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_likes_TweetId",
                table: "likes",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_likes_UserId",
                table: "likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tweethastags_HastagId",
                table: "tweethastags",
                column: "HastagId");

            migrationBuilder.CreateIndex(
                name: "IX_tweethastags_TweetId",
                table: "tweethastags",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_tweets_UserId",
                table: "tweets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_userfollows_FollowerId",
                table: "userfollows",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_userfollows_FollowingId",
                table: "userfollows",
                column: "FollowingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_FollowingUserId",
                table: "UserUser",
                column: "FollowingUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "likes");

            migrationBuilder.DropTable(
                name: "tweethastags");

            migrationBuilder.DropTable(
                name: "userfollows");

            migrationBuilder.DropTable(
                name: "UserUser");

            migrationBuilder.DropTable(
                name: "hastags");

            migrationBuilder.DropTable(
                name: "tweets");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
