-- ترقية / إصلاح المخطط عند وجود جداول قديمة غير متوافقة
USE BilliardDB;
GO

IF COL_LENGTH('dbo.BilliardTables', 'DisplayName') IS NULL
BEGIN
    IF OBJECT_ID(N'dbo.InvoiceLines', N'U') IS NOT NULL DROP TABLE dbo.InvoiceLines;
    IF OBJECT_ID(N'dbo.OrderItems', N'U') IS NOT NULL DROP TABLE dbo.OrderItems;
    IF OBJECT_ID(N'dbo.Invoices', N'U') IS NOT NULL DROP TABLE dbo.Invoices;
    IF OBJECT_ID(N'dbo.PlayerSessions', N'U') IS NOT NULL DROP TABLE dbo.PlayerSessions;
    IF OBJECT_ID(N'dbo.Orders', N'U') IS NOT NULL DROP TABLE dbo.Orders;
    IF OBJECT_ID(N'dbo.Players', N'U') IS NOT NULL DROP TABLE dbo.Players;
    IF OBJECT_ID(N'dbo.Sessions', N'U') IS NOT NULL DROP TABLE dbo.Sessions;
    IF OBJECT_ID(N'dbo.Products', N'U') IS NOT NULL DROP TABLE dbo.Products;
    IF OBJECT_ID(N'dbo.BilliardTables', N'U') IS NOT NULL DROP TABLE dbo.BilliardTables;
END
GO

USE BilliardDB;
GO

IF OBJECT_ID(N'dbo.TableTypes', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TableTypes (
        TableTypeId        INT           NOT NULL PRIMARY KEY,
        TypeName           NVARCHAR(50)  NOT NULL,
        HourlyRate         DECIMAL(10,2) NOT NULL,
        FirstHourRate      DECIMAL(10,2) NULL,
        AdditionalHourRate DECIMAL(10,2) NULL
    );
    INSERT INTO dbo.TableTypes (TableTypeId, TypeName, HourlyRate, FirstHourRate, AdditionalHourRate) VALUES
        (1, N'Snooker', 5.00, 5.00, 5.00),
        (2, N'Black',   4.00, 4.00, 4.00);
END
ELSE
BEGIN
    IF COL_LENGTH('dbo.TableTypes', 'FirstHourRate') IS NULL
        ALTER TABLE dbo.TableTypes ADD FirstHourRate DECIMAL(10,2) NULL;
    
    IF COL_LENGTH('dbo.TableTypes', 'AdditionalHourRate') IS NULL
        ALTER TABLE dbo.TableTypes ADD AdditionalHourRate DECIMAL(10,2) NULL;
    
    UPDATE dbo.TableTypes 
    SET FirstHourRate = ISNULL(FirstHourRate, HourlyRate),
        AdditionalHourRate = ISNULL(AdditionalHourRate, HourlyRate)
    WHERE FirstHourRate IS NULL OR AdditionalHourRate IS NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.TableTypes)
BEGIN
    INSERT INTO dbo.TableTypes (TableTypeId, TypeName, HourlyRate, FirstHourRate, AdditionalHourRate) VALUES
        (1, N'Snooker', 5.00, 5.00, 5.00),
        (2, N'Black',   4.00, 4.00, 4.00);
END
GO

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

    INSERT INTO dbo.BilliardTables (TableNumber, TableTypeId, DisplayName) VALUES
        (1, 1, N'سنوكر 1'), (2, 1, N'سنوكر 2'), (3, 1, N'سنوكر 3'),
        (4, 1, N'سنوكر 4'), (5, 1, N'سنوكر 5'), (6, 1, N'سنوكر 6'),
        (7, 1, N'سنوكر 7'),
        (8, 2, N'بلاك 1'), (9, 2, N'بلاك 2'), (10, 2, N'بلاك 3');
END
GO

-- ترقية: 6 سنوكر + 3 بلاك (أرقام 7-9) → 7 سنوكر + 3 بلاك (8-10)
IF (SELECT COUNT(*) FROM dbo.BilliardTables WHERE TableTypeId = 1) = 6
   AND NOT EXISTS (SELECT 1 FROM dbo.BilliardTables WHERE DisplayName = N'سنوكر 7')
BEGIN
    IF EXISTS (SELECT 1 FROM dbo.BilliardTables WHERE TableTypeId = 2 AND TableNumber = 9)
        UPDATE dbo.BilliardTables SET TableNumber = 10, DisplayName = N'بلاك 3' WHERE TableTypeId = 2 AND TableNumber = 9;
    IF EXISTS (SELECT 1 FROM dbo.BilliardTables WHERE TableTypeId = 2 AND TableNumber = 8)
        UPDATE dbo.BilliardTables SET TableNumber = 9, DisplayName = N'بلاك 2' WHERE TableTypeId = 2 AND TableNumber = 8;
    IF EXISTS (SELECT 1 FROM dbo.BilliardTables WHERE TableTypeId = 2 AND TableNumber = 7)
        UPDATE dbo.BilliardTables SET TableNumber = 8, DisplayName = N'بلاك 1' WHERE TableTypeId = 2 AND TableNumber = 7;

    INSERT INTO dbo.BilliardTables (TableNumber, TableTypeId, DisplayName)
    VALUES (7, 1, N'سنوكر 7');
END
GO

IF OBJECT_ID(N'dbo.Products', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Products (
        ProductId     INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
        ProductName   NVARCHAR(100) NOT NULL,
        Category      NVARCHAR(50)  NULL,
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

IF OBJECT_ID(N'dbo.PlayerSessions', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PlayerSessions (
        PlayerSessionId INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
        TableId         INT           NOT NULL REFERENCES dbo.BilliardTables(TableId),
        PlayerName      NVARCHAR(100) NULL,
        StartTime       DATETIME2     NOT NULL DEFAULT SYSDATETIME(),
        EndTime         DATETIME2     NULL,
        HourlyRate      DECIMAL(10,2) NOT NULL,
        IsActive        BIT           NOT NULL DEFAULT 1,
        IsInvoiced      BIT           NOT NULL DEFAULT 0
    );
    CREATE INDEX IX_PlayerSessions_Table_Active
        ON dbo.PlayerSessions (TableId, IsActive) WHERE IsActive = 1;
END
GO

IF OBJECT_ID(N'dbo.OrderItems', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.OrderItems (
        OrderItemId     INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
        PlayerSessionId INT           NOT NULL REFERENCES dbo.PlayerSessions(PlayerSessionId),
        ProductId       INT           NOT NULL REFERENCES dbo.Products(ProductId),
        Quantity        INT           NOT NULL DEFAULT 1,
        UnitPrice       DECIMAL(10,2) NOT NULL,
        AddedAt         DATETIME2     NOT NULL DEFAULT SYSDATETIME()
    );
END
GO

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

IF OBJECT_ID(N'dbo.InvoiceLines', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.InvoiceLines (
        InvoiceLineId INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
        InvoiceId     INT           NOT NULL REFERENCES dbo.Invoices(InvoiceId),
        LineType      NVARCHAR(20)  NOT NULL,
        Description   NVARCHAR(200) NOT NULL,
        Quantity      DECIMAL(10,2) NOT NULL,
        UnitPrice     DECIMAL(10,2) NOT NULL,
        LineTotal     DECIMAL(10,2) NOT NULL
    );
END
GO
