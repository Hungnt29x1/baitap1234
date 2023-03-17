IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Group] (
    [GroupId] uniqueidentifier NOT NULL DEFAULT ((newid())),
    [GroupCode] varchar(10) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Status] bit NOT NULL,
    [CreatedPerson] nvarchar(max) NULL,
    [FromDate] datetime2 NOT NULL,
    [ToDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Group] PRIMARY KEY ([GroupId])
);
GO

CREATE TABLE [Role] (
    [RoleId] uniqueidentifier NOT NULL DEFAULT ((newid())),
    [RoleCode] varchar(10) NOT NULL,
    [RoleDescription] nvarchar(50) NOT NULL,
    [Alias] nvarchar(max) NULL,
    [Status] bit NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([RoleId])
);
GO

CREATE TABLE [User] (
    [UserId] uniqueidentifier NOT NULL DEFAULT ((newid())),
    [UserName] varchar(10) NOT NULL,
    [Password] nvarchar(20) NOT NULL,
    [RoleId] uniqueidentifier NULL,
    [Status] bit NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_User_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([RoleId]) ON DELETE NO ACTION
);
GO

CREATE UNIQUE INDEX [IX_Group_GroupCode] ON [Group] ([GroupCode]);
GO

CREATE UNIQUE INDEX [IX_Role_RoleCode] ON [Role] ([RoleCode]);
GO

CREATE INDEX [IX_User_RoleId] ON [User] ([RoleId]);
GO

CREATE UNIQUE INDEX [IX_User_UserName] ON [User] ([UserName]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230313030915_123', N'5.0.17');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [User] ADD [CreatedPerson] nvarchar(max) NULL;
GO

ALTER TABLE [User] ADD [Description] nvarchar(max) NULL;
GO

ALTER TABLE [User] ADD [Phone] nvarchar(max) NULL;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Group]') AND [c].[name] = N'ToDate');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Group] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Group] ALTER COLUMN [ToDate] datetime2 NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Group]') AND [c].[name] = N'FromDate');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Group] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Group] ALTER COLUMN [FromDate] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230314082420_133', N'5.0.17');
GO

COMMIT;
GO

