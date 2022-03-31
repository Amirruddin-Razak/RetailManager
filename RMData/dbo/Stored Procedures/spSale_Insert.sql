CREATE PROCEDURE [dbo].[spSale_Insert]
	@Id INT OUTPUT,
	@CashierId NVARCHAR(128),
	@TransactionDate DATETIME2,
	@Subtotal MONEY,
	@Tax MONEY,
	@Total MONEY
AS
BEGIN
	INSERT INTO dbo.Sale (CashierId, TransactionDate, Subtotal, Tax, Total)
	VALUES (@CashierId, @TransactionDate, @Subtotal, @Tax, @Total);

	SELECT @Id = SCOPE_IDENTITY();
END
