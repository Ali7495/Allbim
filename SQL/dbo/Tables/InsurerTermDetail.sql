CREATE TABLE [dbo].[InsurerTermDetail] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [InsurerTermId]   BIGINT         NOT NULL,
    [Order]           INT            NULL,
    [ParentId]        BIGINT         NULL,
    [Field]           NVARCHAR (200) NULL,
    [Criteria]        NVARCHAR (100) NULL,
    [Value]           NVARCHAR (100) NULL,
    [Discount]        NVARCHAR (100) NULL,
    [CalculationType] NVARCHAR (100) NULL,
    [IsCumulative]    BIT            DEFAULT ((1)) NOT NULL,
    [PricingTypeId]   TINYINT        NULL,
    CONSTRAINT [PK_InsurerTermDetail_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InsurerTermDetail_InsurerTerm] FOREIGN KEY ([InsurerTermId]) REFERENCES [dbo].[InsurerTerm] ([Id])
);

