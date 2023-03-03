CREATE TABLE [dbo].[VehicleRuleCategory] (
    [Id]   BIGINT       IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NULL,
    CONSTRAINT [PK_VehicleRuleCategory_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

