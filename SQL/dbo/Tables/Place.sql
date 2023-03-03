CREATE TABLE [dbo].[Place] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (200) NOT NULL,
    [Description] TEXT           NULL,
    [CreatedBy]   BIGINT         NULL,
    [CreatedAt]   DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [UpdatedBy]   BIGINT         NULL,
    [UpdatedAt]   DATETIME2 (7)  DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

