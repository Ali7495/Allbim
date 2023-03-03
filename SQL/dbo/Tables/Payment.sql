CREATE TABLE [dbo].[Payment] (
    [Id]               BIGINT        IDENTITY (1, 1) NOT NULL,
    [Price]            DECIMAL (18)  NOT NULL,
    [Status]           TINYINT       DEFAULT ((1)) NOT NULL,
    [CreatedDateTime]  DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    [PaymentCode]      VARCHAR (100) NULL,
    [Description]      TEXT          NULL,
    [IsDeleted]        BIT           DEFAULT ((0)) NOT NULL,
    [PaymentStatusId]  BIGINT        DEFAULT ((1)) NOT NULL,
    [UpdatedDateTime]  DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    [PaymentGatewayId] BIGINT        NULL,
    CONSTRAINT [PK_Payment_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Payment_PaymentGatewayId] FOREIGN KEY ([PaymentGatewayId]) REFERENCES [dbo].[PaymentGateway] ([Id]),
    CONSTRAINT [FK_Payment_PaymentStatusId] FOREIGN KEY ([PaymentStatusId]) REFERENCES [dbo].[PaymentStatus] ([Id])
);

