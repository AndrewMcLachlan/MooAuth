/*
Post-Deployment Script Template
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.
 Use SQLCMD syntax to include a file in the post-deployment script.
 Example:      :r .\myfile.sql
 Use SQLCMD syntax to reference a variable in the post-deployment script.
 Example:      :setvar TableName MyTable
               SELECT * FROM [$(TableName)]
--------------------------------------------------------------------------------------
*/

-- Merge Value into ConnectorType
MERGE INTO [dbo].[ConnectorType] AS target
USING (VALUES
    (0, 'Entra'),
    (1, 'Auth0')
) AS source (Id, Name)
ON target.Id = source.Id
WHEN MATCHED AND target.Name <> source.Name THEN  UPDATE SET target.Name = source.Name
WHEN NOT MATCHED BY TARGET THEN INSERT (Id, Name) VALUES (source.Id, source.Name);

-- Merge Value into DataSourceType
MERGE INTO [dbo].[DataSourceType] AS target
USING (VALUES
    (0, 'FreeText'),
    (1, 'PickList'),
    (2, 'ApiPickList'),
    (3, 'Checkbox')
) AS source (Id, Name)
ON target.Id = source.Id
WHEN MATCHED AND target.Name <> source.Name THEN UPDATE SET target.Name = source.Name
WHEN NOT MATCHED BY TARGET THEN INSERT (Id, Name) VALUES (source.Id, source.Name);

-- Merge Value into ActorType
MERGE INTO [dbo].[ActorType] AS target
USING (VALUES
    (0, 'User'),
    (1, 'Group')
) AS source (Id, Name)
ON target.Id = source.Id
WHEN MATCHED AND target.Name <> source.Name THEN UPDATE SET target.Name = source.Name
WHEN NOT MATCHED BY TARGET THEN INSERT (Id, Name) VALUES (source.Id, source.Name);