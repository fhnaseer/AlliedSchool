
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/19/2013 17:34:28
-- Generated from EDMX file: D:\Puthay Kaam\GitHub\AlliedSchool\AlliedSchool\AlliedSchool\AlliedSchoolModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SchoolDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_StudentShoppingItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingItems] DROP CONSTRAINT [FK_StudentShoppingItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemShoppingItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingItems] DROP CONSTRAINT [FK_ItemShoppingItem];
GO
IF OBJECT_ID(N'[dbo].[FK_StandardStudent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_StandardStudent];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Students]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Students];
GO
IF OBJECT_ID(N'[dbo].[ShoppingItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShoppingItems];
GO
IF OBJECT_ID(N'[dbo].[Items]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Items];
GO
IF OBJECT_ID(N'[dbo].[Standards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Standards];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Students'
CREATE TABLE [dbo].[Students] (
    [StudentId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [StandardId] int  NOT NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [FatherName] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [FamilyID] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ShoppingItems'
CREATE TABLE [dbo].[ShoppingItems] (
    [ShoppingId] int IDENTITY(1,1) NOT NULL,
    [StudentId] int  NOT NULL,
    [ItemId] int  NOT NULL,
    [Quantity] bigint  NOT NULL,
    [Price] bigint  NOT NULL,
    [IsPaid] bit  NOT NULL
);
GO

-- Creating table 'Items'
CREATE TABLE [dbo].[Items] (
    [ItemId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] bigint  NOT NULL,
    [Quantity] bigint  NOT NULL
);
GO

-- Creating table 'Standards'
CREATE TABLE [dbo].[Standards] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ClassName] nvarchar(max)  NOT NULL,
    [SectionName] nvarchar(max)  NOT NULL,
    [FullName] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [StudentId] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [PK_Students]
    PRIMARY KEY CLUSTERED ([StudentId] ASC);
GO

-- Creating primary key on [ShoppingId] in table 'ShoppingItems'
ALTER TABLE [dbo].[ShoppingItems]
ADD CONSTRAINT [PK_ShoppingItems]
    PRIMARY KEY CLUSTERED ([ShoppingId] ASC);
GO

-- Creating primary key on [ItemId] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [PK_Items]
    PRIMARY KEY CLUSTERED ([ItemId] ASC);
GO

-- Creating primary key on [Id] in table 'Standards'
ALTER TABLE [dbo].[Standards]
ADD CONSTRAINT [PK_Standards]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StudentId] in table 'ShoppingItems'
ALTER TABLE [dbo].[ShoppingItems]
ADD CONSTRAINT [FK_StudentShoppingItem]
    FOREIGN KEY ([StudentId])
    REFERENCES [dbo].[Students]
        ([StudentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentShoppingItem'
CREATE INDEX [IX_FK_StudentShoppingItem]
ON [dbo].[ShoppingItems]
    ([StudentId]);
GO

-- Creating foreign key on [ItemId] in table 'ShoppingItems'
ALTER TABLE [dbo].[ShoppingItems]
ADD CONSTRAINT [FK_ItemShoppingItem]
    FOREIGN KEY ([ItemId])
    REFERENCES [dbo].[Items]
        ([ItemId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemShoppingItem'
CREATE INDEX [IX_FK_ItemShoppingItem]
ON [dbo].[ShoppingItems]
    ([ItemId]);
GO

-- Creating foreign key on [StandardId] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [FK_StandardStudent]
    FOREIGN KEY ([StandardId])
    REFERENCES [dbo].[Standards]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StandardStudent'
CREATE INDEX [IX_FK_StandardStudent]
ON [dbo].[Students]
    ([StandardId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------