using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetGuard.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceCheckInFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BatteryLevel",
                table: "Devices",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthMessage",
                table: "Devices",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Devices",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEncrypted",
                table: "Devices",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRootedOrJailbroken",
                table: "Devices",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsScreenLockEnabled",
                table: "Devices",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCheckInAt",
                table: "Devices",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatteryLevel",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "HealthMessage",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "IsEncrypted",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "IsRootedOrJailbroken",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "IsScreenLockEnabled",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "LastCheckInAt",
                table: "Devices");
        }
    }
}
