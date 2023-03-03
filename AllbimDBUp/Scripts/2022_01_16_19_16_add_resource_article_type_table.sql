
BEGIN TRANSACTION;

CREATE TABLE dbo.ArticleType (
     Id BIGINT IDENTITY
    ,Name NVARCHAR(50) NULL
    ,IsDeleted BIT NOT NULL DEFAULT 0
    ,CONSTRAINT PK_ArticleType_Id PRIMARY KEY CLUSTERED (Id)
)
    
CREATE TABLE dbo.Resource (
     Id BIGINT IDENTITY
    ,Name NVARCHAR(50) NULL
    ,IsDeleted BIT NOT NULL DEFAULT 0
    ,CONSTRAINT PK_Resource_Id PRIMARY KEY CLUSTERED (Id)
)
    

COMMIT;