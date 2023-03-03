ALTER TABLE dbo.InsurerTermDetail
    ADD CONSTRAINT FK_InsurerTermDetail_InsurerTerm FOREIGN KEY (InsurerTermId) REFERENCES dbo.InsurerTerm (Id)