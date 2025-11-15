# SCREEN2_UI.md

# Screen 2 – Order List (Home Screen)
This is the main landing page showing a list of all sales orders.

## Functional Requirements

### 1. Order List Table
Display columns such as:
- Order ID
- Customer Name
- Order Date
- Total Amount (optional)

### 2. Load From API
- Fetch all orders from `/api/orders`.
- Display them in a clean table.

### 3. Add New Button
- Navigates to Screen 1 (Sales Order Entry).

### 4. Double-Click to Edit
- User can double-click a row to open that specific order.
- Loads existing order header + items.

### 5. Navigation
- Edit mode opens Screen 1 with pre-filled values.

### 6. Optional
- Search bar
- Filter by date
- Pagination

