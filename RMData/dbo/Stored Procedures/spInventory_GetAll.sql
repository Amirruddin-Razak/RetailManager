CREATE PROCEDURE [dbo].[spInventory_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [ProductId], [PurchasePrice], [PurchaseQuantity], [PurchaseDate]
	FROM dbo.Inventory;
END
