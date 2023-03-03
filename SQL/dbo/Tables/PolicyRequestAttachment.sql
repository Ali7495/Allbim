CREATE TABLE [dbo].[PolicyRequestAttachment] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [PolicyRequestId] BIGINT         NOT NULL,
    [AttachmentId]    BIGINT         NOT NULL,
    [Name]            NVARCHAR (100) NULL,
    [TypeId]          INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PolicyRequestAttachment_Attachment] FOREIGN KEY ([AttachmentId]) REFERENCES [dbo].[Attachment] ([Id]),
    CONSTRAINT [FK_PolicyRequestAttachment_PolicyRequest] FOREIGN KEY ([PolicyRequestId]) REFERENCES [dbo].[PolicyRequest] ([Id])
);

