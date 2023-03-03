CREATE TABLE [dbo].[Discount] (
    [Id]                 BIGINT        IDENTITY (1, 1) NOT NULL,
    [PersonId]           BIGINT        NULL,
    [InsuranceId]        BIGINT        NULL,
    [InsurerId]          BIGINT        NULL,
    [Value]              INT           NULL,
    [ExpirationDateTime] DATETIME2 (7) NULL,
    [IsUsed]             BIT           DEFAULT ((0)) NOT NULL,
    [CreatedDateTime]    DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    [IsDeleted]          BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Discount_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Discount_InsuranceId] FOREIGN KEY ([InsuranceId]) REFERENCES [dbo].[Insurance] ([Id]),
    CONSTRAINT [FK_Discount_InsurerId] FOREIGN KEY ([InsurerId]) REFERENCES [dbo].[Insurer] ([Id]),
    CONSTRAINT [FK_Discount_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);

