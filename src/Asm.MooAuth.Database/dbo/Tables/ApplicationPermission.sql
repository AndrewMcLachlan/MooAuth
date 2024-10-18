CREATE TABLE [dbo].[ApplicationPermission] (
    [Id] INT NOT NULL PRIMARY KEY,
    [ApplicationId] INT NOT NULL,
    [PermissionId] INT NOT NULL,
    [Created] DATETIME2 NOT NULL CONSTRAINT DF_ApplicationPermission_Created DEFAULT SYSUTCDATETIME(),
    [Modified] DATETIME2 NOT NULL CONSTRAINT DF_ApplicationPermission_Modified DEFAULT SYSUTCDATETIME(),
);