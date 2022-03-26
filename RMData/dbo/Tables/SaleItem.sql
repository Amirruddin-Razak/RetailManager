CREATE TABLE [dbo].[SaleItem]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[SaleId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	[Quantity] INT NOT NULL DEFAULT 1,
	[SalePrice] MONEY NOT NULL,
	[Tax] MONEY NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_SaleItem_Sale] FOREIGN KEY ([SaleId]) REFERENCES [Sale]([Id]), 
    CONSTRAINT [FK_SaleItem_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id])
)
