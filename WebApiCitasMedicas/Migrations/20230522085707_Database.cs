using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiCitasMedicas.Migrations
{
    public partial class Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Especialidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicos_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimeinto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacientes_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Citas_Medicos_MedicosId",
                        column: x => x.MedicosId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estadisticas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalConsultas = table.Column<int>(type: "int", nullable: false),
                    TotalPacientes = table.Column<int>(type: "int", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estadisticas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estadisticas_Medicos_MedicosId",
                        column: x => x.MedicosId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InformacionMedica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Peso = table.Column<float>(type: "real", nullable: false),
                    Altura = table.Column<float>(type: "real", nullable: false),
                    Alergias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enfermedades = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaAtualizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    MedicosId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacionMedica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformacionMedica_Medicos_MedicosId",
                        column: x => x.MedicosId,
                        principalTable: "Medicos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InformacionMedica_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CitasAgendadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitasId = table.Column<int>(type: "int", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitasAgendadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CitasAgendadas_Citas_CitasId",
                        column: x => x.CitasId,
                        principalTable: "Citas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CitasAgendadas_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReporteConsulta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tratamiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Receta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitasAgendadasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReporteConsulta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReporteConsulta_CitasAgendadas_CitasAgendadasId",
                        column: x => x.CitasAgendadasId,
                        principalTable: "CitasAgendadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citas_MedicosId",
                table: "Citas",
                column: "MedicosId");

            migrationBuilder.CreateIndex(
                name: "IX_CitasAgendadas_CitasId",
                table: "CitasAgendadas",
                column: "CitasId");

            migrationBuilder.CreateIndex(
                name: "IX_CitasAgendadas_PacienteId",
                table: "CitasAgendadas",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Estadisticas_MedicosId",
                table: "Estadisticas",
                column: "MedicosId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionMedica_MedicosId",
                table: "InformacionMedica",
                column: "MedicosId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionMedica_PacienteId",
                table: "InformacionMedica",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_UsuarioId",
                table: "Medicos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_UsuarioId",
                table: "Pacientes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ReporteConsulta_CitasAgendadasId",
                table: "ReporteConsulta",
                column: "CitasAgendadasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estadisticas");

            migrationBuilder.DropTable(
                name: "InformacionMedica");

            migrationBuilder.DropTable(
                name: "ReporteConsulta");

            migrationBuilder.DropTable(
                name: "CitasAgendadas");

            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Medicos");
        }
    }
}
