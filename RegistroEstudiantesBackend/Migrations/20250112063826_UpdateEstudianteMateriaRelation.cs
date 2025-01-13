using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroEstudiantesBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEstudianteMateriaRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "EstudiantesMaterias");

            migrationBuilder.CreateTable(
                name: "Coursesxstudents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coursesxstudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coursesxstudents_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coursesxstudents_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coursesxstudents_EstudianteId",
                table: "Coursesxstudents",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Coursesxstudents_MateriaId",
                table: "Coursesxstudents",
                column: "MateriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coursesxstudents");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRegistro",
                table: "EstudiantesMaterias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
