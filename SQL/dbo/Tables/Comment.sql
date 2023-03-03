CREATE TABLE [dbo].[Comment] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [AuthorId]    BIGINT         NULL,
    [ArticleId]   BIGINT         NOT NULL,
    [ParentId]    BIGINT         NULL,
    [Description] NVARCHAR (MAX) NULL,
    [IsDelete]    BIT            DEFAULT ((0)) NOT NULL,
    [IsApproved]  BIT            DEFAULT ((0)) NOT NULL,
    [Score]       INT            NULL,
    CONSTRAINT [PK_Comment_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Comment_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([Id]),
    CONSTRAINT [FK_Comment_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Person] ([Id]),
    CONSTRAINT [FK_Comment_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Comment] ([Id])
);

