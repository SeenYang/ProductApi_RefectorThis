﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ProductsApi.Models.Migrations
{
    public partial class AddProductsSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var product1Id = Guid.NewGuid();
            var product2Id = Guid.NewGuid();
            var product3Id = Guid.NewGuid();

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] {"Id", "Name", "Description", "Price", "DeliveryPrice"},
                values: new object[] { product1Id, "Headphone", "JBL noise cancelling headphone", 69.99, 10.00 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Description", "Price", "DeliveryPrice" },
                values: new object[] { product2Id, "Apple Watch", "Apple watch series 5", 599.00, 15.00 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Description", "Price", "DeliveryPrice" },
                values: new object[] { product3Id, "Razer Mouse", "Best gaming mouse", 89.99, 10.00 });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "Id", "Name", "Description", "ProductId" },
                values: new object[] { Guid.NewGuid(), "Color", "White", product1Id });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "Id", "Name", "Description", "ProductId" },
                values: new object[] { Guid.NewGuid(), "Color", "Black", product1Id });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "Id", "Name", "Description", "ProductId" },
                values: new object[] { Guid.NewGuid(), "Band", "Sport", product2Id });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "Id", "Name", "Description", "ProductId" },
                values: new object[] { Guid.NewGuid(), "Type", "Wireless", product3Id });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
