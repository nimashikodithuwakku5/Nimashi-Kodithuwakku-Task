-- Idempotent seed for Items and Clients for SPILSalesOrderDB
-- Usage: run in SSMS or with sqlcmd against your instance: sqlcmd -S localhost\SQLEXPRESS -i seed_items.sql

USE [SPILSalesOrderDB];
GO

-- Create Item table if it does not exist (safe guard for manual DBs)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Item' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Item (
        ItemId INT IDENTITY(1,1) PRIMARY KEY,
        ItemCode NVARCHAR(50) NOT NULL,
        Description NVARCHAR(200) NOT NULL,
        Price DECIMAL(18,2) NULL
    );
END
GO

-- Insert sample items (no duplicates)
IF NOT EXISTS (SELECT 1 FROM dbo.Item WHERE ItemCode = 'ITEM001')
    INSERT INTO dbo.Item (ItemCode, Description, Price) VALUES ('ITEM001','Widget A - Small', 9.99);
IF NOT EXISTS (SELECT 1 FROM dbo.Item WHERE ItemCode = 'ITEM002')
    INSERT INTO dbo.Item (ItemCode, Description, Price) VALUES ('ITEM002','Widget B - Large', 19.50);
IF NOT EXISTS (SELECT 1 FROM dbo.Item WHERE ItemCode = 'ITEM003')
    INSERT INTO dbo.Item (ItemCode, Description, Price) VALUES ('ITEM003','Gadget Pro', 49.99);
IF NOT EXISTS (SELECT 1 FROM dbo.Item WHERE ItemCode = 'ITEM004')
    INSERT INTO dbo.Item (ItemCode, Description, Price) VALUES ('ITEM004','Accessory Pack', 5.25);
GO

-- Optional: insert clients if they don't exist (EF seeding may already have inserted these)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Client' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Client (
        ClientId INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Address1 NVARCHAR(200) NULL,
        Address2 NVARCHAR(200) NULL,
        City NVARCHAR(100) NULL,
        Address3 NVARCHAR(200) NULL,
        Suburb NVARCHAR(100) NULL,
        State NVARCHAR(50) NULL,
        PostCode NVARCHAR(20) NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Client WHERE Name = 'Acme Corporation')
    INSERT INTO dbo.Client (Name, Address1, Address2, Address3, Suburb, City, State, PostCode) VALUES
    ('Acme Corporation','123 Industry Rd','Suite 10','Building A','Northbay','Metropolis','CA','90001');
IF NOT EXISTS (SELECT 1 FROM dbo.Client WHERE Name = 'Beta Traders')
    INSERT INTO dbo.Client (Name, Address1, Address2, Address3, Suburb, City, State, PostCode) VALUES
    ('Beta Traders','45 Market St','','','Central','Gotham','NY','10001');
IF NOT EXISTS (SELECT 1 FROM dbo.Client WHERE Name = 'Gamma Supplies')
    INSERT INTO dbo.Client (Name, Address1, Address2, Address3, Suburb, City, State, PostCode) VALUES
    ('Gamma Supplies','9 Commerce Ave','Floor 2','','Harbor','Coast City','FL','33010');
GO

PRINT 'Seed script finished.';
