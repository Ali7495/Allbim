CREATE TABLE [dbo].[CompanyCenter] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [CompanyId]   BIGINT         NULL,
    [Name]        NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [CityId]      BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CompanyCenter_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id]),
    CONSTRAINT [FK_InsuranceCenter_CityId] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id])
);

