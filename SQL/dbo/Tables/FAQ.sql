CREATE TABLE [dbo].[FAQ] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Question]  NVARCHAR (MAX) NULL,
    [Answer]    NVARCHAR (MAX) NULL,
    [IsDeleted] BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FAQ_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

