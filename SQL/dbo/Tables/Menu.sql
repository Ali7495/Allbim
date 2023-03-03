CREATE TABLE [dbo].[Menu] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Title]        VARCHAR (100)  NULL,
    [Name]         NVARCHAR (100) NOT NULL,
    [Icon]         VARCHAR (100)  NULL,
    [Order]        BIGINT         NULL,
    [ParentId]     BIGINT         NULL,
    [PermissionId] BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Menu_Menu] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Menu] ([Id]),
    CONSTRAINT [FK_Menu_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([Id]) ON DELETE SET NULL ON UPDATE CASCADE
);

