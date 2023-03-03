CREATE TABLE [dbo].[ArticleType] (
    [Id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50) NULL,
    [IsDeleted] BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ArticleType_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

