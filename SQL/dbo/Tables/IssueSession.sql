CREATE TABLE [dbo].[IssueSession] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (200) NULL,
    [Description] NVARCHAR (300) NULL,
    [IsDeleted]   BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_IssueSession_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

