CREATE TABLE [dbo].[OnlinePayment] (
    [Id]              BIGINT        IDENTITY (1, 1) NOT NULL,
    [PaymentId]       BIGINT        NOT NULL,
    [PaymentSettle]   BIT           DEFAULT ((0)) NOT NULL,
    [PaymentVerify]   BIT           DEFAULT ((0)) NOT NULL,
    [RefId]           VARCHAR (50)  NULL,
    [SaleOrderId]     BIGINT        NULL,
    [SaleReferenceId] VARCHAR (50)  NULL,
    [SettleDate]      DATETIME2 (7) NULL,
    [VerifyDate]      DATETIME2 (7) NULL,
    CONSTRAINT [PK_OnlinePayment_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OnlinePayment_PaymentId] FOREIGN KEY ([PaymentId]) REFERENCES [dbo].[Payment] ([Id])
);

