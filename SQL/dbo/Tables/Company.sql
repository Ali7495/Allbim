CREATE TABLE [dbo].[Company] (
    [Id]                              BIGINT           IDENTITY (1, 1) NOT NULL,
    [Code]                            UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]                            NVARCHAR (200)   NOT NULL,
    [Description]                     TEXT             NULL,
    [IsDeleted]                       BIT              DEFAULT ((0)) NOT NULL,
    [AvatarUrl]                       NVARCHAR (255)   NULL,
    [Summary]                         VARCHAR (MAX)    NULL,
    [ArticleId]                       BIGINT           NULL,
    [EstablishedYear]                 NVARCHAR (25)    NULL,
    [BranchNumber]                    INT              NULL,
    [WealthLevel]                     INT              NULL,
    [DamagePaymentSatisfactionRating] INT              NULL,
    [IsInsurer]                       BIT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Company_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([Id])
);



