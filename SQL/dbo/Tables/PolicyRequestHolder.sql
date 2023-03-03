CREATE TABLE [dbo].[PolicyRequestHolder] (
    [Id]                   BIGINT       IDENTITY (1, 1) NOT NULL,
    [PolicyRequestId]      BIGINT       NOT NULL,
    [PersonId]             BIGINT       NULL,
    [CompanyId]            BIGINT       NULL,
    [IssuedPersonType]     TINYINT      NULL,
    [IssuedPersonRelation] VARCHAR (50) NULL,
    [AddressId]            BIGINT       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PolicyRequestHolder_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id]),
    CONSTRAINT [FK_PolicyRequestHolder_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id]),
    CONSTRAINT [FK_PolicyRequestHolder_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]),
    CONSTRAINT [FK_PolicyRequestHolder_PolicyRequest] FOREIGN KEY ([PolicyRequestId]) REFERENCES [dbo].[PolicyRequest] ([Id])
);



