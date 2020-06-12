using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KoncertManager.DAL.Migrations
{
    public partial class MySqlInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    FormedIn = table.Column<int>(nullable: false),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Concerts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    TicketsAvailable = table.Column<bool>(nullable: false),
                    VenueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Concerts_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConcertBand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConcertId = table.Column<int>(nullable: false),
                    BandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertBand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConcertBand_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConcertBand_Concerts_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bands",
                columns: new[] { "Id", "Country", "FormedIn", "Name" },
                values: new object[,]
                {
                    { 1, "Svédország", 1999, "Sabaton" },
                    { 2, "Finnország", 2015, "Beast in Black" },
                    { 3, "Svédország", 1993, "Hammerfall" },
                    { 4, "Németország", 2003, "Powerwolf" },
                    { 5, "Finnország", 1996, "Nightwish" },
                    { 6, "Magyarország", 1999, "Depresszió" }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Id", "Address", "Capacity", "Name" },
                values: new object[,]
                {
                    { 1, "1117 Budapest, Prielle Kornélia utca 4", 1045, "Barba Negra Music Club" },
                    { 2, "1117 Budapest, Neumann János u. 2", 6000, "Barba Negra Track" },
                    { 3, "1143 Budapest, Stefánia Út 2.", 12500, "Papp László Budapest Sportaréna" },
                    { 4, "1117 Budapest, Petőfi híd", 600, "A38 hajó" }
                });

            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "Id", "Date", "TicketsAvailable", "VenueId" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1 },
                    { 4, new DateTime(2020, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1 },
                    { 6, new DateTime(2020, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1 },
                    { 3, new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2 },
                    { 7, new DateTime(2021, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2 },
                    { 2, new DateTime(2020, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3 },
                    { 5, new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4 }
                });

            migrationBuilder.InsertData(
                table: "ConcertBand",
                columns: new[] { "Id", "BandId", "ConcertId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 6, 6, 4 },
                    { 7, 2, 4 },
                    { 10, 3, 6 },
                    { 11, 1, 6 },
                    { 4, 4, 3 },
                    { 5, 5, 3 },
                    { 3, 3, 2 },
                    { 8, 4, 5 },
                    { 9, 6, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConcertBand_BandId",
                table: "ConcertBand",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcertBand_ConcertId",
                table: "ConcertBand",
                column: "ConcertId");

            migrationBuilder.CreateIndex(
                name: "IX_Concerts_VenueId",
                table: "Concerts",
                column: "VenueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConcertBand");

            migrationBuilder.DropTable(
                name: "Bands");

            migrationBuilder.DropTable(
                name: "Concerts");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
