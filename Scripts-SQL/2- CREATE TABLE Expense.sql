CREATE TABLE [dbo].[Expense]
(
    IdInvoice INT IDENTITY(1,1) PRIMARY KEY,

    [Description] NVARCHAR(4000) NULL,              -- Optional, up to 4000 chars
    [Amount] DECIMAL(18,2) NOT NULL,                -- Required, any valid amount
    [Category] NVARCHAR(50) NOT NULL,               -- Required, must be from specific list
    [ExpenseDate] DATETIME NOT NULL DEFAULT GETDATE(), -- When the expense is recorded
    [LastPaymentDate] DATETIME NULL,                -- Optional, may include past dates
    [Destination] NVARCHAR(100) NULL,               -- Optional, up to 100 chars
    [Status] NVARCHAR(20) NOT NULL                  -- Required: Pending / Paid
);


ALTER TABLE [dbo].[Expense]
ADD CONSTRAINT CK_Expense_Category
CHECK ([Category] IN ('Education', 'Health', 'Entertainment', 'Monthly Expenses', 'Other'));

ALTER TABLE [dbo].[Expense]
ADD CONSTRAINT CK_Expense_Status
CHECK ([Status] IN ('Pending', 'Paid'));
