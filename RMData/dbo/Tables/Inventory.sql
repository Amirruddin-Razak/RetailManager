CREATE TABLE [dbo].[Inventory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductId] INT NOT NULL,
	[PurchasePrice] MONEY NOT NULL,
	[PurchaseQuantity] INT NOT NULL,
	[PurchaseDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [FK_Inventory_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id])
)
