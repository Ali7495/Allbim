CREATE TABLE [dbo].[PolicyRequestIssue] (
    [Id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [PolicyRequestId]   BIGINT         NOT NULL,
    [EmailAddress]      NVARCHAR (100) NULL,
    [NeedPrint]         BIT            DEFAULT ((0)) NULL,
    [ReceiverAddressId] BIGINT         NULL,
    [ReceiveDate]       DATETIME2 (7)  NULL,
    [IssueSessionId]    BIGINT         NULL,
    [WalletId]          BIGINT         NULL,
    [Description]       NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PolicyRequestIssue_IssueSessionId] FOREIGN KEY ([IssueSessionId]) REFERENCES [dbo].[IssueSession] ([Id]),
    CONSTRAINT [FK_PolicyRequestIssue_PolicyRequestId] FOREIGN KEY ([PolicyRequestId]) REFERENCES [dbo].[PolicyRequest] ([Id]),
    CONSTRAINT [FK_PolicyRequestIssue_ReceiverAddressId] FOREIGN KEY ([ReceiverAddressId]) REFERENCES [dbo].[Address] ([Id])
);

