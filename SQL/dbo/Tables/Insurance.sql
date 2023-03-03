CREATE TABLE [dbo].[Insurance] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [CreatedBy]   BIGINT         NULL,
    [CreatedAt]   DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [UpdatedBy]   BIGINT         NULL,
    [UpdatedAt]   DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [Slug]        NVARCHAR (100) NULL,
    [AvatarUrl]   NVARCHAR (255) NULL,
    [IsDeleted]   BIT            DEFAULT ((0)) NOT NULL,
    [Summary]     NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO

GO
