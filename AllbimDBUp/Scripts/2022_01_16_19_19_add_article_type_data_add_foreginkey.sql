
BEGIN TRANSACTION;
SET IDENTITY_INSERT ArticleType ON
INSERT INTO dbo.ArticleType(Id, Name, IsDeleted) VALUES(1, N'صفحه', CONVERT(bit, 'False'))
INSERT INTO dbo.ArticleType(Id, Name, IsDeleted) VALUES(2, N'مقاله', CONVERT(bit, 'False'))
SET IDENTITY_INSERT ArticleType OFF;

COMMIT;



BEGIN TRANSACTION;
ALTER TABLE dbo.Article
    ADD ArticleTypeId bigint NOT NULL DEFAULT 2

ALTER TABLE dbo.Article
    ADD CONSTRAINT FK_Article_ArticleTypeId FOREIGN KEY (ArticleTypeId) REFERENCES dbo.ArticleType (Id)
COMMIT;

