
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/29/2020 20:54:05
-- Generated from EDMX file: D:\Data\Documents\Workspaces\IFRS\IFRS\IFRS.DAL\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [data];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int  NOT NULL,
    [Name] varchar(128)  NULL,
    [BranchId] nchar(10)  NULL,
    [BranchCode] nchar(10)  NULL,
    [CapitalOSLKR] nchar(10)  NULL,
    [StatusCode] nchar(10)  NULL,
    [TotalPresentValue] nchar(10)  NULL,
    [ImpairementAmount] nchar(10)  null,
    [IimpairementStatus] nchar(10)  null,
    [EntryUserId] nchar(10)  null,
    [AsAtDate] datetime  null,
    [DelFlag] nchar(10)  null,
    [RelationshipManager] nchar(10)  null,
    [Verify_1] nchar(10)  null,
    [Verify_2] nchar(10)  null,
    [Verify_3] nchar(10)  null,
    [Verify_4] nchar(10)  null,
    [q1] nchar(10)  NULL,
    [q2] nchar(10)  NULL,
    [q3] nchar(10)  NULL,
    [q4] nchar(10)  NULL,
    [q5] nchar(10)  NULL,
    [q6] nchar(10)  NULL,
    [q7] nchar(10)  NULL,
    [q8] nchar(10)  NULL,
    [q9] nchar(10)  NULL,
    [q10] nchar(10)  NULL,
    [q11] nchar(10)  NULL,
    [q12] nchar(10)  NULL,
    [q13] nchar(10)  NULL,
    [q14] nchar(10)  NULL,
    [q15] nchar(10)  NULL,
    [q16] nchar(10)  NULL,
    [q17] nchar(10)  NULL,
    [q18] nchar(10)  NULL,
    [q19] nchar(10)  NULL,
    [q20] nchar(10)  NULL,
    [q21] nchar(10)  NULL,
    [q22] nchar(10)  NULL,
    [q23] nchar(10)  NULL,
    [q24] nchar(10)  NULL,
    [q25] nchar(10)  NULL,
    [q26] nchar(10)  NULL,
    [q27] nchar(10)  NULL,
    [q28] nchar(10)  NULL,
    [q29] nchar(10)  NULL,
    [q30] nchar(10)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [NameWithInitials] nvarchar(max)  NOT NULL,
    [DepartmentId] nvarchar(max)  NOT NULL,
    [DepartmentName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [ReportingId] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------