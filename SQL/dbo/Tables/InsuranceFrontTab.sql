CREATE TABLE [dbo].[InsuranceFrontTab] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [InsuranceId] BIGINT        NOT NULL,
    [Name]        VARCHAR (100) NULL,
    [Title]       VARCHAR (100) NULL,
    [IsDeleted]   BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_InsuranceFrontTab] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsuranceFrontTab_Insurance] FOREIGN KEY ([InsuranceId]) REFERENCES [dbo].[Insurance] ([Id])
);

