CREATE TABLE [dbo].[Role] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (100) NOT NULL,
    [IsDeleted] BIT            DEFAULT ((0)) NOT NULL,
    [Caption]   NVARCHAR (100) NULL,
    [ParentId]  BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Role_Role] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Role] ([Id])
);



