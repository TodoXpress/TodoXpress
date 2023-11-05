using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoXpress.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class inital_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Calendar");

            migrationBuilder.CreateTable(
                name: "Color",
                schema: "Calendar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    A = table.Column<int>(type: "integer", nullable: false),
                    R = table.Column<int>(type: "integer", nullable: false),
                    G = table.Column<int>(type: "integer", nullable: false),
                    B = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SerialEvents",
                schema: "Calendar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Itterations = table.Column<int>(type: "integer", nullable: false),
                    EndOfSeries = table.Column<DateOnly>(type: "date", nullable: false),
                    MultiDaySerialEvent = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerialEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Calendar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                schema: "Calendar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ColorId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendars_Color_ColorId",
                        column: x => x.ColorId,
                        principalSchema: "Calendar",
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calendars_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "Calendar",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                schema: "Calendar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    MeetingUrl = table.Column<string>(type: "text", nullable: true),
                    IsFullDay = table.Column<bool>(type: "boolean", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ShowAs = table.Column<int>(type: "integer", nullable: false),
                    IsSerialEvent = table.Column<bool>(type: "boolean", nullable: false),
                    SerialEventId = table.Column<Guid>(type: "uuid", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CalendarId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalSchema: "Calendar",
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_SerialEvents_SerialEventId",
                        column: x => x.SerialEventId,
                        principalSchema: "Calendar",
                        principalTable: "SerialEvents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FileAttachments",
                schema: "Calendar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<byte[]>(type: "bytea", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileAttachments_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "Calendar",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_ColorId",
                schema: "Calendar",
                table: "Calendars",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_OwnerId",
                schema: "Calendar",
                table: "Calendars",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CalendarId",
                schema: "Calendar",
                table: "Events",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SerialEventId",
                schema: "Calendar",
                table: "Events",
                column: "SerialEventId");

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_EventId",
                schema: "Calendar",
                table: "FileAttachments",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileAttachments",
                schema: "Calendar");

            migrationBuilder.DropTable(
                name: "Events",
                schema: "Calendar");

            migrationBuilder.DropTable(
                name: "Calendars",
                schema: "Calendar");

            migrationBuilder.DropTable(
                name: "SerialEvents",
                schema: "Calendar");

            migrationBuilder.DropTable(
                name: "Color",
                schema: "Calendar");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Calendar");
        }
    }
}
