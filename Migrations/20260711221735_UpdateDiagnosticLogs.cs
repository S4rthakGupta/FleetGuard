using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetGuard.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiagnosticLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Event",
                table: "DiagnosticsLog");

            migrationBuilder.DropColumn(
                name: "LogLevel",
                table: "DiagnosticsLog");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "DiagnosticsLog",
                newName: "HealthMessage");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "DiagnosticsLog",
                newName: "CheckedInAt");

            migrationBuilder.AddColumn<int>(
                name: "BatteryLevel",
                table: "DiagnosticsLog",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "DiagnosticsLog",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatteryLevel",
                table: "DiagnosticsLog");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DiagnosticsLog");

            migrationBuilder.RenameColumn(
                name: "HealthMessage",
                table: "DiagnosticsLog",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "CheckedInAt",
                table: "DiagnosticsLog",
                newName: "Message");

            migrationBuilder.AddColumn<string>(
                name: "Event",
                table: "DiagnosticsLog",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogLevel",
                table: "DiagnosticsLog",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
