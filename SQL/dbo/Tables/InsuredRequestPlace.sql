CREATE TABLE [dbo].[InsuredRequestPlace] (
    [Id]               BIGINT IDENTITY (1, 1) NOT NULL,
    [InsuredRequestId] BIGINT NOT NULL,
    [PlaceTypeId]      INT    NOT NULL,
    [PlaceId]          BIGINT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsuredRequestPlace_InsuredRequest] FOREIGN KEY ([InsuredRequestId]) REFERENCES [dbo].[InsuredRequest] ([Id]),
    CONSTRAINT [FK_InsuredRequestPlace_Place] FOREIGN KEY ([PlaceId]) REFERENCES [dbo].[Place] ([Id])
);



