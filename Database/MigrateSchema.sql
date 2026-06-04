-- ============================================================
-- AlJamal Billiard Hall - Schema Migration / Verification (SQLite version)
-- ============================================================

-- ------------------------------------------------------------
-- 1) TableTypes
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS TableTypes (
    TableTypeId        INTEGER PRIMARY KEY,
    TypeName           TEXT NOT NULL,
    HourlyRate         DECIMAL(10,2) NOT NULL,
    FirstHourRate      DECIMAL(10,2) NULL,
    AdditionalHourRate DECIMAL(10,2) NULL
);

INSERT OR IGNORE INTO TableTypes (TableTypeId, TypeName, HourlyRate, FirstHourRate, AdditionalHourRate) VALUES
    (1, 'Snooker', 5.00, 5.00, 5.00),
    (2, 'Black',   4.00, 4.00, 4.00);

-- ------------------------------------------------------------
-- 2) BilliardTables
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS BilliardTables (
    TableId       INTEGER PRIMARY KEY AUTOINCREMENT,
    TableNumber   INTEGER NOT NULL UNIQUE,
    TableTypeId   INTEGER NOT NULL REFERENCES TableTypes(TableTypeId),
    DisplayName   TEXT NOT NULL,
    IsActive      INTEGER NOT NULL DEFAULT 1
);

INSERT OR IGNORE INTO BilliardTables (TableId, TableNumber, TableTypeId, DisplayName) VALUES
    (1, 1, 1, 'سنوكر 1'),
    (2, 2, 1, 'سنوكر 2'),
    (3, 3, 1, 'سنوكر 3'),
    (4, 4, 1, 'سنوكر 4'),
    (5, 5, 1, 'سنوكر 5'),
    (6, 6, 1, 'سنوكر 6'),
    (7, 7, 1, 'سنوكر 7'),
    (8, 8, 2, 'بلاك 1'),
    (9, 9, 2, 'بلاك 2'),
    (10, 10, 2, 'بلاك 3');

-- ------------------------------------------------------------
-- 3) Products
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Products (
    ProductId     INTEGER PRIMARY KEY AUTOINCREMENT,
    ProductName   TEXT NOT NULL,
    Category      TEXT NULL,
    UnitPrice     DECIMAL(10,2) NOT NULL,
    IsActive      INTEGER NOT NULL DEFAULT 1
);

-- ------------------------------------------------------------
-- 4) PlayerSessions
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS PlayerSessions (
    PlayerSessionId INTEGER PRIMARY KEY AUTOINCREMENT,
    TableId         INTEGER NOT NULL REFERENCES BilliardTables(TableId),
    PlayerName      TEXT NULL,
    StartTime       TEXT NOT NULL DEFAULT (datetime('now', 'localtime')),
    EndTime         TEXT NULL,
    HourlyRate      DECIMAL(10,2) NOT NULL,
    IsActive        INTEGER NOT NULL DEFAULT 1,
    IsInvoiced      INTEGER NOT NULL DEFAULT 0
);

CREATE INDEX IF NOT EXISTS IX_PlayerSessions_Table_Active
    ON PlayerSessions (TableId, IsActive)
    WHERE IsActive = 1;

-- ------------------------------------------------------------
-- 5) OrderItems
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS OrderItems (
    OrderItemId     INTEGER PRIMARY KEY AUTOINCREMENT,
    PlayerSessionId INTEGER NOT NULL REFERENCES PlayerSessions(PlayerSessionId),
    ProductId       INTEGER NOT NULL REFERENCES Products(ProductId),
    Quantity        INTEGER NOT NULL DEFAULT 1,
    UnitPrice       DECIMAL(10,2) NOT NULL,
    AddedAt         TEXT NOT NULL DEFAULT (datetime('now', 'localtime'))
);

-- ------------------------------------------------------------
-- 6) Invoices
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Invoices (
    InvoiceId       INTEGER PRIMARY KEY AUTOINCREMENT,
    PlayerSessionId INTEGER NOT NULL REFERENCES PlayerSessions(PlayerSessionId),
    PlayMinutes     INTEGER NOT NULL,
    PlayAmount      DECIMAL(10,2) NOT NULL,
    OrdersAmount    DECIMAL(10,2) NOT NULL,
    TotalAmount     DECIMAL(10,2) NOT NULL,
    CreatedAt       TEXT NOT NULL DEFAULT (datetime('now', 'localtime')),
    PrintedAt       TEXT NULL
);

-- ------------------------------------------------------------
-- 7) InvoiceLines
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS InvoiceLines (
    InvoiceLineId INTEGER PRIMARY KEY AUTOINCREMENT,
    InvoiceId     INTEGER NOT NULL REFERENCES Invoices(InvoiceId),
    LineType      TEXT NOT NULL,
    Description   TEXT NOT NULL,
    Quantity      DECIMAL(10,2) NOT NULL,
    UnitPrice     DECIMAL(10,2) NOT NULL,
    LineTotal     DECIMAL(10,2) NOT NULL
);
