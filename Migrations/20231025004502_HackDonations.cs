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
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationTag",
                columns: table => new
                {
                    OrganizationListId = table.Column<int>(type: "integer", nullable: false),
                    TagListId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationTag", x => new { x.OrganizationListId, x.TagListId });
                    table.ForeignKey(
                        name: "FK_OrganizationTag_Organizations_OrganizationListId",
                        column: x => x.OrganizationListId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationTag_Tags_TagListId",
                        column: x => x.TagListId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Description", "ImageUrl", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "UNICEF works in over 190 countries and territories to save children's lives, to defend their rights, and to help them fulfill their potential, from early childhood through adolescence.", "https://th.bing.com/th/id/R.7b5717ff47e176e9ac2a5580df80d6db?rik=N4afUMyWSF696Q&pid=ImgRaw&r=0", "UNICEF", 1 },
                    { 2, "Doctors Without Borders provides medical care to those affected by conflict, epidemics, disasters, or exclusion from healthcare. They are an international medical humanitarian organization that delivers emergency aid to people in need.", "https://4.bp.blogspot.com/-IJAS3gWYhCE/V8SZeGbKD8I/AAAAAAAAQFI/SyJeu-SzCpkh8R1NzePSysZ3Kni5oDtbQCLcB/s1600/msf_dual_english_cmyk_0.jpg", "Doctors Without Borders", 2 }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Health and Medical" },
                    { 2, "Environmental and Conservation" },
                    { 3, "Child Welfare and Development" },
                    { 4, "Social Welfare and Poverty Alleviation" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "Email", "ImageUrl", "Name", "PhoneNumber", "Uid" },
                values: new object[,]
                {
                    { 1, "The greatest person you'll ever meet.", "riley@email.com", "https://th.bing.com/th/id/R.f08431063da214d8c07452cca215447f?rik=7gKQvCXgiLVQXw&pid=ImgRaw&r=0", "Riley Tullis", "123-456-7890", "" },
                    { 2, "Eh, he's ok.", "jovanni@email.com", "https://th.bing.com/th/id/R.e733fb390ae9a3c28ca2389bd2466be7?rik=tGXnQ7Yf6T1kBQ&pid=ImgRaw&r=0", "Jovanni Feliz", "098-765-4321", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationTag_TagListId",
                table: "OrganizationTag",
                column: "TagListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationTag");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
