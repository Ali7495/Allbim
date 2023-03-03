
BEGIN TRANSACTION;
IF EXISTS(SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) LIKE 'dbo' AND name like 'PaymentStatus')
DROP TABLE dbo.PaymentStatus;
GO


CREATE TABLE dbo.PaymentStatus (
     Id BIGINT IDENTITY
    ,Name NVARCHAR(50) NULL
    ,isDeleted BIT NOT NULL DEFAULT (0)
    ,CONSTRAINT PK_PaymentStatus_Id PRIMARY KEY CLUSTERED (Id)
) ON [PRIMARY]
SET IDENTITY_INSERT PaymentStatus ON;
INSERT INTO dbo.PaymentStatus(Id, Name) VALUES(1, N'در انتظار پرداخت')
SET IDENTITY_INSERT PaymentStatus OFF;

ALTER TABLE dbo.PolicyRequestFactor
    ADD IsDeleted bit NOT NULL DEFAULT 0;
ALTER TABLE dbo.Payment
    ADD IsDeleted bit NOT NULL DEFAULT 0;
-- ALTER TABLE dbo.Payment
-- DROP CONSTRAINT DF__Payment__Status__3C34F16F;
-- ALTER TABLE dbo.Payment
-- DROP COLUMN Status;
Go
ALTER TABLE dbo.Payment
    ADD PaymentStatusId BIGINT NOT NULL DEFAULT 1;
ALTER TABLE dbo.Payment
    ADD CONSTRAINT FK_Payment_PaymentStatusId FOREIGN KEY (PaymentStatusId) REFERENCES dbo.PaymentStatus (Id)


COMMIT;
