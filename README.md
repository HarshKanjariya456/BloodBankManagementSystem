# Blood Bank Management System

A web-based Blood Bank Management System developed as a CSE Component Mini Project for **.NET Technologies (01CE1523)**. The application is built with **ASP.NET Core MVC (.NET 10), C#, Entity Framework Core, SQL Server LocalDB, ASP.NET Core Identity, and Bootstrap 5**.

It supports administrator operations for donor management, blood inventory, and blood requests, along with public/user-side blood request submission and tracking.

## Project Features

### Administrator
- Admin login using ASP.NET Core Identity.
- Dashboard showing donor, blood-unit, and request statistics.
- Donor management: add, view, edit, delete, and search donors by city and blood group.
- Donor eligibility check using the 90-day donation interval.
- Record donation: update the donor's last-donation date and add a unit to the matching blood-group stock.
- Blood stock view and management for all eight blood groups, including low-stock/out-of-stock highlighting.
- Blood request management: review requests, approve or reject them, check available stock, and deduct stock when a request is approved.

### Public / User Side
- Public landing page.
- Submit blood requests without using the administrator area.
- Track a submitted request using the project's tracking feature.

## Technology Stack

| Component | Technology |
|---|---|
| Framework | ASP.NET Core MVC 10 |
| Language | C# 13 / C# |
| Front End | Razor Views, HTML5, CSS3, Bootstrap 5, FontAwesome |
| Data Access | Entity Framework Core 10 (Code-First Migrations) |
| Database | Microsoft SQL Server LocalDB |
| Authentication | ASP.NET Core Identity |
| IDE | Visual Studio 2022 |

## Repository / Project Structure

```text
BloodBankManagementSystem/
├── Controllers/          # MVC controllers
├── Models/               # Donor, request, stock and view models
├── Views/                 # Razor views
├── Data/                  # EF Core DbContext
├── Migrations/             # Entity Framework migrations
├── Database/
│   ├── BloodBankDB.sql    # SQL database creation script
│   └── README.txt         # Database script instructions
├── wwwroot/                # Static files and libraries
├── Program.cs              # Application configuration and admin seeding
├── appsettings.json        # LocalDB connection string
├── BloodBankManagementSystem.csproj
├── BloodBankManagementSystem.slnx
├── Project_Report.pdf
└── README.md
```

## Requirements

- Windows
- Visual Studio 2022 with the **ASP.NET and web development** workload
- .NET 10 SDK
- SQL Server LocalDB (normally installed with Visual Studio)
- Internet connection on first build, if NuGet packages need to be restored

## Run the Project in Visual Studio

### 1. Extract and open

1. Extract the project ZIP.
2. Open `BloodBankManagementSystem.slnx` in Visual Studio 2022.
3. Allow Visual Studio to restore NuGet packages.

### 2. Create / update the database using Entity Framework migrations

1. In Visual Studio, open **Tools → NuGet Package Manager → Package Manager Console**.
2. Ensure the project `BloodBankManagementSystem` is selected as the Default project in the Package Manager Console.
3. Run:

   ```powershell
   Update-Database
   ```

4. Wait for the migration command to complete successfully. The project uses the LocalDB connection configured in `appsettings.json`:

   ```text
   Server=(localdb)\MSSQLLocalDB;Database=BloodBankDB;Trusted_Connection=True;MultipleActiveResultSets=true
   ```

The migrations create/update the database schema, including Identity tables. The initial blood-stock records for the eight blood groups are included in the database setup.

### 3. Build and run

1. Build the solution (**Build → Build Solution**).
2. Select **IIS Express** (or the project launch profile available in Visual Studio).
3. Click the green **Run** button or press **F5**.
4. The application should open in your browser.

### 4. Administrator login

The application seeds an administrator account during startup if the account does not already exist:

| Field | Value |
|---|---|
| Email / Username | `admin@bloodbank.com` |
| Password | `Admin@123` |

Use these credentials to access the administrator features. Public/user-side features are available through the application’s public pages.

## Database Files

The project includes a SQL script at `Database/BloodBankDB.sql` as an alternative database setup/reference file.

**Recommended setup for the project:** use the Entity Framework migration command `Update-Database` in Visual Studio, as described above.

To use the SQL script instead:
1. Open SQL Server Management Studio (SSMS).
2. Connect to the appropriate SQL Server instance. For the project's configured LocalDB, use `(localdb)\MSSQLLocalDB` if your SSMS connection supports LocalDB.
3. Open `Database/BloodBankDB.sql` and execute it.
4. Confirm that `BloodBankDB` and its tables were created.

The supplied SQL script contains database schema/setup and initial blood-stock data; it does not contain real donor, request, or user records.

## Testing Checklist

- [ ] Application builds successfully.
- [ ] Database migrations complete successfully.
- [ ] Admin login works.
- [ ] Dashboard loads.
- [ ] Donor records can be added, edited, viewed, and deleted.
- [ ] Donor eligibility is checked.
- [ ] Donation recording updates the blood stock.
- [ ] Blood stock displays the eight blood groups.
- [ ] Public blood request submission works.
- [ ] Administrator can review and approve/reject requests.
- [ ] Request tracking works.
- [ ] Records remain in the database after application restart.

## Team Members

- Harsh Kanjariya
- Rajvirsinh Dodiya
- Arun Busa

## Academic Project

**Course:** .NET Technologies (01CE1523)  
**Project:** Blood Bank Management System  
**Department:** Computer Engineering  
**Faculty of Engineering & Technology, Marwadi University, Rajkot**

## Future Scope

Possible future enhancements include donation-history reporting, email/SMS notifications, concurrent request-handling improvements, and additional staff roles. These are future enhancements and are not claimed as current implemented features.
