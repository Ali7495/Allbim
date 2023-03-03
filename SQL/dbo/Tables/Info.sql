CREATE TABLE [dbo].[Info] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Key]       NVARCHAR (255) NULL,
    [Value]     NVARCHAR (MAX) NULL,
    [Slug]      NVARCHAR (100) NULL,
    [IsDeleted] BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Info_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

