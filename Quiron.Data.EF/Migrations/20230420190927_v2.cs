using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Quiron.Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uf = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEstado = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cidade_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Estado",
                columns: new[] { "Id", "Nome", "Uf" },
                values: new object[,]
                {
                    { new Guid("362c52b3-b9db-4aca-a48f-6e47aa77f819"), "Ceará", "CE" },
                    { new Guid("c4a41075-59a0-4e87-8a1c-0d542bc90155"), "Rio Grande do Norte", "RN" }
                });

            migrationBuilder.InsertData(
                table: "Cidade",
                columns: new[] { "Id", "IdEstado", "Nome" },
                values: new object[,]
                {
                    { new Guid("1d6209ec-a2d3-4536-8568-ee58cd8c46aa"), new Guid("c4a41075-59a0-4e87-8a1c-0d542bc90155"), "Pipa" },
                    { new Guid("373fad00-4ace-4c53-abbd-4fa11212cd88"), new Guid("362c52b3-b9db-4aca-a48f-6e47aa77f819"), "Fortaleza" },
                    { new Guid("70604862-7558-4b46-b6e1-787f6a20eb7c"), new Guid("c4a41075-59a0-4e87-8a1c-0d542bc90155"), "Natal" },
                    { new Guid("a5dc78eb-d526-42e1-bf5d-ba8a571a8b69"), new Guid("362c52b3-b9db-4aca-a48f-6e47aa77f819"), "Caucaia" },
                    { new Guid("e374123a-423f-45b3-994f-68065e291f9d"), new Guid("362c52b3-b9db-4aca-a48f-6e47aa77f819"), "Maracanaú" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cidade_IdEstado",
                table: "Cidade",
                column: "IdEstado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cidade");

            migrationBuilder.DropTable(
                name: "Estado");
        }
    }
}
