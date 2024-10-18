CREATE TABLE [dbo].[Connector] (
    [Id] INT NOT NULL IDENTITY(1,1),
    [Name] NVARCHAR(50) NOT NULL,
    [ConnectorTypeId] INT NOT NULL,
    [ClientId] NVARCHAR(100) NOT NULL,
    [Slug] NVARCHAR(50) NOT NULL,
    [Config] NVARCHAR(4000) NOT NULL,
    [Created] DATETIME2 NOT NULL CONSTRAINT DF_Connector_Created DEFAULT SYSUTCDATETIME(),
    [Modified] DATETIME2 NOT NULL CONSTRAINT DF_Connector_Modified DEFAULT SYSUTCDATETIME(),
    CONSTRAINT [PK_Connector] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Connector_ConnectorType] FOREIGN KEY ([ConnectorTypeId]) REFERENCES [dbo].[ConnectorType] ([Id])
);