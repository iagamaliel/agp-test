USE [TechnicalTest]
GO

/****** Object:  StoredProcedure [dbo].[UpdateExpense]    Script Date: 10/17/2025 12:31:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[UpdateExpense]
(
    @IdInvoice INT,
    @Description NVARCHAR(4000) = NULL,
    @Amount DECIMAL(18,2),
    @Category NVARCHAR(100),
    @ExpenseDate DATE,
    @LastPaymentDate DATE = NULL,
    @Destination NVARCHAR(100) = NULL,
    @Status NVARCHAR(50),
    @Code INT OUTPUT,
    @Message NVARCHAR(255) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE [dbo].[Expense]
        SET
            [Description] = @Description,
            [Amount] = @Amount,
            [Category] = @Category,
            [ExpenseDate] = @ExpenseDate,
            [LastPaymentDate] = @LastPaymentDate,
            [Destination] = @Destination,
            [Status] = @Status
        WHERE
            [IdInvoice] = @IdInvoice;

        COMMIT TRANSACTION;

        SET @Code = 1;
        SET @Message = N'Invoice updated successfully.';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @Code = 0;
        SET @Message = ERROR_MESSAGE();
    END CATCH;
END
GO


