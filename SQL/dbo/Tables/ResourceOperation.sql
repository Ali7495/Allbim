CREATE TABLE [dbo].[ResourceOperation] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (100) NULL,
    [Class]        NVARCHAR (100) NULL,
    [Key]          NVARCHAR (100) NULL,
    [ResourceId]   BIGINT         NULL,
    [PermissionId] BIGINT         NULL,
    [IsDeleted]    BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ResourceOperation_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ResourceOperation_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([Id]),
    CONSTRAINT [FK_ResourceOperation_ResourceId] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resource] ([Id])
);

