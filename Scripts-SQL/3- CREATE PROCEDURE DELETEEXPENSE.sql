USE [TechnicalTest]
GO

/****** Object:  StoredProcedure [dbo].[DeleteExpense]    Script Date: 10/17/2025 12:30:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteExpense]
(
    @IdInvoice INT,
    @Code INT OUTPUT,
    @Message NVARCHAR(255) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        -- Delete directamente sin transacción
        DELETE FROM Expense
        WHERE IdInvoice = @IdInvoice;

        -- Assignment Code y Message
        SET @Code = 1;
        SET @Message = 'Expense deleted successfully.';
    END TRY
    BEGIN CATCH
        -- Captura cualquier error
        SET @Code = 0;
        SET @Message = ERROR_MESSAGE();
    END CATCH
END
GO


