CREATE TABLE [dbo].[InsuranceFAQ] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Question]    NVARCHAR (MAX) NULL,
    [Answer]      NVARCHAR (MAX) NULL,
    [IsDeleted]   BIT            DEFAULT ((0)) NOT NULL,
    [InsuranceId] BIGINT         NOT NULL,
    CONSTRAINT [PK_InsuranceFAQ_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsuranceFAQ_InsuranceId] FOREIGN KEY ([InsuranceId]) REFERENCES [dbo].[Insurance] ([Id])
);

