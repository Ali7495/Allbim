
BEGIN TRANSACTION;
CREATE TABLE dbo.CompanyAgentPerson (
                                        Id BIGINT IDENTITY
    ,CompanyAgentId BIGINT NULL
    ,PersonId BIGINT NULL
    ,Position NVARCHAR(50) NULL
    ,CreatedDate DATETIME2 NOT NULL DEFAULT (GETDATE())
    ,IsDeleted BIT NOT NULL DEFAULT (0)
    ,CONSTRAINT PK_CompanyAgentPerson_Id PRIMARY KEY CLUSTERED (Id)
) ON [PRIMARY]

ALTER TABLE dbo.CompanyAgentPerson
    ADD CONSTRAINT FK_CompanyAgentPerson_PersonId FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id)

ALTER TABLE dbo.CompanyAgentPerson
    ADD CONSTRAINT FK_CompanyAgentPerson_CompanyAgentId FOREIGN KEY (CompanyAgentId) REFERENCES dbo.CompanyAgent (Id)


CREATE TABLE dbo.PolicyRequestFactorDetails (
                                                Id BIGINT IDENTITY
    ,PolicyRequestFactorId BIGINT NULL
    ,Amount Decimal NOT NULL
    ,CalculationTypeId TINYINT NULL
    ,Description NVARCHAR(max) NULL
    ,CreatedDate DATETIME2 NOT NULL DEFAULT (GETDATE())
    ,IsDeleted BIT NOT NULL DEFAULT (0)
    ,CONSTRAINT PK_PolicyRequestFactorDetails_Id PRIMARY KEY CLUSTERED (Id)
) ON [PRIMARY]


ALTER TABLE dbo.PolicyRequestFactorDetails
    ADD CONSTRAINT FK_PolicyRequestFactorDetails_PolicyRequestFactorId FOREIGN KEY (PolicyRequestFactorId) REFERENCES dbo.PolicyRequestFactor (Id)

ALTER TABLE dbo.Payment
DROP COLUMN PaidDateTime;
ALTER TABLE dbo.Payment
Add UpdatedDateTime DATETIME2 NOT NULL DEFAULT (GETDATE());

COMMIT;
