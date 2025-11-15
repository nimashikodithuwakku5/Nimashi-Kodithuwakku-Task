using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPILSalesOrder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderAndClientFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "OrderHeader",
                newName: "Suburb");

            migrationBuilder.AddColumn<string>(
                name: "Address3",
                table: "OrderHeader",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "OrderHeader",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "OrderHeader",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "OrderHeader",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "OrderHeader",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNo",
                table: "OrderHeader",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "OrderHeader",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderDetail",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address3",
                table: "Client",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Client",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Client",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suburb",
                table: "Client",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address3",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "ReferenceNo",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "State",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Address3",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Suburb",
                table: "Client");

            migrationBuilder.RenameColumn(
                name: "Suburb",
                table: "OrderHeader",
                newName: "City");
        }
    }
}
