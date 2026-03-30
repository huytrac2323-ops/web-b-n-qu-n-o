using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomersTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Safely rename existing product columns if they exist (avoid ambiguous sp_rename errors)
            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.columns c JOIN sys.objects o ON c.object_id = o.object_id WHERE o.object_id = OBJECT_ID(N'[dbo].[Products]') AND c.name = 'Name')
    EXEC sp_rename N'dbo.Products.Name', N'ProductName', N'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns c JOIN sys.objects o ON c.object_id = o.object_id WHERE o.object_id = OBJECT_ID(N'[dbo].[Products]') AND c.name = 'Id')
    EXEC sp_rename N'dbo.Products.Id', N'ProductID', N'COLUMN';
" );

            // Add product columns only if they do not already exist to avoid errors on databases with those columns
            migrationBuilder.Sql(@"
IF COL_LENGTH('dbo.Products','BaseUnitID') IS NULL
    ALTER TABLE [Products] ADD [BaseUnitID] bigint NOT NULL DEFAULT(0);
IF COL_LENGTH('dbo.Products','CategoryID') IS NULL
    ALTER TABLE [Products] ADD [CategoryID] bigint NULL;
IF COL_LENGTH('dbo.Products','CompanyID') IS NULL
    ALTER TABLE [Products] ADD [CompanyID] bigint NOT NULL DEFAULT(0);
IF COL_LENGTH('dbo.Products','CreatedAt') IS NULL
    ALTER TABLE [Products] ADD [CreatedAt] datetime2 NOT NULL DEFAULT(GETDATE());
IF COL_LENGTH('dbo.Products','DefaultTaxRateID') IS NULL
    ALTER TABLE [Products] ADD [DefaultTaxRateID] bigint NULL;
IF COL_LENGTH('dbo.Products','IsActive') IS NULL
    ALTER TABLE [Products] ADD [IsActive] bit NOT NULL DEFAULT(0);
IF COL_LENGTH('dbo.Products','IsDeleted') IS NULL
    ALTER TABLE [Products] ADD [IsDeleted] bit NOT NULL DEFAULT(0);
IF COL_LENGTH('dbo.Products','ProductCode') IS NULL
    ALTER TABLE [Products] ADD [ProductCode] varchar(max) NULL;
IF COL_LENGTH('dbo.Products','ProductTypeID') IS NULL
    ALTER TABLE [Products] ADD [ProductTypeID] bigint NOT NULL DEFAULT(0);
" );

            // Create CustomerGroups table if it does not exist
            migrationBuilder.Sql(@"
IF OBJECT_ID('dbo.CustomerGroups','U') IS NULL
BEGIN
    CREATE TABLE [CustomerGroups] (
        [CustomerGroupId] bigint NOT NULL IDENTITY,
        [Name] nvarchar(200) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_CustomerGroups] PRIMARY KEY ([CustomerGroupId])
    );
END
");

            // Create Customers table if it does not exist
            migrationBuilder.Sql(@"
IF OBJECT_ID('dbo.Customers','U') IS NULL
BEGIN
    CREATE TABLE [Customers] (
        [CustomerId] bigint NOT NULL IDENTITY,
        [Name] nvarchar(200) NOT NULL,
        [Email] nvarchar(200) NOT NULL,
        [Phone] nvarchar(50) NOT NULL,
        [CustomerGroupId] bigint NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId])
    );
    IF OBJECT_ID('dbo.CustomerGroups','U') IS NOT NULL
    BEGIN
        ALTER TABLE [Customers] ADD CONSTRAINT [FK_Customers_CustomerGroups_CustomerGroupId] FOREIGN KEY([CustomerGroupId]) REFERENCES [CustomerGroups]([CustomerGroupId]) ON DELETE SET NULL;
    END
END
");

            // Create CustomerAddresses table if it does not exist
            migrationBuilder.Sql(@"
IF OBJECT_ID('dbo.CustomerAddresses','U') IS NULL
BEGIN
    CREATE TABLE [CustomerAddresses] (
        [CustomerAddressId] bigint NOT NULL IDENTITY,
        [CustomerId] bigint NOT NULL,
        [AddressLine] nvarchar(500) NOT NULL,
        [City] nvarchar(200) NOT NULL,
        [State] nvarchar(200) NOT NULL,
        [PostalCode] nvarchar(50) NOT NULL,
        [Country] nvarchar(200) NOT NULL,
        [IsPrimary] bit NOT NULL,
        CONSTRAINT [PK_CustomerAddresses] PRIMARY KEY ([CustomerAddressId])
    );
    IF OBJECT_ID('dbo.Customers','U') IS NOT NULL
    BEGIN
        ALTER TABLE [CustomerAddresses] ADD CONSTRAINT [FK_CustomerAddresses_Customers_CustomerId] FOREIGN KEY([CustomerId]) REFERENCES [Customers]([CustomerId]) ON DELETE CASCADE;
    END
END
");

            // Create indexes if not exist
            migrationBuilder.Sql(@"
IF OBJECT_ID('dbo.CustomerAddresses','U') IS NOT NULL AND NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_CustomerAddresses_CustomerId' AND object_id = OBJECT_ID('dbo.CustomerAddresses'))
    CREATE INDEX [IX_CustomerAddresses_CustomerId] ON [CustomerAddresses]([CustomerId]);
IF OBJECT_ID('dbo.Customers','U') IS NOT NULL AND NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Customers_CustomerGroupId' AND object_id = OBJECT_ID('dbo.Customers'))
    CREATE INDEX [IX_Customers_CustomerGroupId] ON [Customers]([CustomerGroupId]);
" );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAddresses");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CustomerGroups");

            migrationBuilder.DropColumn(
                name: "BaseUnitID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DefaultTaxRateID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypeID",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Products",
                newName: "Id");
        }
    }
}
