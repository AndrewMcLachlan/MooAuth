CREATE TABLE ActorRoleResource (
    Id INT NOT NULL IDENTITY(1,1),
    ActorId INT NOT NULL,
    RoleId INT NOT NULL,
    DataSourceId INT NULL,
    ResourceValue NVARCHAR(255) NULL,
    Created DATETIME2 NOT NULL,
    Modified DATETIME2 NOT NULL,
    CONSTRAINT PK_ActorRoleResource PRIMARY KEY (Id),
    CONSTRAINT FK_ActorRoleResource_Actor FOREIGN KEY (ActorId) REFERENCES Actor (Id),
    CONSTRAINT FK_ActorRoleResource_DataSource FOREIGN KEY (DataSourceId) REFERENCES DataSource (Id)
  )