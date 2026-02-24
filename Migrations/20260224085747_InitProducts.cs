using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BIBLIOGEST.Migrations
{
    /// <inheritdoc />
    public partial class InitProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Auteur = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ISBN = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prix = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Categorie = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuantiteStock = table.Column<int>(type: "int", nullable: false),
                    Fournisseur = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreation = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModification = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Auteur", "Categorie", "DateCreation", "DateModification", "Description", "Fournisseur", "ISBN", "Nom", "Prix", "QuantiteStock" },
                values: new object[,]
                {
                    { 1, "Antoine de Saint-Exupéry", "Roman", new DateTime(2026, 2, 24, 9, 57, 46, 970, DateTimeKind.Local).AddTicks(888), null, "Un conte philosophique et poétique sous l'apparence d'un conte pour enfants.", "Gallimard", "978-2-07-040850-4", "Le Petit Prince", 12.50m, 45 },
                    { 2, "George Orwell", "Science-Fiction", new DateTime(2026, 2, 24, 9, 57, 46, 970, DateTimeKind.Local).AddTicks(1164), null, "Roman dystopique décrivant une société totalitaire.", "Folio", "978-2-07-036822-8", "1984", 8.90m, 28 },
                    { 3, "Victor Hugo", "Roman", new DateTime(2026, 2, 24, 9, 57, 46, 970, DateTimeKind.Local).AddTicks(1167), null, "Chef-d'œuvre de la littérature française du XIXe siècle.", "Le Livre de Poche", "978-2-253-00496-8", "Les Misérables", 15.00m, 12 },
                    { 4, "J.K. Rowling", "Fantaisie", new DateTime(2026, 2, 24, 9, 57, 46, 970, DateTimeKind.Local).AddTicks(1170), null, "Premier tome de la saga Harry Potter.", "Gallimard Jeunesse", "978-2-07-054127-0", "Harry Potter à l'école des sorciers", 18.50m, 65 },
                    { 5, "Robert C. Martin", "Informatique", new DateTime(2026, 2, 24, 9, 57, 46, 970, DateTimeKind.Local).AddTicks(1172), null, "Guide pratique pour écrire du code propre et maintenable.", "Pearson", "978-0-13-235088-4", "Clean Code", 45.00m, 8 },
                    { 6, "Yuval Noah Harari", "Histoire", new DateTime(2026, 2, 24, 9, 57, 46, 970, DateTimeKind.Local).AddTicks(1175), null, "Une brève histoire de l'humanité.", "Albin Michel", "978-2-226-25710-4", "Sapiens", 24.00m, 32 },
                    { 7, "Eiichiro Oda", "Manga", new DateTime(2026, 2, 24, 9, 57, 46, 970, DateTimeKind.Local).AddTicks(1177), null, "Le début des aventures de Luffy.", "Glénat", "978-2-7234-3329-7", "One Piece Tome 1", 6.90m, 0 },
                    { 8, "Sun Tzu", "Histoire", new DateTime(2026, 2, 24, 9, 57, 46, 970, DateTimeKind.Local).AddTicks(1180), null, "Traité de stratégie militaire chinois.", "Flammarion", "978-2-08-070508-0", "L'Art de la Guerre", 5.90m, 22 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Auteur",
                table: "Products",
                column: "Auteur");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Categorie",
                table: "Products",
                column: "Categorie");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ISBN",
                table: "Products",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Nom",
                table: "Products",
                column: "Nom");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
