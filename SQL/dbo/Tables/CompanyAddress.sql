CREATE TABLE [dbo].[CompanyAddress] (
    [Id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [AddressId] BIGINT        NOT NULL,
    [CompanyId] BIGINT        NOT NULL,
    [CreatedBy] BIGINT        NULL,
    [CreatedAt] DATETIME2 (7) DEFAULT (getdate()) NULL,
    [UpdatedBy] BIGINT        NULL,
    [UpdatedAt] DATETIME2 (7) DEFAULT (getdate()) NULL,
    [IsDeleted] BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CompanyAddress_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_CompanyAddress_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);



