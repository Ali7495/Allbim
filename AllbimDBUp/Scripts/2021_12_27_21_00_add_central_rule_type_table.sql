
BEGIN TRANSACTION;


---------------------------------------- Insert Enumeration Data --------------------------------------------

INSERT [dbo].[Enumeration] ( [ParentId], [CategoryName], [CategoryCaption], [EnumId], [EnumCaption], [Order], [IsEnable], [Description], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES ( NULL, N'DataType', N'نوع داده', 1, N'داده ورودی', 1, 1, NULL, NULL, CAST(N'2021-08-29T23:58:03.9200000' AS DateTime2), NULL, CAST(N'2021-08-29T23:58:03.9200000' AS DateTime2))
INSERT [dbo].[Enumeration] ( [ParentId], [CategoryName], [CategoryCaption], [EnumId], [EnumCaption], [Order], [IsEnable], [Description], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES ( NULL, N'DataType', N'نوع داده', 2, N'داده محاسبه شده', 1, 1, NULL, NULL, CAST(N'2021-08-29T23:58:29.4066667' AS DateTime2), NULL, CAST(N'2021-08-29T23:58:29.4066667' AS DateTime2))



---------------------------------------- Alter InsuranceField Table --------------------------------------------

ALTER TABLE [dbo].[InsuranceField] ADD InsuranceFieldTypeId bigint default (1);


---------------------------------------- Create CentralRuleType Table --------------------------------------------
CREATE TABLE [dbo].[CentralRuleType](
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [InsuranceFieldId] bigint NOT NULL,
    [RuleCaption] nvarchar(MAX) NOT NULL,
    [Order] int NOT NULL,
    [Field] nvarchar(MAX),
    [RelatedResource] nvarchar(MAX) ,
    [ResourceTypeId] tinyint NOT NULL,
    [PricingTypeId] tinyint NOT NULL,
    CONSTRAINT [PK_CentralRuleType] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]

ALTER TABLE [dbo].[CentralRuleType]  WITH CHECK ADD  CONSTRAINT [FK_CentralRuleType_InsuranceField] FOREIGN KEY([InsuranceFieldId])
    REFERENCES [dbo].[InsuranceField] ([Id])

ALTER TABLE [dbo].[CentralRuleType] CHECK CONSTRAINT [FK_CentralRuleType_InsuranceField]




---------------------------------------- Alter InsuranceCentralRule Table --------------------------------------------

ALTER TABLE [dbo].[InsuranceCentralRule]  DROP CONSTRAINT [FK_InsuranceCentralRule_Insurance]

ALTER TABLE [dbo].[InsuranceCentralRule] DROP COLUMN [InsuranceId]

ALTER TABLE [dbo].[InsuranceCentralRule] ADD [CentralRuleTypeId] bigint

ALTER TABLE [dbo].[InsuranceCentralRule] ADD [Criteria] NVARCHAR(5)

ALTER TABLE [dbo].[InsuranceCentralRule] ADD [Discount] NVARCHAR(256)

ALTER TABLE [dbo].[InsuranceCentralRule] ADD [CalculationTypeId] tinyint

ALTER TABLE [dbo].[InsuranceCentralRule] ADD [PricingTypeId] tinyint

ALTER TABLE [dbo].[InsuranceCentralRule] ADD [ConditionTypeId] tinyint

ALTER TABLE [dbo].[InsuranceCentralRule]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceCentralRule_CentralRuleType] FOREIGN KEY([CentralRuleTypeId])
    REFERENCES [dbo].[CentralRuleType] ([Id])

ALTER TABLE [dbo].[InsuranceCentralRule] CHECK CONSTRAINT [FK_InsuranceCentralRule_CentralRuleType]





COMMIT;