using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDevSteam.Migrations
{
    /// <inheritdoc />
    public partial class Tabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlImagem",
                table: "Jogos",
                newName: "DescricaoNome");

            migrationBuilder.AddColumn<string>(
                name: "Baner",
                table: "Jogos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "JogosMidia",
                columns: table => new
                {
                    JogoMidiaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JogoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JogoId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogosMidia", x => x.JogoMidiaId);
                    table.ForeignKey(
                        name: "FK_JogosMidia_Jogos_JogoId1",
                        column: x => x.JogoId1,
                        principalTable: "Jogos",
                        principalColumn: "JogoId");
                });

            migrationBuilder.CreateTable(
                name: "JogosCategorias",
                columns: table => new
                {
                    JogoCategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogosCategorias", x => x.JogoCategoriaId);
                    table.ForeignKey(
                        name: "FK_JogosCategorias_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JogosCategorias_Jogos_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogos",
                        principalColumn: "JogoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JogosCategorias_CategoriaId",
                table: "JogosCategorias",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_JogosCategorias_JogoId",
                table: "JogosCategorias",
                column: "JogoId");

            migrationBuilder.CreateIndex(
                name: "IX_JogosMidia_JogoId1",
                table: "JogosMidia",
                column: "JogoId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JogosCategorias");

            migrationBuilder.DropTable(
                name: "JogosMidia");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropColumn(
                name: "Baner",
                table: "Jogos");

            migrationBuilder.RenameColumn(
                name: "DescricaoNome",
                table: "Jogos",
                newName: "UrlImagem");
        }
    }
}
