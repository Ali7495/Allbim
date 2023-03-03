ALTER TABLE PolicyRequestIssue
    ADD CONSTRAINT FK_PolicyRequestIssue_IssueSessionId FOREIGN KEY (IssueSessionId) REFERENCES dbo.IssueSession (Id)
