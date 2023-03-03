CREATE TABLE [dbo].[PersonCompany] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [PersonId]    BIGINT        NULL,
    [CompanyId]   BIGINT        NULL,
    [Position]    NVARCHAR (50) NULL,
    [CreatedDate] DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    [IsDeleted]   BIT           DEFAULT ((0)) NOT NULL,
    [ParentId]    BIGINT        NULL,
    CONSTRAINT [PK_table1_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonCompany_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id]),
    CONSTRAINT [FK_PersonCompany_PersonCompanyId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[PersonCompany] ([Id]),
    CONSTRAINT [FK_PersonCompany_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);

