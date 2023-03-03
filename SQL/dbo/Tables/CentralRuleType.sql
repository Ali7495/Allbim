CREATE TABLE [dbo].[CentralRuleType] (
    [Id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [InsuranceFieldId] BIGINT         NOT NULL,
    [RuleCaption]      NVARCHAR (MAX) NOT NULL,
    [Order]            INT            NOT NULL,
    [Field]            NVARCHAR (MAX) NULL,
    [RelatedResource]  NVARCHAR (MAX) NULL,
    [ResourceTypeId]   TINYINT        NOT NULL,
    [PricingTypeId]    TINYINT        NOT NULL,
    CONSTRAINT [PK_CentralRuleType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CentralRuleType_InsuranceField] FOREIGN KEY ([InsuranceFieldId]) REFERENCES [dbo].[InsuranceField] ([Id])
);

