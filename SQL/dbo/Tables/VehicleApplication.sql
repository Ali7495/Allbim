CREATE TABLE [dbo].[VehicleApplication] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [VehicleTypeId] BIGINT         NOT NULL,
    [Name]          NVARCHAR (200) NULL,
    CONSTRAINT [PK_VehicleApplication_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VehicleApplication_VehicleTypeId] FOREIGN KEY ([VehicleTypeId]) REFERENCES [dbo].[VehicleType] ([Id])
);

