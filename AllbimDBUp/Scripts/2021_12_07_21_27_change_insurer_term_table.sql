
ALTER TABLE [dbo].[InsurerTerm] ADD [ConditionTypeId] TINYINT NULL ;

ALTER TABLE [dbo].[InsurerTerm] ADD [InsuranceTermTypeId] BIGINT NULL ;
ALTER TABLE dbo.InsurerTerm
    ADD CONSTRAINT FK_InsurerTerm_InsuranceTermType FOREIGN KEY (InsuranceTermTypeId) REFERENCES dbo.InsuranceTermType (Id) ;