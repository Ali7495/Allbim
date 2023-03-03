CREATE TABLE [dbo].[PaymentStatus] (
    [Id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50) NULL,
    [isDeleted] BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PaymentStatus_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

