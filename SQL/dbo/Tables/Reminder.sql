CREATE TABLE [dbo].[Reminder] (
    [Id]               BIGINT        IDENTITY (1, 1) NOT NULL,
    [InsuranceId]      BIGINT        NULL,
    [ReminderPeriodId] BIGINT        NULL,
    [Description]      TEXT          NULL,
    [DueDate]          DATETIME2 (7) NULL,
    [CityId]           BIGINT        NULL,
    [PersonId]         BIGINT        NULL,
    [IsDeleted]        BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Reminder_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Reminder_CityId] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id]),
    CONSTRAINT [FK_Reminder_InsuranceId] FOREIGN KEY ([InsuranceId]) REFERENCES [dbo].[Insurance] ([Id]),
    CONSTRAINT [FK_Reminder_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]),
    CONSTRAINT [FK_Reminder_ReminderPeriodId] FOREIGN KEY ([ReminderPeriodId]) REFERENCES [dbo].[ReminderPeriod] ([Id])
);

