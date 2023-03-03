CREATE TABLE [dbo].[PlaceAddress] (
    [Id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [AddressId] BIGINT        NOT NULL,
    [PlaceId]   BIGINT        NOT NULL,
    [CreatedBy] BIGINT        NULL,
    [CreatedAt] DATETIME2 (7) DEFAULT (getdate()) NULL,
    [UpdatedBy] BIGINT        NULL,
    [UpdatedAt] DATETIME2 (7) DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PlaceAddress_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PlaceAddress_Place] FOREIGN KEY ([PlaceId]) REFERENCES [dbo].[Place] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

