CREATE TABLE [dbo].[Person] (
    [Id]           BIGINT           IDENTITY (1, 1) NOT NULL,
    [Code]         UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [FirstName]    NVARCHAR (100)   NULL,
    [LastName]     NVARCHAR (100)   NULL,
    [NationalCode] NVARCHAR (10)    NULL,
    [Identity]     NVARCHAR (10)    NULL,
    [FatherName]   NVARCHAR (100)   NULL,
    [BirthDate]    DATETIME2 (7)    NULL,
    [GenderId]     TINYINT          NOT NULL,
    [MarriageId]   TINYINT          NULL,
    [MillitaryId]  TINYINT          NULL,
    [IsDeleted]    BIT              DEFAULT ((0)) NOT NULL,
    [JobName]      VARCHAR (1000)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



