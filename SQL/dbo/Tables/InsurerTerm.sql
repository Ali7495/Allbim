CREATE TABLE [dbo].[InsurerTerm] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [InsurerId]           BIGINT         NOT NULL,
    [Value]               NVARCHAR (100) NOT NULL,
    [Discount]            NVARCHAR (100) NOT NULL,
    [CreatedBy]           BIGINT         NULL,
    [CreatedAt]           DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [UpdatedBy]           BIGINT         NULL,
    [UpdatedAt]           DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [IsCumulative]        BIT            DEFAULT ((0)) NOT NULL,
    [PricingTypeId]       TINYINT        NULL,
    [ConditionTypeId]     TINYINT        NULL,
    [InsuranceTermTypeId] BIGINT         NULL,
    [CalculationTypeId]   TINYINT        DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsurerTerm_InsuranceTermType] FOREIGN KEY ([InsuranceTermTypeId]) REFERENCES [dbo].[InsuranceTermType] ([Id]),
    CONSTRAINT [FK_InsurerTerm_Insurer] FOREIGN KEY ([InsurerId]) REFERENCES [dbo].[Insurer] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);




GO
