
BEGIN TRANSACTION;
ALTER TABLE dbo.Payment
    Add PaymentGatewayId BIGINT NOT NULL;

ALTER TABLE dbo.Payment
    ADD CONSTRAINT FK_Payment_PaymentGatewayId FOREIGN KEY (PaymentGatewayId) REFERENCES dbo.PaymentGateway (Id)


COMMIT;
