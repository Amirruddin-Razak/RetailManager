﻿CREATE TABLE [dbo].[Sale]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[CashierId] NVARCHAR(128) NOT NULL,
	[TransactionDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[Subtotal] MONEY NOT NULL,
	[Tax] MONEY NOT NULL,
	[Total] MONEY NOT NULL, 
    CONSTRAINT [FK_Sale_User] FOREIGN KEY ([CashierId]) REFERENCES [User]([Id])

)
