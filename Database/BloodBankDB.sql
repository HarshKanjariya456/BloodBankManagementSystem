IF DB_ID(N'BloodBankDB') IS NULL
    CREATE DATABASE [BloodBankDB];
GO
USE [BloodBankDB];
GO

CREATE TABLE [dbo].[BloodRequests] (
    [Id] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_BloodRequests] PRIMARY KEY,
    [PatientName] nvarchar(max) NOT NULL,
    [HospitalName] nvarchar(max) NOT NULL,
    [BloodGroup] int NOT NULL,
    [UnitsRequired] int NOT NULL,
    [ContactNumber] nvarchar(max) NOT NULL,
    [RequestDate] datetime2 NOT NULL,
    [Status] int NOT NULL
);

CREATE TABLE [dbo].[BloodStocks] (
    [Id] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_BloodStocks] PRIMARY KEY,
    [BloodGroup] int NOT NULL,
    [Units] int NOT NULL,
    [LastUpdated] datetime2 NOT NULL
);

CREATE TABLE [dbo].[Donors] (
    [Id] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Donors] PRIMARY KEY,
    [FullName] nvarchar(100) NOT NULL,
    [Age] int NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [BloodGroup] int NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NULL,
    [City] nvarchar(max) NOT NULL,
    [LastDonationDate] datetime2 NULL
);

CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(450) NOT NULL CONSTRAINT [PK_AspNetRoles] PRIMARY KEY,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL
);

CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(450) NOT NULL CONSTRAINT [PK_AspNetUsers] PRIMARY KEY,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL
);

CREATE TABLE [dbo].[AspNetRoleClaims] (
    [Id] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles]([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles]([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]([RoleId]);
CREATE UNIQUE INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
CREATE INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]([UserId]);
CREATE INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]([UserId]);
CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]([RoleId]);
CREATE INDEX [EmailIndex] ON [dbo].[AspNetUsers]([NormalizedEmail]);
CREATE UNIQUE INDEX [UserNameIndex] ON [dbo].[AspNetUsers]([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

SET IDENTITY_INSERT [dbo].[BloodStocks] ON;
INSERT INTO [dbo].[BloodStocks] ([Id], [BloodGroup], [Units], [LastUpdated]) VALUES
(1, 1, 0, '2026-01-01T00:00:00'),
(2, 2, 0, '2026-01-01T00:00:00'),
(3, 3, 0, '2026-01-01T00:00:00'),
(4, 4, 0, '2026-01-01T00:00:00'),
(5, 5, 0, '2026-01-01T00:00:00'),
(6, 6, 0, '2026-01-01T00:00:00'),
(7, 7, 0, '2026-01-01T00:00:00'),
(8, 8, 0, '2026-01-01T00:00:00');
SET IDENTITY_INSERT [dbo].[BloodStocks] OFF;

CREATE TABLE [dbo].[__EFMigrationsHistory] (
    [MigrationId] nvarchar(150) NOT NULL,
    [ProductVersion] nvarchar(32) NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
);
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES
('20261107113113_InitialCreate', '10.0.14'),
('20261115063941_AddIdentity', '10.0.14');
GO
PRINT 'BloodBankDB schema created successfully.';
