CREATE PROCEDURE [dbo].[spSaleItem_Insert]
	@Id INT OUTPUT,
	@SaleId INT,
	@ProductId INT,
	@Quantity INT,
	@SalePrice MONEY,
	@Tax MONEY
AS
BEGIN
	INSERT INTO dbo.SaleItem (SaleId, ProductId, Quantity, SalePrice, Tax)
	VALUES (@SaleId, @ProductId, @Quantity, @SalePrice, @Tax);

	SELECT @Id = SCOPE_IDENTITY();
END
