CREATE TABLE [dbo].[RegisterTemp] (
    [Id]             BIGINT        IDENTITY (1, 1) NOT NULL,
    [Mobile]         VARCHAR (20)  NULL,
    [Code]           VARCHAR (10)  NULL,
    [Ip]             VARCHAR (50)  NULL,
    [ExpirationDate] DATETIME2 (7) NULL,
    [CreatedDate]    DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_RegisterTemp_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

