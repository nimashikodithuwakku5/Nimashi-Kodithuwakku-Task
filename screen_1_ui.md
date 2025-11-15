# SCREEN1_UI.md

# Screen 1 – Sales Order Entry
This screen is used to create or edit a sales order.

## Functional Requirements

### 1. Customer Name Dropdown
- Populate from Client table via API.
- When selected, auto-fill address fields.

### 2. Address Fields
- Address1
- Address2
- City
- These fields auto-fill but user can edit manually.

### 3. Item Table
Each row contains:
- Item Code (dropdown - from Item table)
- Description (dropdown - from Item table)
- Note (text)
- Quantity (number)
- Tax Rate (number %)
- Excl Amount (auto-calculated)
- Tax Amount (auto-calculated)
- Incl Amount (auto-calculated)

### 4. Calculations
```
Excl = Quantity * Price
Tax  = Excl * TaxRate / 100
Incl = Excl + Tax
```

### 5. Multiple Item Rows
- User can add several items.
- Each line calculates its total.

### 6. Save Order
- Save header + details to the database.
- Use API endpoint.

### 7. Optional Print Option
- Print each order using any reporting tool.

