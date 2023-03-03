CREATE TABLE [dbo].[ArticleSection] (
    [Id]        BIGINT IDENTITY (1, 1) NOT NULL,
    [ArticleId] BIGINT NOT NULL,
    [SectionId] INT    NOT NULL,
    CONSTRAINT [PK_ArticleSection_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ArticleSection_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([Id])
);

