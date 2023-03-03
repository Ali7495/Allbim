CREATE TABLE [dbo].[PersonAttachment] (
    [Id]           BIGINT IDENTITY (1, 1) NOT NULL,
    [PersonId]     BIGINT NULL,
    [AttachmentId] BIGINT NULL,
    [TypeId]       INT    NOT NULL,
    [IsDeleted]    BIT    DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PersonAttachment_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonAttachment_AttachmentId] FOREIGN KEY ([AttachmentId]) REFERENCES [dbo].[Attachment] ([Id]),
    CONSTRAINT [FK_PersonAttachment_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);

