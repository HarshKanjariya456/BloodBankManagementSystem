# Blood Bank Management System

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

## Technologies
- ASP.NET Core MVC, C#
- Entity Framework Core, SQL Server LocalDB
- ASP.NET Core Identity
- Bootstrap 5

## How to run
1. Clone the repository and open the solution in Visual Studio.
2. Open Package Manager Console and run `Update-Database`.
3. Press F5.
4. Login: `admin@bloodbank.com` / `Admin@123`

## Future scope
- Donation history table
- Email/SMS notifications
- Concurrency handling for simultaneous approvals
- Role-based access (staff vs admin)
