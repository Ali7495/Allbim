CREATE TABLE [dbo].[InsuranceStep] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [InsuranceId] BIGINT        NULL,
    [StepName]    VARCHAR (100) NULL,
    [StepOrder]   INT           DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_InsuranceStep_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsuranceStep_InsuranceId] FOREIGN KEY ([InsuranceId]) REFERENCES [dbo].[Insurance] ([Id])
);

