CREATE TABLE [dbo].[TownShip] (
    [Id]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (200) NOT NULL,
    [ProvinceId] BIGINT         NOT NULL,
    [CreatedBy]  BIGINT         NULL,
    [CreatedAt]  DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [UpdatedBy]  BIGINT         NULL,
    [UpdatedAt]  DATETIME2 (7)  DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TownShip_Province] FOREIGN KEY ([ProvinceId]) REFERENCES [dbo].[Province] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

