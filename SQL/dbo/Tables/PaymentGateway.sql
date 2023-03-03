CREATE TABLE [dbo].[PaymentGateway] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [TerminalId]  VARCHAR (255)  NULL,
    [IsDeleted]   BIT            DEFAULT ((0)) NOT NULL,
    [AllowOnline] BIT            DEFAULT ((1)) NOT NULL,
    [AllowManual] BIT            DEFAULT ((1)) NOT NULL,
    [Name]        NVARCHAR (200) NULL,
    CONSTRAINT [PK_PaymentGateway_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

