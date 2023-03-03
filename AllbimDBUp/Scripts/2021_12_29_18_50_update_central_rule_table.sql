
BEGIN TRANSACTION;

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 3,
    [Type] = 1
Where FieldType = N'VehicleRuleCategory'

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 21
Where FieldType = N'MaxDelayPenaltyDay'

    COMMIT;