CREATE PROCEDURE [dbo].[spProduct_Update]
	@Id INT,
	@QuantityInStock INT,
	@ReservedQuantity INT
AS
BEGIN
	UPDATE dbo.Product 
	SET QuantityInStock = @QuantityInStock, ReservedQuantity = ReservedQuantity
	WHERE Id = @Id;
END
