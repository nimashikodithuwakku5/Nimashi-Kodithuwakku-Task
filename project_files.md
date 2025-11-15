# SPIL Labs Sales Order Web App Project

This document consolidates all project details, instructions, and folder structures for the Sales Order web application assignment.

---

## 1. Backend Architecture (.NET Core)

### 1.1 Recommended Architecture
- **Layered / Clean Architecture**
- Core concerns:
  - **API Layer** – Controllers, routing, authentication.
  - **Business Logic Layer (Services)** – Core application logic.
  - **Data Access Layer** – Repository + Entity Framework Core.
  - **Domain Layer** – Entities and interfaces.

### 1.2 Tech Stack
- .NET Core Web API
- Entity Framework Core
- SQL Server (Express recommended)
- DTOs + AutoMapper
- Dependency Injection

### 1.3 Folder Structure
```
/SPILSalesOrder
  /API
    /Controllers
    /Models (DTOs)
  /Application
    /Interfaces
    /Services
  /Domain
    /Entities
  /Infrastructure
    /Data
    /Repositories
```

### 1.4 Domain Entities
- Client
- Item
- SalesOrder
- SalesOrderItem

### 1.5 API Endpoints
- GET /api/client → list clients
- GET /api/item → list items
- POST /api/salesorder → save order
- GET /api/salesorder → list orders

---

## 2. Frontend Architecture (React)

### 2.1 Tech Stack
- React (Functional Components + Hooks)
- Redux Toolkit (state management)
- React Router (navigation)
- Axios (API communication)
- Tailwind CSS (styling)

### 2.2 Folder Structure
```
/src
  /components
  /pages
  /redux
    store.js
  /slices
  /services
  /hooks
  /utils
```

### 2.3 Key Pages & Components
- **Screen 1: Sales Order Page**
  - Customer dropdown (auto-fill addresses)
  - Item code/description dropdown
  - Table for multiple items
  - ExclAmount, TaxAmount, InclAmount calculations
  - Save order button

- **Screen 2: Home Page**
  - List of added orders (grid/table)
  - Add New button → opens Screen 1
  - Edit order option
  - Double-click to view details

### 2.4 Tailwind Utilities
- Layout: flex, grid, gap-*, p-*, m-*
- Typography: text-sm, text-lg, font-semibold
- Colors: bg-gray-100, bg-white, text-gray-700, border-gray-300
- Reusable components: Form controls, Table/Grid

---

## 3. Installation & Setup Instructions

### 3.1 Backend
1. Install **.NET 8 SDK**: https://dotnet.microsoft.com/en-us/download/dotnet/8.0
2. Install **SQL Server Express**: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
3. Install **SSMS (SQL Server Management Studio)**: https://learn.microsoft.com/en-us/ssms/install/install
4. Open VS Code → C# extension for IntelliSense
5. Create Web API project:
```
mkdir SPILSalesOrder
cd SPILSalesOrder
dotnet new webapi -n SPILSalesOrder.API
```

### 3.2 Frontend
1. Install **Node.js**: https://nodejs.org/
2. Create React project:
```
npx create-react-app frontend
cd frontend
npm install @reduxjs/toolkit react-redux axios react-router-dom
npm install -D tailwindcss
npx tailwindcss init
```
3. Setup Redux slices and Axios services
4. Create components/pages as per Screen 1 and Screen 2

---

## 4. Important Notes
- Assignment time: 2 days
- They expect solution **up to the stage completed**
- Focus on:
  - Correct folder structure
  - Basic working API + frontend connection
  - CRUD functionality
- Full perfection not required (authentication, full validation, fancy printing are optional)

---

## 5. Helpful Links
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SSMS](https://learn.microsoft.com/en-us/ssms/install/install)
- [Node.js](https://nodejs.org/)

---

## 6. Next Steps
- Install all required software
- Setup backend project and database
- Setup frontend project
- Build Screen 1 → add order
- Build Screen 2 → list orders and edit
- Connect frontend → backend via Axios
- Test functionality and push to GitHub

