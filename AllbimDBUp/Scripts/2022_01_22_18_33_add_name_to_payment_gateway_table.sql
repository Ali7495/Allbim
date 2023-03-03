
BEGIN TRANSACTION;
ALTER TABLE dbo.PaymentGateway
    ADD Name nvarchar(200) ;



COMMIT;
