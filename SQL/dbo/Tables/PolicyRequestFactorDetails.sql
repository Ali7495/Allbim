CREATE TABLE [dbo].[PolicyRequestFactorDetails] (
    [Id]                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [PolicyRequestFactorId] BIGINT         NULL,
    [Amount]                DECIMAL (18)   NOT NULL,
    [CalculationTypeId]     TINYINT        NULL,
    [Description]           NVARCHAR (MAX) NULL,
    [CreatedDate]           DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [IsDeleted]             BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PolicyRequestFactorDetails_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PolicyRequestFactorDetails_PolicyRequestFactorId] FOREIGN KEY ([PolicyRequestFactorId]) REFERENCES [dbo].[PolicyRequestFactor] ([Id])
);

