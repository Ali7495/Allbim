CREATE TABLE [dbo].[Insurer] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [InsuranceId] BIGINT        NOT NULL,
    [CompanyId]   BIGINT        NOT NULL,
    [CreatedBy]   BIGINT        NULL,
    [CreatedAt]   DATETIME2 (7) DEFAULT (getdate()) NULL,
    [UpdatedBy]   BIGINT        NULL,
    [UpdatedAt]   DATETIME2 (7) DEFAULT (getdate()) NULL,
    [ArticleId]   BIGINT        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Insurer_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([Id]),
    CONSTRAINT [FK_Insurer_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Insurer_Insurance] FOREIGN KEY ([InsuranceId]) REFERENCES [dbo].[Insurance] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);



