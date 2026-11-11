using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace curs.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    IsBuiltIn = table.Column<bool>(type: "boolean", nullable: false),
                    WidthMm = table.Column<double>(type: "double precision", nullable: false),
                    HeightMm = table.Column<double>(type: "double precision", nullable: false),
                    BackgroundColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    PreviewPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    WidthMm = table.Column<double>(type: "double precision", nullable: false),
                    HeightMm = table.Column<double>(type: "double precision", nullable: false),
                    BackgroundColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    TemplateId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCards_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CardElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ElementType = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PositionX = table.Column<double>(type: "double precision", nullable: false),
                    PositionY = table.Column<double>(type: "double precision", nullable: false),
                    Width = table.Column<double>(type: "double precision", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    Rotation = table.Column<double>(type: "double precision", nullable: false),
                    Opacity = table.Column<double>(type: "double precision", nullable: false),
                    ZOrder = table.Column<int>(type: "integer", nullable: false),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: false),
                    StrokeColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    StrokeThickness = table.Column<double>(type: "double precision", nullable: false),
                    FillColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    BusinessCardId = table.Column<int>(type: "integer", nullable: true),
                    TemplateId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardElements", x => x.Id);
                    table.CheckConstraint("CK_CardElements_ExactlyOneOwner", "(\"BusinessCardId\" IS NULL) <> (\"TemplateId\" IS NULL)");
                    table.ForeignKey(
                        name: "FK_CardElements_BusinessCards_BusinessCardId",
                        column: x => x.BusinessCardId,
                        principalTable: "BusinessCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardElements_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImagePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Stretch = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ElementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageProperties_CardElements_ElementId",
                        column: x => x.ElementId,
                        principalTable: "CardElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    FontFamily = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FontSize = table.Column<double>(type: "double precision", nullable: false),
                    IsBold = table.Column<bool>(type: "boolean", nullable: false),
                    IsItalic = table.Column<bool>(type: "boolean", nullable: false),
                    IsUnderline = table.Column<bool>(type: "boolean", nullable: false),
                    Foreground = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    TextAlignment = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    ElementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextProperties_CardElements_ElementId",
                        column: x => x.ElementId,
                        principalTable: "CardElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Position = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Organization = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true),
                    Website = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PhotoPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_TextProperties_Id",
                        column: x => x.Id,
                        principalTable: "TextProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCards_TemplateId",
                table: "BusinessCards",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CardElements_BusinessCardId",
                table: "CardElements",
                column: "BusinessCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardElements_TemplateId",
                table: "CardElements",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageProperties_ElementId",
                table: "ImageProperties",
                column: "ElementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TextProperties_ElementId",
                table: "TextProperties",
                column: "ElementId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "ImageProperties");

            migrationBuilder.DropTable(
                name: "TextProperties");

            migrationBuilder.DropTable(
                name: "CardElements");

            migrationBuilder.DropTable(
                name: "BusinessCards");

            migrationBuilder.DropTable(
                name: "Templates");
        }
    }
}
