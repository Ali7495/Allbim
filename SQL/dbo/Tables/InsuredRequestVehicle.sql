CREATE TABLE [dbo].[InsuredRequestVehicle] (
    [Id]               BIGINT IDENTITY (1, 1) NOT NULL,
    [InsuredRequestId] BIGINT NOT NULL,
    [OwnerPersonId]    BIGINT NULL,
    [OwnerCompanyId]   BIGINT NULL,
    [VehicleId]        BIGINT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsuredRequestVehicle_Company] FOREIGN KEY ([OwnerCompanyId]) REFERENCES [dbo].[Company] ([Id]),
    CONSTRAINT [FK_InsuredRequestVehicle_InsuredRequest] FOREIGN KEY ([InsuredRequestId]) REFERENCES [dbo].[InsuredRequest] ([Id]),
    CONSTRAINT [FK_InsuredRequestVehicle_Person] FOREIGN KEY ([OwnerPersonId]) REFERENCES [dbo].[Person] ([Id]),
    CONSTRAINT [FK_InsuredRequestVehicle_Vehicle] FOREIGN KEY ([VehicleId]) REFERENCES [dbo].[Vehicle] ([Id])
);



