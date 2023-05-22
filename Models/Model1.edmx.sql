
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/20/2023 16:31:43
-- Generated from EDMX file: D:\Help\WebScraping_Project\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FashionEase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderDetails_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_Order];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetails_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Color_Color]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Color] DROP CONSTRAINT [FK_Product_Color_Color];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Color_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Color] DROP CONSTRAINT [FK_Product_Color_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_Review_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Review] DROP CONSTRAINT [FK_Review_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_User_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Role];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Color]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Color];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO
IF OBJECT_ID(N'[dbo].[OrderDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetails];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[Product_Color]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product_Color];
GO
IF OBJECT_ID(N'[dbo].[Review]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Review];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[Submissions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Submissions];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Colors'
CREATE TABLE [dbo].[Colors] (
    [ColorID] int  NOT NULL,
    [name] varchar(max)  NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [UpdatedOn] datetime  NULL,
    [CreatedBy] bigint  NULL,
    [CreatedOn] datetime  NULL,
    [UpdatedBy] bigint  NULL,
    [IsActive] bit  NULL,
    [BillingAddress] varchar(50)  NULL,
    [ShipppingAddress] varchar(50)  NULL,
    [Email] varchar(50)  NULL,
    [Phone] varchar(50)  NULL,
    [Name] varchar(50)  NULL,
    [Amount] float  NULL,
    [UpdateShipping] nchar(10)  NULL,
    [OrderStatus] varchar(50)  NULL
);
GO

-- Creating table 'OrderDetails'
CREATE TABLE [dbo].[OrderDetails] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [OrderId] bigint  NULL,
    [ProductId] bigint  NULL,
    [Quantity] float  NULL,
    [CreatedOn] datetime  NULL,
    [CreatedBy] int  NULL,
    [UpdatedBy] int  NULL,
    [UpdatedOn] datetime  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Description] varchar(max)  NULL,
    [Price] float  NULL,
    [Quantity] float  NULL,
    [Image] varchar(max)  NULL,
    [Image2] varchar(max)  NULL,
    [Image3] varchar(max)  NULL,
    [Image4] varchar(max)  NULL,
    [CreatedOn] datetime  NULL,
    [CreatedBy] int  NULL,
    [UpdatedBy] int  NULL,
    [UpdatedOn] datetime  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'Product_Color'
CREATE TABLE [dbo].[Product_Color] (
    [ProductColorId] int IDENTITY(1,1) NOT NULL,
    [ProductId] bigint  NULL,
    [ColorId] int  NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(max)  NULL
);
GO

-- Creating table 'Submissions'
CREATE TABLE [dbo].[Submissions] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [Name] varchar(max)  NULL,
    [Email] varchar(max)  NULL,
    [Phone] bigint  NULL,
    [Subject] varchar(max)  NULL,
    [Message] varchar(max)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] varchar(max)  NULL,
    [Email] varchar(max)  NULL,
    [Contact] varchar(max)  NULL,
    [Address] varchar(max)  NULL,
    [IsActive] bit  NULL,
    [CreatedBy] bigint  NULL,
    [CreatedOn] datetime  NULL,
    [UpdatedBy] bigint  NULL,
    [UpdatedOn] datetime  NULL,
    [RoleId] int  NULL,
    [Password] varchar(max)  NULL,
    [ShippingAddress] nchar(10)  NULL
);
GO

-- Creating table 'Reviews'
CREATE TABLE [dbo].[Reviews] (
    [Id] bigint  NOT NULL,
    [ReviewerName] varchar(max)  NULL,
    [ReviewText] varchar(max)  NULL,
    [ReviewSubject] varchar(max)  NULL,
    [ReviewDate] datetime  NULL,
    [Ratings] int  NULL,
    [ProductId] bigint  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ColorID] in table 'Colors'
ALTER TABLE [dbo].[Colors]
ADD CONSTRAINT [PK_Colors]
    PRIMARY KEY CLUSTERED ([ColorID] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [PK_OrderDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ProductColorId] in table 'Product_Color'
ALTER TABLE [dbo].[Product_Color]
ADD CONSTRAINT [PK_Product_Color]
    PRIMARY KEY CLUSTERED ([ProductColorId] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'Submissions'
ALTER TABLE [dbo].[Submissions]
ADD CONSTRAINT [PK_Submissions]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [PK_Reviews]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ColorId] in table 'Product_Color'
ALTER TABLE [dbo].[Product_Color]
ADD CONSTRAINT [FK_Product_Color_Color]
    FOREIGN KEY ([ColorId])
    REFERENCES [dbo].[Colors]
        ([ColorID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Color_Color'
CREATE INDEX [IX_FK_Product_Color_Color]
ON [dbo].[Product_Color]
    ([ColorId]);
GO

-- Creating foreign key on [OrderId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetails_Order]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetails_Order'
CREATE INDEX [IX_FK_OrderDetails_Order]
ON [dbo].[OrderDetails]
    ([OrderId]);
GO

-- Creating foreign key on [ProductId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetails_Product]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetails_Product'
CREATE INDEX [IX_FK_OrderDetails_Product]
ON [dbo].[OrderDetails]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'Product_Color'
ALTER TABLE [dbo].[Product_Color]
ADD CONSTRAINT [FK_Product_Color_Product]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Color_Product'
CREATE INDEX [IX_FK_Product_Color_Product]
ON [dbo].[Product_Color]
    ([ProductId]);
GO

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_User_Role]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_Role'
CREATE INDEX [IX_FK_User_Role]
ON [dbo].[Users]
    ([RoleId]);
GO

-- Creating foreign key on [ProductId] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [FK_Review_Product]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Review_Product'
CREATE INDEX [IX_FK_Review_Product]
ON [dbo].[Reviews]
    ([ProductId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------