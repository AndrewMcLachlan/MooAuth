CREATE TABLE dbo.Actor
(
    Id INT NOT NULL IDENTITY(1,1),
    ExternalId NVARCHAR(100) NOT NULL,
    ConnectorId INT NOT NULL,
    ActorTypeId INT NOT NULL,
    Created DATETIME2 NOT NULL,
    Modified DATETIME2 NOT NULL,
    CONSTRAINT PK_Actor PRIMARY KEY (Id),
    CONSTRAINT FK_Actor_Connector FOREIGN KEY (ConnectorId) REFERENCES Connector (Id),
    CONSTRAINT FK_Actor_ActorType FOREIGN KEY (ActorTypeId) REFERENCES ActorType (Id)
);