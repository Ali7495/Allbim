CREATE TABLE [dbo].[Centers] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [CityId]      BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Centers_CityId] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id])
);

