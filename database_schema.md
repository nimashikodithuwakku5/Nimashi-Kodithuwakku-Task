# DATABASE_SCHEMA.md

# Database Schema
This schema supports the Sales Order system.

---
# 1. Client Table
Stores customer information.

## Columns
- **ClientId** (int, PK, Identity)
- **Name** (varchar(100), NOT NULL)
- **Address1** (varchar(200))
- **Address2** (varchar(200))
- **City** (varchar(100))

---
# 2. Item Table
Stores available items for ordering.

## Columns
- **ItemId** (int, PK, Identity)
- **ItemCode** (varchar(50), NOT NULL)
- **Description** (varchar(200), NOT NULL)
- **Price** (decimal(18,2), NOT NULL)

---
# 3. OrderHeader Table
Stores main order-level info.

## Columns
- **OrderId** (int, PK, Identity)
- **ClientId** (int, FK → Client.ClientId)
- **OrderDate** (datetime, NOT NULL)
- **Address1** (varchar(200))
- **Address2** (varchar(200))
- **City** (varchar(100))

---
# 4. OrderDetail Table
Stores item-level details.

## Columns
- **OrderDetailId** (int, PK, Identity)
- **OrderId** (int, FK → OrderHeader.OrderId)
- **ItemId** (int, FK → Item.ItemId)
- **Description** (varchar(200))
- **Note** (varchar(300))
- **Quantity** (decimal(18,2))
- **TaxRate** (decimal(5,2))
- **ExclAmount** (decimal(18,2))
- **TaxAmount** (decimal(18,2))
- **InclAmount** (decimal(18,2))

---
# Relationships
- **Client 1 → Many OrderHeader**
- **OrderHeader 1 → Many OrderDetail**
- **Item 1 → Many OrderDetail**

---
# SQL Create Script (Optional)
```sql
CREATE TABLE Client (
    ClientId INT PRIMARY KEY IDENTITY,
    Name VARCHAR(100) NOT NULL,
    Address1 VARCHAR(200),
    Address2 VARCHAR(200),
    City VARCHAR(100)
);

CREATE TABLE Item (
    ItemId INT PRIMARY KEY IDENTITY,
    ItemCode VARCHAR(50) NOT NULL,
    Description VARCHAR(200) NOT NULL,
    Price DECIMAL(18,2) NOT NULL
);

CREATE TABLE OrderHeader (
    OrderId INT PRIMARY KEY IDENTITY,
    ClientId INT FOREIGN KEY REFERENCES Client(ClientId),
    OrderDate DATETIME NOT NULL,
    Address1 VARCHAR(200),
    Address2 VARCHAR(200),
    City VARCHAR(100)
);

CREATE TABLE OrderDetail (
    OrderDetailId INT PRIMARY KEY IDENTITY,
    OrderId INT FOREIGN KEY REFERENCES OrderHeader(OrderId),
    ItemId INT FOREIGN KEY REFERENCES Item(ItemId),
    Description VARCHAR(200),
    Note VARCHAR(300),
    Quantity DECIMAL(18,2),
    TaxRate DECIMAL(5,2),
    ExclAmount DECIMAL(18,2),
    TaxAmount DECIMAL(18,2),
    InclAmount DECIMAL(18,2)
);
```

