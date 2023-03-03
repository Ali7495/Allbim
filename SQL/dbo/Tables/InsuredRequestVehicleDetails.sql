CREATE TABLE [dbo].[InsuredRequestVehicleDetails] (
    [Id]                      BIGINT         IDENTITY (1, 1) NOT NULL,
    [InsuredRequestVehicleId] BIGINT         NOT NULL,
    [Key]                     NVARCHAR (200) NOT NULL,
    [Value]                   NVARCHAR (200) NOT NULL,
    [CreatedYear]             INT            NOT NULL,
    [Description]             TEXT           NULL,
    [CreatedBy]               BIGINT         NULL,
    [CreatedAt]               DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [UpdatedBy]               BIGINT         NULL,
    [UpdatedAt]               DATETIME2 (7)  DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsuredRequestVehicleDetails_InsuredRequestVehicle] FOREIGN KEY ([InsuredRequestVehicleId]) REFERENCES [dbo].[InsuredRequestVehicle] ([Id])
);

