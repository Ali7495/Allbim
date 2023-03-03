
BEGIN TRANSACTION;


----------------- Update Insurer Term -----------------
UPDATE [InsurerTerm]
SET [InsuranceTermTypeId] = 60,
    [ConditionTypeId] = 1
WHERE [Field] = N'BodyBaseCost'

UPDATE [InsurerTerm]
SET [InsuranceTermTypeId] = 61,
    [ConditionTypeId] = 1
WHERE [Field] = N'MaxFinancialCover'


UPDATE [InsurerTerm]
SET [InsuranceTermTypeId] = 62,
    [ConditionTypeId] = 1
WHERE [Field] = N'CreditDuration'





UPDATE InsurerTerm
SET CalculationTypeId=1
WHERE CalculationType='-';
UPDATE InsurerTerm
SET CalculationTypeId=2
WHERE CalculationType='+';


UPDATE InsuranceCentralRule
SET CalculationTypeId=2,ConditionTypeId=1;




ALTER TABLE InsurerTerm
DROP COLUMN [Criteria],[CalculationType],[Type],[Field];


ALTER TABLE InsuranceCentralRule
DROP COLUMN [Criteria],[FieldId],[Type];

UPDATE InsurerTerm
SET CreatedBy=1;

COMMIT;