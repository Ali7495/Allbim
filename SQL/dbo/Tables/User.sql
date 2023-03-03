CREATE TABLE [dbo].[User] (
    [Id]                     BIGINT           IDENTITY (1, 1) NOT NULL,
    [PersonId]               BIGINT           NOT NULL,
    [Code]                   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Username]               NVARCHAR (50)    NOT NULL,
    [Password]               NVARCHAR (200)   NULL,
    [Email]                  NVARCHAR (200)   NULL,
    [EmailVerifiedAt]        DATETIME2 (7)    NULL,
    [IsEnable]               TINYINT          DEFAULT ((0)) NULL,
    [IsVerified]             TINYINT          DEFAULT ((0)) NULL,
    [IsTwoStepVerification]  TINYINT          DEFAULT ((0)) NULL,
    [TwoStepCode]            NVARCHAR (10)    NULL,
    [TwoStepExpiration]      DATETIME2 (7)    NULL,
    [IsLoginNotify]          TINYINT          DEFAULT ((0)) NULL,
    [VerificationCode]       VARCHAR (10)     NULL,
    [VerificationExpiration] DATETIME2 (7)    NULL,
    [LastLogOnDate]          DATETIME2 (7)    NULL,
    [CreatedDate]            DATETIME2 (7)    NULL,
    [ChangePasswordCode]     VARCHAR (10)     NULL,
    [IsDeleted]              BIT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);



