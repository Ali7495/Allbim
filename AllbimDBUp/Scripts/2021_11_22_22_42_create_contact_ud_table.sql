CREATE TABLE dbo.ContactUs (
                               Id BIGINT IDENTITY
    ,Email NVARCHAR(300) NULL
    ,Title NVARCHAR(300) NULL
    ,Description NVARCHAR(MAX) NULL
    ,Answer NVARCHAR(MAX) NULL
    ,CreatedDateTime DATETIME2 NOT NULL DEFAULT GETDATE()
    ,IsDeleted BIT NOT NULL DEFAULT 0
    ,CONSTRAINT PK_ContactUs_Id PRIMARY KEY CLUSTERED (Id)
)
