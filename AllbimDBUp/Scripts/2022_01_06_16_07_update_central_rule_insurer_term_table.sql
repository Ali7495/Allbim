
BEGIN TRANSACTION;




------------- Insurance Field ---------------

SET IDENTITY_INSERT [dbo].[InsuranceField] ON 
INSERT [InsuranceField] (Id,InsuranceId,[Key],[Type],[Description],InsuranceFieldTypeId) VALUES(177,6,N'BodyDamage',N'int',N'خسارت بدنه',2)
INSERT [InsuranceField] (Id,InsuranceId,[Key],[Type],[Description],InsuranceFieldTypeId) VALUES(178,6,N'NoDamage',N'int',N'عدم خسارت',2)
INSERT [InsuranceField] (Id,InsuranceId,[Key],[Type],[Description],InsuranceFieldTypeId) VALUES(179,11,N'MultipleCondition',N'int',N'شرایط چندگانه',2)
SET IDENTITY_INSERT [dbo].[InsuranceField] OFF


------------- Central Rule Type ---------------

SET IDENTITY_INSERT [dbo].[CentralRuleType] ON 
INSERT [CentralRuleType] (Id,InsuranceFieldId,RuleCaption,[Order],Field,RelatedResource,ResourceTypeId,PricingTypeId) VALUES(24,177,N'خسارت بدنه',1,N'BodyDamage',NULL,3,2)
INSERT [CentralRuleType] (Id,InsuranceFieldId,RuleCaption,[Order],Field,RelatedResource,ResourceTypeId,PricingTypeId) VALUES(25,178,N'عدم خسارت',1,N'NoDamage',NULL,3,2)
SET IDENTITY_INSERT [dbo].[CentralRuleType] OFF


------------- Insurance Central Rule Update ---------------

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 24
WHERE FieldType = N'BodyDamage'

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 25
WHERE FieldType = N'NoDamage'


------------- Insurance Term Type ---------------

    SET IDENTITY_INSERT [dbo].[InsuranceTermType] ON
INSERT [InsuranceTermType] (Id,InsuranceFieldId,TermCaption,[Order],Field,RelatedResource,ResourceTypeId,PricingTypeId) VALUES(63,179,N'شرایط چندگانه',1,N'MultipleCondition',NULL,3,2)
SET IDENTITY_INSERT [dbo].[InsuranceTermType] OFF


------------- Insurer Term ---------------

UPDATE [InsurerTerm]
SET [InsuranceTermTypeId] = 63,
    [ConditionTypeId] = 1
WHERE Id = 64






UPDATE InsuranceCentralRule
SET PricingTypeId = 1
WHERE FieldType = N'VehicleRuleCategory'

UPDATE InsuranceCentralRule
SET PricingTypeId = 2
WHERE FieldType = N'NoDamage'


UPDATE InsuranceCentralRule
SET PricingTypeId = 2
WHERE FieldType = N'BodyDamage'


UPDATE InsuranceCentralRule
SET PricingTypeId = 1
WHERE FieldType = N'MaxDelayPenaltyDay'





COMMIT;