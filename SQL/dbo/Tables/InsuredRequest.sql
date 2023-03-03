CREATE TABLE [dbo].[InsuredRequest] (
    [Id]              BIGINT IDENTITY (1, 1) NOT NULL,
    [PolicyRequestId] BIGINT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsuredRequest_PolicyRequest] FOREIGN KEY ([PolicyRequestId]) REFERENCES [dbo].[PolicyRequest] ([Id])
);



