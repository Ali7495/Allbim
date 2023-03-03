CREATE TABLE [dbo].[Attachment] (
    [Id]        BIGINT           IDENTITY (1, 1) NOT NULL,
    [Code]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]      NVARCHAR (256)   NULL,
    [Path]      NVARCHAR (500)   NULL,
    [Extension] NVARCHAR (100)   NULL,
    [Data]      VARBINARY (MAX)  NULL,
    [IsDeleted] BIT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

