CREATE TABLE [dbo].[ArticleCategory] (
    [Id]         BIGINT IDENTITY (1, 1) NOT NULL,
    [ArticleId]  BIGINT NULL,
    [CategoryId] BIGINT NULL,
    [IsDeleted]  BIT    DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ArticleCategory_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ArticleCategory_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([Id]),
    CONSTRAINT [FK_ArticleCategory_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id])
);

