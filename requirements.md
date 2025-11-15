# REQUIREMENTS.md

## Project Overview
Build a web application using:
- **Backend:** .NET Core Web API
- **Frontend:** React + Redux Toolkit
- **Database:** SQL Server

Includes two main screens:
- **Screen 1:** Sales Order Entry
- **Screen 2:** Home / Order List

Partial completion is acceptable.

## Backend Requirements
- Clean Architecture or Layered Architecture
- API Layer (Controllers, routing)
- Application Layer (Interfaces, Services)
- Domain Layer (Entities)
- Infrastructure Layer (DbContext, Repositories)
- Entity Framework Core
- AutoMapper (DTO mapping)
- Dependency Injection
- SQL Server Database

## Recommended Backend Structure
```
/ProjectName
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

## Frontend Requirements
- React functional components
- React Hooks
- Redux Toolkit
- React Router
- Axios
- Tailwind CSS

## Recommended Frontend Structure
```
/src
  /components
  /pages
  /redux
    /slices
    store.js
  /services
  /hooks
  /utils
```

