CREATE TABLE [dbo].[Enumeration] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [ParentId]        BIGINT         NULL,
    [CategoryName]    NVARCHAR (100) NOT NULL,
    [CategoryCaption] NVARCHAR (100) NOT NULL,
    [EnumId]          INT            NOT NULL,
    [EnumCaption]     NVARCHAR (100) NOT NULL,
    [Order]           TINYINT        DEFAULT ((1)) NULL,
    [IsEnable]        TINYINT        DEFAULT ((1)) NULL,
    [Description]     TEXT           NULL,
    [CreatedBy]       BIGINT         NULL,
    [CreatedAt]       DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [UpdatedBy]       BIGINT         NULL,
    [UpdatedAt]       DATETIME2 (7)  DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Enumeration_Enumeration] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Enumeration] ([Id])
);

