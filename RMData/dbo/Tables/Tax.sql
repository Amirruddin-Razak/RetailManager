﻿CREATE TABLE [dbo].[Tax]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Category] NVARCHAR(10) NOT NULL,
	[TaxPercentage] MONEY NOT NULL DEFAULT 0
)
