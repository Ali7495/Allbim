CREATE TABLE [dbo].[PolicyRequestStatus] (
    [Id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50) NULL,
    [isDeleted] BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PolicyRequestStatus_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

