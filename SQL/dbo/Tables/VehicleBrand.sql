CREATE TABLE [dbo].[VehicleBrand] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (200) NOT NULL,
    [Description]   TEXT           NULL,
    [CreatedBy]     BIGINT         NULL,
    [CreatedAt]     DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [VehicleTypeId] BIGINT         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VehicleBrand_VehicleType] FOREIGN KEY ([VehicleTypeId]) REFERENCES [dbo].[VehicleType] ([Id])
);

