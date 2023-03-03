CREATE TABLE [dbo].[InsuredRequestPerson] (
    [Id]               BIGINT IDENTITY (1, 1) NOT NULL,
    [InsuredRequestId] BIGINT NOT NULL,
    [PersonId]         BIGINT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsuredRequestPerson__Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]),
    CONSTRAINT [FK_InsuredRequestPerson_InsuredRequest] FOREIGN KEY ([InsuredRequestId]) REFERENCES [dbo].[InsuredRequest] ([Id])
);



