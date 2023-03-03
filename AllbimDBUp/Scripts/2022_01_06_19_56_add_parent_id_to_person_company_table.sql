
BEGIN TRANSACTION;
ALTER TABLE PersonCompany
    ADD ParentId BIGINT NULL;

ALTER TABLE PersonCompany
    ADD CONSTRAINT FK_PersonCompany_PersonCompanyId FOREIGN KEY (ParentId) REFERENCES dbo.PersonCompany (Id)

COMMIT;