CREATE TABLE [dbo].[Product] (
    [ProductId] INT             IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (255)  NOT NULL,
    [ModelYear] SMALLINT        NOT NULL,
    [ListPrice] DECIMAL (10, 2) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

