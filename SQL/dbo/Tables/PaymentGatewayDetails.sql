CREATE TABLE [dbo].[PaymentGatewayDetails] (
    [Id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [PaymentGatewayId] BIGINT         NOT NULL,
    [Key]              NVARCHAR (200) NOT NULL,
    [Value]            NVARCHAR (MAX) NOT NULL,
    [IsDeleted]        BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentGatewayDetails_PaymentGateway] FOREIGN KEY ([PaymentGatewayId]) REFERENCES [dbo].[PaymentGateway] ([Id])
);

