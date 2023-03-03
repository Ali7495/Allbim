
BEGIN TRANSACTION;
CREATE TABLE dbo.PaymentGatewayDetails(
                                          Id BIGINT IDENTITY
    ,PaymentGatewayId BIGINT NOT NULL
    ,[Key] NVARCHAR(200) NOT NULL
    ,[Value] NVARCHAR(MAX) NOT NULL
    ,IsDeleted BIT NOT NULL DEFAULT (0)
    ,PRIMARY KEY CLUSTERED (Id)
    ) ON [PRIMARY]

ALTER TABLE dbo.PaymentGatewayDetails
    ADD CONSTRAINT FK_PaymentGatewayDetails_PaymentGateway FOREIGN KEY (PaymentGatewayId) REFERENCES dbo.PaymentGateway (Id)


ALTER TABLE dbo.PaymentGateway
    DROP COLUMN Username;

ALTER TABLE dbo.PaymentGateway
    DROP COLUMN Password;
ALTER TABLE dbo.PaymentGateway
    DROP COLUMN CardNumber;
ALTER TABLE dbo.PaymentGateway
    DROP COLUMN AccountNumber;
ALTER TABLE dbo.PaymentGateway
    DROP COLUMN AllowOnline;
ALTER TABLE dbo.PaymentGateway
    DROP COLUMN AllowManual;








COMMIT;


BEGIN TRANSACTION;


ALTER TABLE dbo.PaymentGateway
    ADD IsDeleted bit NOT NULL DEFAULT 0;

ALTER TABLE dbo.PaymentGateway
    ADD AllowOnline bit NOT NULL DEFAULT 1;

ALTER TABLE dbo.PaymentGateway
    ADD AllowManual bit NOT NULL DEFAULT 1;



COMMIT;
