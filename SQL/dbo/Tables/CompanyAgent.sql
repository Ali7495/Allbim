CREATE TABLE [dbo].[CompanyAgent] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [CompanyId]   BIGINT         NOT NULL,
    [PersonId]    BIGINT         NOT NULL,
    [CityId]      BIGINT         NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [IsDeleted]   BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CompanyAgent_CityId] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id]),
    CONSTRAINT [FK_CompanyAgent_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id]),
    CONSTRAINT [FK_CompanyAgent_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);

