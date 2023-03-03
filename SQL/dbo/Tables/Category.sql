CREATE TABLE [dbo].[Category] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (100) NULL,
    [Slug]      NVARCHAR (255) NULL,
    [IsDeleted] BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Category_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

