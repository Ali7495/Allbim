CREATE TABLE [dbo].[CompanyCenterSchedule] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [CompanyCenterId] BIGINT         NULL,
    [Name]            NVARCHAR (200) NOT NULL,
    [Description]     NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CompanyCenterSchedule_CompanyCenterId] FOREIGN KEY ([CompanyCenterId]) REFERENCES [dbo].[CompanyCenter] ([Id])
);

