using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interview_Test.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchemaFixPermissionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileTb_UserTb_Id",
                table: "UserProfileTb");

            migrationBuilder.DropIndex(
                name: "IX_UserProfileTb_Id",
                table: "UserProfileTb");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserProfileTb",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Permission",
                table: "PermissionTb",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileTb_Id",
                table: "UserProfileTb",
                column: "Id",
                unique: true,
                filter: "[Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileTb_UserTb_Id",
                table: "UserProfileTb",
                column: "Id",
                principalTable: "UserTb",
                principalColumn: "Id");

            // ========== Seed Master Data with Duplicate Check ==========
            
            // Seed Roles
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM RoleTb WHERE RoleId = 1)
                    INSERT INTO RoleTb (RoleId, RoleName) VALUES (1, 'pick operation');
                
                IF NOT EXISTS (SELECT 1 FROM RoleTb WHERE RoleId = 2)
                    INSERT INTO RoleTb (RoleId, RoleName) VALUES (2, 'pack operation');
                
                IF NOT EXISTS (SELECT 1 FROM RoleTb WHERE RoleId = 3)
                    INSERT INTO RoleTb (RoleId, RoleName) VALUES (3, 'document operation');
            ");

            // Seed Permissions
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM PermissionTb WHERE PermissionId = 1)
                    INSERT INTO PermissionTb (PermissionId, Permission) VALUES (1, '1-01-picking-info');
                
                IF NOT EXISTS (SELECT 1 FROM PermissionTb WHERE PermissionId = 2)
                    INSERT INTO PermissionTb (PermissionId, Permission) VALUES (2, '1-02-picking-start');
                
                IF NOT EXISTS (SELECT 1 FROM PermissionTb WHERE PermissionId = 3)
                    INSERT INTO PermissionTb (PermissionId, Permission) VALUES (3, '1-03-picking-confirm');
                
                IF NOT EXISTS (SELECT 1 FROM PermissionTb WHERE PermissionId = 4)
                    INSERT INTO PermissionTb (PermissionId, Permission) VALUES (4, '1-04-picking-report');
                
                IF NOT EXISTS (SELECT 1 FROM PermissionTb WHERE PermissionId = 5)
                    INSERT INTO PermissionTb (PermissionId, Permission) VALUES (5, '2-01-packing-info');
                
                IF NOT EXISTS (SELECT 1 FROM PermissionTb WHERE PermissionId = 6)
                    INSERT INTO PermissionTb (PermissionId, Permission) VALUES (6, '2-02-packing-start');
                
                IF NOT EXISTS (SELECT 1 FROM PermissionTb WHERE PermissionId = 7)
                    INSERT INTO PermissionTb (PermissionId, Permission) VALUES (7, '2-03-packing-confirm');
                
                IF NOT EXISTS (SELECT 1 FROM PermissionTb WHERE PermissionId = 8)
                    INSERT INTO PermissionTb (PermissionId, Permission) VALUES (8, '2-04-packing-report');
                
                IF NOT EXISTS (SELECT 1 FROM PermissionTb WHERE PermissionId = 9)
                    INSERT INTO PermissionTb (PermissionId, Permission) VALUES (9, '3-01-printing-label');
            ");

            // Seed Role-Permission Mappings
            migrationBuilder.Sql(@"
                -- Role 1: pick operation
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 1 AND PermissionId = 1)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (1, 1);
                
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 1 AND PermissionId = 2)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (1, 2);
                
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 1 AND PermissionId = 3)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (1, 3);
                
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 1 AND PermissionId = 4)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (1, 4);
                
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 1 AND PermissionId = 9)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (1, 9);
                
                -- Role 2: pack operation
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 2 AND PermissionId = 4)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (2, 4);
                
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 2 AND PermissionId = 5)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (2, 5);
                
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 2 AND PermissionId = 6)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (2, 6);
                
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 2 AND PermissionId = 7)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (2, 7);
                
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 2 AND PermissionId = 8)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (2, 8);
                
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 2 AND PermissionId = 9)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (2, 9);
                
                -- Role 3: document operation
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 3 AND PermissionId = 4)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (3, 4);
                
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 3 AND PermissionId = 8)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (3, 8);
                
                IF NOT EXISTS (SELECT 1 FROM RolePermissionTb WHERE RoleId = 3 AND PermissionId = 9)
                    INSERT INTO RolePermissionTb (RoleId, PermissionId) VALUES (3, 9);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileTb_UserTb_Id",
                table: "UserProfileTb");

            migrationBuilder.DropIndex(
                name: "IX_UserProfileTb_Id",
                table: "UserProfileTb");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserProfileTb",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Permission",
                table: "PermissionTb",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileTb_Id",
                table: "UserProfileTb",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileTb_UserTb_Id",
                table: "UserProfileTb",
                column: "Id",
                principalTable: "UserTb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
