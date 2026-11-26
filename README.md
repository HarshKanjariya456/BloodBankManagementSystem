# Blood Bank Management System

[![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://codespaces.new/HarshKanjariya456/BloodBankManagementSystem)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-10.0-blue.svg)](https://docs.microsoft.com/ef/core/)

> 🚀 **1-Click Live Cloud Launch:** Click the **Open in GitHub Codespaces** button above to automatically launch the project, spin up the database, and run the live web app in your browser!

A web-based application built with ASP.NET Core MVC (.NET 10), C# and
Entity Framework Core to manage blood donors, blood inventory and blood requests.

## Features
- Admin login with ASP.NET Core Identity
- Dashboard with donor, stock and request summaries
- Donor management (CRUD, search by city and blood group)
- 90-day donation eligibility check
- Record Donation: updates donor date and adds 1 unit to stock
- Blood stock management with low-stock and out-of-stock highlights
- Blood requests with approve/reject; approval checks and deducts stock

## Team Members & Work Distribution

| Member | Role & Module | Responsibilities & Completed Work |
| :--- | :--- | :--- |
| **Harsh Kanjariya** | **System Architecture & Authentication** | • Scaffolding project and configuring Entity Framework Core with SQL Server LocalDB<br>• Implementing ASP.NET Core Identity for secure admin login and session management (`AccountController`, `LoginViewModel`)<br>• Designing and implementing Admin Dashboard with real-time summary statistics cards (`HomeController`, `DashboardViewModel`)<br>• Initial database migrations and seeding |
| **Rajvir Dodiya (Rajvir_dodiya)** | **Donor & Blood Stock Management** | • Complete Donor CRUD system (Create, Read, Update, Delete) with search by city and blood group (`DonorsController`, `Donor.cs`)<br>• 90-day donation eligibility validation business logic<br>• **Record Donation** workflow: automatically updates donor's last donation date and increments corresponding blood stock by 1 unit<br>• Blood Stock Inventory management with real-time stock levels and low-stock warning indicators (`BloodStockController`, `BloodStock.cs`) |
| **Arun Busa (arunbusa06)** | **Blood Requests & Public Tracking Portal** | • Blood Request submission models, validation, and listing (`BloodRequestsController`, `BloodRequest.cs`)<br>• Request Approval & Rejection engine with automatic stock availability check and inventory deduction<br>• Public-facing Blood Request form and online request tracking portal (`Landing.cshtml`, `Track.cshtml`, `Submitted.cshtml`)<br>• Responsive Bootstrap 5 theme design, custom navbar navigation, and UI polish (`site.css`, `_Layout.cshtml`) |

## Technologies
- ASP.NET Core MVC, C#
- Entity Framework Core, SQL Server LocalDB
- ASP.NET Core Identity
- Bootstrap 5

## How to run

### Option A: 1-Click Live in GitHub (Zero Setup)
Click **[![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://codespaces.new/HarshKanjariya456/BloodBankManagementSystem)** — GitHub automatically configures the environment, restores dependencies, auto-migrates the database, and launches the live web app in your browser!

### Option B: Run Locally
1. Clone the repository and open the solution in Visual Studio or VS Code.
2. Press **F5** (or run `dotnet run`). The database, seed data, and admin account are **automatically created and migrated on first launch**!
3. Default Admin Login: `admin@bloodbank.com` / `Admin@123`

## Project Viva & Defense Guide
A complete 3-member technical viva examination question and answer guide is included in:
📄 **[BloodBankManagementSystem_Viva_Preparation_Guide.pdf](BloodBankManagementSystem_Viva_Preparation_Guide.pdf)** (covering detailed questions for Harsh, Rajvir, and Arun).

## Future scope
- Donation history table
- Email/SMS notifications
- Concurrency handling for simultaneous approvals
- Role-based access (staff vs admin)
