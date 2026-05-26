-- ============================================================
-- AlJamal Billiard Hall - Database Schema
-- Run on: (localdb)\MSSQLLocalDB
-- ============================================================

IF DB_ID(N'BilliardDB') IS NULL
    CREATE DATABASE BilliardDB;
GO

USE BilliardDB;
GO

-- ------------------------------------------------------------
-- 1) أنواع الطاولات
-- ------------------------------------------------------------
IF OBJECT_ID(N'dbo.TableTypes', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TableTypes (
        TableTypeId   INT           NOT NULL PRIMARY KEY,
        TypeName      NVARCHAR(50)  NOT NULL,  -- Snooker / Black
        HourlyRate    DECIMAL(10,2) NOT NULL   -- سعر الساعة لهذا النوع
    );

    INSERT INTO dbo.TableTypes (TableTypeId, TypeName, HourlyRate) VALUES
        (1, N'Snooker', 5.00),   -- عدّل الأسعار حسب محلك
        (2, N'Black',   4.00);
END
GO

-- ------------------------------------------------------------
-- 2) الطاولات (6 سنوكر + 3 بلاك = 9)
-- ------------------------------------------------------------
IF OBJECT_ID(N'dbo.BilliardTables', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.BilliardTables (
        TableId       INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
        TableNumber   INT           NOT NULL,
        TableTypeId   INT           NOT NULL REFERENCES dbo.TableTypes(TableTypeId),
        DisplayName   NVARCHAR(50)  NOT NULL,
        IsActive      BIT           NOT NULL DEFAULT 1,
        CONSTRAINT UQ_BilliardTables_Number UNIQUE (TableNumber)
    );

    -- سنوكر 1-7
    INSERT INTO dbo.BilliardTables (TableNumber, TableTypeId, DisplayName) VALUES
        (1, 1, N'سنوكر 1'),
        (2, 1, N'سنوكر 2'),
        (3, 1, N'سنوكر 3'),
        (4, 1, N'سنوكر 4'),
        (5, 1, N'سنوكر 5'),
        (6, 1, N'سنوكر 6'),
        (7, 1, N'سنوكر 7');
    -- بلاك 1-3
    INSERT INTO dbo.BilliardTables (TableNumber, TableTypeId, DisplayName) VALUES
        (8, 2, N'بلاك 1'),
        (9, 2, N'بلاك 2'),
        (10, 2, N'بلاك 3');
END
GO

-- ------------------------------------------------------------
-- 3) قائمة المنتجات (مشروبات، شبس، ...)
-- ------------------------------------------------------------
IF OBJECT_ID(N'dbo.Products', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Products (
        ProductId     INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
        ProductName   NVARCHAR(100) NOT NULL,
        Category      NVARCHAR(50)  NULL,      -- مشروبات / وجبات / ...
        UnitPrice     DECIMAL(10,2) NOT NULL,
        IsActive      BIT           NOT NULL DEFAULT 1
    );

    INSERT INTO dbo.Products (ProductName, Category, UnitPrice) VALUES
        (N'ماء', N'مشروبات', 0.50),
        (N'بيبسي', N'مشروبات', 1.00),
        (N'عصير', N'مشروبات', 1.50),
        (N'شبس', N'وجبات', 1.00),
        (N'سندويش', N'وجبات', 2.50);
END
GO

-- ------------------------------------------------------------
-- 4) جلسة اللاعب (مؤقت لكل زبون على طاولة)
-- ------------------------------------------------------------
IF OBJECT_ID(N'dbo.PlayerSessions', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PlayerSessions (
        PlayerSessionId INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
        TableId         INT           NOT NULL REFERENCES dbo.BilliardTables(TableId),
        PlayerName      NVARCHAR(100) NULL,     -- اختياري: اسم أو رقم
        StartTime       DATETIME2     NOT NULL DEFAULT SYSDATETIME(),
        EndTime         DATETIME2     NULL,
        HourlyRate      DECIMAL(10,2) NOT NULL, -- نسخة من السعر وقت البدء
        IsActive        BIT           NOT NULL DEFAULT 1,
        IsInvoiced      BIT           NOT NULL DEFAULT 0
    );

    CREATE INDEX IX_PlayerSessions_Table_Active
        ON dbo.PlayerSessions (TableId, IsActive)
        WHERE IsActive = 1;
END
GO

-- ------------------------------------------------------------
-- 5) طلبات الزبون (مشروبات وغيرها) — مرتبطة باللاعب
-- ------------------------------------------------------------
IF OBJECT_ID(N'dbo.OrderItems', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.OrderItems (
        OrderItemId     INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
        PlayerSessionId INT           NOT NULL REFERENCES dbo.PlayerSessions(PlayerSessionId),
        ProductId       INT           NOT NULL REFERENCES dbo.Products(ProductId),
        Quantity        INT           NOT NULL DEFAULT 1,
        UnitPrice       DECIMAL(10,2) NOT NULL,  -- سعر وقت الإضافة
        AddedAt         DATETIME2     NOT NULL DEFAULT SYSDATETIME()
    );
END
GO

-- ------------------------------------------------------------
-- 6) الفاتورة (بعد إنهاء الزبون)
-- ------------------------------------------------------------
IF OBJECT_ID(N'dbo.Invoices', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Invoices (
        InvoiceId       INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
        PlayerSessionId INT           NOT NULL REFERENCES dbo.PlayerSessions(PlayerSessionId),
        PlayMinutes     INT           NOT NULL,
        PlayAmount      DECIMAL(10,2) NOT NULL,
        OrdersAmount    DECIMAL(10,2) NOT NULL,
        TotalAmount     DECIMAL(10,2) NOT NULL,
        CreatedAt       DATETIME2     NOT NULL DEFAULT SYSDATETIME(),
        PrintedAt       DATETIME2     NULL
    );
END
GO

-- ------------------------------------------------------------
-- 7) تفاصيل الفاتورة (للطباعة والأرشيف)
-- ------------------------------------------------------------
IF OBJECT_ID(N'dbo.InvoiceLines', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.InvoiceLines (
        InvoiceLineId INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
        InvoiceId     INT           NOT NULL REFERENCES dbo.Invoices(InvoiceId),
        LineType      NVARCHAR(20)  NOT NULL,  -- Time / Product
        Description   NVARCHAR(200) NOT NULL,
        Quantity      DECIMAL(10,2) NOT NULL,
        UnitPrice     DECIMAL(10,2) NOT NULL,
        LineTotal     DECIMAL(10,2) NOT NULL
    );
END
GO

-- ------------------------------------------------------------
-- إزالة الجدول القديم إن وُجد (من النسخة الأولى)
-- ------------------------------------------------------------
IF OBJECT_ID(N'dbo.Sessions', N'U') IS NOT NULL
    DROP TABLE dbo.Sessions;
GO
