CREATE TABLE [dbo].[InsuredRequestCompany] (
    [Id]               BIGINT IDENTITY (1, 1) NOT NULL,
    [InsuredRequestId] BIGINT NOT NULL,
    [CompanyId]        BIGINT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsuredRequestCompany__Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id]),
    CONSTRAINT [FK_InsuredRequestCompany_InsuredRequest] FOREIGN KEY ([InsuredRequestId]) REFERENCES [dbo].[InsuredRequest] ([Id])
);



