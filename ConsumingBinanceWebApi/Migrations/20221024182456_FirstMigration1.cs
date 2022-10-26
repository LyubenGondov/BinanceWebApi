using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsumingBinanceWebApi.Migrations
{
    public partial class FirstMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Symbols",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventType = table.Column<string>(nullable: true),
                    EventTime = table.Column<long>(nullable: false),
                    Symbol = table.Column<string>(nullable: true),
                    AgregateTradeId = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    FirstTradeID = table.Column<long>(nullable: false),
                    LastTradeId = table.Column<long>(nullable: false),
                    TradeTime = table.Column<long>(nullable: false),
                    IsBuyer = table.Column<bool>(nullable: false),
                    Ignore = table.Column<bool>(nullable: false),
                    DateInsert = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbols", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Symbols");
        }
    }
}
