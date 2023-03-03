BEGIN TRANSACTION;

UPDATE dbo.Role SET ParentId = 3 WHERE Id = 1;

UPDATE dbo.Role SET ParentId = 1 WHERE Id = 2;

SET IDENTITY_INSERT role ON;
INSERT INTO dbo.Role(Id, Name, IsDeleted, Caption, ParentId) VALUES(5, N'CompanyExpert', CONVERT(bit, 'False'), N'کارشناس بیمه', 1)
INSERT INTO dbo.Role(Id, Name, IsDeleted, Caption, ParentId) VALUES(6, N'CompanyAgentExpert', CONVERT(bit, 'False'), N' کارشناس نمایندگی بیمه', 2)
SET IDENTITY_INSERT role OFF;


COMMIT;