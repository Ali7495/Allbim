CREATE TABLE [dbo].[InsuredRequestRelatedPerson] (
    [Id]               BIGINT IDENTITY (1, 1) NOT NULL,
    [InsuredRequestId] BIGINT NOT NULL,
    [RelationTypeId]   INT    NOT NULL,
    [PersonId]         BIGINT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsuredRequestRelatedPerson_InsuredRequest] FOREIGN KEY ([InsuredRequestId]) REFERENCES [dbo].[InsuredRequest] ([Id]),
    CONSTRAINT [FK_InsuredRequestRelatedPerson_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);



