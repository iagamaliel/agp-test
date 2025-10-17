CREATE PROCEDURE [dbo].[CreateExpense]
(
    @Description NVARCHAR(4000) = NULL,
    @Amount DECIMAL(18,2),
    @Category NVARCHAR(100),
    @ExpenseDate DATETIME,
    @LastPaymentDate DATETIME = NULL,
    @Destination NVARCHAR(100) = NULL,
    @Status NVARCHAR(50),
    @Code INT OUTPUT,
    @Message NVARCHAR(255) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        INSERT INTO [dbo].[Expense]
        (
            [Description],
            [Amount],
            [Category],
            [ExpenseDate],
            [LastPaymentDate],
            [Destination],
            [Status]
        )
        VALUES
        (
            @Description,
            @Amount,
            @Category,
            @ExpenseDate,
            @LastPaymentDate,
            @Destination,
            @Status
        );

        SET @Code = 1;
        SET @Message = N'Expense created successfully.';
    END TRY
    BEGIN CATCH
        SET @Code = 0;
        SET @Message = ERROR_MESSAGE();
    END CATCH;
END
