CREATE TABLE [dbo].[PolicyRequest] (
    [Id]                    BIGINT           IDENTITY (1, 1) NOT NULL,
    [Code]                  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [RequestPersonId]       BIGINT           NOT NULL,
    [Title]                 NVARCHAR (100)   NULL,
    [InsurerId]             BIGINT           NOT NULL,
    [PolicyNumber]          NVARCHAR (100)   NOT NULL,
    [Description]           NVARCHAR (MAX)   NULL,
    [IsDeleted]             TINYINT          DEFAULT ((0)) NOT NULL,
    [CreatedDate]           DATETIME2 (7)    DEFAULT (getdate()) NULL,
    [PolicyRequestStatusId] BIGINT           NULL,
    [ReviewerId]            BIGINT           NULL,
    [AgentSelectionTypeId]  TINYINT          DEFAULT ((1)) NOT NULL,
    [AgentSelectedId]       BIGINT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PolicyRequest_AgentSelectedId] FOREIGN KEY ([AgentSelectedId]) REFERENCES [dbo].[CompanyAgent] ([Id]),
    CONSTRAINT [FK_PolicyRequest_Insurer] FOREIGN KEY ([InsurerId]) REFERENCES [dbo].[Insurer] ([Id]),
    CONSTRAINT [FK_PolicyRequest_Person] FOREIGN KEY ([RequestPersonId]) REFERENCES [dbo].[Person] ([Id]),
    CONSTRAINT [FK_PolicyRequest_PolicyRequestStatusId] FOREIGN KEY ([PolicyRequestStatusId]) REFERENCES [dbo].[PolicyRequestStatus] ([Id]),
    CONSTRAINT [FK_PolicyRequest_ReviewerId] FOREIGN KEY ([ReviewerId]) REFERENCES [dbo].[Person] ([Id])
);



