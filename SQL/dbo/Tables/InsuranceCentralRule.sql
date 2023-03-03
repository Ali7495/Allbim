CREATE TABLE [dbo].[InsuranceCentralRule] (
    [Id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [JalaliYear]        NVARCHAR (100) NOT NULL,
    [GregorianYear]     NVARCHAR (100) NOT NULL,
    [FieldType]         NVARCHAR (200) NOT NULL,
    [Value]             NVARCHAR (100) NOT NULL,
    [CreatedBy]         BIGINT         NULL,
    [CreatedAt]         DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [IsCumulative]      BIT            DEFAULT ((0)) NOT NULL,
    [CentralRuleTypeId] BIGINT         NULL,
    [Discount]          NVARCHAR (256) NULL,
    [CalculationTypeId] TINYINT        NULL,
    [PricingTypeId]     TINYINT        NULL,
    [ConditionTypeId]   TINYINT        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsuranceCentralRule_CentralRuleType] FOREIGN KEY ([CentralRuleTypeId]) REFERENCES [dbo].[CentralRuleType] ([Id])
);

