﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplift.DataAccess.Data.Migrations
{
    public partial class addOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orderHeders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    ZipCode = table.Column<string>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    ServiceCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderHeders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderHeaderId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    ServiceName = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderDetails_orderHeders_OrderHeaderId",
                        column: x => x.OrderHeaderId,
                        principalTable: "orderHeders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderDetails_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_OrderHeaderId",
                table: "orderDetails",
                column: "OrderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_ServiceId",
                table: "orderDetails",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderDetails");

            migrationBuilder.DropTable(
                name: "orderHeders");
        }
    }
}
