CREATE TABLE [dbo].[OrderItem] (
    [OrderId]     INT             NOT NULL,
    [OrderItemId] INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255)  NOT NULL,
    [ProductId]   INT             NOT NULL,
    [Quantity]    INT             NOT NULL,
    [ListPrice]   DECIMAL (10, 2) NOT NULL,
    [Discount]    DECIMAL (4, 2)  DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED ([OrderId] ASC, [OrderItemId] ASC),
    CONSTRAINT [FK_OrderItem_REF_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([OrderId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_OrderItem_REF_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE
);



