CREATE TABLE [dbo].[Province] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (200) NOT NULL,
    [CreatedBy] BIGINT         NULL,
    [CreatedAt] DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [UpdatedBy] BIGINT         NULL,
    [UpdatedAt] DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [IsDeleted] BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



