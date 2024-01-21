using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoXpress.Api.Identity.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionScope_Permissions_PermissionId",
                table: "PermissionScope");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionScope_Scopes_ScopesId",
                table: "PermissionScope");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetRoles_RoleId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionScope",
                table: "PermissionScope");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Permissions");

            migrationBuilder.RenameTable(
                name: "PermissionScope",
                newName: "PermissionScopes");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "PermissionScopes",
                newName: "PermissionsId");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionScope_ScopesId",
                table: "PermissionScopes",
                newName: "IX_PermissionScopes_ScopesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionScopes",
                table: "PermissionScopes",
                columns: new[] { "PermissionsId", "ScopesId" });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    PermissionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.PermissionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RolesId",
                table: "RolePermissions",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionScopes_Permissions_PermissionsId",
                table: "PermissionScopes",
                column: "PermissionsId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionScopes_Scopes_ScopesId",
                table: "PermissionScopes",
                column: "ScopesId",
                principalTable: "Scopes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionScopes_Permissions_PermissionsId",
                table: "PermissionScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionScopes_Scopes_ScopesId",
                table: "PermissionScopes");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionScopes",
                table: "PermissionScopes");

            migrationBuilder.RenameTable(
                name: "PermissionScopes",
                newName: "PermissionScope");

            migrationBuilder.RenameColumn(
                name: "PermissionsId",
                table: "PermissionScope",
                newName: "PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionScopes_ScopesId",
                table: "PermissionScope",
                newName: "IX_PermissionScope_ScopesId");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Permissions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionScope",
                table: "PermissionScope",
                columns: new[] { "PermissionId", "ScopesId" });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionScope_Permissions_PermissionId",
                table: "PermissionScope",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionScope_Scopes_ScopesId",
                table: "PermissionScope",
                column: "ScopesId",
                principalTable: "Scopes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetRoles_RoleId",
                table: "Permissions",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }
    }
}
