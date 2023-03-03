CREATE TABLE [dbo].[PolicyRequestComment] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [PolicyRequestId] BIGINT         NOT NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [AuthorId]        BIGINT         NOT NULL,
    [AuthorTypeId]    TINYINT        NULL,
    [IsDeleted]       BIT            DEFAULT ((0)) NOT NULL,
    [createdDateTime] DATETIME2 (7)  DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_PolicyRequestComment_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PolicyRequestComment_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Person] ([Id]),
    CONSTRAINT [FK_PolicyRequestComment_PolicyRequestId] FOREIGN KEY ([PolicyRequestId]) REFERENCES [dbo].[PolicyRequest] ([Id])
);

