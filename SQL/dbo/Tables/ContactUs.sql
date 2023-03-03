CREATE TABLE [dbo].[ContactUs] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [Email]           NVARCHAR (300) NULL,
    [Title]           NVARCHAR (300) NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [Answer]          NVARCHAR (MAX) NULL,
    [CreatedDateTime] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [IsDeleted]       BIT            DEFAULT ((0)) NOT NULL,
    [TrackingNumber]  BIGINT         NOT NULL,
    CONSTRAINT [PK_ContactUs_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

