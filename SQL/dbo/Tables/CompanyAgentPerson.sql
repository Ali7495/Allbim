CREATE TABLE [dbo].[CompanyAgentPerson] (
    [Id]             BIGINT        IDENTITY (1, 1) NOT NULL,
    [CompanyAgentId] BIGINT        NULL,
    [PersonId]       BIGINT        NULL,
    [Position]       NVARCHAR (50) NULL,
    [CreatedDate]    DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    [IsDeleted]      BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CompanyAgentPerson_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CompanyAgentPerson_CompanyAgentId] FOREIGN KEY ([CompanyAgentId]) REFERENCES [dbo].[CompanyAgent] ([Id]),
    CONSTRAINT [FK_CompanyAgentPerson_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);

