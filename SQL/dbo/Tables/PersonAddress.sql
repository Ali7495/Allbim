CREATE TABLE [dbo].[PersonAddress] (
    [Id]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [AddressId]     BIGINT        NOT NULL,
    [PersonId]      BIGINT        NOT NULL,
    [CreatedBy]     BIGINT        NULL,
    [CreatedAt]     DATETIME2 (7) DEFAULT (getdate()) NULL,
    [UpdatedBy]     BIGINT        NULL,
    [UpdatedAt]     DATETIME2 (7) DEFAULT (getdate()) NULL,
    [IsDeleted]     BIT           DEFAULT ((0)) NOT NULL,
    [AddressTypeId] BIGINT        DEFAULT ((2)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonAddress_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PersonAddress_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);



