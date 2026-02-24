using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BIBLIOGEST.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Auteur", "Categorie", "DateCreation", "DateModification", "Description", "Fournisseur", "ISBN", "Nom", "Prix", "QuantiteStock" },
                values: new object[,]
                {
                    { 1, "Antoine de Saint-Exupéry", "Roman", new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Local), null, "Un conte philosophique et poétique sous l'apparence d'un conte pour enfants.", "Gallimard", "978-2-07-040850-4", "Le Petit Prince", 12.50m, 45 },
                    { 2, "George Orwell", "Science-Fiction", new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Local), null, "Roman dystopique décrivant une société totalitaire.", "Folio", "978-2-07-036822-8", "1984", 8.90m, 28 },
                    { 3, "Victor Hugo", "Roman", new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Local), null, "Chef-d'œuvre de la littérature française du XIXe siècle.", "Le Livre de Poche", "978-2-253-00496-8", "Les Misérables", 15.00m, 12 },
                    { 4, "J.K. Rowling", "Fantaisie", new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Local), null, "Premier tome de la saga Harry Potter.", "Gallimard Jeunesse", "978-2-07-054127-0", "Harry Potter à l'école des sorciers", 18.50m, 65 },
                    { 5, "Robert C. Martin", "Informatique", new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Local), null, "Guide pratique pour écrire du code propre et maintenable.", "Pearson", "978-0-13-235088-4", "Clean Code", 45.00m, 8 },
                    { 6, "Yuval Noah Harari", "Histoire", new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Local), null, "Une brève histoire de l'humanité.", "Albin Michel", "978-2-226-25710-4", "Sapiens", 24.00m, 32 },
                    { 7, "Eiichiro Oda", "Manga", new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Local), null, "Le début des aventures de Luffy.", "Glénat", "978-2-7234-3329-7", "One Piece Tome 1", 6.90m, 0 },
                    { 8, "Sun Tzu", "Histoire", new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Local), null, "Traité de stratégie militaire chinois.", "Flammarion", "978-2-08-070508-0", "L'Art de la Guerre", 5.90m, 22 }
                });
        }
    }
}
