
BEGIN TRANSACTION;

INSERT INTO dbo.Info([Key], Value, Slug, IsDeleted) VALUES(N'Phone', N'021222665802', N'phone', CONVERT(bit, 'False'))
    COMMIT;