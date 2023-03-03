CREATE TABLE [dbo].[PolicyRequestCommentAttachment] (
    [Id]                     BIGINT IDENTITY (1, 1) NOT NULL,
    [PolicyRequestCommentId] BIGINT NULL,
    [AttachmentId]           BIGINT NULL,
    [AttachmentTypeId]       INT    NULL,
    [IsDeleted]              BIT    DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PolicyRequestCommentAttachment_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PolicyRequestCommentAttachment_AttachmentId] FOREIGN KEY ([AttachmentId]) REFERENCES [dbo].[Attachment] ([Id]),
    CONSTRAINT [FK_PolicyRequestCommentAttachment_PolicyRequestCommentId] FOREIGN KEY ([PolicyRequestCommentId]) REFERENCES [dbo].[PolicyRequestComment] ([Id])
);

