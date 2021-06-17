using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BjBygg.Infrastructure.data.migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 45, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 12, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaderSettings",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    ConfirmTimesheetsMonthly = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MissionTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCommandStatuses",
                columns: table => new
                {
                    UserName = table.Column<string>(nullable: false),
                    CommandId = table.Column<string>(nullable: true),
                    HasSucceeded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCommandStatuses", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "EmployerUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: false),
                    EmployerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerUsers_Employers_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 12, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 400, nullable: true),
                    FileName = table.Column<string>(maxLength: 40, nullable: true),
                    Finished = table.Column<bool>(nullable: false, defaultValue: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    MissionTypeId = table.Column<string>(nullable: true),
                    EmployerId = table.Column<string>(nullable: true),
                    Position_Latitude = table.Column<double>(nullable: true),
                    Position_Longitude = table.Column<double>(nullable: true),
                    Position_IsExact = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Missions_Employers_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Missions_MissionTypes_MissionTypeId",
                        column: x => x.MissionTypeId,
                        principalTable: "MissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MissionDocuments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 45, nullable: false),
                    MissionId = table.Column<string>(nullable: false),
                    FileName = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionDocuments_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MissionImages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    MissionId = table.Column<string>(nullable: false),
                    FileName = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionImages_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MissionNotes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 75, nullable: true),
                    Content = table.Column<string>(maxLength: 400, nullable: false),
                    MissionId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionNotes_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Timesheets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: false),
                    MissionId = table.Column<string>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    TotalHours = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Comment = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timesheets_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerUsers_EmployerId",
                table: "EmployerUsers",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionDocuments_MissionId",
                table: "MissionDocuments",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionImages_MissionId",
                table: "MissionImages",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionNotes_MissionId",
                table: "MissionNotes",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_EmployerId",
                table: "Missions",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_MissionTypeId",
                table: "Missions",
                column: "MissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_MissionId",
                table: "Timesheets",
                column: "MissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerUsers");

            migrationBuilder.DropTable(
                name: "LeaderSettings");

            migrationBuilder.DropTable(
                name: "MissionDocuments");

            migrationBuilder.DropTable(
                name: "MissionImages");

            migrationBuilder.DropTable(
                name: "MissionNotes");

            migrationBuilder.DropTable(
                name: "Timesheets");

            migrationBuilder.DropTable(
                name: "UserCommandStatuses");

            migrationBuilder.DropTable(
                name: "Missions");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropTable(
                name: "MissionTypes");
        }
    }
}
