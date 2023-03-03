CREATE TABLE [dbo].[Address] (
    [Id]          BIGINT           IDENTITY (1, 1) NOT NULL,
    [Code]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]        NVARCHAR (200)   NULL,
    [CityId]      BIGINT           NOT NULL,
    [Description] TEXT             NULL,
    [ZoneNumber]  NVARCHAR (10)    NULL,
    [CreatedBy]   BIGINT           NULL,
    [CreatedAt]   DATETIME2 (7)    DEFAULT (getdate()) NULL,
    [UpdatedBy]   BIGINT           NULL,
    [UpdatedAt]   DATETIME2 (7)    DEFAULT (getdate()) NULL,
    [IsDeleted]   BIT              DEFAULT ((0)) NOT NULL,
    [Phone]       VARCHAR (20)     NULL,
    [Mobile]      VARCHAR (20)     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Address_City] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);



