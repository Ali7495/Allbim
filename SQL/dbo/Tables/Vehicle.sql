CREATE TABLE [dbo].[Vehicle] (
    [Id]                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR (200) NOT NULL,
    [Description]           TEXT           NULL,
    [CreatedBy]             BIGINT         NULL,
    [CreatedAt]             DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [UpdatedBy]             BIGINT         NULL,
    [UpdatedAt]             DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [VehicleBrandId]        BIGINT         NOT NULL,
    [VehicleRuleCategoryId] BIGINT         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Vehicle_VehicleBrand] FOREIGN KEY ([VehicleBrandId]) REFERENCES [dbo].[VehicleBrand] ([Id]),
    CONSTRAINT [FK_Vehicle_VehicleRuleCategoryId] FOREIGN KEY ([VehicleRuleCategoryId]) REFERENCES [dbo].[VehicleRuleCategory] ([Id])
);



