using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BomTratoData.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aprovador",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aprovador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escritorios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "varchar(100)", nullable: true),
                    Number = table.Column<string>(type: "varchar(100)", nullable: true),
                    State = table.Column<string>(type: "varchar(100)", nullable: true),
                    Cep = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "varchar(100)", nullable: true),
                    District = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escritorios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    AprovadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EscritorioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Aproved = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ComplainerName = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processos_Aprovador_AprovadorId",
                        column: x => x.AprovadorId,
                        principalTable: "Aprovador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Processos_Escritorios_EscritorioId",
                        column: x => x.EscritorioId,
                        principalTable: "Escritorios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processos_AprovadorId",
                table: "Processos",
                column: "AprovadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Processos_EscritorioId",
                table: "Processos",
                column: "EscritorioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Processos");

            migrationBuilder.DropTable(
                name: "Aprovador");

            migrationBuilder.DropTable(
                name: "Escritorios");
        }
    }
}
