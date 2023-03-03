ALTER TABLE dbo.PolicyRequest
    ADD AgentSelectionTypeId TINYINT NOT NULL DEFAULT 1,
  AgentSelectedId BIGINT NULL;


ALTER TABLE dbo.PolicyRequest
    ADD CONSTRAINT FK_PolicyRequest_AgentSelectedId FOREIGN KEY (AgentSelectedId) REFERENCES dbo.CompanyAgent (Id)