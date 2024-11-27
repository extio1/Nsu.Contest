using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Nsu.Contest.Entity;
using Nsu.Contest.Util;

#nullable disable

namespace Nsu.Contest.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Points = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    EmployeeType = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContestJunior",
                columns: table => new
                {
                    ContestId = table.Column<Guid>(type: "uuid", nullable: false),
                    JuniorsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestJunior", x => new { x.ContestId, x.JuniorsId });
                    table.ForeignKey(
                        name: "FK_ContestJunior_Contest_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContestJunior_Employees_JuniorsId",
                        column: x => x.JuniorsId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContestTeamlead",
                columns: table => new
                {
                    ContestId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamleadsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestTeamlead", x => new { x.ContestId, x.TeamleadsId });
                    table.ForeignKey(
                        name: "FK_ContestTeamlead_Contest_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContestTeamlead_Employees_TeamleadsId",
                        column: x => x.TeamleadsId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamleadId = table.Column<int>(type: "integer", nullable: false),
                    JuniorId = table.Column<int>(type: "integer", nullable: false),
                    ContestId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Contest_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Team_Employees_JuniorId",
                        column: x => x.JuniorId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Team_Employees_TeamleadId",
                        column: x => x.TeamleadId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ForEmployeeId = table.Column<int>(type: "integer", nullable: false),
                    DesiredEmployees = table.Column<int[]>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlist_Employees_ForEmployeeId",
                        column: x => x.ForEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContestJunior_JuniorsId",
                table: "ContestJunior",
                column: "JuniorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestTeamlead_TeamleadsId",
                table: "ContestTeamlead",
                column: "TeamleadsId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_ContestId",
                table: "Team",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_JuniorId",
                table: "Team",
                column: "JuniorId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_TeamleadId",
                table: "Team",
                column: "TeamleadId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_ForEmployeeId",
                table: "Wishlist",
                column: "ForEmployeeId");

            EmployeeReader reader = new();
            var juniors = reader.ReadEmployees<Junior>("Res/Juniors20.csv");
            var teamleads = reader.ReadEmployees<Teamlead>("Res/Teamleads20.csv");
            Console.WriteLine(juniors);
            foreach (var j in juniors)
            {
                migrationBuilder.InsertData(
                    table: "Employees",
                    columns: new[] { "Id", "Name", "EmployeeType" },
                    values: new object[] { j.Id, j.Name, "Teamlead" }
                );
            }

            foreach (var t in teamleads)
            {
                migrationBuilder.InsertData(
                    table: "Employees",
                    columns: new[] { "Id", "Name", "EmployeeType" },
                    values: new object[] { t.Id, t.Name, "Teamlead" }
                );
            }
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContestJunior");

            migrationBuilder.DropTable(
                name: "ContestTeamlead");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.DropTable(
                name: "Contest");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
