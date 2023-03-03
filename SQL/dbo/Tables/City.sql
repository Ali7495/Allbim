CREATE TABLE [dbo].[City] (
    [Id]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (200) NOT NULL,
    [TownShipId] BIGINT         NOT NULL,
    [CreatedBy]  BIGINT         NULL,
    [CreatedAt]  DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [UpdatedBy]  BIGINT         NULL,
    [UpdatedAt]  DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [IsDeleted]  BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_City_TownShip] FOREIGN KEY ([TownShipId]) REFERENCES [dbo].[TownShip] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);



