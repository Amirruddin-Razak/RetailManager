CREATE PROCEDURE [dbo].[spProduct_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT p.Id, p.ProductName, p.[Description], p.RetailPrice, p.QuantityInStock, p.ReservedQuantity, t.TaxPercentage
	FROM dbo.Product p INNER JOIN dbo.Tax t ON p.TaxId = t.Id
	ORDER BY p.ProductName;
END
