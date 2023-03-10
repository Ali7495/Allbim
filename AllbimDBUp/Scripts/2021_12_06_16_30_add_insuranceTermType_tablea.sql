CREATE TABLE [dbo].[InsuranceTermType](
	[Id] bigint IDENTITY(1,1) NOT NULL,
	[InsuranceFieldId] bigint NOT NULL,
	[TermCaption] nvarchar(MAX) NOT NULL,
	[Order] int NOT NULL,
	[Field] nvarchar(MAX),
	[RelatedResource] nvarchar(MAX) NOT NULL,
	[ResourceTypeId] tinyint NOT NULL,
	[PricingTypeId] tinyint NOT NULL,
 CONSTRAINT [PK_InsuranceTermType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[InsuranceTermType]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceTermType_InsuranceField] FOREIGN KEY([InsuranceFieldId])
REFERENCES [dbo].[InsuranceField] ([Id])

ALTER TABLE [dbo].[InsuranceTermType] CHECK CONSTRAINT [FK_InsuranceTermType_InsuranceField]
