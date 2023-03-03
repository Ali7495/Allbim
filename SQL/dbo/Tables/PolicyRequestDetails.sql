CREATE TABLE [dbo].[PolicyRequestDetails] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [PolicyRequestId] BIGINT         NOT NULL,
    [Type]            TINYINT        NOT NULL,
    [Field]           NVARCHAR (200) NOT NULL,
    [Criteria]        NVARCHAR (100) NOT NULL,
    [Value]           NVARCHAR (100) NOT NULL,
    [Discount]        NVARCHAR (100) NOT NULL,
    [CalculationType] NVARCHAR (100) NOT NULL,
    [UserInput]       VARCHAR (100)  NULL,
    [InsurerTermId]   BIGINT         NULL,
    [IsCumulative]    BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PolicyRequestDetails_InsurerTermId] FOREIGN KEY ([InsurerTermId]) REFERENCES [dbo].[InsurerTerm] ([Id]),
    CONSTRAINT [FK_PolicyRequestDetails_PolicyRequest] FOREIGN KEY ([PolicyRequestId]) REFERENCES [dbo].[PolicyRequest] ([Id])
);



