CREATE PROCEDURE [dbo].[spSale_SaleReport]
AS
BEGIN
	SELECT s.TransactionDate, s.Subtotal, s.Tax, s.Total, u.FirstName, u.LastName, u.EmailAddress
	FROM dbo.Sale s
	INNER JOIN dbo.[User] u ON s.CashierId = u.Id;
END
