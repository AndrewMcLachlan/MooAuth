CREATE TABLE [dbo].[RolePermission] (
    RoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    [Created] DATETIME2 NOT NULL CONSTRAINT DF_RolePermission_Created DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_RolePermission PRIMARY KEY (RoleId, PermissionId),
    CONSTRAINT FK_RolePermission_Role FOREIGN KEY (RoleId) REFERENCES [dbo].[Role](Id),
    CONSTRAINT FK_RolePermission_Permssion FOREIGN KEY (PermissionId) REFERENCES [dbo].[Permission](Id),
)