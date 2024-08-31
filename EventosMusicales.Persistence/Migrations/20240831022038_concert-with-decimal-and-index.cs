using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventosMusicales.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class concertwithdecimalandindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GenerosMusicales",
                table: "GenerosMusicales");

            migrationBuilder.EnsureSchema(
                name: "Musicales");

            migrationBuilder.RenameTable(
                name: "GenerosMusicales",
                newName: "Generos",
                newSchema: "Musicales");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Generos",
                schema: "Musicales",
                table: "Generos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Concierto",
                schema: "Musicales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Place = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    DateEvent = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ImageUrl = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    TicketsQuantity = table.Column<int>(type: "int", nullable: false),
                    Finalized = table.Column<bool>(type: "bit", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concierto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Concierto_Generos_GenreId",
                        column: x => x.GenreId,
                        principalSchema: "Musicales",
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Concierto_GenreId",
                schema: "Musicales",
                table: "Concierto",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Concierto_Title",
                schema: "Musicales",
                table: "Concierto",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Concierto",
                schema: "Musicales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Generos",
                schema: "Musicales",
                table: "Generos");

            migrationBuilder.RenameTable(
                name: "Generos",
                schema: "Musicales",
                newName: "GenerosMusicales");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenerosMusicales",
                table: "GenerosMusicales",
                column: "Id");
        }
    }
}
