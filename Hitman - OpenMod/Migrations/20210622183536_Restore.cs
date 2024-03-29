﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hitman.Migrations
{
    public partial class Restore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "Hits");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimePlaced",
                table: "Hits",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "TargetPlayerId",
                table: "Hits",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "HirerPlayerId",
                table: "Hits",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "Bounty",
                table: "Hits",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hits",
                table: "Hits",
                column: "HitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Hits",
                table: "Hits");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimePlaced",
                table: "Hits",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "TargetPlayerId",
                table: "Hits",
                type: "text",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "HirerPlayerId",
                table: "Hits",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Bounty",
                table: "Hits",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "Hits",
                column: "HitId");
        }
    }
}
