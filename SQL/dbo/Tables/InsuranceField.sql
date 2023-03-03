CREATE TABLE [dbo].[InsuranceField] (
    [Id]                   BIGINT         IDENTITY (1, 1) NOT NULL,
    [InsuranceId]          BIGINT         NOT NULL,
    [Key]                  NVARCHAR (200) NOT NULL,
    [Type]                 NVARCHAR (200) NULL,
    [Description]          NVARCHAR (MAX) NULL,
    [InsuranceFieldTypeId] BIGINT         DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsuranceField_Insurance] FOREIGN KEY ([InsuranceId]) REFERENCES [dbo].[Insurance] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

