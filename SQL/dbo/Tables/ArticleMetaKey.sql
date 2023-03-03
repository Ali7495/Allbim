CREATE TABLE [dbo].[ArticleMetaKey] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [ArticleId] BIGINT         NOT NULL,
    [Key]       NVARCHAR (255) NULL,
    [Value]     NVARCHAR (255) NULL,
    [IsDeleted] BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ArticleMeta_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ArticleMetaKey_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([Id])
);

