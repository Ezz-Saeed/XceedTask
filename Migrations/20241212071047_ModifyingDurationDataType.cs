using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XceedTask.Migrations
{
    /// <inheritdoc />
    public partial class ModifyingDurationDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "Products",
                type: "time",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
