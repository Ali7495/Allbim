CREATE TABLE [dbo].[ReminderPeriod] (
    [Id]       BIGINT       IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (50) NULL,
    [IsDelete] BIT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ReminderPeriod_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

