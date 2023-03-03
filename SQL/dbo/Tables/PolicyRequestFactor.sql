CREATE TABLE [dbo].[PolicyRequestFactor] (
    [Id]              BIGINT IDENTITY (1, 1) NOT NULL,
    [PaymentId]       BIGINT NULL,
    [PolicyRequestId] BIGINT NULL,
    [IsDeleted]       BIT    DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PolicyRequestFactor_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PolicyRequestFactor_PaymentId] FOREIGN KEY ([PaymentId]) REFERENCES [dbo].[Payment] ([Id]),
    CONSTRAINT [FK_PolicyRequestFactor_PolicyRequestId] FOREIGN KEY ([PolicyRequestId]) REFERENCES [dbo].[PolicyRequest] ([Id])
);

