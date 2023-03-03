
BEGIN TRANSACTION;
UPDATE Article
SET ArticleTypeId=1
WHERE Slug IS NOT NULL;



CREATE TABLE dbo.ResourceOperation (
                                       Id BIGINT IDENTITY
    ,Title NVARCHAR(100) NULL
    ,Class NVARCHAR(100) NULL
    ,[Key] NVARCHAR(100) NULL
    ,ResourceId BIGINT NULL
    ,PermissionId BIGINT NULL
    ,IsDeleted BIT NOT NULL DEFAULT 0
    ,CONSTRAINT PK_ResourceOperation_Id PRIMARY KEY CLUSTERED (Id)
    );

ALTER TABLE dbo.ResourceOperation
    ADD CONSTRAINT FK_ResourceOperation_ResourceId FOREIGN KEY (ResourceId) REFERENCES dbo.Resource (Id);

ALTER TABLE dbo.ResourceOperation
    ADD CONSTRAINT FK_ResourceOperation_PermissionId FOREIGN KEY (PermissionId) REFERENCES dbo.Permission (Id);


SET IDENTITY_INSERT Resource ON;
INSERT INTO dbo.Resource(Id, Name) VALUES(1, N'policy-request')
INSERT INTO dbo.Resource(Id, Name) VALUES(2, N'person')
INSERT INTO dbo.Resource(Id, Name) VALUES(3, N'user')
INSERT INTO dbo.Resource(Id, Name) VALUES(4, N'company')

                                                                                                                                                                                 
SET IDENTITY_INSERT Resource OFF;


COMMIT;

