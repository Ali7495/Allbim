CREATE TABLE [dbo].[PolicyRequestInspection] (
    [Id]                      BIGINT        IDENTITY (1, 1) NOT NULL,
    [PolicyRequestId]         BIGINT        NOT NULL,
    [InspectionTypeId]        TINYINT       DEFAULT ((0)) NULL,
    [InspectionAddressId]     BIGINT        NULL,
    [InspectionSessionDate]   DATETIME2 (7) NULL,
    [CompanyCenterScheduleId] BIGINT        NULL,
    [CreatedDateTime]         DATETIME2 (7) NULL,
    [InspectionSessionId]     BIGINT        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PolicyRequesInspection_InspectionAddressId] FOREIGN KEY ([InspectionAddressId]) REFERENCES [dbo].[Address] ([Id]),
    CONSTRAINT [FK_PolicyRequesInspection_PolicyRequestId] FOREIGN KEY ([PolicyRequestId]) REFERENCES [dbo].[PolicyRequest] ([Id]),
    CONSTRAINT [FK_PolicyRequestInspection_CompanyCenterScheduleId] FOREIGN KEY ([CompanyCenterScheduleId]) REFERENCES [dbo].[CompanyCenterSchedule] ([Id]),
    CONSTRAINT [FK_PolicyRequestInspection_InspectionSessionId] FOREIGN KEY ([InspectionSessionId]) REFERENCES [dbo].[InspectionSession] ([Id])
);

