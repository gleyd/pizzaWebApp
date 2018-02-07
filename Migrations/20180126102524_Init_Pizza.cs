using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAppPizza.Migrations
{
    public partial class Init_Pizza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Command",
                columns: table => new
                {
                    IdCommand = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommandDate = table.Column<DateTime>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Command", x => x.IdCommand);
                });

            migrationBuilder.CreateTable(
                name: "Pizza",
                columns: table => new
                {
                    IdPizza = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    PriceHT = table.Column<decimal>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizza", x => x.IdPizza);
                });

            migrationBuilder.CreateTable(
                name: "DetailCommand",
                columns: table => new
                {
                    IdDetailCommand = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCommand = table.Column<int>(nullable: false),
                    IdPizza = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailCommand", x => x.IdDetailCommand);
                    table.ForeignKey(
                        name: "FK_DetailCommand_Command_IdPizza",
                        column: x => x.IdPizza,
                        principalTable: "Command",
                        principalColumn: "IdCommand",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailCommand_Pizza_IdPizza",
                        column: x => x.IdPizza,
                        principalTable: "Pizza",
                        principalColumn: "IdPizza",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetailCommand_IdPizza",
                table: "DetailCommand",
                column: "IdPizza");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailCommand");

            migrationBuilder.DropTable(
                name: "Command");

            migrationBuilder.DropTable(
                name: "Pizza");
        }
    }
}
