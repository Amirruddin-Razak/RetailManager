CREATE PROCEDURE [dbo].[spInventory_Insert]
	@ProductId INT,
	@PurchasePrice MONEY,
	@PurchaseQuantity INT,
	@PurchaseDate DATETIME2
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Inventory([ProductId], [PurchasePrice], [PurchaseQuantity], [PurchaseDate])
	VALUES (@ProductId, @PurchasePrice, @PurchaseQuantity, @PurchaseDate);
END
