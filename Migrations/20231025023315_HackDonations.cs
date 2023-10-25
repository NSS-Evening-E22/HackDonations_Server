using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HackDonations_Server.Migrations
{
    /// <inheritdoc />
    public partial class HackDonations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "donations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    payment_type = table.Column<string>(type: "text", nullable: false),
                    donation_amount = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_donations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    bio = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "comment_organization",
                columns: table => new
                {
                    comment_list_id = table.Column<int>(type: "integer", nullable: false),
                    organization_list_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment_organization", x => new { x.comment_list_id, x.organization_list_id });
                    table.ForeignKey(
                        name: "fk_comment_organization_comments_comment_list_id",
                        column: x => x.comment_list_id,
                        principalTable: "comments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comment_organization_organizations_organization_list_id",
                        column: x => x.organization_list_id,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "donation_organization",
                columns: table => new
                {
                    donation_list_id = table.Column<int>(type: "integer", nullable: false),
                    organization_list_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_donation_organization", x => new { x.donation_list_id, x.organization_list_id });
                    table.ForeignKey(
                        name: "fk_donation_organization_donations_donation_list_id",
                        column: x => x.donation_list_id,
                        principalTable: "donations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_donation_organization_organizations_organization_list_id",
                        column: x => x.organization_list_id,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_tag",
                columns: table => new
                {
                    organization_list_id = table.Column<int>(type: "integer", nullable: false),
                    tag_list_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization_tag", x => new { x.organization_list_id, x.tag_list_id });
                    table.ForeignKey(
                        name: "fk_organization_tag_organizations_organization_list_id",
                        column: x => x.organization_list_id,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organization_tag_tags_tag_list_id",
                        column: x => x.tag_list_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "organizations",
                columns: new[] { "id", "description", "image_url", "title", "user_id" },
                values: new object[,]
                {
                    { 1, "UNICEF works in over 190 countries and territories to save children's lives, to defend their rights, and to help them fulfill their potential, from early childhood through adolescence.", "https://th.bing.com/th/id/R.7b5717ff47e176e9ac2a5580df80d6db?rik=N4afUMyWSF696Q&pid=ImgRaw&r=0", "UNICEF", 1 },
                    { 2, "Doctors Without Borders provides medical care to those affected by conflict, epidemics, disasters, or exclusion from healthcare. They are an international medical humanitarian organization that delivers emergency aid to people in need.", "https://4.bp.blogspot.com/-IJAS3gWYhCE/V8SZeGbKD8I/AAAAAAAAQFI/SyJeu-SzCpkh8R1NzePSysZ3Kni5oDtbQCLcB/s1600/msf_dual_english_cmyk_0.jpg", "Doctors Without Borders", 2 }
                });

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Health and Medical" },
                    { 2, "Environmental and Conservation" },
                    { 3, "Child Welfare and Development" },
                    { 4, "Social Welfare and Poverty Alleviation" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "bio", "email", "image_url", "name", "phone_number", "uid" },
                values: new object[,]
                {
                    { 1, "The greatest person you'll ever meet.", "riley@email.com", "https://th.bing.com/th/id/R.f08431063da214d8c07452cca215447f?rik=7gKQvCXgiLVQXw&pid=ImgRaw&r=0", "Riley Tullis", "123-456-7890", "" },
                    { 2, "Eh, he's ok.", "jovanni@email.com", "https://th.bing.com/th/id/R.e733fb390ae9a3c28ca2389bd2466be7?rik=tGXnQ7Yf6T1kBQ&pid=ImgRaw&r=0", "Jovanni Feliz", "098-765-4321", "" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_comment_organization_organization_list_id",
                table: "comment_organization",
                column: "organization_list_id");

            migrationBuilder.CreateIndex(
                name: "ix_donation_organization_organization_list_id",
                table: "donation_organization",
                column: "organization_list_id");

            migrationBuilder.CreateIndex(
                name: "ix_organization_tag_tag_list_id",
                table: "organization_tag",
                column: "tag_list_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment_organization");

            migrationBuilder.DropTable(
                name: "donation_organization");

            migrationBuilder.DropTable(
                name: "organization_tag");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "donations");

            migrationBuilder.DropTable(
                name: "organizations");

            migrationBuilder.DropTable(
                name: "tags");
        }
    }
}
