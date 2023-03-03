CREATE TABLE [dbo].[Article] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (256) NULL,
    [Summary]         NVARCHAR (500) NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [Priority]        TINYINT        NULL,
    [IsActivated]     BIT            DEFAULT ((1)) NOT NULL,
    [IsArchived]      BIT            DEFAULT ((0)) NOT NULL,
    [CreatedDateTime] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [IsDeleted]       BIT            DEFAULT ((0)) NOT NULL,
    [AuthorId]        BIGINT         NULL,
    [slug]            NVARCHAR (50)  NULL,
    [ArticleTypeId]   BIGINT         DEFAULT ((2)) NOT NULL,
    CONSTRAINT [PK_Article_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Article_ArticleTypeId] FOREIGN KEY ([ArticleTypeId]) REFERENCES [dbo].[ArticleType] ([Id]),
    CONSTRAINT [FK_Article_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Person] ([Id])
);

