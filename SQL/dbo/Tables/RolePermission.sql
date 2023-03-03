CREATE TABLE [dbo].[RolePermission] (
    [Id]           BIGINT IDENTITY (1, 1) NOT NULL,
    [RoleId]       BIGINT NOT NULL,
    [PermissionId] BIGINT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RolePermission_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_RolePermission_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

