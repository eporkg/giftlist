using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gavelister.Data.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberBought",
                table: "Gift");

            migrationBuilder.DropColumn(
                name: "NumberRequested",
                table: "Gift");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Gift");

            migrationBuilder.AddColumn<int>(
                name: "AmountBought",
                table: "Gift",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AmountRequested",
                table: "Gift",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountBought",
                table: "Gift");

            migrationBuilder.DropColumn(
                name: "AmountRequested",
                table: "Gift");

            migrationBuilder.AddColumn<int>(
                name: "NumberBought",
                table: "Gift",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberRequested",
                table: "Gift",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Gift",
                nullable: true);
        }
    }
}
