IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190420034425_Initials')
BEGIN
    CREATE TABLE [tbl_Company] (
        [CompanyId] int NOT NULL IDENTITY,
        [CompanyName] nvarchar(255) NOT NULL,
        [LogoUrl] nvarchar(max) NULL,
        [Address] nvarchar(255) NOT NULL,
        [Email] nvarchar(255) NOT NULL,
        [Website] nvarchar(255) NOT NULL,
        [Language] nvarchar(255) NOT NULL,
        CONSTRAINT [PK_tbl_Company] PRIMARY KEY ([CompanyId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190420034425_Initials')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190420034425_Initials', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190420040040_CompanyContactTable')
BEGIN
    CREATE TABLE [tbl_CompanyContact] (
        [CompanyContactId] int NOT NULL IDENTITY,
        [ContactName] nvarchar(255) NOT NULL,
        [ContactEmail] nvarchar(255) NOT NULL,
        [ContactTelephone] nvarchar(255) NOT NULL,
        [CompanyId] int NOT NULL,
        CONSTRAINT [PK_tbl_CompanyContact] PRIMARY KEY ([CompanyContactId]),
        CONSTRAINT [FK_tbl_CompanyContact_tbl_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [tbl_Company] ([CompanyId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190420040040_CompanyContactTable')
BEGIN
    CREATE INDEX [IX_tbl_CompanyContact_CompanyId] ON [tbl_CompanyContact] ([CompanyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190420040040_CompanyContactTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190420040040_CompanyContactTable', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607124520_RemovedOneToManyCompanyContact')
BEGIN
    ALTER TABLE [tbl_CompanyContact] DROP CONSTRAINT [FK_tbl_CompanyContact_tbl_Company_CompanyId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607124520_RemovedOneToManyCompanyContact')
BEGIN
    DROP INDEX [IX_tbl_CompanyContact_CompanyId] ON [tbl_CompanyContact];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607124520_RemovedOneToManyCompanyContact')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_CompanyContact]') AND [c].[name] = N'CompanyId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [tbl_CompanyContact] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [tbl_CompanyContact] DROP COLUMN [CompanyId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607124520_RemovedOneToManyCompanyContact')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190607124520_RemovedOneToManyCompanyContact', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607130121_AddOneToOneCompany_CompanyContact')
BEGIN
    ALTER TABLE [tbl_CompanyContact] ADD [CompanyId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607130121_AddOneToOneCompany_CompanyContact')
BEGIN
    CREATE UNIQUE INDEX [IX_tbl_CompanyContact_tbl_Company_CompanyId] ON [tbl_CompanyContact] ([CompanyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607130121_AddOneToOneCompany_CompanyContact')
BEGIN
    ALTER TABLE [tbl_CompanyContact] ADD CONSTRAINT [FK_tbl_CompanyContact_tbl_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [tbl_Company] ([CompanyId]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607130121_AddOneToOneCompany_CompanyContact')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190607130121_AddOneToOneCompany_CompanyContact', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190814080618_BaseRecordTable')
BEGIN
    CREATE TABLE [tbl_BaseRecord] (
        [BaseRecordId] int NOT NULL IDENTITY,
        [KeyName] nvarchar(50) NOT NULL,
        [Description] nvarchar(255) NULL,
        CONSTRAINT [PK_tbl_BaseRecord] PRIMARY KEY ([BaseRecordId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190814080618_BaseRecordTable')
BEGIN
    CREATE TABLE [tbl_BaseRecordItem] (
        [BaseRecordItemId] int NOT NULL IDENTITY,
        [BaseRecordId] int NOT NULL,
        [ValueName] nvarchar(225) NOT NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_tbl_BaseRecordItem] PRIMARY KEY ([BaseRecordItemId]),
        CONSTRAINT [FK_tbl_BaseRecordItem_tbl_BaseRecord_BaseRecordId] FOREIGN KEY ([BaseRecordId]) REFERENCES [tbl_BaseRecord] ([BaseRecordId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190814080618_BaseRecordTable')
BEGIN
    CREATE INDEX [IX_tbl_BaseRecordItem_BaseRecordId] ON [tbl_BaseRecordItem] ([BaseRecordId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190814080618_BaseRecordTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190814080618_BaseRecordTable', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190817180019_ClientTable')
BEGIN
    CREATE TABLE [tbl_Client] (
        [ClientId] int NOT NULL IDENTITY,
        [Firstname] nvarchar(50) NOT NULL,
        [Middlename] nvarchar(50) NOT NULL,
        [Surname] nvarchar(50) NOT NULL,
        [Email] nvarchar(50) NOT NULL,
        [About] nvarchar(255) NOT NULL,
        [Hobbies] nvarchar(255) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [Keyworker] nvarchar(50) NOT NULL,
        [IdNumber] nvarchar(50) NOT NULL,
        [GenderId] int NOT NULL,
        [NumberOfCalls] int NOT NULL,
        [AreaCodeId] int NOT NULL,
        [TeritoryId] int NOT NULL,
        [ServiceId] int NOT NULL,
        [ProvisionVenue] nvarchar(50) NOT NULL,
        [PostCode] nvarchar(50) NOT NULL,
        [Rate] decimal(18,2) NOT NULL,
        [TeamLeader] nvarchar(50) NOT NULL,
        [DateOfBirth] nvarchar(15) NOT NULL,
        [Telephone] nvarchar(50) NOT NULL,
        [LanguageId] int NOT NULL,
        [KeySafe] nvarchar(50) NOT NULL,
        [ChoiceOfStaffId] int NOT NULL,
        [StatusId] int NOT NULL,
        [CapacityId] int NOT NULL,
        [ProviderReference] nvarchar(50) NOT NULL,
        [NumberOfStaff] int NOT NULL,
        CONSTRAINT [PK_tbl_Client] PRIMARY KEY ([ClientId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190817180019_ClientTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190817180019_ClientTable', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191013144448_tbl_ClientInvolvingPartyItem')
BEGIN
    CREATE TABLE [tbl_ClientInvolvingPartyItem] (
        [Deleted] bit NOT NULL,
        [ClientInvolvingPartyItemId] int NOT NULL IDENTITY,
        [ItemName] nvarchar(100) NOT NULL,
        [Description] nvarchar(225) NOT NULL,
        CONSTRAINT [PK_tbl_ClientInvolvingPartyItem] PRIMARY KEY ([ClientInvolvingPartyItemId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191013144448_tbl_ClientInvolvingPartyItem')
BEGIN
    CREATE UNIQUE INDEX [IX_tbl_Client_IdNumber] ON [tbl_Client] ([IdNumber]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191013144448_tbl_ClientInvolvingPartyItem')
BEGIN
    CREATE UNIQUE INDEX [IX_tbl_ClientInvolvingPartyItem_ItemName] ON [tbl_ClientInvolvingPartyItem] ([ItemName]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191013144448_tbl_ClientInvolvingPartyItem')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191013144448_tbl_ClientInvolvingPartyItem', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191013164959_tbl_ClientInvolvingParty')
BEGIN
    CREATE TABLE [tbl_ClientInvolvingParty] (
        [ClientInvolvingPartyId] int NOT NULL IDENTITY,
        [ClientInvolvingPartyItemId] int NOT NULL,
        [ClientId] int NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [Address] nvarchar(225) NOT NULL,
        [Email] nvarchar(125) NOT NULL,
        [Telephone] nvarchar(50) NOT NULL,
        [Relationship] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_tbl_ClientInvolvingParty] PRIMARY KEY ([ClientInvolvingPartyId]),
        CONSTRAINT [FK_tbl_ClientInvolvingParty_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_ClientInvolvingParty_tbl_ClientInvolvingPartyItem_ClientInvolvingPartyItemId] FOREIGN KEY ([ClientInvolvingPartyItemId]) REFERENCES [tbl_ClientInvolvingPartyItem] ([ClientInvolvingPartyItemId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191013164959_tbl_ClientInvolvingParty')
BEGIN
    CREATE INDEX [IX_tbl_ClientInvolvingParty_ClientId] ON [tbl_ClientInvolvingParty] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191013164959_tbl_ClientInvolvingParty')
BEGIN
    CREATE INDEX [IX_tbl_ClientInvolvingParty_ClientInvolvingPartyItemId] ON [tbl_ClientInvolvingParty] ([ClientInvolvingPartyItemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191013164959_tbl_ClientInvolvingParty')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191013164959_tbl_ClientInvolvingParty', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191118064119_ClientRegulatoryContact')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client]') AND [c].[name] = N'Rate');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [tbl_Client] ALTER COLUMN [Rate] decimal(18,2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191118064119_ClientRegulatoryContact')
BEGIN
    CREATE TABLE [tbl_ClientRegulatoryContact] (
        [ClientRegulatoryContactId] int NOT NULL IDENTITY,
        [ClientId] int NOT NULL,
        [BaseRecordItemId] int NOT NULL,
        [DatePerformed] datetime2 NULL,
        [DueDate] datetime2 NULL,
        [Evidence] nvarchar(max) NULL,
        CONSTRAINT [PK_tbl_ClientRegulatoryContact] PRIMARY KEY ([ClientRegulatoryContactId]),
        CONSTRAINT [FK_tbl_ClientRegulatoryContact_tbl_BaseRecordItem_BaseRecordItemId] FOREIGN KEY ([BaseRecordItemId]) REFERENCES [tbl_BaseRecordItem] ([BaseRecordItemId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_ClientRegulatoryContact_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191118064119_ClientRegulatoryContact')
BEGIN
    CREATE INDEX [IX_tbl_ClientRegulatoryContact_BaseRecordItemId] ON [tbl_ClientRegulatoryContact] ([BaseRecordItemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191118064119_ClientRegulatoryContact')
BEGIN
    CREATE INDEX [IX_tbl_ClientRegulatoryContact_ClientId] ON [tbl_ClientRegulatoryContact] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191118064119_ClientRegulatoryContact')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191118064119_ClientRegulatoryContact', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191118073427_ClientRotaType')
BEGIN
    CREATE TABLE [tbl_ClientRotaType] (
        [Deleted] bit NOT NULL,
        [ClientRotaTypeId] int NOT NULL IDENTITY,
        [RotaType] nvarchar(15) NOT NULL,
        CONSTRAINT [PK_tbl_ClientRotaType] PRIMARY KEY ([ClientRotaTypeId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191118073427_ClientRotaType')
BEGIN
    CREATE UNIQUE INDEX [IX_tbl_ClientRotaType_RotaType] ON [tbl_ClientRotaType] ([RotaType]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191118073427_ClientRotaType')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191118073427_ClientRotaType', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191119222419_tbl_ClientRota')
BEGIN
    CREATE TABLE [tbl_ClientRota] (
        [Deleted] bit NOT NULL,
        [RotaId] int NOT NULL IDENTITY,
        [NumberOfStaff] int NOT NULL,
        [RotaName] nvarchar(225) NOT NULL,
        [Gender] nvarchar(15) NOT NULL,
        [Area] nvarchar(225) NULL,
        CONSTRAINT [PK_tbl_ClientRota] PRIMARY KEY ([RotaId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191119222419_tbl_ClientRota')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191119222419_tbl_ClientRota', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124192014_tbl_RotaTask')
BEGIN
    CREATE TABLE [tbl_RotaTask] (
        [Deleted] bit NOT NULL,
        [RotaTaskId] int NOT NULL IDENTITY,
        [TaskName] nvarchar(125) NOT NULL,
        [GivenAcronym] nvarchar(50) NOT NULL,
        [NotGivenAcronym] nvarchar(50) NOT NULL,
        [Remark] nvarchar(125) NULL,
        CONSTRAINT [PK_tbl_RotaTask] PRIMARY KEY ([RotaTaskId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124192014_tbl_RotaTask')
BEGIN
    CREATE UNIQUE INDEX [IX_tbl_RotaTask_GivenAcronym] ON [tbl_RotaTask] ([GivenAcronym]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124192014_tbl_RotaTask')
BEGIN
    CREATE UNIQUE INDEX [IX_tbl_RotaTask_NotGivenAcronym] ON [tbl_RotaTask] ([NotGivenAcronym]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124192014_tbl_RotaTask')
BEGIN
    CREATE UNIQUE INDEX [IX_tbl_RotaTask_TaskName] ON [tbl_RotaTask] ([TaskName]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124192014_tbl_RotaTask')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191124192014_tbl_RotaTask', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129074957_ClientRota_Rename')
BEGIN
    ALTER TABLE [tbl_ClientRota] DROP CONSTRAINT [PK_tbl_ClientRota];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129074957_ClientRota_Rename')
BEGIN
    EXEC sp_rename N'[tbl_ClientRota]', N'tbl_ClientRotaName';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129074957_ClientRota_Rename')
BEGIN
    ALTER TABLE [tbl_ClientRotaName] ADD CONSTRAINT [PK_tbl_ClientRotaName] PRIMARY KEY ([RotaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129074957_ClientRota_Rename')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191129074957_ClientRota_Rename', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129082313_Table_ClientRota')
BEGIN
    CREATE TABLE [tbl_ClientRota] (
        [ClientRotaId] int NOT NULL IDENTITY,
        [ClientId] int NOT NULL,
        [ClientRotaTypeId] int NOT NULL,
        CONSTRAINT [PK_tbl_ClientRota] PRIMARY KEY ([ClientRotaId]),
        CONSTRAINT [FK_tbl_ClientRota_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_ClientRota_tbl_ClientRotaType_ClientRotaTypeId] FOREIGN KEY ([ClientRotaTypeId]) REFERENCES [tbl_ClientRotaType] ([ClientRotaTypeId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129082313_Table_ClientRota')
BEGIN
    CREATE INDEX [IX_tbl_ClientRota_ClientId] ON [tbl_ClientRota] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129082313_Table_ClientRota')
BEGIN
    CREATE INDEX [IX_tbl_ClientRota_ClientRotaTypeId] ON [tbl_ClientRota] ([ClientRotaTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129082313_Table_ClientRota')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191129082313_Table_ClientRota', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129084029_Table_RotaDayofWeek')
BEGIN
    CREATE TABLE [tbl_RotaDayofWeek] (
        [RotaDayofWeekId] int NOT NULL IDENTITY,
        [DayofWeek] nvarchar(15) NOT NULL,
        CONSTRAINT [PK_tbl_RotaDayofWeek] PRIMARY KEY ([RotaDayofWeekId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129084029_Table_RotaDayofWeek')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191129084029_Table_RotaDayofWeek', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129085206_Table_ClientRotaDays')
BEGIN
    CREATE TABLE [tbl_ClientRotaDays] (
        [ClientRotaDaysId] int NOT NULL,
        [ClientRotaId] int NOT NULL,
        [RotaDayofWeekId] int NOT NULL,
        [StartTime] nvarchar(25) NOT NULL,
        [StopTime] nvarchar(25) NOT NULL,
        CONSTRAINT [PK_tbl_ClientRotaDays] PRIMARY KEY ([ClientRotaDaysId]),
        CONSTRAINT [FK_tbl_ClientRotaDays_tbl_ClientRota_ClientRotaDaysId] FOREIGN KEY ([ClientRotaDaysId]) REFERENCES [tbl_ClientRota] ([ClientRotaId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_ClientRotaDays_tbl_RotaDayofWeek_RotaDayofWeekId] FOREIGN KEY ([RotaDayofWeekId]) REFERENCES [tbl_RotaDayofWeek] ([RotaDayofWeekId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129085206_Table_ClientRotaDays')
BEGIN
    CREATE INDEX [IX_tbl_ClientRotaDays_RotaDayofWeekId] ON [tbl_ClientRotaDays] ([RotaDayofWeekId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129085206_Table_ClientRotaDays')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191129085206_Table_ClientRotaDays', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129090019_RotaDayofWeek_Seed')
BEGIN
    ALTER TABLE [tbl_RotaDayofWeek] ADD [Deleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129090019_RotaDayofWeek_Seed')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RotaDayofWeekId', N'DayofWeek', N'Deleted') AND [object_id] = OBJECT_ID(N'[tbl_RotaDayofWeek]'))
        SET IDENTITY_INSERT [tbl_RotaDayofWeek] ON;
    INSERT INTO [tbl_RotaDayofWeek] ([RotaDayofWeekId], [DayofWeek], [Deleted])
    VALUES (1, N'Monday', CAST(0 AS bit)),
    (2, N'Tuesday', CAST(0 AS bit)),
    (3, N'Wednesday', CAST(0 AS bit)),
    (4, N'Thursday', CAST(0 AS bit)),
    (5, N'Friday', CAST(0 AS bit)),
    (6, N'Saturday', CAST(0 AS bit)),
    (7, N'Sunday', CAST(0 AS bit));
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RotaDayofWeekId', N'DayofWeek', N'Deleted') AND [object_id] = OBJECT_ID(N'[tbl_RotaDayofWeek]'))
        SET IDENTITY_INSERT [tbl_RotaDayofWeek] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129090019_RotaDayofWeek_Seed')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191129090019_RotaDayofWeek_Seed', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129093244_Table_ClientRotaTask')
BEGIN
    CREATE TABLE [tbl_ClientRotaTask] (
        [ClientRotaTaskId] int NOT NULL IDENTITY,
        [RotaTaskId] int NOT NULL,
        [ClientRotaDaysId] int NOT NULL,
        CONSTRAINT [PK_tbl_ClientRotaTask] PRIMARY KEY ([ClientRotaTaskId]),
        CONSTRAINT [FK_tbl_ClientRotaTask_tbl_ClientRotaDays_ClientRotaDaysId] FOREIGN KEY ([ClientRotaDaysId]) REFERENCES [tbl_ClientRotaDays] ([ClientRotaDaysId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_ClientRotaTask_tbl_RotaTask_RotaTaskId] FOREIGN KEY ([RotaTaskId]) REFERENCES [tbl_RotaTask] ([RotaTaskId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129093244_Table_ClientRotaTask')
BEGIN
    CREATE INDEX [IX_tbl_ClientRotaTask_ClientRotaDaysId] ON [tbl_ClientRotaTask] ([ClientRotaDaysId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129093244_Table_ClientRotaTask')
BEGIN
    CREATE INDEX [IX_tbl_ClientRotaTask_RotaTaskId] ON [tbl_ClientRotaTask] ([RotaTaskId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129093244_Table_ClientRotaTask')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191129093244_Table_ClientRotaTask', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129113405_Table_ClientRotaDays_Fk_Change_1')
BEGIN
    ALTER TABLE [tbl_ClientRotaDays] DROP CONSTRAINT [FK_tbl_ClientRotaDays_tbl_ClientRota_ClientRotaDaysId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129113405_Table_ClientRotaDays_Fk_Change_1')
BEGIN
    ALTER TABLE [tbl_ClientRotaTask] DROP CONSTRAINT [FK_tbl_ClientRotaTask_tbl_ClientRotaDays_ClientRotaDaysId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129113405_Table_ClientRotaDays_Fk_Change_1')
BEGIN
    ALTER TABLE [tbl_ClientRotaDays] DROP CONSTRAINT [PK_tbl_ClientRotaDays];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129113405_Table_ClientRotaDays_Fk_Change_1')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_ClientRotaDays]') AND [c].[name] = N'ClientRotaDaysId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [tbl_ClientRotaDays] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [tbl_ClientRotaDays] DROP COLUMN [ClientRotaDaysId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129113405_Table_ClientRotaDays_Fk_Change_1')
BEGIN
    ALTER TABLE [tbl_ClientRotaDays] ADD [Id] int NOT NULL IDENTITY;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129113405_Table_ClientRotaDays_Fk_Change_1')
BEGIN
    ALTER TABLE [tbl_ClientRotaDays] ADD CONSTRAINT [PK_tbl_ClientRotaDays] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129113405_Table_ClientRotaDays_Fk_Change_1')
BEGIN
    CREATE INDEX [IX_tbl_ClientRotaDays_ClientRotaId] ON [tbl_ClientRotaDays] ([ClientRotaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129113405_Table_ClientRotaDays_Fk_Change_1')
BEGIN
    ALTER TABLE [tbl_ClientRotaDays] ADD CONSTRAINT [FK_tbl_ClientRotaDays_tbl_ClientRota_ClientRotaId] FOREIGN KEY ([ClientRotaId]) REFERENCES [tbl_ClientRota] ([ClientRotaId]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129113405_Table_ClientRotaDays_Fk_Change_1')
BEGIN
    ALTER TABLE [tbl_ClientRotaTask] ADD CONSTRAINT [FK_tbl_ClientRotaTask_tbl_ClientRotaDays_ClientRotaDaysId] FOREIGN KEY ([ClientRotaDaysId]) REFERENCES [tbl_ClientRotaDays] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129113405_Table_ClientRotaDays_Fk_Change_1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191129113405_Table_ClientRotaDays_Fk_Change_1', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129113749_Table_ClientRotaDays_Fk_Change_2')
BEGIN
    EXEC sp_rename N'[tbl_ClientRotaDays].[Id]', N'ClientRotaDaysId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129113749_Table_ClientRotaDays_Fk_Change_2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191129113749_Table_ClientRotaDays_Fk_Change_2', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191216171659_Tbl_Client_UniqueId')
BEGIN
    ALTER TABLE [tbl_Client] ADD [UniqueId] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191216171659_Tbl_Client_UniqueId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191216171659_Tbl_Client_UniqueId', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191224075604_tbl_Client_PassportPath')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client]') AND [c].[name] = N'UniqueId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [tbl_Client] ALTER COLUMN [UniqueId] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191224075604_tbl_Client_PassportPath')
BEGIN
    ALTER TABLE [tbl_Client] ADD [PassportFilePath] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191224075604_tbl_Client_PassportPath')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191224075604_tbl_Client_PassportPath', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200101095937_tbl_ClientCareDetailsHeading')
BEGIN
    CREATE TABLE [tbl_ClientCareDetailsHeading] (
        [Deleted] bit NOT NULL,
        [ClientCareDetailsHeadingId] int NOT NULL IDENTITY,
        [Heading] nvarchar(225) NOT NULL,
        CONSTRAINT [PK_tbl_ClientCareDetailsHeading] PRIMARY KEY ([ClientCareDetailsHeadingId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200101095937_tbl_ClientCareDetailsHeading')
BEGIN
    CREATE UNIQUE INDEX [IX_tbl_ClientCareDetailsHeading_Heading] ON [tbl_ClientCareDetailsHeading] ([Heading]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200101095937_tbl_ClientCareDetailsHeading')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200101095937_tbl_ClientCareDetailsHeading', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200101104259_tbl_ClientCareDetailsTask')
BEGIN
    CREATE TABLE [tbl_ClientCareDetailsTask] (
        [ClientCareDetailsTaskId] int NOT NULL IDENTITY,
        [ClientCareDetailsHeadingId] int NOT NULL,
        [Task] nvarchar(225) NOT NULL,
        CONSTRAINT [PK_tbl_ClientCareDetailsTask] PRIMARY KEY ([ClientCareDetailsTaskId]),
        CONSTRAINT [FK_tbl_ClientCareDetailsTask_tbl_ClientCareDetailsHeading_ClientCareDetailsHeadingId] FOREIGN KEY ([ClientCareDetailsHeadingId]) REFERENCES [tbl_ClientCareDetailsHeading] ([ClientCareDetailsHeadingId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200101104259_tbl_ClientCareDetailsTask')
BEGIN
    CREATE INDEX [IX_tbl_ClientCareDetailsTask_ClientCareDetailsHeadingId] ON [tbl_ClientCareDetailsTask] ([ClientCareDetailsHeadingId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200101104259_tbl_ClientCareDetailsTask')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200101104259_tbl_ClientCareDetailsTask', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200102170131_tbl_ClientCareDetails')
BEGIN
    CREATE TABLE [tbl_ClientCareDetails] (
        [ClientCareDetailsId] int NOT NULL IDENTITY,
        [ClientCareDetailsTaskId] int NOT NULL,
        [ClientId] int NOT NULL,
        [Description] nvarchar(250) NULL,
        [Risk] nvarchar(250) NOT NULL,
        [Mitigation] nvarchar(250) NULL,
        [Location] nvarchar(250) NULL,
        [Remark] nvarchar(250) NULL,
        CONSTRAINT [PK_tbl_ClientCareDetails] PRIMARY KEY ([ClientCareDetailsId]),
        CONSTRAINT [FK_tbl_ClientCareDetails_tbl_ClientCareDetailsTask_ClientCareDetailsTaskId] FOREIGN KEY ([ClientCareDetailsTaskId]) REFERENCES [tbl_ClientCareDetailsTask] ([ClientCareDetailsTaskId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_ClientCareDetails_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200102170131_tbl_ClientCareDetails')
BEGIN
    CREATE INDEX [IX_tbl_ClientCareDetails_ClientCareDetailsTaskId] ON [tbl_ClientCareDetails] ([ClientCareDetailsTaskId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200102170131_tbl_ClientCareDetails')
BEGIN
    CREATE INDEX [IX_tbl_ClientCareDetails_ClientId] ON [tbl_ClientCareDetails] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200102170131_tbl_ClientCareDetails')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200102170131_tbl_ClientCareDetails', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200104101927_tbl_ClientCareDetailsTask_Delete')
BEGIN
    ALTER TABLE [tbl_ClientCareDetailsTask] ADD [Deleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200104101927_tbl_ClientCareDetailsTask_Delete')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200104101927_tbl_ClientCareDetailsTask_Delete', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200113165502_tbl_StaffPersonalInfo')
BEGIN
    CREATE TABLE [tbl_StaffPersonalInfo] (
        [StaffPersonalInfoId] int NOT NULL IDENTITY,
        [RegistrationId] nvarchar(20) NULL,
        [FirstName] nvarchar(50) NOT NULL,
        [MiddleName] nvarchar(50) NULL,
        [LastName] nvarchar(50) NOT NULL,
        [DateOfBirth] nvarchar(max) NOT NULL,
        [Telephone] nvarchar(25) NOT NULL,
        [ProfilePix] nvarchar(max) NOT NULL,
        [Address] nvarchar(225) NOT NULL,
        [AboutMe] nvarchar(225) NULL,
        [Hobbies] nvarchar(225) NULL,
        [Email] nvarchar(225) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [Keyworker] nvarchar(50) NOT NULL,
        [IdNumber] nvarchar(50) NULL,
        [Gender] nvarchar(7) NOT NULL,
        [PostCode] nvarchar(50) NOT NULL,
        [Rate] decimal(18,2) NULL,
        [TeamLeader] nvarchar(50) NULL,
        [WorkTeam] nvarchar(50) NULL,
        [Passcode] nvarchar(15) NULL,
        [CanDrive] nvarchar(3) NOT NULL,
        [DrivingLicense] nvarchar(max) NULL,
        [DrivingLicenseExpiryDate] datetime2 NULL,
        [RightToWork] nvarchar(3) NOT NULL,
        [RightToWorkAttachment] nvarchar(max) NULL,
        [RightToWorkExpiryDate] datetime2 NULL,
        [DBS] nvarchar(3) NOT NULL,
        [DBSAttachment] nvarchar(max) NULL,
        [DBSExpiryDate] datetime2 NULL,
        [DBSUpdateNo] nvarchar(50) NULL,
        [NI] nvarchar(3) NOT NULL,
        [NIAttachment] nvarchar(max) NULL,
        [NIExpiryDate] datetime2 NULL,
        [CV] nvarchar(max) NOT NULL,
        [CoverLetter] nvarchar(max) NOT NULL,
        [Self_PYE] nvarchar(3) NOT NULL,
        [Self_PYEAttachment] nvarchar(max) NULL,
        CONSTRAINT [PK_tbl_StaffPersonalInfo] PRIMARY KEY ([StaffPersonalInfoId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200113165502_tbl_StaffPersonalInfo')
BEGIN
    CREATE UNIQUE INDEX [IX_tbl_StaffPersonalInfo_RegistrationId] ON [tbl_StaffPersonalInfo] ([RegistrationId]) WHERE [RegistrationId] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200113165502_tbl_StaffPersonalInfo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200113165502_tbl_StaffPersonalInfo', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200126151921_tbl_StaffEducation')
BEGIN
    CREATE TABLE [tbl_StaffEducation] (
        [StaffEducationId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [Institution] nvarchar(255) NOT NULL,
        [Certificate] nvarchar(125) NOT NULL,
        [Location] nvarchar(255) NOT NULL,
        [Address] nvarchar(255) NOT NULL,
        [StartDate] nvarchar(25) NOT NULL,
        [EndDate] nvarchar(25) NULL,
        [CertificateAttachment] nvarchar(max) NULL,
        CONSTRAINT [PK_tbl_StaffEducation] PRIMARY KEY ([StaffEducationId]),
        CONSTRAINT [FK_tbl_StaffEducation_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200126151921_tbl_StaffEducation')
BEGIN
    CREATE INDEX [IX_tbl_StaffEducation_StaffPersonalInfoId] ON [tbl_StaffEducation] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200126151921_tbl_StaffEducation')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200126151921_tbl_StaffEducation', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200126152535_tbl_StaffTraining')
BEGIN
    CREATE TABLE [tbl_StaffTraining] (
        [StaffTrainingId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [Training] nvarchar(255) NOT NULL,
        [Certificate] nvarchar(125) NOT NULL,
        [Location] nvarchar(255) NOT NULL,
        [Trainer] nvarchar(125) NOT NULL,
        [StartDate] nvarchar(25) NOT NULL,
        [ExpiredDate] nvarchar(25) NULL,
        [CertificateAttachment] nvarchar(max) NULL,
        CONSTRAINT [PK_tbl_StaffTraining] PRIMARY KEY ([StaffTrainingId]),
        CONSTRAINT [FK_tbl_StaffTraining_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200126152535_tbl_StaffTraining')
BEGIN
    CREATE INDEX [IX_tbl_StaffTraining_StaffPersonalInfoId] ON [tbl_StaffTraining] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200126152535_tbl_StaffTraining')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200126152535_tbl_StaffTraining', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200126153446_tbl_StaffReferee')
BEGIN
    CREATE TABLE [tbl_StaffReferee] (
        [StaffRefereeId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [Referee] nvarchar(125) NOT NULL,
        [CompanyName] nvarchar(125) NOT NULL,
        [Address] nvarchar(255) NOT NULL,
        [PhoneNumber] nvarchar(50) NOT NULL,
        [Email] nvarchar(125) NOT NULL,
        [PositionofReferee] nvarchar(25) NOT NULL,
        [Attachment] nvarchar(max) NULL,
        CONSTRAINT [PK_tbl_StaffReferee] PRIMARY KEY ([StaffRefereeId]),
        CONSTRAINT [FK_tbl_StaffReferee_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200126153446_tbl_StaffReferee')
BEGIN
    CREATE INDEX [IX_tbl_StaffReferee_StaffPersonalInfoId] ON [tbl_StaffReferee] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200126153446_tbl_StaffReferee')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200126153446_tbl_StaffReferee', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200127062314_tbl_StaffPersonalInfo_Keyworker')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffPersonalInfo]') AND [c].[name] = N'Keyworker');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffPersonalInfo] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [tbl_StaffPersonalInfo] ALTER COLUMN [Keyworker] nvarchar(50) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200127062314_tbl_StaffPersonalInfo_Keyworker')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200127062314_tbl_StaffPersonalInfo_Keyworker', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200130061054_tbl_Client_UniqueId_NotRequired')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client]') AND [c].[name] = N'UniqueId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [tbl_Client] ALTER COLUMN [UniqueId] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200130061054_tbl_Client_UniqueId_NotRequired')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200130061054_tbl_Client_UniqueId_NotRequired', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200130181137_tbl_StaffPersonalInfo_Fieldlength')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffPersonalInfo]') AND [c].[name] = N'Telephone');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffPersonalInfo] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [tbl_StaffPersonalInfo] ALTER COLUMN [Telephone] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200130181137_tbl_StaffPersonalInfo_Fieldlength')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffPersonalInfo]') AND [c].[name] = N'DateOfBirth');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffPersonalInfo] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [tbl_StaffPersonalInfo] ALTER COLUMN [DateOfBirth] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200130181137_tbl_StaffPersonalInfo_Fieldlength')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200130181137_tbl_StaffPersonalInfo_Fieldlength', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200204204546_tbl_staffpersonalinfo_status')
BEGIN
    ALTER TABLE [tbl_StaffPersonalInfo] ADD [Status] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200204204546_tbl_staffpersonalinfo_status')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200204204546_tbl_staffpersonalinfo_status', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200205210946_StaffPersonalInfoComment')
BEGIN
    CREATE TABLE [tbl_StaffPersonalInfoComment] (
        [StaffPersonalInfoCommentId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [CommentBy_UserId] int NULL,
        [Comment] nvarchar(250) NOT NULL,
        [CommentOn] datetime2 NOT NULL,
        CONSTRAINT [PK_tbl_StaffPersonalInfoComment] PRIMARY KEY ([StaffPersonalInfoCommentId]),
        CONSTRAINT [FK_tbl_StaffPersonalInfoComment_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200205210946_StaffPersonalInfoComment')
BEGIN
    CREATE INDEX [IX_tbl_StaffPersonalInfoComment_StaffPersonalInfoId] ON [tbl_StaffPersonalInfoComment] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200205210946_StaffPersonalInfoComment')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200205210946_StaffPersonalInfoComment', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200215143335_tbl_StaffRegulatoryContact')
BEGIN
    CREATE TABLE [tbl_StaffRegulatoryContact] (
        [StaffRegulatoryContactId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BaseRecordItemId] int NOT NULL,
        [DatePerformed] datetime2 NULL,
        [DueDate] datetime2 NULL,
        [Evidence] nvarchar(max) NULL,
        CONSTRAINT [PK_tbl_StaffRegulatoryContact] PRIMARY KEY ([StaffRegulatoryContactId]),
        CONSTRAINT [FK_tbl_StaffRegulatoryContact_tbl_BaseRecordItem_BaseRecordItemId] FOREIGN KEY ([BaseRecordItemId]) REFERENCES [tbl_BaseRecordItem] ([BaseRecordItemId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_StaffRegulatoryContact_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200215143335_tbl_StaffRegulatoryContact')
BEGIN
    CREATE INDEX [IX_tbl_StaffRegulatoryContact_BaseRecordItemId] ON [tbl_StaffRegulatoryContact] ([BaseRecordItemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200215143335_tbl_StaffRegulatoryContact')
BEGIN
    CREATE INDEX [IX_tbl_StaffRegulatoryContact_StaffPersonalInfoId] ON [tbl_StaffRegulatoryContact] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200215143335_tbl_StaffRegulatoryContact')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200215143335_tbl_StaffRegulatoryContact', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200222155851_tbl_StaffCommunication')
BEGIN
    CREATE TABLE [tbl_StaffCommunication] (
        [StaffCommunicationId] int NOT NULL IDENTITY,
        [ValueDate] datetime2 NOT NULL,
        [Concern] nvarchar(500) NOT NULL,
        [CommunicationClass] int NOT NULL,
        [PersonInvolved] int NOT NULL,
        [ExpectedAction] nvarchar(255) NOT NULL,
        [ActionTaken] nvarchar(255) NOT NULL,
        [Status] int NOT NULL,
        [Telephone] nvarchar(50) NOT NULL,
        [PersonResponsibleForAction] int NOT NULL,
        [Attachment] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_tbl_StaffCommunication] PRIMARY KEY ([StaffCommunicationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200222155851_tbl_StaffCommunication')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200222155851_tbl_StaffCommunication', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200223073146_tbl_StaffCommunication_Attachment')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffCommunication]') AND [c].[name] = N'Attachment');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffCommunication] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [tbl_StaffCommunication] ALTER COLUMN [Attachment] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200223073146_tbl_StaffCommunication_Attachment')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200223073146_tbl_StaffCommunication_Attachment', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200304093921_Untowards')
BEGIN
    CREATE TABLE [tbl_Untowards] (
        [UntowardsId] int NOT NULL IDENTITY,
        [TicketNumber] nvarchar(50) NOT NULL,
        [Date] nvarchar(15) NOT NULL,
        [Subject] nvarchar(225) NOT NULL,
        [TimeOfCall] nvarchar(15) NOT NULL,
        [PersonReporting] nvarchar(100) NULL,
        [PersonReportingTelephone] nvarchar(50) NULL,
        [PersonReportingEmail] nvarchar(225) NULL,
        [Details] nvarchar(225) NOT NULL,
        [ActionStatus] nvarchar(7) NOT NULL,
        [Priority] nvarchar(7) NOT NULL,
        [ActionTaken] nvarchar(225) NOT NULL,
        [ActionRequired] nvarchar(225) NOT NULL,
        [FinalActionToCloseCase] nvarchar(225) NULL,
        [ExpectedDateAndTimeOfFeedback] nvarchar(225) NOT NULL,
        [IsBlackListRequired] bit NOT NULL,
        [HomeCareClientId] int NOT NULL,
        [IsHospitalEntry] bit NOT NULL,
        [HospitalEntryReason] nvarchar(225) NOT NULL,
        [IsHospitalExit] bit NOT NULL,
        [HospitalExitDetails] nvarchar(225) NOT NULL,
        [TypeofRequiredNotification] int NOT NULL,
        [ShouldNotifyInvolvingStaff] bit NOT NULL,
        [Attachment] nvarchar(max) NOT NULL,
        [Others] nvarchar(225) NOT NULL,
        CONSTRAINT [PK_tbl_Untowards] PRIMARY KEY ([UntowardsId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200304093921_Untowards')
BEGIN
    CREATE TABLE [tbl_UntowardsOfficerToAct] (
        [UntowardsOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [UntowardsId] int NOT NULL,
        CONSTRAINT [PK_tbl_UntowardsOfficerToAct] PRIMARY KEY ([UntowardsOfficerToActId]),
        CONSTRAINT [FK_tbl_UntowardsOfficerToAct_tbl_Untowards_UntowardsId] FOREIGN KEY ([UntowardsId]) REFERENCES [tbl_Untowards] ([UntowardsId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200304093921_Untowards')
BEGIN
    CREATE TABLE [tbl_UntowardsStaffInvolved] (
        [UntowardsStaffInvolvedId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [UntowardsId] int NOT NULL,
        CONSTRAINT [PK_tbl_UntowardsStaffInvolved] PRIMARY KEY ([UntowardsStaffInvolvedId]),
        CONSTRAINT [FK_tbl_UntowardsStaffInvolved_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_UntowardsStaffInvolved_tbl_Untowards_UntowardsId] FOREIGN KEY ([UntowardsId]) REFERENCES [tbl_Untowards] ([UntowardsId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200304093921_Untowards')
BEGIN
    CREATE INDEX [IX_tbl_UntowardsOfficerToAct_UntowardsId] ON [tbl_UntowardsOfficerToAct] ([UntowardsId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200304093921_Untowards')
BEGIN
    CREATE INDEX [IX_tbl_UntowardsStaffInvolved_StaffPersonalInfoId] ON [tbl_UntowardsStaffInvolved] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200304093921_Untowards')
BEGIN
    CREATE INDEX [IX_tbl_UntowardsStaffInvolved_UntowardsId] ON [tbl_UntowardsStaffInvolved] ([UntowardsId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200304093921_Untowards')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200304093921_Untowards', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200304103911_Untowards_Update')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Untowards]') AND [c].[name] = N'Others');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Untowards] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [tbl_Untowards] ALTER COLUMN [Others] nvarchar(225) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200304103911_Untowards_Update')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Untowards]') AND [c].[name] = N'HospitalExitDetails');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Untowards] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [tbl_Untowards] ALTER COLUMN [HospitalExitDetails] nvarchar(225) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200304103911_Untowards_Update')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Untowards]') AND [c].[name] = N'HospitalEntryReason');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Untowards] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [tbl_Untowards] ALTER COLUMN [HospitalEntryReason] nvarchar(225) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200304103911_Untowards_Update')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Untowards]') AND [c].[name] = N'ExpectedDateAndTimeOfFeedback');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Untowards] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [tbl_Untowards] ALTER COLUMN [ExpectedDateAndTimeOfFeedback] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200304103911_Untowards_Update')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Untowards]') AND [c].[name] = N'Attachment');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Untowards] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [tbl_Untowards] ALTER COLUMN [Attachment] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200304103911_Untowards_Update')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200304103911_Untowards_Update', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200307141450_tbl_StaffEmergencyContact')
BEGIN
    CREATE TABLE [tbl_StaffEmergencyContact] (
        [StaffEmergencyContactId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [ContactName] nvarchar(100) NOT NULL,
        [Telephone] nvarchar(50) NOT NULL,
        [Email] nvarchar(100) NULL,
        [Relationship] nvarchar(50) NOT NULL,
        [Address] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_tbl_StaffEmergencyContact] PRIMARY KEY ([StaffEmergencyContactId]),
        CONSTRAINT [FK_tbl_StaffEmergencyContact_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200307141450_tbl_StaffEmergencyContact')
BEGIN
    CREATE INDEX [IX_tbl_UntowardsOfficerToAct_StaffPersonalInfoId] ON [tbl_UntowardsOfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200307141450_tbl_StaffEmergencyContact')
BEGIN
    CREATE INDEX [IX_tbl_StaffEmergencyContact_StaffPersonalInfoId] ON [tbl_StaffEmergencyContact] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200307141450_tbl_StaffEmergencyContact')
BEGIN
    ALTER TABLE [tbl_UntowardsOfficerToAct] ADD CONSTRAINT [FK_tbl_UntowardsOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200307141450_tbl_StaffEmergencyContact')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200307141450_tbl_StaffEmergencyContact', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200313213730_tbl_ShiftBooking')
BEGIN
    CREATE TABLE [tbl_ShiftBooking] (
        [ShiftBookingId] int NOT NULL IDENTITY,
        [ShiftDate] nvarchar(15) NOT NULL,
        [Rota] int NOT NULL,
        [NumberOfStaff] int NOT NULL,
        [StartTime] nvarchar(15) NOT NULL,
        [StopTime] nvarchar(15) NOT NULL,
        [Remark] nvarchar(225) NOT NULL,
        [Team] int NOT NULL,
        [DriverRequired] bit NOT NULL,
        [PublishTo] nvarchar(max) NULL,
        CONSTRAINT [PK_tbl_ShiftBooking] PRIMARY KEY ([ShiftBookingId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200313213730_tbl_ShiftBooking')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200313213730_tbl_ShiftBooking', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200314090909_tbl_ShiftBooking_Team_StaffPersonalInfId')
BEGIN
    EXEC sp_rename N'[tbl_ShiftBooking].[Team]', N'Team_StaffPersonalInfoId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200314090909_tbl_ShiftBooking_Team_StaffPersonalInfId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200314090909_tbl_ShiftBooking_Team_StaffPersonalInfId', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200314114759_tbl_StaffWorkTeam')
BEGIN
    ALTER TABLE [tbl_StaffPersonalInfo] ADD [StaffWorkTeamId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200314114759_tbl_StaffWorkTeam')
BEGIN
    CREATE TABLE [tbl_StaffWorkTeam] (
        [StaffWorkTeamId] int NOT NULL IDENTITY,
        [WorkTeam] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_tbl_StaffWorkTeam] PRIMARY KEY ([StaffWorkTeamId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200314114759_tbl_StaffWorkTeam')
BEGIN
    CREATE INDEX [IX_tbl_StaffPersonalInfo_StaffWorkTeamId] ON [tbl_StaffPersonalInfo] ([StaffWorkTeamId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200314114759_tbl_StaffWorkTeam')
BEGIN
    ALTER TABLE [tbl_StaffPersonalInfo] ADD CONSTRAINT [FK_tbl_StaffPersonalInfo_tbl_StaffWorkTeam_StaffWorkTeamId] FOREIGN KEY ([StaffWorkTeamId]) REFERENCES [tbl_StaffWorkTeam] ([StaffWorkTeamId]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200314114759_tbl_StaffWorkTeam')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200314114759_tbl_StaffWorkTeam', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200314120521_tbl_ShiftBooking_PublishTo')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_ShiftBooking]') AND [c].[name] = N'PublishTo');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [tbl_ShiftBooking] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [tbl_ShiftBooking] ALTER COLUMN [PublishTo] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200314120521_tbl_ShiftBooking_PublishTo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200314120521_tbl_ShiftBooking_PublishTo', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322043103_tbl_StaffShiftBooking')
BEGIN
    CREATE TABLE [tbl_StaffShiftBooking] (
        [StaffShiftBookingId] int NOT NULL IDENTITY,
        [RotaId] int NOT NULL,
        [MonthIndex] int NOT NULL,
        [MonthName] nvarchar(25) NOT NULL,
        [Year] int NOT NULL,
        [StaffPersonalInfoId] int NOT NULL,
        CONSTRAINT [PK_tbl_StaffShiftBooking] PRIMARY KEY ([StaffShiftBookingId]),
        CONSTRAINT [FK_tbl_StaffShiftBooking_tbl_ClientRotaName_RotaId] FOREIGN KEY ([RotaId]) REFERENCES [tbl_ClientRotaName] ([RotaId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_StaffShiftBooking_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322043103_tbl_StaffShiftBooking')
BEGIN
    CREATE INDEX [IX_tbl_StaffShiftBooking_RotaId] ON [tbl_StaffShiftBooking] ([RotaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322043103_tbl_StaffShiftBooking')
BEGIN
    CREATE INDEX [IX_tbl_StaffShiftBooking_StaffPersonalInfoId] ON [tbl_StaffShiftBooking] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322043103_tbl_StaffShiftBooking')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200322043103_tbl_StaffShiftBooking', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322093348_tbl_StaffShiftBookingDay')
BEGIN
    CREATE TABLE [tbl_StaffShiftBookingDay] (
        [StaffShiftBookingDayId] int NOT NULL IDENTITY,
        [StaffShiftBookingId] int NOT NULL,
        [Day] nvarchar(2) NOT NULL,
        [WeekDay] nvarchar(15) NOT NULL,
        CONSTRAINT [PK_tbl_StaffShiftBookingDay] PRIMARY KEY ([StaffShiftBookingDayId]),
        CONSTRAINT [FK_tbl_StaffShiftBookingDay_tbl_StaffShiftBooking_StaffShiftBookingId] FOREIGN KEY ([StaffShiftBookingId]) REFERENCES [tbl_StaffShiftBooking] ([StaffShiftBookingId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322093348_tbl_StaffShiftBookingDay')
BEGIN
    CREATE INDEX [IX_tbl_StaffShiftBookingDay_StaffShiftBookingId] ON [tbl_StaffShiftBookingDay] ([StaffShiftBookingId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322093348_tbl_StaffShiftBookingDay')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200322093348_tbl_StaffShiftBookingDay', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322132008_tbl_StaffShiftBooking_ColumnChange')
BEGIN
    delete from tbl_staffshiftbooking
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322132008_tbl_StaffShiftBooking_ColumnChange')
BEGIN
    ALTER TABLE [tbl_StaffShiftBooking] DROP CONSTRAINT [FK_tbl_StaffShiftBooking_tbl_ClientRotaName_RotaId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322132008_tbl_StaffShiftBooking_ColumnChange')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffShiftBooking]') AND [c].[name] = N'MonthIndex');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffShiftBooking] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [tbl_StaffShiftBooking] DROP COLUMN [MonthIndex];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322132008_tbl_StaffShiftBooking_ColumnChange')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffShiftBooking]') AND [c].[name] = N'MonthName');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffShiftBooking] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [tbl_StaffShiftBooking] DROP COLUMN [MonthName];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322132008_tbl_StaffShiftBooking_ColumnChange')
BEGIN
    EXEC sp_rename N'[tbl_StaffShiftBooking].[Year]', N'ShiftBookingId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322132008_tbl_StaffShiftBooking_ColumnChange')
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffShiftBooking]') AND [c].[name] = N'RotaId');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffShiftBooking] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [tbl_StaffShiftBooking] ALTER COLUMN [RotaId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322132008_tbl_StaffShiftBooking_ColumnChange')
BEGIN
    CREATE INDEX [IX_tbl_StaffShiftBooking_ShiftBookingId] ON [tbl_StaffShiftBooking] ([ShiftBookingId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322132008_tbl_StaffShiftBooking_ColumnChange')
BEGIN
    ALTER TABLE [tbl_StaffShiftBooking] ADD CONSTRAINT [FK_tbl_StaffShiftBooking_tbl_ClientRotaName_RotaId] FOREIGN KEY ([RotaId]) REFERENCES [tbl_ClientRotaName] ([RotaId]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322132008_tbl_StaffShiftBooking_ColumnChange')
BEGIN
    ALTER TABLE [tbl_StaffShiftBooking] ADD CONSTRAINT [FK_tbl_StaffShiftBooking_tbl_ShiftBooking_ShiftBookingId] FOREIGN KEY ([ShiftBookingId]) REFERENCES [tbl_ShiftBooking] ([ShiftBookingId]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200322132008_tbl_StaffShiftBooking_ColumnChange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200322132008_tbl_StaffShiftBooking_ColumnChange', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    ALTER TABLE [tbl_StaffPersonalInfo] ADD [ApplicationUserId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE UNIQUE INDEX [IX_tbl_StaffPersonalInfo_ApplicationUserId] ON [tbl_StaffPersonalInfo] ([ApplicationUserId]) WHERE [ApplicationUserId] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    ALTER TABLE [tbl_StaffPersonalInfo] ADD CONSTRAINT [FK_tbl_StaffPersonalInfo_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200330214117_CreateIdentitySchema')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200330214117_CreateIdentitySchema', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200407124525_tbl_Medication')
BEGIN
    CREATE TABLE [tbl_Medication] (
        [MedicationId] int NOT NULL IDENTITY,
        [Deleted] bit NOT NULL,
        [MedicatiionName] nvarchar(225) NOT NULL,
        [Strength] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_tbl_Medication] PRIMARY KEY ([MedicationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200407124525_tbl_Medication')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200407124525_tbl_Medication', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200407145923_tbl_Medication_CorrectedMedicationName')
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Medication]') AND [c].[name] = N'MedicatiionName');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Medication] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [tbl_Medication] DROP COLUMN [MedicatiionName];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200407145923_tbl_Medication_CorrectedMedicationName')
BEGIN
    ALTER TABLE [tbl_Medication] ADD [MedicationName] nvarchar(225) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200407145923_tbl_Medication_CorrectedMedicationName')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200407145923_tbl_Medication_CorrectedMedicationName', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409091218_tbl_MedicationManufacturer')
BEGIN
    CREATE TABLE [tbl_MedicationManufacturer] (
        [MedicationManufacturerId] int NOT NULL IDENTITY,
        [Deleted] bit NOT NULL,
        [Manufacturer] nvarchar(255) NOT NULL,
        CONSTRAINT [PK_tbl_MedicationManufacturer] PRIMARY KEY ([MedicationManufacturerId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409091218_tbl_MedicationManufacturer')
BEGIN
    CREATE UNIQUE INDEX [IX_tbl_MedicationManufacturer_Manufacturer] ON [tbl_MedicationManufacturer] ([Manufacturer]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409091218_tbl_MedicationManufacturer')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200409091218_tbl_MedicationManufacturer', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409221751_tbl_ClientMedication_Days_Periods')
BEGIN
    CREATE TABLE [tbl_ClientMedication] (
        [ClientMedicationId] int NOT NULL IDENTITY,
        [MedicationId] int NOT NULL,
        [MedicationManufacturerId] int NOT NULL,
        [ExpiryDate] nvarchar(15) NOT NULL,
        [Dossage] nvarchar(50) NOT NULL,
        [Frequency] nvarchar(50) NOT NULL,
        [Gap_Hour] int NOT NULL,
        [Route] nvarchar(50) NOT NULL,
        [StartDate] nvarchar(15) NOT NULL,
        [StopDate] nvarchar(50) NOT NULL,
        [Status] nvarchar(50) NOT NULL,
        [Remark] nvarchar(250) NOT NULL,
        CONSTRAINT [PK_tbl_ClientMedication] PRIMARY KEY ([ClientMedicationId]),
        CONSTRAINT [FK_tbl_ClientMedication_tbl_Medication_MedicationId] FOREIGN KEY ([MedicationId]) REFERENCES [tbl_Medication] ([MedicationId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_ClientMedication_tbl_MedicationManufacturer_MedicationManufacturerId] FOREIGN KEY ([MedicationManufacturerId]) REFERENCES [tbl_MedicationManufacturer] ([MedicationManufacturerId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409221751_tbl_ClientMedication_Days_Periods')
BEGIN
    CREATE TABLE [tbl_ClientMedicationDay] (
        [ClientMedicationDayId] int NOT NULL IDENTITY,
        [ClientMedicationId] int NOT NULL,
        [RotaDayofWeekId] int NOT NULL,
        [ClientMedicationId1] int NULL,
        CONSTRAINT [PK_tbl_ClientMedicationDay] PRIMARY KEY ([ClientMedicationDayId]),
        CONSTRAINT [FK_tbl_ClientMedicationDay_tbl_ClientMedication_ClientMedicationId] FOREIGN KEY ([ClientMedicationId]) REFERENCES [tbl_ClientMedication] ([ClientMedicationId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_ClientMedicationDay_tbl_ClientMedication_ClientMedicationId1] FOREIGN KEY ([ClientMedicationId1]) REFERENCES [tbl_ClientMedication] ([ClientMedicationId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_tbl_ClientMedicationDay_tbl_RotaDayofWeek_RotaDayofWeekId] FOREIGN KEY ([RotaDayofWeekId]) REFERENCES [tbl_RotaDayofWeek] ([RotaDayofWeekId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409221751_tbl_ClientMedication_Days_Periods')
BEGIN
    CREATE TABLE [tbl_ClientMedicationPeriod] (
        [ClientMedicationPeriodId] int NOT NULL IDENTITY,
        [ClientRotaTypeId] int NOT NULL,
        [ClientMedicationDayId] int NOT NULL,
        CONSTRAINT [PK_tbl_ClientMedicationPeriod] PRIMARY KEY ([ClientMedicationPeriodId]),
        CONSTRAINT [FK_tbl_ClientMedicationPeriod_tbl_ClientMedicationDay_ClientMedicationDayId] FOREIGN KEY ([ClientMedicationDayId]) REFERENCES [tbl_ClientMedicationDay] ([ClientMedicationDayId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_ClientMedicationPeriod_tbl_ClientRotaType_ClientRotaTypeId] FOREIGN KEY ([ClientRotaTypeId]) REFERENCES [tbl_ClientRotaType] ([ClientRotaTypeId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409221751_tbl_ClientMedication_Days_Periods')
BEGIN
    CREATE INDEX [IX_tbl_ClientMedication_MedicationId] ON [tbl_ClientMedication] ([MedicationId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409221751_tbl_ClientMedication_Days_Periods')
BEGIN
    CREATE INDEX [IX_tbl_ClientMedication_MedicationManufacturerId] ON [tbl_ClientMedication] ([MedicationManufacturerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409221751_tbl_ClientMedication_Days_Periods')
BEGIN
    CREATE INDEX [IX_tbl_ClientMedicationDay_ClientMedicationId] ON [tbl_ClientMedicationDay] ([ClientMedicationId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409221751_tbl_ClientMedication_Days_Periods')
BEGIN
    CREATE INDEX [IX_tbl_ClientMedicationDay_ClientMedicationId1] ON [tbl_ClientMedicationDay] ([ClientMedicationId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409221751_tbl_ClientMedication_Days_Periods')
BEGIN
    CREATE UNIQUE INDEX [IX_tbl_ClientMedicationDay_RotaDayofWeekId] ON [tbl_ClientMedicationDay] ([RotaDayofWeekId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409221751_tbl_ClientMedication_Days_Periods')
BEGIN
    CREATE INDEX [IX_tbl_ClientMedicationPeriod_ClientMedicationDayId] ON [tbl_ClientMedicationPeriod] ([ClientMedicationDayId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409221751_tbl_ClientMedication_Days_Periods')
BEGIN
    CREATE INDEX [IX_tbl_ClientMedicationPeriod_ClientRotaTypeId] ON [tbl_ClientMedicationPeriod] ([ClientRotaTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409221751_tbl_ClientMedication_Days_Periods')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200409221751_tbl_ClientMedication_Days_Periods', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200410010854_tbl_ClientMedication_Add_ClientId')
BEGIN
    ALTER TABLE [tbl_ClientMedication] ADD [ClientId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200410010854_tbl_ClientMedication_Add_ClientId')
BEGIN
    CREATE INDEX [IX_tbl_ClientMedication_ClientId] ON [tbl_ClientMedication] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200410010854_tbl_ClientMedication_Add_ClientId')
BEGIN
    ALTER TABLE [tbl_ClientMedication] ADD CONSTRAINT [FK_tbl_ClientMedication_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200410010854_tbl_ClientMedication_Add_ClientId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200410010854_tbl_ClientMedication_Add_ClientId', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200410142424_tbl_ClientMedicationDay_Modify_RotaDayofWeekId')
BEGIN
    DROP INDEX [IX_tbl_ClientMedicationDay_RotaDayofWeekId] ON [tbl_ClientMedicationDay];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200410142424_tbl_ClientMedicationDay_Modify_RotaDayofWeekId')
BEGIN
    CREATE INDEX [IX_tbl_ClientMedicationDay_RotaDayofWeekId] ON [tbl_ClientMedicationDay] ([RotaDayofWeekId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200410142424_tbl_ClientMedicationDay_Modify_RotaDayofWeekId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200410142424_tbl_ClientMedicationDay_Modify_RotaDayofWeekId', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200424232352_tbl_StaffRota_StaffRotaPeriod_StaffRotaPartner')
BEGIN
    CREATE TABLE [tbl_StaffRota] (
        [StaffRotaId] int NOT NULL IDENTITY,
        [RotaDate] nvarchar(15) NOT NULL,
        [Staff] int NOT NULL,
        [RotaId] int NOT NULL,
        [Remark] nvarchar(225) NOT NULL,
        [ReferenceNumber] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_tbl_StaffRota] PRIMARY KEY ([StaffRotaId]),
        CONSTRAINT [FK_tbl_StaffRota_tbl_ClientRotaName_RotaId] FOREIGN KEY ([RotaId]) REFERENCES [tbl_ClientRotaName] ([RotaId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200424232352_tbl_StaffRota_StaffRotaPeriod_StaffRotaPartner')
BEGIN
    CREATE TABLE [tbl_StaffRotaPartner] (
        [StaffRotaPartnerId] int NOT NULL IDENTITY,
        [StaffRotaId] int NOT NULL,
        [StaffId] int NOT NULL,
        CONSTRAINT [PK_tbl_StaffRotaPartner] PRIMARY KEY ([StaffRotaPartnerId]),
        CONSTRAINT [FK_tbl_StaffRotaPartner_tbl_StaffRota_StaffRotaId] FOREIGN KEY ([StaffRotaId]) REFERENCES [tbl_StaffRota] ([StaffRotaId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200424232352_tbl_StaffRota_StaffRotaPeriod_StaffRotaPartner')
BEGIN
    CREATE TABLE [tbl_StaffRotaPeriod] (
        [StaffRotaPeriodId] int NOT NULL IDENTITY,
        [StaffRotaId] int NOT NULL,
        [ClientRotaTypeId] int NOT NULL,
        CONSTRAINT [PK_tbl_StaffRotaPeriod] PRIMARY KEY ([StaffRotaPeriodId]),
        CONSTRAINT [FK_tbl_StaffRotaPeriod_tbl_ClientRotaType_ClientRotaTypeId] FOREIGN KEY ([ClientRotaTypeId]) REFERENCES [tbl_ClientRotaType] ([ClientRotaTypeId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_StaffRotaPeriod_tbl_StaffRota_StaffRotaId] FOREIGN KEY ([StaffRotaId]) REFERENCES [tbl_StaffRota] ([StaffRotaId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200424232352_tbl_StaffRota_StaffRotaPeriod_StaffRotaPartner')
BEGIN
    CREATE INDEX [IX_tbl_StaffRota_RotaId] ON [tbl_StaffRota] ([RotaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200424232352_tbl_StaffRota_StaffRotaPeriod_StaffRotaPartner')
BEGIN
    CREATE INDEX [IX_tbl_StaffRotaPartner_StaffRotaId] ON [tbl_StaffRotaPartner] ([StaffRotaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200424232352_tbl_StaffRota_StaffRotaPeriod_StaffRotaPartner')
BEGIN
    CREATE INDEX [IX_tbl_StaffRotaPeriod_ClientRotaTypeId] ON [tbl_StaffRotaPeriod] ([ClientRotaTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200424232352_tbl_StaffRota_StaffRotaPeriod_StaffRotaPartner')
BEGIN
    CREATE INDEX [IX_tbl_StaffRotaPeriod_StaffRotaId] ON [tbl_StaffRotaPeriod] ([StaffRotaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200424232352_tbl_StaffRota_StaffRotaPeriod_StaffRotaPartner')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200424232352_tbl_StaffRota_StaffRotaPeriod_StaffRotaPartner', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200424234433_tbl_StaffRota_Remark_RefNumber')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffRota]') AND [c].[name] = N'Remark');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffRota] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [tbl_StaffRota] ALTER COLUMN [Remark] nvarchar(225) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200424234433_tbl_StaffRota_Remark_RefNumber')
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffRota]') AND [c].[name] = N'ReferenceNumber');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffRota] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [tbl_StaffRota] ALTER COLUMN [ReferenceNumber] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200424234433_tbl_StaffRota_Remark_RefNumber')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200424234433_tbl_StaffRota_Remark_RefNumber', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200430100034_tbl_StaffRotaDynamicAddition')
BEGIN
    CREATE TABLE [tbl_StaffRotaDynamicAddition] (
        [StaffRotaDynamicAdditionId] int NOT NULL IDENTITY,
        [Deleted] bit NOT NULL,
        [ItemName] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_tbl_StaffRotaDynamicAddition] PRIMARY KEY ([StaffRotaDynamicAdditionId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200430100034_tbl_StaffRotaDynamicAddition')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200430100034_tbl_StaffRotaDynamicAddition', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200430233111_tbl_StaffRotaItem')
BEGIN
    CREATE TABLE [tbl_StaffRotaItem] (
        [StaffRotaItemId] int NOT NULL IDENTITY,
        [StaffRotaId] int NOT NULL,
        CONSTRAINT [PK_tbl_StaffRotaItem] PRIMARY KEY ([StaffRotaItemId]),
        CONSTRAINT [FK_tbl_StaffRotaItem_tbl_StaffRota_StaffRotaId] FOREIGN KEY ([StaffRotaId]) REFERENCES [tbl_StaffRota] ([StaffRotaId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200430233111_tbl_StaffRotaItem')
BEGIN
    CREATE INDEX [IX_tbl_StaffRotaItem_StaffRotaId] ON [tbl_StaffRotaItem] ([StaffRotaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200430233111_tbl_StaffRotaItem')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200430233111_tbl_StaffRotaItem', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200501002338_tbl_StaffRotaItem_StaffRotaDynamicAdditionId')
BEGIN
    ALTER TABLE [tbl_StaffRotaItem] ADD [StaffRotaDynamicAdditionId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200501002338_tbl_StaffRotaItem_StaffRotaDynamicAdditionId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200501002338_tbl_StaffRotaItem_StaffRotaDynamicAdditionId', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200501212302_tbl_staffRota_RotaDate_ChangeColumn')
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffRota]') AND [c].[name] = N'RotaDate');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffRota] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [tbl_StaffRota] ALTER COLUMN [RotaDate] datetime2 NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200501212302_tbl_staffRota_RotaDate_ChangeColumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200501212302_tbl_staffRota_RotaDate_ChangeColumn', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200501214217_tbl_staffRota_RotaDate_DateType')
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffRota]') AND [c].[name] = N'RotaDate');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffRota] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [tbl_StaffRota] ALTER COLUMN [RotaDate] date NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200501214217_tbl_staffRota_RotaDate_DateType')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200501214217_tbl_staffRota_RotaDate_DateType', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200504201125_tbl_ClientRotaDays_RotaId')
BEGIN
    ALTER TABLE [tbl_ClientRotaDays] ADD [RotaId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200504201125_tbl_ClientRotaDays_RotaId')
BEGIN
    CREATE INDEX [IX_tbl_ClientRotaDays_RotaId] ON [tbl_ClientRotaDays] ([RotaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200504201125_tbl_ClientRotaDays_RotaId')
BEGIN
    ALTER TABLE [tbl_ClientRotaDays] ADD CONSTRAINT [FK_tbl_ClientRotaDays_tbl_ClientRotaName_RotaId] FOREIGN KEY ([RotaId]) REFERENCES [tbl_ClientRotaName] ([RotaId]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200504201125_tbl_ClientRotaDays_RotaId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200504201125_tbl_ClientRotaDays_RotaId', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200509013557_tbl_StaffRotaPeriod_ClockIn_ClockOut')
BEGIN
    ALTER TABLE [tbl_StaffRotaPeriod] ADD [ClockInTime] nvarchar(15) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200509013557_tbl_StaffRotaPeriod_ClockIn_ClockOut')
BEGIN
    ALTER TABLE [tbl_StaffRotaPeriod] ADD [ClockOutTime] nvarchar(15) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200509013557_tbl_StaffRotaPeriod_ClockIn_ClockOut')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200509013557_tbl_StaffRotaPeriod_ClockIn_ClockOut', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200510181743_tbl_StaffRota_RotaDayofWeekId')
BEGIN
    ALTER TABLE [tbl_StaffRota] ADD [RotaDayofWeekId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200510181743_tbl_StaffRota_RotaDayofWeekId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200510181743_tbl_StaffRota_RotaDayofWeekId', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200513114541_tbl_StaffRotaPeriod_Edit')
BEGIN
    ALTER TABLE [tbl_StaffRotaPeriod] ADD [ClickInAddress] nvarchar(100) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200513114541_tbl_StaffRotaPeriod_Edit')
BEGIN
    ALTER TABLE [tbl_StaffRotaPeriod] ADD [ClickOutAddress] nvarchar(100) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200513114541_tbl_StaffRotaPeriod_Edit')
BEGIN
    ALTER TABLE [tbl_StaffRotaPeriod] ADD [Comment] nvarchar(225) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200513114541_tbl_StaffRotaPeriod_Edit')
BEGIN
    ALTER TABLE [tbl_StaffRotaPeriod] ADD [Feedback] nvarchar(225) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200513114541_tbl_StaffRotaPeriod_Edit')
BEGIN
    ALTER TABLE [tbl_StaffRotaPeriod] ADD [HandOver] nvarchar(225) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200513114541_tbl_StaffRotaPeriod_Edit')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200513114541_tbl_StaffRotaPeriod_Edit', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200513222053_tbl_StaffRotaPeriod_ColumnRename')
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffRotaPeriod]') AND [c].[name] = N'ClickInAddress');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffRotaPeriod] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [tbl_StaffRotaPeriod] DROP COLUMN [ClickInAddress];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200513222053_tbl_StaffRotaPeriod_ColumnRename')
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffRotaPeriod]') AND [c].[name] = N'ClickOutAddress');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffRotaPeriod] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [tbl_StaffRotaPeriod] DROP COLUMN [ClickOutAddress];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200513222053_tbl_StaffRotaPeriod_ColumnRename')
BEGIN
    ALTER TABLE [tbl_StaffRotaPeriod] ADD [ClockInAddress] nvarchar(100) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200513222053_tbl_StaffRotaPeriod_ColumnRename')
BEGIN
    ALTER TABLE [tbl_StaffRotaPeriod] ADD [ClockOutAddress] nvarchar(100) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200513222053_tbl_StaffRotaPeriod_ColumnRename')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200513222053_tbl_StaffRotaPeriod_ColumnRename', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518230543_tbl_StaffRating_tbl_staffpersonalinfoUpdate')
BEGIN
    ALTER TABLE [tbl_StaffPersonalInfo] ADD [EmploymentDate] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518230543_tbl_StaffRating_tbl_staffpersonalinfoUpdate')
BEGIN
    ALTER TABLE [tbl_StaffPersonalInfo] ADD [HasIdCard] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518230543_tbl_StaffRating_tbl_staffpersonalinfoUpdate')
BEGIN
    ALTER TABLE [tbl_StaffPersonalInfo] ADD [HasUniform] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518230543_tbl_StaffRating_tbl_staffpersonalinfoUpdate')
BEGIN
    ALTER TABLE [tbl_StaffPersonalInfo] ADD [IsTeamLeader] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518230543_tbl_StaffRating_tbl_staffpersonalinfoUpdate')
BEGIN
    ALTER TABLE [tbl_StaffPersonalInfo] ADD [JobCategory] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518230543_tbl_StaffRating_tbl_staffpersonalinfoUpdate')
BEGIN
    ALTER TABLE [tbl_StaffPersonalInfo] ADD [PlaceOfBirth] nvarchar(50) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518230543_tbl_StaffRating_tbl_staffpersonalinfoUpdate')
BEGIN
    CREATE TABLE [tbl_StaffRating] (
        [StaffRatingId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [ClientId] int NOT NULL,
        [Comment] nvarchar(max) NOT NULL,
        [CommentDate] datetime2 NOT NULL,
        [Rating] int NOT NULL,
        [RatedBy] int NOT NULL,
        CONSTRAINT [PK_tbl_StaffRating] PRIMARY KEY ([StaffRatingId]),
        CONSTRAINT [FK_tbl_StaffRating_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518230543_tbl_StaffRating_tbl_staffpersonalinfoUpdate')
BEGIN
    CREATE INDEX [IX_tbl_StaffRating_StaffPersonalInfoId] ON [tbl_StaffRating] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518230543_tbl_StaffRating_tbl_staffpersonalinfoUpdate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200518230543_tbl_StaffRating_tbl_staffpersonalinfoUpdate', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200519032015_tbl_StaffRating_ChangeColumn_RatedBy')
BEGIN
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffRating]') AND [c].[name] = N'RatedBy');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffRating] DROP CONSTRAINT [' + @var25 + '];');
    ALTER TABLE [tbl_StaffRating] DROP COLUMN [RatedBy];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200519032015_tbl_StaffRating_ChangeColumn_RatedBy')
BEGIN
    ALTER TABLE [tbl_StaffRating] ADD [SubmittedBy] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200519032015_tbl_StaffRating_ChangeColumn_RatedBy')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200519032015_tbl_StaffRating_ChangeColumn_RatedBy', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520123950_tbl_StaffBlackList')
BEGIN
    CREATE TABLE [tbl_StaffBlackList] (
        [StaffBlackListId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [ClientId] int NOT NULL,
        [Comment] nvarchar(225) NOT NULL,
        CONSTRAINT [PK_tbl_StaffBlackList] PRIMARY KEY ([StaffBlackListId]),
        CONSTRAINT [FK_tbl_StaffBlackList_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_StaffBlackList_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520123950_tbl_StaffBlackList')
BEGIN
    CREATE INDEX [IX_tbl_StaffBlackList_ClientId] ON [tbl_StaffBlackList] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520123950_tbl_StaffBlackList')
BEGIN
    CREATE INDEX [IX_tbl_StaffBlackList_StaffPersonalInfoId] ON [tbl_StaffBlackList] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520123950_tbl_StaffBlackList')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200520123950_tbl_StaffBlackList', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520204051_tbl_StaffBlackList_Date')
BEGIN
    ALTER TABLE [tbl_StaffBlackList] ADD [Date] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520204051_tbl_StaffBlackList_Date')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200520204051_tbl_StaffBlackList_Date', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200529211456_tbl_Communication')
BEGIN
    CREATE TABLE [tbl_Communication] (
        [CommunicationId] int NOT NULL IDENTITY,
        [FromUserId] int NOT NULL,
        [ToUserId] int NOT NULL,
        [Message] nvarchar(max) NOT NULL,
        [CommuncationDate] datetime2 NOT NULL,
        [IsRead] bit NOT NULL,
        CONSTRAINT [PK_tbl_Communication] PRIMARY KEY ([CommunicationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200529211456_tbl_Communication')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200529211456_tbl_Communication', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200531092517_tbl_Communication_From_To_ChangeType')
BEGIN
    DECLARE @var26 sysname;
    SELECT @var26 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Communication]') AND [c].[name] = N'ToUserId');
    IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Communication] DROP CONSTRAINT [' + @var26 + '];');
    ALTER TABLE [tbl_Communication] ALTER COLUMN [ToUserId] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200531092517_tbl_Communication_From_To_ChangeType')
BEGIN
    DECLARE @var27 sysname;
    SELECT @var27 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Communication]') AND [c].[name] = N'FromUserId');
    IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Communication] DROP CONSTRAINT [' + @var27 + '];');
    ALTER TABLE [tbl_Communication] ALTER COLUMN [FromUserId] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200531092517_tbl_Communication_From_To_ChangeType')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200531092517_tbl_Communication_From_To_ChangeType', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200605002639_tbl_Communication_Add_Subject')
BEGIN
    ALTER TABLE [tbl_Communication] ADD [Subject] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200605002639_tbl_Communication_Add_Subject')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200605002639_tbl_Communication_Add_Subject', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200605004754_tbl_Communication_Add_Subject_Update')
BEGIN
    DECLARE @var28 sysname;
    SELECT @var28 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Communication]') AND [c].[name] = N'Subject');
    IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Communication] DROP CONSTRAINT [' + @var28 + '];');
    ALTER TABLE [tbl_Communication] ALTER COLUMN [Subject] nvarchar(125) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200605004754_tbl_Communication_Add_Subject_Update')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200605004754_tbl_Communication_Add_Subject_Update', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200620235151_tbl_BaseRecordItem_GoogleForms')
BEGIN
    ALTER TABLE [tbl_BaseRecordItem] ADD [AddLink] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200620235151_tbl_BaseRecordItem_GoogleForms')
BEGIN
    ALTER TABLE [tbl_BaseRecordItem] ADD [HasGoogleForm] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200620235151_tbl_BaseRecordItem_GoogleForms')
BEGIN
    ALTER TABLE [tbl_BaseRecordItem] ADD [ViewLink] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200620235151_tbl_BaseRecordItem_GoogleForms')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200620235151_tbl_BaseRecordItem_GoogleForms', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200704223233_tbl_StaffIncidentReporting')
BEGIN
    CREATE TABLE [tbl_StaffIncidentReporting] (
        [StaffIncidentReportingId] int NOT NULL IDENTITY,
        [ReportingStaffId] int NULL,
        [ClientId] int NOT NULL,
        [StaffInvolvedId] int NOT NULL,
        [IncidentType] int NOT NULL,
        [IncidentDetails] nvarchar(max) NOT NULL,
        [ActionTaken] nvarchar(250) NULL,
        [Witness] nvarchar(max) NULL,
        [Attachment] nvarchar(max) NULL,
        CONSTRAINT [PK_tbl_StaffIncidentReporting] PRIMARY KEY ([StaffIncidentReportingId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200704223233_tbl_StaffIncidentReporting')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200704223233_tbl_StaffIncidentReporting', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200704224927_tbl_StaffIncidentReporting_LoggedById_LoggedDate')
BEGIN
    ALTER TABLE [tbl_StaffIncidentReporting] ADD [LoggedById] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200704224927_tbl_StaffIncidentReporting_LoggedById_LoggedDate')
BEGIN
    ALTER TABLE [tbl_StaffIncidentReporting] ADD [LoggedDate] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.0000000+00:00';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200704224927_tbl_StaffIncidentReporting_LoggedById_LoggedDate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200704224927_tbl_StaffIncidentReporting_LoggedById_LoggedDate', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200705231208_tbl_StaffIncidentReporting_LoggedById_Update')
BEGIN
    DECLARE @var29 sysname;
    SELECT @var29 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffIncidentReporting]') AND [c].[name] = N'LoggedById');
    IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffIncidentReporting] DROP CONSTRAINT [' + @var29 + '];');
    ALTER TABLE [tbl_StaffIncidentReporting] ALTER COLUMN [LoggedById] nvarchar(225) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200705231208_tbl_StaffIncidentReporting_LoggedById_Update')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200705231208_tbl_StaffIncidentReporting_LoggedById_Update', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200706234919_tbl_ClientServiceDetail')
BEGIN
    CREATE TABLE [tbl_ClientServiceDetail] (
        [ClientServiceDetailId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [ClientId] int NOT NULL,
        [AmountGiven] decimal(18,2) NOT NULL,
        [AmountReturned] decimal(18,2) NOT NULL,
        [ServiceDate] datetimeoffset NOT NULL,
        CONSTRAINT [PK_tbl_ClientServiceDetail] PRIMARY KEY ([ClientServiceDetailId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200706234919_tbl_ClientServiceDetail')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200706234919_tbl_ClientServiceDetail', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200707000948_tbl_ClientServiceDetailReceipt')
BEGIN
    CREATE TABLE [tbl_ClientServiceDetailReceipt] (
        [ClientServiceDetailReceiptId] int NOT NULL IDENTITY,
        [ClientServiceDetailId] int NOT NULL,
        [Attachment] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_tbl_ClientServiceDetailReceipt] PRIMARY KEY ([ClientServiceDetailReceiptId]),
        CONSTRAINT [FK_tbl_ClientServiceDetailReceipt_tbl_ClientServiceDetail_ClientServiceDetailId] FOREIGN KEY ([ClientServiceDetailId]) REFERENCES [tbl_ClientServiceDetail] ([ClientServiceDetailId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200707000948_tbl_ClientServiceDetailReceipt')
BEGIN
    CREATE INDEX [IX_tbl_ClientServiceDetailReceipt_ClientServiceDetailId] ON [tbl_ClientServiceDetailReceipt] ([ClientServiceDetailId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200707000948_tbl_ClientServiceDetailReceipt')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200707000948_tbl_ClientServiceDetailReceipt', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200707002432_tbl_ClientServiceDetailItem')
BEGIN
    CREATE TABLE [tbl_ClientServiceDetailItem] (
        [ClientServiceDetailItemId] int NOT NULL IDENTITY,
        [ClientServiceDetailId] int NOT NULL,
        [ItemName] nvarchar(225) NOT NULL,
        [Quantity] int NOT NULL,
        [Rate] decimal(18,2) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_tbl_ClientServiceDetailItem] PRIMARY KEY ([ClientServiceDetailItemId]),
        CONSTRAINT [FK_tbl_ClientServiceDetailItem_tbl_ClientServiceDetail_ClientServiceDetailId] FOREIGN KEY ([ClientServiceDetailId]) REFERENCES [tbl_ClientServiceDetail] ([ClientServiceDetailId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200707002432_tbl_ClientServiceDetailItem')
BEGIN
    CREATE INDEX [IX_tbl_ClientServiceDetailItem_ClientServiceDetailId] ON [tbl_ClientServiceDetailItem] ([ClientServiceDetailId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200707002432_tbl_ClientServiceDetailItem')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200707002432_tbl_ClientServiceDetailItem', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200710224859_tbl_Investigation')
BEGIN
    CREATE TABLE [tbl_Investigation] (
        [InvestigationId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [ClientId] int NOT NULL,
        [IncidentClass] int NOT NULL,
        [Remark] nvarchar(500) NOT NULL,
        [IncidentDate] datetimeoffset NOT NULL,
        [ConclusionDate] datetimeoffset NULL,
        CONSTRAINT [PK_tbl_Investigation] PRIMARY KEY ([InvestigationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200710224859_tbl_Investigation')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200710224859_tbl_Investigation', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200710231015_tbl_InvestigationAttachment')
BEGIN
    CREATE TABLE [tbl_InvestigationAttachment] (
        [InvestigationAttachmentId] int NOT NULL IDENTITY,
        [InvestigationId] int NOT NULL,
        [Attachment] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_tbl_InvestigationAttachment] PRIMARY KEY ([InvestigationAttachmentId]),
        CONSTRAINT [FK_tbl_InvestigationAttachment_tbl_Investigation_InvestigationId] FOREIGN KEY ([InvestigationId]) REFERENCES [tbl_Investigation] ([InvestigationId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200710231015_tbl_InvestigationAttachment')
BEGIN
    CREATE INDEX [IX_tbl_InvestigationAttachment_InvestigationId] ON [tbl_InvestigationAttachment] ([InvestigationId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200710231015_tbl_InvestigationAttachment')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200710231015_tbl_InvestigationAttachment', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711132334_tbl_Investigation_Remark_Update')
BEGIN
    DECLARE @var30 sysname;
    SELECT @var30 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Investigation]') AND [c].[name] = N'Remark');
    IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Investigation] DROP CONSTRAINT [' + @var30 + '];');
    ALTER TABLE [tbl_Investigation] ALTER COLUMN [Remark] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711132334_tbl_Investigation_Remark_Update')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200711132334_tbl_Investigation_Remark_Update', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926204534_tbl_ShiftBookingBlockedDays')
BEGIN
    CREATE TABLE [tbl_ShiftBookingBlockedDays] (
        [ShiftBookingBlockedDaysId] int NOT NULL IDENTITY,
        [ShiftBookingId] int NOT NULL,
        [Day] nvarchar(2) NOT NULL,
        [WeekDay] nvarchar(15) NOT NULL,
        CONSTRAINT [PK_tbl_ShiftBookingBlockedDays] PRIMARY KEY ([ShiftBookingBlockedDaysId]),
        CONSTRAINT [FK_tbl_ShiftBookingBlockedDays_tbl_ShiftBooking_ShiftBookingId] FOREIGN KEY ([ShiftBookingId]) REFERENCES [tbl_ShiftBooking] ([ShiftBookingId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926204534_tbl_ShiftBookingBlockedDays')
BEGIN
    CREATE INDEX [IX_tbl_ShiftBookingBlockedDays_ShiftBookingId] ON [tbl_ShiftBookingBlockedDays] ([ShiftBookingId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926204534_tbl_ShiftBookingBlockedDays')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200926204534_tbl_ShiftBookingBlockedDays', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201205205628_tbl_Untowards_HospitalName')
BEGIN
    ALTER TABLE [tbl_Untowards] ADD [EntryHospitalName] nvarchar(250) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201205205628_tbl_Untowards_HospitalName')
BEGIN
    ALTER TABLE [tbl_Untowards] ADD [ExitHospitalName] nvarchar(250) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201205205628_tbl_Untowards_HospitalName')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201205205628_tbl_Untowards_HospitalName', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210315180315_tbl_StaffRotaPeriod_ClockIn_ClockOut_DateTimeOffset')
BEGIN
    DECLARE @var31 sysname;
    SELECT @var31 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffRotaPeriod]') AND [c].[name] = N'ClockOutTime');
    IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffRotaPeriod] DROP CONSTRAINT [' + @var31 + '];');
    ALTER TABLE [tbl_StaffRotaPeriod] ALTER COLUMN [ClockOutTime] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210315180315_tbl_StaffRotaPeriod_ClockIn_ClockOut_DateTimeOffset')
BEGIN
    DECLARE @var32 sysname;
    SELECT @var32 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffRotaPeriod]') AND [c].[name] = N'ClockOutAddress');
    IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffRotaPeriod] DROP CONSTRAINT [' + @var32 + '];');
    ALTER TABLE [tbl_StaffRotaPeriod] ALTER COLUMN [ClockOutAddress] nvarchar(300) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210315180315_tbl_StaffRotaPeriod_ClockIn_ClockOut_DateTimeOffset')
BEGIN
    DECLARE @var33 sysname;
    SELECT @var33 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffRotaPeriod]') AND [c].[name] = N'ClockInTime');
    IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffRotaPeriod] DROP CONSTRAINT [' + @var33 + '];');
    ALTER TABLE [tbl_StaffRotaPeriod] ALTER COLUMN [ClockInTime] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210315180315_tbl_StaffRotaPeriod_ClockIn_ClockOut_DateTimeOffset')
BEGIN
    DECLARE @var34 sysname;
    SELECT @var34 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_StaffRotaPeriod]') AND [c].[name] = N'ClockInAddress');
    IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [tbl_StaffRotaPeriod] DROP CONSTRAINT [' + @var34 + '];');
    ALTER TABLE [tbl_StaffRotaPeriod] ALTER COLUMN [ClockInAddress] nvarchar(300) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210315180315_tbl_StaffRotaPeriod_ClockIn_ClockOut_DateTimeOffset')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210315180315_tbl_StaffRotaPeriod_ClockIn_ClockOut_DateTimeOffset', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210316051855_tbl_StaffRotaPeriod_ClockIn_ClockOut_Mode')
BEGIN
    ALTER TABLE [tbl_StaffRotaPeriod] ADD [ClockInMode] nvarchar(225) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210316051855_tbl_StaffRotaPeriod_ClockIn_ClockOut_Mode')
BEGIN
    ALTER TABLE [tbl_StaffRotaPeriod] ADD [ClockOutMode] nvarchar(225) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210316051855_tbl_StaffRotaPeriod_ClockIn_ClockOut_Mode')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210316051855_tbl_StaffRotaPeriod_ClockIn_ClockOut_Mode', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210316053937_tbl_StaffRotaTask')
BEGIN
    CREATE TABLE [tbl_StaffRotaTask] (
        [StaffRotaTaskId] int NOT NULL IDENTITY,
        [StaffRotaPeriodId] int NOT NULL,
        [RotaTaskId] int NOT NULL,
        [IsGiven] bit NOT NULL,
        CONSTRAINT [PK_tbl_StaffRotaTask] PRIMARY KEY ([StaffRotaTaskId]),
        CONSTRAINT [FK_tbl_StaffRotaTask_tbl_StaffRotaPeriod_StaffRotaPeriodId] FOREIGN KEY ([StaffRotaPeriodId]) REFERENCES [tbl_StaffRotaPeriod] ([StaffRotaPeriodId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210316053937_tbl_StaffRotaTask')
BEGIN
    CREATE INDEX [IX_tbl_StaffRotaTask_StaffRotaPeriodId] ON [tbl_StaffRotaTask] ([StaffRotaPeriodId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210316053937_tbl_StaffRotaTask')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210316053937_tbl_StaffRotaTask', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210320074658_tbl_Client_Address_Geolocation')
BEGIN
    ALTER TABLE [tbl_Client] ADD [Address] nvarchar(250) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210320074658_tbl_Client_Address_Geolocation')
BEGIN
    ALTER TABLE [tbl_Client] ADD [Latitude] nvarchar(250) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210320074658_tbl_Client_Address_Geolocation')
BEGIN
    ALTER TABLE [tbl_Client] ADD [Longitude] nvarchar(250) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210320074658_tbl_Client_Address_Geolocation')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210320074658_tbl_Client_Address_Geolocation', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DROP INDEX [IX_tbl_Client_ComplainRegister_ComplainId] ON [tbl_Client_ComplainRegister];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    ALTER TABLE [tbl_StaffRotaPeriod] ADD [StartTime] nvarchar(50) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    ALTER TABLE [tbl_StaffRotaPeriod] ADD [StopTime] nvarchar(50) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DECLARE @var35 sysname;
    SELECT @var35 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client_ComplainRegister]') AND [c].[name] = N'SOURCEOFCOMPLAINTS');
    IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client_ComplainRegister] DROP CONSTRAINT [' + @var35 + '];');
    ALTER TABLE [tbl_Client_ComplainRegister] ALTER COLUMN [SOURCEOFCOMPLAINTS] nvarchar(255) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DECLARE @var36 sysname;
    SELECT @var36 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client_ComplainRegister]') AND [c].[name] = N'ROOTCAUSE');
    IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client_ComplainRegister] DROP CONSTRAINT [' + @var36 + '];');
    ALTER TABLE [tbl_Client_ComplainRegister] ALTER COLUMN [ROOTCAUSE] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DECLARE @var37 sysname;
    SELECT @var37 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client_ComplainRegister]') AND [c].[name] = N'REMARK');
    IF @var37 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client_ComplainRegister] DROP CONSTRAINT [' + @var37 + '];');
    ALTER TABLE [tbl_Client_ComplainRegister] ALTER COLUMN [REMARK] nvarchar(255) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DECLARE @var38 sysname;
    SELECT @var38 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client_ComplainRegister]') AND [c].[name] = N'LINK');
    IF @var38 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client_ComplainRegister] DROP CONSTRAINT [' + @var38 + '];');
    ALTER TABLE [tbl_Client_ComplainRegister] ALTER COLUMN [LINK] nvarchar(255) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DECLARE @var39 sysname;
    SELECT @var39 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client_ComplainRegister]') AND [c].[name] = N'LETTERTOSTAFF');
    IF @var39 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client_ComplainRegister] DROP CONSTRAINT [' + @var39 + '];');
    ALTER TABLE [tbl_Client_ComplainRegister] ALTER COLUMN [LETTERTOSTAFF] nvarchar(255) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DECLARE @var40 sysname;
    SELECT @var40 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client_ComplainRegister]') AND [c].[name] = N'IRFNUMBER ');
    IF @var40 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client_ComplainRegister] DROP CONSTRAINT [' + @var40 + '];');
    ALTER TABLE [tbl_Client_ComplainRegister] ALTER COLUMN [IRFNUMBER ] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DECLARE @var41 sysname;
    SELECT @var41 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client_ComplainRegister]') AND [c].[name] = N'INVESTIGATIONOUTCOME');
    IF @var41 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client_ComplainRegister] DROP CONSTRAINT [' + @var41 + '];');
    ALTER TABLE [tbl_Client_ComplainRegister] ALTER COLUMN [INVESTIGATIONOUTCOME] nvarchar(255) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DECLARE @var42 sysname;
    SELECT @var42 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client_ComplainRegister]') AND [c].[name] = N'FINALRESPONSETOFAMILY');
    IF @var42 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client_ComplainRegister] DROP CONSTRAINT [' + @var42 + '];');
    ALTER TABLE [tbl_Client_ComplainRegister] ALTER COLUMN [FINALRESPONSETOFAMILY] nvarchar(255) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DECLARE @var43 sysname;
    SELECT @var43 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client_ComplainRegister]') AND [c].[name] = N'EvidenceFilePath');
    IF @var43 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client_ComplainRegister] DROP CONSTRAINT [' + @var43 + '];');
    ALTER TABLE [tbl_Client_ComplainRegister] ALTER COLUMN [EvidenceFilePath] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DECLARE @var44 sysname;
    SELECT @var44 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client_ComplainRegister]') AND [c].[name] = N'DATEOFACKNOWLEDGEMENT');
    IF @var44 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client_ComplainRegister] DROP CONSTRAINT [' + @var44 + '];');
    ALTER TABLE [tbl_Client_ComplainRegister] ALTER COLUMN [DATEOFACKNOWLEDGEMENT] datetime2 NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DECLARE @var45 sysname;
    SELECT @var45 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client_ComplainRegister]') AND [c].[name] = N'CONCERNSRAISED');
    IF @var45 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client_ComplainRegister] DROP CONSTRAINT [' + @var45 + '];');
    ALTER TABLE [tbl_Client_ComplainRegister] ALTER COLUMN [CONCERNSRAISED] nvarchar(255) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DECLARE @var46 sysname;
    SELECT @var46 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client_ComplainRegister]') AND [c].[name] = N'COMPLAINANTCONTACT');
    IF @var46 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client_ComplainRegister] DROP CONSTRAINT [' + @var46 + '];');
    ALTER TABLE [tbl_Client_ComplainRegister] ALTER COLUMN [COMPLAINANTCONTACT] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    DECLARE @var47 sysname;
    SELECT @var47 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Client_ComplainRegister]') AND [c].[name] = N'ACTIONTAKEN');
    IF @var47 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Client_ComplainRegister] DROP CONSTRAINT [' + @var47 + '];');
    ALTER TABLE [tbl_Client_ComplainRegister] ALTER COLUMN [ACTIONTAKEN] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210523234333_tbl_StaffRotaPeriod_StartTime_StopTime', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_BloodCoagulationRecord] (
        [BloodRecordId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] datetime2 NOT NULL,
        [Indication] int NOT NULL,
        [TargetINR] int NOT NULL,
        [TargetINRAttach] nvarchar(max) NULL,
        [StartDate] datetime2 NOT NULL,
        [CurrentDose] int NOT NULL,
        [INR] int NOT NULL,
        [NewDose] int NOT NULL,
        [NewINR] int NOT NULL,
        [BloodStatus] int NOT NULL,
        [Comment] nvarchar(max) NOT NULL,
        [PhysicianResponce] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remark] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_BloodCoagulationRecord] PRIMARY KEY ([BloodRecordId]),
        CONSTRAINT [FK_tbl_Client_BloodCoagulationRecord_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_BloodPressure] (
        [BloodPressureId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] datetime2 NOT NULL,
        [GoalSystolic] int NOT NULL,
        [GoalDiastolic] int NOT NULL,
        [ReadingSystolic] int NOT NULL,
        [ReadingDiastolic] int NOT NULL,
        [StatusImage] int NOT NULL,
        [Comment] nvarchar(max) NOT NULL,
        [StatusAttach] nvarchar(max) NULL,
        [PhysicianResponse] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_BloodPressure] PRIMARY KEY ([BloodPressureId]),
        CONSTRAINT [FK_tbl_Client_BloodPressure_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_BMIChart] (
        [BMIChartId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] datetime2 NOT NULL,
        [Height] int NOT NULL,
        [Weight] nvarchar(max) NOT NULL,
        [NumberRange] int NOT NULL,
        [SeeChart] int NOT NULL,
        [SeeChartAttach] nvarchar(max) NULL,
        [Comment] nvarchar(max) NOT NULL,
        [PhysicianResponse] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_BMIChart] PRIMARY KEY ([BMIChartId]),
        CONSTRAINT [FK_tbl_Client_BMIChart_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_BodyTemp] (
        [BodyTempId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] datetime2 NOT NULL,
        [TargetTemp] int NOT NULL,
        [TargetTempAttach] nvarchar(max) NULL,
        [CurrentReading] nvarchar(max) NOT NULL,
        [SeeChart] int NOT NULL,
        [SeeChartAttach] nvarchar(max) NULL,
        [Comment] nvarchar(max) NOT NULL,
        [PhysicianResponse] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_BodyTemp] PRIMARY KEY ([BodyTempId]),
        CONSTRAINT [FK_tbl_Client_BodyTemp_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_BowelMovement] (
        [BowelMovementId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] datetime2 NOT NULL,
        [Type] int NOT NULL,
        [TypeAttach] nvarchar(max) NULL,
        [Size] int NOT NULL,
        [Color] int NOT NULL,
        [ColorAttach] nvarchar(max) NULL,
        [StatusImage] int NOT NULL,
        [StatusAttach] nvarchar(max) NULL,
        [Comment] nvarchar(max) NOT NULL,
        [PhysicianResponse] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_BowelMovement] PRIMARY KEY ([BowelMovementId]),
        CONSTRAINT [FK_tbl_Client_BowelMovement_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_ComplainRegister] (
        [ComplainId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [ClientId] int NOT NULL,
        [LINK] nvarchar(max) NOT NULL,
        [IRFNUMBER ] nvarchar(max) NOT NULL,
        [INCIDENTDATE] datetime2 NOT NULL,
        [DATERECIEVED] datetime2 NOT NULL,
        [DATEOFACKNOWLEDGEMENT] datetime2 NOT NULL,
        [SOURCEOFCOMPLAINTS] nvarchar(max) NOT NULL,
        [COMPLAINANTCONTACT] nvarchar(max) NOT NULL,
        [CONCERNSRAISED] nvarchar(max) NOT NULL,
        [DUEDATE] datetime2 NOT NULL,
        [LETTERTOSTAFF] nvarchar(max) NOT NULL,
        [INVESTIGATIONOUTCOME] nvarchar(max) NOT NULL,
        [ACTIONTAKEN] nvarchar(max) NOT NULL,
        [FINALRESPONSETOFAMILY] nvarchar(max) NOT NULL,
        [ROOTCAUSE] nvarchar(max) NOT NULL,
        [REMARK] nvarchar(max) NOT NULL,
        [StatusId] int NOT NULL,
        [EvidenceFilePath] nvarchar(max) NULL,
        CONSTRAINT [PK_tbl_Client_ComplainRegister] PRIMARY KEY ([ComplainId]),
        CONSTRAINT [FK_tbl_Client_ComplainRegister_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_EyeHealthMonitoring] (
        [EyeHealthId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] datetime2 NOT NULL,
        [ToolUsed] int NOT NULL,
        [ToolUsedAttach] nvarchar(max) NULL,
        [MethodUsed] int NOT NULL,
        [MethodUsedAttach] nvarchar(max) NULL,
        [TargetSet] int NOT NULL,
        [CurrentScore] int NOT NULL,
        [PatientGlasses] int NOT NULL,
        [Comment] nvarchar(max) NOT NULL,
        [StatusImage] int NOT NULL,
        [StatusAttach] nvarchar(max) NULL,
        [PhysicianResponse] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_EyeHealthMonitoring] PRIMARY KEY ([EyeHealthId]),
        CONSTRAINT [FK_tbl_Client_EyeHealthMonitoring_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_FoodIntake] (
        [FoodIntakeId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] datetime2 NOT NULL,
        [Goal] int NOT NULL,
        [CurrentIntake] int NOT NULL,
        [Comment] nvarchar(max) NOT NULL,
        [StatusImage] int NOT NULL,
        [StatusAttach] nvarchar(max) NULL,
        [PhysicianResponse] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_FoodIntake] PRIMARY KEY ([FoodIntakeId]),
        CONSTRAINT [FK_tbl_Client_FoodIntake_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_HeartRate] (
        [HeartRateId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] datetime2 NOT NULL,
        [TargetHR] int NOT NULL,
        [TargetHRAttach] nvarchar(max) NULL,
        [Gender] int NOT NULL,
        [GenderAttach] nvarchar(max) NULL,
        [Age] int NOT NULL,
        [BeatsPerSeconds] int NOT NULL,
        [Comment] nvarchar(max) NOT NULL,
        [SeeChart] int NOT NULL,
        [SeeChartAttach] nvarchar(max) NULL,
        [PhysicianResponse] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_HeartRate] PRIMARY KEY ([HeartRateId]),
        CONSTRAINT [FK_tbl_Client_HeartRate_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_LogAudit] (
        [LogAuditId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [NextDueDate] datetime2 NOT NULL,
        [IsCareExpected] nvarchar(max) NOT NULL,
        [IsCareDifference] nvarchar(max) NOT NULL,
        [ProperDocumentation] nvarchar(max) NOT NULL,
        [ImproperDocumentation] nvarchar(max) NOT NULL,
        [Communication] nvarchar(max) NOT NULL,
        [ThinkingServiceUsers] nvarchar(max) NOT NULL,
        [ThinkingStaff] nvarchar(max) NOT NULL,
        [ThinkingStaffStop] nvarchar(max) NOT NULL,
        [Observations] nvarchar(max) NOT NULL,
        [NameOfAuditor] nvarchar(max) NOT NULL,
        [ActionRecommended] nvarchar(max) NOT NULL,
        [ActionTaken] nvarchar(max) NOT NULL,
        [EvidenceOfActionTaken] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [RepeatOfIncident] int NOT NULL,
        [RotCause] nvarchar(max) NOT NULL,
        [LessonLearntAndShared] nvarchar(max) NOT NULL,
        [LogURL] nvarchar(max) NOT NULL,
        [EvidenceFilePath] nvarchar(max) NULL,
        [StaffPersonalInfoId] int NULL,
        CONSTRAINT [PK_tbl_Client_LogAudit] PRIMARY KEY ([LogAuditId]),
        CONSTRAINT [FK_tbl_Client_LogAudit_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Client_LogAudit_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_MealType] (
        [ClientMealTypeId] int NOT NULL IDENTITY,
        [Deleted] bit NOT NULL,
        [MealType] nvarchar(15) NOT NULL,
        CONSTRAINT [PK_tbl_Client_MealType] PRIMARY KEY ([ClientMealTypeId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_MedAudit] (
        [MedAuditId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [NextDueDate] datetime2 NOT NULL,
        [GapsInAdmistration] int NOT NULL,
        [RightsOfMedication] nvarchar(max) NOT NULL,
        [MarChartReview] int NOT NULL,
        [MedicationConcern] int NOT NULL,
        [HardCopyReview] int NOT NULL,
        [ThinkingServiceUsers] nvarchar(max) NOT NULL,
        [MedicationSupplyEfficiency] int NOT NULL,
        [MedicationInfoUploadEefficiency] int NOT NULL,
        [Observations] nvarchar(max) NOT NULL,
        [ActionRecommended] nvarchar(max) NOT NULL,
        [ActionTaken] nvarchar(max) NOT NULL,
        [EvidenceOfActionTaken] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [RepeatOfIncident] int NOT NULL,
        [RotCause] nvarchar(max) NOT NULL,
        [LessonLearntAndShared] nvarchar(max) NOT NULL,
        [LogURL] nvarchar(max) NOT NULL,
        [Attachment] nvarchar(max) NULL,
        [StaffPersonalInfoId] int NULL,
        CONSTRAINT [PK_tbl_Client_MedAudit] PRIMARY KEY ([MedAuditId]),
        CONSTRAINT [FK_tbl_Client_MedAudit_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Client_MedAudit_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_MgtVisit] (
        [VisitId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [NextCheckDate] datetime2 NOT NULL,
        [RateServiceRecieving] int NOT NULL,
        [RateManagers] int NOT NULL,
        [HowToComplain] int NOT NULL,
        [ServiceRecommended] int NOT NULL,
        [ImprovementExpect] nvarchar(max) NOT NULL,
        [Observation] nvarchar(max) NOT NULL,
        [ActionRequired] nvarchar(max) NOT NULL,
        [ActionsTakenByMPCC] nvarchar(max) NOT NULL,
        [EvidenceOfActionTaken] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [RotCause] nvarchar(max) NOT NULL,
        [LessonLearntAndShared] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [URL] nvarchar(max) NOT NULL,
        [Attachment] nvarchar(max) NULL,
        [StaffPersonalInfoId] int NULL,
        CONSTRAINT [PK_tbl_Client_MgtVisit] PRIMARY KEY ([VisitId]),
        CONSTRAINT [FK_tbl_Client_MgtVisit_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Client_MgtVisit_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_Nutrition] (
        [NutritionId] int NOT NULL IDENTITY,
        [ClientId] int NOT NULL,
        [StaffId] int NOT NULL,
        [DATEFROM] datetime2 NOT NULL,
        [DATETO] datetime2 NOT NULL,
        [MealSpecialNote] nvarchar(max) NOT NULL,
        [ShoppingSpecialNote] nvarchar(max) NOT NULL,
        [CleaningSpecialNote] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_tbl_Client_Nutrition] PRIMARY KEY ([NutritionId]),
        CONSTRAINT [FK_tbl_Client_Nutrition_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_tbl_Client_Nutrition_tbl_StaffPersonalInfo_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_Oxygenlvl] (
        [OxygenLvlId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] datetime2 NOT NULL,
        [TargetOxygen] int NOT NULL,
        [TargetOxygenAttach] nvarchar(max) NULL,
        [CurrentReading] nvarchar(max) NOT NULL,
        [SeeChart] int NOT NULL,
        [SeeChartAttach] nvarchar(max) NULL,
        [Comment] nvarchar(max) NOT NULL,
        [PhysicianResponse] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_Oxygenlvl] PRIMARY KEY ([OxygenLvlId]),
        CONSTRAINT [FK_tbl_Client_Oxygenlvl_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_PainChart] (
        [PainChartId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] datetime2 NOT NULL,
        [Type] int NOT NULL,
        [TypeAttach] nvarchar(max) NULL,
        [Location] int NOT NULL,
        [LocationAttach] nvarchar(max) NULL,
        [PainLvl] int NOT NULL,
        [Comment] nvarchar(max) NOT NULL,
        [StatusImage] int NOT NULL,
        [StatusAttach] nvarchar(max) NULL,
        [PhysicianResponse] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_PainChart] PRIMARY KEY ([PainChartId]),
        CONSTRAINT [FK_tbl_Client_PainChart_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_Program] (
        [ProgramId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [NextCheckDate] datetime2 NOT NULL,
        [ProgramOfChoice] int NOT NULL,
        [DaysOfChoice] int NOT NULL,
        [PlaceLocationProgram] int NOT NULL,
        [DetailsOfProgram] int NOT NULL,
        [Observation] nvarchar(max) NOT NULL,
        [ActionRequired] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [URL] nvarchar(max) NOT NULL,
        [Attachment] nvarchar(max) NULL,
        [StaffPersonalInfoId] int NULL,
        CONSTRAINT [PK_tbl_Client_Program] PRIMARY KEY ([ProgramId]),
        CONSTRAINT [FK_tbl_Client_Program_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Client_Program_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_PulseRate] (
        [PulseRateId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] datetime2 NOT NULL,
        [TargetPulse] int NOT NULL,
        [TargetPulseAttach] nvarchar(max) NULL,
        [CurrentPulse] nvarchar(max) NOT NULL,
        [Chart] nvarchar(max) NOT NULL,
        [SeeChart] int NOT NULL,
        [SeeChartAttach] nvarchar(max) NULL,
        [Comment] nvarchar(max) NOT NULL,
        [PhysicianResponse] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_PulseRate] PRIMARY KEY ([PulseRateId]),
        CONSTRAINT [FK_tbl_Client_PulseRate_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_Seizure] (
        [SeizureId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] datetime2 NOT NULL,
        [SeizureType] int NOT NULL,
        [SeizureTypeAttach] nvarchar(max) NULL,
        [SeizureLength] int NOT NULL,
        [SeizureLengthAttach] nvarchar(max) NULL,
        [Often] int NOT NULL,
        [WhatHappened] nvarchar(max) NOT NULL,
        [StatusImage] int NOT NULL,
        [StatusAttach] nvarchar(max) NULL,
        [PhysicianResponse] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_Seizure] PRIMARY KEY ([SeizureId]),
        CONSTRAINT [FK_tbl_Client_Seizure_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_ServiceWatch] (
        [WatchId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [NextCheckDate] datetime2 NOT NULL,
        [Incident] int NOT NULL,
        [Details] int NOT NULL,
        [Contact] int NOT NULL,
        [Observation] nvarchar(max) NOT NULL,
        [ActionRequired] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [URL] nvarchar(max) NOT NULL,
        [Attachment] nvarchar(max) NULL,
        [StaffPersonalInfoId] int NULL,
        CONSTRAINT [PK_tbl_Client_ServiceWatch] PRIMARY KEY ([WatchId]),
        CONSTRAINT [FK_tbl_Client_ServiceWatch_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Client_ServiceWatch_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_Voice] (
        [VoiceId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [NextCheckDate] datetime2 NOT NULL,
        [RateServiceRecieving] int NOT NULL,
        [RateStaffAttending] int NOT NULL,
        [OfficeStaffSupport] int NOT NULL,
        [AreasOfImprovements] nvarchar(max) NOT NULL,
        [SomethingSpecial] nvarchar(max) NOT NULL,
        [InterestedInPrograms] int NOT NULL,
        [HealthGoalShortTerm] nvarchar(max) NOT NULL,
        [HealthGoalLongTerm] nvarchar(max) NOT NULL,
        [ActionRequired] nvarchar(max) NOT NULL,
        [ActionsTakenByMPCC] nvarchar(max) NOT NULL,
        [EvidenceOfActionTaken] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [RotCause] nvarchar(max) NOT NULL,
        [LessonLearntAndShared] nvarchar(max) NOT NULL,
        [URL] nvarchar(max) NOT NULL,
        [Attachment] nvarchar(max) NULL,
        [StaffPersonalInfoId] int NULL,
        CONSTRAINT [PK_tbl_Client_Voice] PRIMARY KEY ([VoiceId]),
        CONSTRAINT [FK_tbl_Client_Voice_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Client_Voice_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_WoundCare] (
        [WoundCareId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NULL,
        [ClientId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] datetime2 NOT NULL,
        [Goal] int NOT NULL,
        [Type] int NOT NULL,
        [TypeAttach] nvarchar(max) NULL,
        [UlcerStage] int NOT NULL,
        [UlcerStageAttach] nvarchar(max) NULL,
        [Measurment] int NOT NULL,
        [MeasurementAttach] nvarchar(max) NULL,
        [PainLvl] int NOT NULL,
        [Location] int NOT NULL,
        [LocationAttach] nvarchar(max) NULL,
        [WoundCause] int NOT NULL,
        [Comment] nvarchar(max) NOT NULL,
        [StatusImage] int NOT NULL,
        [StatusAttach] nvarchar(max) NULL,
        [PhysicianResponse] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_WoundCare] PRIMARY KEY ([WoundCareId]),
        CONSTRAINT [FK_tbl_Client_WoundCare_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Enotice_] (
        [EnoticeId] int NOT NULL IDENTITY,
        [Date] datetime2 NOT NULL,
        [PublishTo] int NOT NULL,
        [Heading] nvarchar(max) NOT NULL,
        [Note] nvarchar(max) NOT NULL,
        [PublishBy] nvarchar(max) NOT NULL,
        [Image] nvarchar(max) NOT NULL,
        [Video] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_tbl_Enotice_] PRIMARY KEY ([EnoticeId]),
        CONSTRAINT [FK_tbl_Enotice__tbl_Client_PublishTo] FOREIGN KEY ([PublishTo]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Incoming_Meds] (
        [IncomingMedsId] int NOT NULL IDENTITY,
        [Date] datetime2 NOT NULL,
        [UserName] int NOT NULL,
        [StaffName] nvarchar(max) NOT NULL,
        [StartDate] nvarchar(max) NOT NULL,
        [ChartImage] nvarchar(max) NOT NULL,
        [MedsImage] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Incoming_Meds] PRIMARY KEY ([IncomingMedsId]),
        CONSTRAINT [FK_tbl_Incoming_Meds_tbl_Client_UserName] FOREIGN KEY ([UserName]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Resources_] (
        [ResourcesId] int NOT NULL IDENTITY,
        [Date] datetime2 NOT NULL,
        [PublishTo] int NOT NULL,
        [Heading] nvarchar(max) NOT NULL,
        [Note] nvarchar(max) NOT NULL,
        [PublishBy] nvarchar(max) NOT NULL,
        [Image] nvarchar(max) NOT NULL,
        [Video] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_tbl_Resources_] PRIMARY KEY ([ResourcesId]),
        CONSTRAINT [FK_tbl_Resources__tbl_Client_PublishTo] FOREIGN KEY ([PublishTo]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Staff_AdlObs] (
        [ObservationID] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [StaffId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [NextCheckDate] datetime2 NOT NULL,
        [Details] nvarchar(255) NOT NULL,
        [ClientId] int NOT NULL,
        [UnderstandingofEquipment] int NOT NULL,
        [UnderstandingofService] int NOT NULL,
        [UnderstandingofControl] int NOT NULL,
        [FivePrinciples] int NOT NULL,
        [Comments] nvarchar(255) NOT NULL,
        [ActionRequired] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [URL] nvarchar(max) NOT NULL,
        [Attachment] nvarchar(max) NOT NULL,
        [StaffPersonalInfoId] int NULL,
        CONSTRAINT [PK_tbl_Staff_AdlObs] PRIMARY KEY ([ObservationID]),
        CONSTRAINT [FK_tbl_Staff_AdlObs_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Staff_AdlObs_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Staff_KeyWorkerVoice] (
        [KeyWorkerId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [StaffId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [NextCheckDate] datetime2 NOT NULL,
        [Details] nvarchar(max) NOT NULL,
        [NotComfortableServices] int NOT NULL,
        [ServicesRequiresTime] int NOT NULL,
        [ServicesRequiresServices] int NOT NULL,
        [WellSupportedServices] int NOT NULL,
        [ChangesWeNeed] nvarchar(max) NOT NULL,
        [NutritionalChanges] nvarchar(max) NOT NULL,
        [HealthAndWellNessChanges] nvarchar(max) NOT NULL,
        [MedicationChanges] nvarchar(max) NOT NULL,
        [MovingAndHandling] nvarchar(max) NOT NULL,
        [RiskAssessment] nvarchar(max) NOT NULL,
        [ActionRequired] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [URL] nvarchar(max) NOT NULL,
        [Attachment] nvarchar(max) NOT NULL,
        [StaffPersonalInfoId] int NULL,
        CONSTRAINT [PK_tbl_Staff_KeyWorkerVoice] PRIMARY KEY ([KeyWorkerId]),
        CONSTRAINT [FK_tbl_Staff_KeyWorkerVoice_tbl_Client_ServicesRequiresServices] FOREIGN KEY ([ServicesRequiresServices]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Staff_KeyWorkerVoice_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Staff_MedCompObs] (
        [MedCompId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [StaffId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [NextCheckDate] datetime2 NOT NULL,
        [Details] nvarchar(max) NOT NULL,
        [ClientId] int NOT NULL,
        [UnderstandingofMedication] int NOT NULL,
        [UnderstandingofRights] int NOT NULL,
        [ReadingMedicalPrescriptions] int NOT NULL,
        [CarePlan] int NOT NULL,
        [RateStaff] int NOT NULL,
        [ActionRequired] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [URL] nvarchar(max) NOT NULL,
        [Attachment] nvarchar(max) NOT NULL,
        [StaffPersonalInfoId] int NULL,
        CONSTRAINT [PK_tbl_Staff_MedCompObs] PRIMARY KEY ([MedCompId]),
        CONSTRAINT [FK_tbl_Staff_MedCompObs_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Staff_MedCompObs_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Staff_OneToOne] (
        [OneToOneId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [StaffId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [NextCheckDate] datetime2 NOT NULL,
        [Purpose] nvarchar(max) NOT NULL,
        [PreviousSupervision] int NOT NULL,
        [StaffImprovedInAreas] nvarchar(max) NOT NULL,
        [CurrentEventArea] nvarchar(max) NOT NULL,
        [StaffConclusion] nvarchar(max) NOT NULL,
        [DecisionsReached] nvarchar(max) NOT NULL,
        [ImprovementRecorded] nvarchar(max) NOT NULL,
        [ActionRequired] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [URL] nvarchar(max) NOT NULL,
        [Attachment] nvarchar(max) NOT NULL,
        [StaffPersonalInfoId] int NULL,
        CONSTRAINT [PK_tbl_Staff_OneToOne] PRIMARY KEY ([OneToOneId]),
        CONSTRAINT [FK_tbl_Staff_OneToOne_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Staff_Reference] (
        [StaffReferenceId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [StaffId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [ApplicantRole] int NOT NULL,
        [DateofEmployement] int NOT NULL,
        [DateofExit] nvarchar(max) NOT NULL,
        [RehireStaff] nvarchar(max) NOT NULL,
        [Relationship] nvarchar(max) NOT NULL,
        [TeamWork] int NOT NULL,
        [Integrity] int NOT NULL,
        [Knowledgeable] int NOT NULL,
        [WorkUnderPressure] int NOT NULL,
        [Caring] int NOT NULL,
        [PreviousExperience] int NOT NULL,
        [RefreeName] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Contact] nvarchar(max) NOT NULL,
        [Attachment] nvarchar(max) NOT NULL,
        [ConfirmedBy] int NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Staff_Reference] PRIMARY KEY ([StaffReferenceId]),
        CONSTRAINT [FK_tbl_Staff_Reference_tbl_Client_ApplicantRole] FOREIGN KEY ([ApplicantRole]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Staff_Reference_tbl_StaffPersonalInfo_ConfirmedBy] FOREIGN KEY ([ConfirmedBy]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Staff_SpotCheck] (
        [SpotCheckId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [StaffId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [NextCheckDate] datetime2 NOT NULL,
        [Details] nvarchar(max) NOT NULL,
        [ClientId] int NOT NULL,
        [StaffArriveOnTime] int NOT NULL,
        [StaffDressCode] int NOT NULL,
        [AreaComments] nvarchar(max) NOT NULL,
        [ActionRequired] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [URL] nvarchar(max) NOT NULL,
        [Attachment] nvarchar(max) NOT NULL,
        [StaffPersonalInfoId] int NULL,
        CONSTRAINT [PK_tbl_Staff_SpotCheck] PRIMARY KEY ([SpotCheckId]),
        CONSTRAINT [FK_tbl_Staff_SpotCheck_tbl_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Staff_SpotCheck_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Staff_SupervisionAppraisal] (
        [StaffSupervisionAppraisalId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [StaffId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [NextCheckDate] datetime2 NOT NULL,
        [Details] nvarchar(max) NOT NULL,
        [StaffRating] int NOT NULL,
        [ProfessionalDevelopment] int NOT NULL,
        [StaffComplaints] int NOT NULL,
        [FiveStarRating] nvarchar(max) NOT NULL,
        [StaffDevelopment] nvarchar(max) NOT NULL,
        [StaffAbility] nvarchar(max) NOT NULL,
        [NoAbilityToSupport] nvarchar(max) NOT NULL,
        [CondourAndWhistleBlowing] nvarchar(max) NOT NULL,
        [NoCondourAndWhistleBlowing] nvarchar(max) NOT NULL,
        [StaffSupportAreas] int NOT NULL,
        [ActionRequired] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [URL] nvarchar(max) NOT NULL,
        [Attachment] nvarchar(max) NOT NULL,
        [StaffPersonalInfoId] int NULL,
        CONSTRAINT [PK_tbl_Staff_SupervisionAppraisal] PRIMARY KEY ([StaffSupervisionAppraisalId]),
        CONSTRAINT [FK_tbl_Staff_SupervisionAppraisal_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Staff_Survey] (
        [StaffSurveyId] int NOT NULL IDENTITY,
        [Reference] nvarchar(max) NOT NULL,
        [StaffId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [NextCheckDate] datetime2 NOT NULL,
        [Details] nvarchar(max) NOT NULL,
        [AdequateTrainingReceived] int NOT NULL,
        [HealthCareServicesSatisfaction] int NOT NULL,
        [SupportFromCompany] int NOT NULL,
        [CompanyManagement] int NOT NULL,
        [AccessToPolicies] int NOT NULL,
        [WorkEnvironmentSuggestions] nvarchar(max) NOT NULL,
        [AreaRequiringImprovements] nvarchar(max) NOT NULL,
        [ActionRequired] nvarchar(max) NOT NULL,
        [Deadline] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [URL] nvarchar(max) NOT NULL,
        [Attachment] nvarchar(max) NOT NULL,
        [StaffPersonalInfoId] int NULL,
        CONSTRAINT [PK_tbl_Staff_Survey] PRIMARY KEY ([StaffSurveyId]),
        CONSTRAINT [FK_tbl_Staff_Survey_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Whisttle_Blower] (
        [WhisttleBlowerId] int NOT NULL IDENTITY,
        [Date] datetime2 NOT NULL,
        [UserName] int NOT NULL,
        [StaffName] nvarchar(max) NOT NULL,
        [IncidentDate] nvarchar(max) NOT NULL,
        [Happening] nvarchar(max) NOT NULL,
        [Evidence] nvarchar(max) NOT NULL,
        [Witness] int NOT NULL,
        [LikeCalling] int NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_tbl_Whisttle_Blower] PRIMARY KEY ([WhisttleBlowerId]),
        CONSTRAINT [FK_tbl_Whisttle_Blower_tbl_Client_UserName] FOREIGN KEY ([UserName]) REFERENCES [tbl_Client] ([ClientId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BloodCoag_OfficerToAct] (
        [BloodCoagOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BloodRecordId] int NOT NULL,
        CONSTRAINT [PK_tbl_BloodCoag_OfficerToAct] PRIMARY KEY ([BloodCoagOfficerToActId]),
        CONSTRAINT [FK_tbl_BloodCoag_OfficerToAct_tbl_Client_BloodCoagulationRecord_BloodRecordId] FOREIGN KEY ([BloodRecordId]) REFERENCES [tbl_Client_BloodCoagulationRecord] ([BloodRecordId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BloodCoag_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BloodCoag_Physician] (
        [BloodCoagPhysicianId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BloodRecordId] int NOT NULL,
        CONSTRAINT [PK_tbl_BloodCoag_Physician] PRIMARY KEY ([BloodCoagPhysicianId]),
        CONSTRAINT [FK_tbl_BloodCoag_Physician_tbl_Client_BloodCoagulationRecord_BloodRecordId] FOREIGN KEY ([BloodRecordId]) REFERENCES [tbl_Client_BloodCoagulationRecord] ([BloodRecordId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BloodCoag_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BloodCoag_StaffName] (
        [BloodCoagStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BloodRecordId] int NOT NULL,
        CONSTRAINT [PK_tbl_BloodCoag_StaffName] PRIMARY KEY ([BloodCoagStaffNameId]),
        CONSTRAINT [FK_tbl_BloodCoag_StaffName_tbl_Client_BloodCoagulationRecord_BloodRecordId] FOREIGN KEY ([BloodRecordId]) REFERENCES [tbl_Client_BloodCoagulationRecord] ([BloodRecordId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BloodCoag_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BloodPressure_OfficerToAct] (
        [BloodPressureOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BloodPressureId] int NOT NULL,
        CONSTRAINT [PK_tbl_BloodPressure_OfficerToAct] PRIMARY KEY ([BloodPressureOfficerToActId]),
        CONSTRAINT [FK_tbl_BloodPressure_OfficerToAct_tbl_Client_BloodPressure_BloodPressureId] FOREIGN KEY ([BloodPressureId]) REFERENCES [tbl_Client_BloodPressure] ([BloodPressureId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BloodPressure_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BloodPressure_Physician] (
        [BloodPressurePhysicianId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BloodPressureId] int NOT NULL,
        CONSTRAINT [PK_tbl_BloodPressure_Physician] PRIMARY KEY ([BloodPressurePhysicianId]),
        CONSTRAINT [FK_tbl_BloodPressure_Physician_tbl_Client_BloodPressure_BloodPressureId] FOREIGN KEY ([BloodPressureId]) REFERENCES [tbl_Client_BloodPressure] ([BloodPressureId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BloodPressure_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BloodPressure_StaffName] (
        [BloodPressureStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BloodPressureId] int NOT NULL,
        CONSTRAINT [PK_tbl_BloodPressure_StaffName] PRIMARY KEY ([BloodPressureStaffNameId]),
        CONSTRAINT [FK_tbl_BloodPressure_StaffName_tbl_Client_BloodPressure_BloodPressureId] FOREIGN KEY ([BloodPressureId]) REFERENCES [tbl_Client_BloodPressure] ([BloodPressureId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BloodPressure_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BMIChart_OfficerToAct] (
        [BMIChartOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BMIChartId] int NOT NULL,
        CONSTRAINT [PK_tbl_BMIChart_OfficerToAct] PRIMARY KEY ([BMIChartOfficerToActId]),
        CONSTRAINT [FK_tbl_BMIChart_OfficerToAct_tbl_Client_BMIChart_BMIChartId] FOREIGN KEY ([BMIChartId]) REFERENCES [tbl_Client_BMIChart] ([BMIChartId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BMIChart_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BMIChart_Physician] (
        [BMIChartPhysicianId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BMIChartId] int NOT NULL,
        CONSTRAINT [PK_tbl_BMIChart_Physician] PRIMARY KEY ([BMIChartPhysicianId]),
        CONSTRAINT [FK_tbl_BMIChart_Physician_tbl_Client_BMIChart_BMIChartId] FOREIGN KEY ([BMIChartId]) REFERENCES [tbl_Client_BMIChart] ([BMIChartId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BMIChart_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BMIChart_StaffName] (
        [BMIChartStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BMIChartId] int NOT NULL,
        CONSTRAINT [PK_tbl_BMIChart_StaffName] PRIMARY KEY ([BMIChartStaffNameId]),
        CONSTRAINT [FK_tbl_BMIChart_StaffName_tbl_Client_BMIChart_BMIChartId] FOREIGN KEY ([BMIChartId]) REFERENCES [tbl_Client_BMIChart] ([BMIChartId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BMIChart_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BodyTemp_OfficerToAct] (
        [BodyTempOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BodyTempId] int NOT NULL,
        CONSTRAINT [PK_tbl_BodyTemp_OfficerToAct] PRIMARY KEY ([BodyTempOfficerToActId]),
        CONSTRAINT [FK_tbl_BodyTemp_OfficerToAct_tbl_Client_BodyTemp_BodyTempId] FOREIGN KEY ([BodyTempId]) REFERENCES [tbl_Client_BodyTemp] ([BodyTempId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BodyTemp_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BodyTemp_Physician] (
        [BodyTempPhysicianId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BodyTempId] int NOT NULL,
        CONSTRAINT [PK_tbl_BodyTemp_Physician] PRIMARY KEY ([BodyTempPhysicianId]),
        CONSTRAINT [FK_tbl_BodyTemp_Physician_tbl_Client_BodyTemp_BodyTempId] FOREIGN KEY ([BodyTempId]) REFERENCES [tbl_Client_BodyTemp] ([BodyTempId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BodyTemp_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BodyTemp_StaffName] (
        [BodyTempStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BodyTempId] int NOT NULL,
        CONSTRAINT [PK_tbl_BodyTemp_StaffName] PRIMARY KEY ([BodyTempStaffNameId]),
        CONSTRAINT [FK_tbl_BodyTemp_StaffName_tbl_Client_BodyTemp_BodyTempId] FOREIGN KEY ([BodyTempId]) REFERENCES [tbl_Client_BodyTemp] ([BodyTempId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BodyTemp_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BowelMovement_OfficerToAct] (
        [BowelMovementOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BowelMovementId] int NOT NULL,
        CONSTRAINT [PK_tbl_BowelMovement_OfficerToAct] PRIMARY KEY ([BowelMovementOfficerToActId]),
        CONSTRAINT [FK_tbl_BowelMovement_OfficerToAct_tbl_Client_BowelMovement_BowelMovementId] FOREIGN KEY ([BowelMovementId]) REFERENCES [tbl_Client_BowelMovement] ([BowelMovementId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BowelMovement_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BowelMovement_Physician] (
        [BowelMovementPhysicianId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BowelMovementId] int NOT NULL,
        CONSTRAINT [PK_tbl_BowelMovement_Physician] PRIMARY KEY ([BowelMovementPhysicianId]),
        CONSTRAINT [FK_tbl_BowelMovement_Physician_tbl_Client_BowelMovement_BowelMovementId] FOREIGN KEY ([BowelMovementId]) REFERENCES [tbl_Client_BowelMovement] ([BowelMovementId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BowelMovement_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_BowelMovement_StaffName] (
        [BowelMovementStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [BowelMovementId] int NOT NULL,
        CONSTRAINT [PK_tbl_BowelMovement_StaffName] PRIMARY KEY ([BowelMovementStaffNameId]),
        CONSTRAINT [FK_tbl_BowelMovement_StaffName_tbl_Client_BowelMovement_BowelMovementId] FOREIGN KEY ([BowelMovementId]) REFERENCES [tbl_Client_BowelMovement] ([BowelMovementId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_BowelMovement_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Complain_OfficerToAct] (
        [ComplainOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [ComplainId] int NOT NULL,
        CONSTRAINT [PK_tbl_Complain_OfficerToAct] PRIMARY KEY ([ComplainOfficerToActId]),
        CONSTRAINT [FK_tbl_Complain_OfficerToAct_tbl_Client_ComplainRegister_ComplainId] FOREIGN KEY ([ComplainId]) REFERENCES [tbl_Client_ComplainRegister] ([ComplainId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Complain_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Complain_StaffName] (
        [ComplainStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [ComplainId] int NOT NULL,
        CONSTRAINT [PK_tbl_Complain_StaffName] PRIMARY KEY ([ComplainStaffNameId]),
        CONSTRAINT [FK_tbl_Complain_StaffName_tbl_Client_ComplainRegister_ComplainId] FOREIGN KEY ([ComplainId]) REFERENCES [tbl_Client_ComplainRegister] ([ComplainId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Complain_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_EyeHealth_OfficerToAct] (
        [EyeHealthOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [EyeHealthId] int NOT NULL,
        CONSTRAINT [PK_tbl_EyeHealth_OfficerToAct] PRIMARY KEY ([EyeHealthOfficerToActId]),
        CONSTRAINT [FK_tbl_EyeHealth_OfficerToAct_tbl_Client_EyeHealthMonitoring_EyeHealthId] FOREIGN KEY ([EyeHealthId]) REFERENCES [tbl_Client_EyeHealthMonitoring] ([EyeHealthId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_EyeHealth_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_EyeHealth_Physician] (
        [EyeHealthPhysicianId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [EyeHealthId] int NOT NULL,
        CONSTRAINT [PK_tbl_EyeHealth_Physician] PRIMARY KEY ([EyeHealthPhysicianId]),
        CONSTRAINT [FK_tbl_EyeHealth_Physician_tbl_Client_EyeHealthMonitoring_EyeHealthId] FOREIGN KEY ([EyeHealthId]) REFERENCES [tbl_Client_EyeHealthMonitoring] ([EyeHealthId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_EyeHealth_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_EyeHealth_StaffName] (
        [EyeHealthStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [EyeHealthId] int NOT NULL,
        CONSTRAINT [PK_tbl_EyeHealth_StaffName] PRIMARY KEY ([EyeHealthStaffNameId]),
        CONSTRAINT [FK_tbl_EyeHealth_StaffName_tbl_Client_EyeHealthMonitoring_EyeHealthId] FOREIGN KEY ([EyeHealthId]) REFERENCES [tbl_Client_EyeHealthMonitoring] ([EyeHealthId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_EyeHealth_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_FoodIntake_OfficerToAct] (
        [FoodIntakeOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [FoodIntakeId] int NOT NULL,
        CONSTRAINT [PK_tbl_FoodIntake_OfficerToAct] PRIMARY KEY ([FoodIntakeOfficerToActId]),
        CONSTRAINT [FK_tbl_FoodIntake_OfficerToAct_tbl_Client_FoodIntake_FoodIntakeId] FOREIGN KEY ([FoodIntakeId]) REFERENCES [tbl_Client_FoodIntake] ([FoodIntakeId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_FoodIntake_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_FoodIntake_Physician] (
        [FoodIntakePhysicianId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [FoodIntakeId] int NOT NULL,
        CONSTRAINT [PK_tbl_FoodIntake_Physician] PRIMARY KEY ([FoodIntakePhysicianId]),
        CONSTRAINT [FK_tbl_FoodIntake_Physician_tbl_Client_FoodIntake_FoodIntakeId] FOREIGN KEY ([FoodIntakeId]) REFERENCES [tbl_Client_FoodIntake] ([FoodIntakeId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_FoodIntake_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_FoodIntake_StaffName] (
        [FoodIntakeStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [FoodIntakeId] int NOT NULL,
        CONSTRAINT [PK_tbl_FoodIntake_StaffName] PRIMARY KEY ([FoodIntakeStaffNameId]),
        CONSTRAINT [FK_tbl_FoodIntake_StaffName_tbl_Client_FoodIntake_FoodIntakeId] FOREIGN KEY ([FoodIntakeId]) REFERENCES [tbl_Client_FoodIntake] ([FoodIntakeId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_FoodIntake_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_HeartRate_OfficerToAct] (
        [HeartRateOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [HeartRateId] int NOT NULL,
        CONSTRAINT [PK_tbl_HeartRate_OfficerToAct] PRIMARY KEY ([HeartRateOfficerToActId]),
        CONSTRAINT [FK_tbl_HeartRate_OfficerToAct_tbl_Client_HeartRate_HeartRateId] FOREIGN KEY ([HeartRateId]) REFERENCES [tbl_Client_HeartRate] ([HeartRateId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_HeartRate_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_HeartRate_Physician] (
        [HeartRatePhysicianId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [HeartRateId] int NOT NULL,
        CONSTRAINT [PK_tbl_HeartRate_Physician] PRIMARY KEY ([HeartRatePhysicianId]),
        CONSTRAINT [FK_tbl_HeartRate_Physician_tbl_Client_HeartRate_HeartRateId] FOREIGN KEY ([HeartRateId]) REFERENCES [tbl_Client_HeartRate] ([HeartRateId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_HeartRate_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_HeartRate_StaffName] (
        [HeartRateStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [HeartRateId] int NOT NULL,
        CONSTRAINT [PK_tbl_HeartRate_StaffName] PRIMARY KEY ([HeartRateStaffNameId]),
        CONSTRAINT [FK_tbl_HeartRate_StaffName_tbl_Client_HeartRate_HeartRateId] FOREIGN KEY ([HeartRateId]) REFERENCES [tbl_Client_HeartRate] ([HeartRateId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_HeartRate_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_LogAudit_OfficerToAct] (
        [LogAuditOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [LogAuditId] int NOT NULL,
        CONSTRAINT [PK_tbl_LogAudit_OfficerToAct] PRIMARY KEY ([LogAuditOfficerToActId]),
        CONSTRAINT [FK_tbl_LogAudit_OfficerToAct_tbl_Client_LogAudit_LogAuditId] FOREIGN KEY ([LogAuditId]) REFERENCES [tbl_Client_LogAudit] ([LogAuditId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_LogAudit_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_MedAudit_AuditorName] (
        [MedAuditStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [MedAuditId] int NOT NULL,
        CONSTRAINT [PK_tbl_MedAudit_AuditorName] PRIMARY KEY ([MedAuditStaffNameId]),
        CONSTRAINT [FK_tbl_MedAudit_AuditorName_tbl_Client_MedAudit_MedAuditId] FOREIGN KEY ([MedAuditId]) REFERENCES [tbl_Client_MedAudit] ([MedAuditId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_MedAudit_AuditorName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_MedAudit_OfficerToAct] (
        [MedAuditOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [MedAuditId] int NOT NULL,
        CONSTRAINT [PK_tbl_MedAudit_OfficerToAct] PRIMARY KEY ([MedAuditOfficerToActId]),
        CONSTRAINT [FK_tbl_MedAudit_OfficerToAct_tbl_Client_MedAudit_MedAuditId] FOREIGN KEY ([MedAuditId]) REFERENCES [tbl_Client_MedAudit] ([MedAuditId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_MedAudit_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Visit_OfficerToAct] (
        [VisitOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [VisitId] int NOT NULL,
        CONSTRAINT [PK_tbl_Visit_OfficerToAct] PRIMARY KEY ([VisitOfficerToActId]),
        CONSTRAINT [FK_tbl_Visit_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Visit_OfficerToAct_tbl_Client_MgtVisit_VisitId] FOREIGN KEY ([VisitId]) REFERENCES [tbl_Client_MgtVisit] ([VisitId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Visit_StaffName] (
        [VisitStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [VisitId] int NOT NULL,
        CONSTRAINT [PK_tbl_Visit_StaffName] PRIMARY KEY ([VisitStaffNameId]),
        CONSTRAINT [FK_tbl_Visit_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Visit_StaffName_tbl_Client_MgtVisit_VisitId] FOREIGN KEY ([VisitId]) REFERENCES [tbl_Client_MgtVisit] ([VisitId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_Cleaning] (
        [CleaningId] int NOT NULL IDENTITY,
        [NutritionId] int NOT NULL,
        [AreasAndItems] int NOT NULL,
        [Details] nvarchar(255) NOT NULL,
        [SafetyHazard] nvarchar(50) NOT NULL,
        [LocationOfItem] nvarchar(50) NOT NULL,
        [DescOfItem] nvarchar(50) NOT NULL,
        [MinuteAlloted] datetime2 NOT NULL,
        [Disposal] nvarchar(50) NOT NULL,
        [WhereToGet] int NOT NULL,
        [WhereToKeep] nvarchar(max) NOT NULL,
        [SEEVIDEO] nvarchar(255) NOT NULL,
        [Image] nvarchar(max) NOT NULL,
        [DAYOFCLEANING] nvarchar(50) NOT NULL,
        [DATEFROM] datetime2 NOT NULL,
        [DATETO] datetime2 NOT NULL,
        [STAFFId] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_Cleaning] PRIMARY KEY ([CleaningId]),
        CONSTRAINT [FK_tbl_Client_Cleaning_tbl_Client_Nutrition_NutritionId] FOREIGN KEY ([NutritionId]) REFERENCES [tbl_Client_Nutrition] ([NutritionId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Client_Cleaning_tbl_StaffPersonalInfo_STAFFId] FOREIGN KEY ([STAFFId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_MealDay] (
        [ClientMealId] int NOT NULL IDENTITY,
        [NutritionId] int NOT NULL,
        [MealDayofWeekId] int NOT NULL,
        [ClientMealTypeId] int NOT NULL,
        [TypeId] int NOT NULL,
        [MEALDETAILS] nvarchar(255) NOT NULL,
        [HOWTOPREPARE] nvarchar(255) NOT NULL,
        [SEEVIDEO] nvarchar(255) NOT NULL,
        [PICTURE] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_tbl_Client_MealDay] PRIMARY KEY ([ClientMealId]),
        CONSTRAINT [FK_tbl_Client_MealDay_tbl_Client_MealType_ClientMealTypeId] FOREIGN KEY ([ClientMealTypeId]) REFERENCES [tbl_Client_MealType] ([ClientMealTypeId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Client_MealDay_tbl_RotaDayofWeek_MealDayofWeekId] FOREIGN KEY ([MealDayofWeekId]) REFERENCES [tbl_RotaDayofWeek] ([RotaDayofWeekId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Client_MealDay_tbl_Client_Nutrition_NutritionId] FOREIGN KEY ([NutritionId]) REFERENCES [tbl_Client_Nutrition] ([NutritionId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Client_Shopping] (
        [ShoppingId] int NOT NULL IDENTITY,
        [NutritionId] int NOT NULL,
        [MeansOfPurchase] nvarchar(100) NOT NULL,
        [LocationOfPurchase] nvarchar(255) NOT NULL,
        [Item] nvarchar(100) NOT NULL,
        [Description] nvarchar(255) NOT NULL,
        [Quantity] int NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [Image] nvarchar(max) NOT NULL,
        [DAYOFSHOPPING] nvarchar(50) NOT NULL,
        [DATEFROM] datetime2 NOT NULL,
        [DATETO] datetime2 NOT NULL,
        [STAFFId] int NOT NULL,
        CONSTRAINT [PK_tbl_Client_Shopping] PRIMARY KEY ([ShoppingId]),
        CONSTRAINT [FK_tbl_Client_Shopping_tbl_Client_Nutrition_NutritionId] FOREIGN KEY ([NutritionId]) REFERENCES [tbl_Client_Nutrition] ([NutritionId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Client_Shopping_tbl_StaffPersonalInfo_STAFFId] FOREIGN KEY ([STAFFId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_OxygenLvl_OfficerToAct] (
        [OxygenLvlOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [OxygenLvlId] int NOT NULL,
        CONSTRAINT [PK_tbl_OxygenLvl_OfficerToAct] PRIMARY KEY ([OxygenLvlOfficerToActId]),
        CONSTRAINT [FK_tbl_OxygenLvl_OfficerToAct_tbl_Client_Oxygenlvl_OxygenLvlId] FOREIGN KEY ([OxygenLvlId]) REFERENCES [tbl_Client_Oxygenlvl] ([OxygenLvlId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_OxygenLvl_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_OxygenLvl_Physician] (
        [OxygenLvlPhysicianId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [OxygenLvlId] int NOT NULL,
        CONSTRAINT [PK_tbl_OxygenLvl_Physician] PRIMARY KEY ([OxygenLvlPhysicianId]),
        CONSTRAINT [FK_tbl_OxygenLvl_Physician_tbl_Client_Oxygenlvl_OxygenLvlId] FOREIGN KEY ([OxygenLvlId]) REFERENCES [tbl_Client_Oxygenlvl] ([OxygenLvlId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_OxygenLvl_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_OxygenLvl_StaffName] (
        [OxygenLvlStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [OxygenLvlId] int NOT NULL,
        CONSTRAINT [PK_tbl_OxygenLvl_StaffName] PRIMARY KEY ([OxygenLvlStaffNameId]),
        CONSTRAINT [FK_tbl_OxygenLvl_StaffName_tbl_Client_Oxygenlvl_OxygenLvlId] FOREIGN KEY ([OxygenLvlId]) REFERENCES [tbl_Client_Oxygenlvl] ([OxygenLvlId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_OxygenLvl_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_PainChart_OfficerToAct] (
        [PainChartOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [PainChartId] int NOT NULL,
        CONSTRAINT [PK_tbl_PainChart_OfficerToAct] PRIMARY KEY ([PainChartOfficerToActId]),
        CONSTRAINT [FK_tbl_PainChart_OfficerToAct_tbl_Client_PainChart_PainChartId] FOREIGN KEY ([PainChartId]) REFERENCES [tbl_Client_PainChart] ([PainChartId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_PainChart_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_PainChart_Physician] (
        [PainChartPhysicianId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [PainChartId] int NOT NULL,
        CONSTRAINT [PK_tbl_PainChart_Physician] PRIMARY KEY ([PainChartPhysicianId]),
        CONSTRAINT [FK_tbl_PainChart_Physician_tbl_Client_PainChart_PainChartId] FOREIGN KEY ([PainChartId]) REFERENCES [tbl_Client_PainChart] ([PainChartId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_PainChart_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_PainChart_StaffName] (
        [PainChartStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [PainChartId] int NOT NULL,
        CONSTRAINT [PK_tbl_PainChart_StaffName] PRIMARY KEY ([PainChartStaffNameId]),
        CONSTRAINT [FK_tbl_PainChart_StaffName_tbl_Client_PainChart_PainChartId] FOREIGN KEY ([PainChartId]) REFERENCES [tbl_Client_PainChart] ([PainChartId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_PainChart_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Program_OfficerToAct] (
        [ProgramOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [ProgramId] int NOT NULL,
        CONSTRAINT [PK_tbl_Program_OfficerToAct] PRIMARY KEY ([ProgramOfficerToActId]),
        CONSTRAINT [FK_tbl_Program_OfficerToAct_tbl_Client_Program_ProgramId] FOREIGN KEY ([ProgramId]) REFERENCES [tbl_Client_Program] ([ProgramId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Program_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_PulseRate_OfficerToAct] (
        [PulseRateOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [PulseRateId] int NOT NULL,
        CONSTRAINT [PK_tbl_PulseRate_OfficerToAct] PRIMARY KEY ([PulseRateOfficerToActId]),
        CONSTRAINT [FK_tbl_PulseRate_OfficerToAct_tbl_Client_PulseRate_PulseRateId] FOREIGN KEY ([PulseRateId]) REFERENCES [tbl_Client_PulseRate] ([PulseRateId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_PulseRate_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_PulseRate_Physician] (
        [PulseRatePhysicianId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [PulseRateId] int NOT NULL,
        CONSTRAINT [PK_tbl_PulseRate_Physician] PRIMARY KEY ([PulseRatePhysicianId]),
        CONSTRAINT [FK_tbl_PulseRate_Physician_tbl_Client_PulseRate_PulseRateId] FOREIGN KEY ([PulseRateId]) REFERENCES [tbl_Client_PulseRate] ([PulseRateId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_PulseRate_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_PulseRate_StaffName] (
        [PulseRateStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [PulseRateId] int NOT NULL,
        CONSTRAINT [PK_tbl_PulseRate_StaffName] PRIMARY KEY ([PulseRateStaffNameId]),
        CONSTRAINT [FK_tbl_PulseRate_StaffName_tbl_Client_PulseRate_PulseRateId] FOREIGN KEY ([PulseRateId]) REFERENCES [tbl_Client_PulseRate] ([PulseRateId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_PulseRate_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Seizure_OfficerToAct] (
        [SeizureOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [SeizureId] int NOT NULL,
        CONSTRAINT [PK_tbl_Seizure_OfficerToAct] PRIMARY KEY ([SeizureOfficerToActId]),
        CONSTRAINT [FK_tbl_Seizure_OfficerToAct_tbl_Client_Seizure_SeizureId] FOREIGN KEY ([SeizureId]) REFERENCES [tbl_Client_Seizure] ([SeizureId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Seizure_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Seizure_Physician] (
        [SeizurePhysicianId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [SeizureId] int NOT NULL,
        CONSTRAINT [PK_tbl_Seizure_Physician] PRIMARY KEY ([SeizurePhysicianId]),
        CONSTRAINT [FK_tbl_Seizure_Physician_tbl_Client_Seizure_SeizureId] FOREIGN KEY ([SeizureId]) REFERENCES [tbl_Client_Seizure] ([SeizureId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Seizure_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Seizure_StaffName] (
        [SeizureStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [SeizureId] int NOT NULL,
        CONSTRAINT [PK_tbl_Seizure_StaffName] PRIMARY KEY ([SeizureStaffNameId]),
        CONSTRAINT [FK_tbl_Seizure_StaffName_tbl_Client_Seizure_SeizureId] FOREIGN KEY ([SeizureId]) REFERENCES [tbl_Client_Seizure] ([SeizureId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Seizure_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Service_OfficerToAct] (
        [ServiceOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [ServiceId] int NOT NULL,
        CONSTRAINT [PK_tbl_Service_OfficerToAct] PRIMARY KEY ([ServiceOfficerToActId]),
        CONSTRAINT [FK_tbl_Service_OfficerToAct_tbl_Client_ServiceWatch_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [tbl_Client_ServiceWatch] ([WatchId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Service_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Service_StaffName] (
        [ServiceStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [ServiceId] int NOT NULL,
        CONSTRAINT [PK_tbl_Service_StaffName] PRIMARY KEY ([ServiceStaffNameId]),
        CONSTRAINT [FK_tbl_Service_StaffName_tbl_Client_ServiceWatch_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [tbl_Client_ServiceWatch] ([WatchId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Service_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Voice_CallerName] (
        [VoiceCallerNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [VoiceId] int NOT NULL,
        CONSTRAINT [PK_tbl_Voice_CallerName] PRIMARY KEY ([VoiceCallerNameId]),
        CONSTRAINT [FK_tbl_Voice_CallerName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Voice_CallerName_tbl_Client_Voice_VoiceId] FOREIGN KEY ([VoiceId]) REFERENCES [tbl_Client_Voice] ([VoiceId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Voice_GoodStaff] (
        [VoiceGoodStaffId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [VoiceId] int NOT NULL,
        CONSTRAINT [PK_tbl_Voice_GoodStaff] PRIMARY KEY ([VoiceGoodStaffId]),
        CONSTRAINT [FK_tbl_Voice_GoodStaff_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Voice_GoodStaff_tbl_Client_Voice_VoiceId] FOREIGN KEY ([VoiceId]) REFERENCES [tbl_Client_Voice] ([VoiceId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Voice_OfficerToAct] (
        [VoiceOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [VoiceId] int NOT NULL,
        CONSTRAINT [PK_tbl_Voice_OfficerToAct] PRIMARY KEY ([VoiceOfficerToActId]),
        CONSTRAINT [FK_tbl_Voice_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Voice_OfficerToAct_tbl_Client_Voice_VoiceId] FOREIGN KEY ([VoiceId]) REFERENCES [tbl_Client_Voice] ([VoiceId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Voice_PoorStaff] (
        [VoicePoorStaffId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [VoiceId] int NOT NULL,
        CONSTRAINT [PK_tbl_Voice_PoorStaff] PRIMARY KEY ([VoicePoorStaffId]),
        CONSTRAINT [FK_tbl_Voice_PoorStaff_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Voice_PoorStaff_tbl_Client_Voice_VoiceId] FOREIGN KEY ([VoiceId]) REFERENCES [tbl_Client_Voice] ([VoiceId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_WoundCare_OfficerToAct] (
        [WoundCareOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [WoundCareId] int NOT NULL,
        CONSTRAINT [PK_tbl_WoundCare_OfficerToAct] PRIMARY KEY ([WoundCareOfficerToActId]),
        CONSTRAINT [FK_tbl_WoundCare_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_WoundCare_OfficerToAct_tbl_Client_WoundCare_WoundCareId] FOREIGN KEY ([WoundCareId]) REFERENCES [tbl_Client_WoundCare] ([WoundCareId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_WoundCare_Physician] (
        [WoundCarePhysicianId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [WoundCareId] int NOT NULL,
        CONSTRAINT [PK_tbl_WoundCare_Physician] PRIMARY KEY ([WoundCarePhysicianId]),
        CONSTRAINT [FK_tbl_WoundCare_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_WoundCare_Physician_tbl_Client_WoundCare_WoundCareId] FOREIGN KEY ([WoundCareId]) REFERENCES [tbl_Client_WoundCare] ([WoundCareId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_WoundCare_StaffName] (
        [WoundCareStaffNameId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [WoundCareId] int NOT NULL,
        CONSTRAINT [PK_tbl_WoundCare_StaffName] PRIMARY KEY ([WoundCareStaffNameId]),
        CONSTRAINT [FK_tbl_WoundCare_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_WoundCare_StaffName_tbl_Client_WoundCare_WoundCareId] FOREIGN KEY ([WoundCareId]) REFERENCES [tbl_Client_WoundCare] ([WoundCareId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_AdlObs_OfficerToAct] (
        [AdlObsOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [ObservationId] int NOT NULL,
        CONSTRAINT [PK_tbl_AdlObs_OfficerToAct] PRIMARY KEY ([AdlObsOfficerToActId]),
        CONSTRAINT [FK_tbl_AdlObs_OfficerToAct_tbl_Staff_AdlObs_ObservationId] FOREIGN KEY ([ObservationId]) REFERENCES [tbl_Staff_AdlObs] ([ObservationID]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_AdlObs_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_KeyWorker_OfficerToAct] (
        [KeyWorkerOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [KeyWorkerId] int NOT NULL,
        CONSTRAINT [PK_tbl_KeyWorker_OfficerToAct] PRIMARY KEY ([KeyWorkerOfficerToActId]),
        CONSTRAINT [FK_tbl_KeyWorker_OfficerToAct_tbl_Staff_KeyWorkerVoice_KeyWorkerId] FOREIGN KEY ([KeyWorkerId]) REFERENCES [tbl_Staff_KeyWorkerVoice] ([KeyWorkerId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_KeyWorker_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_KeyWorker_StaffName] (
        [KeyWorkerWorkteamId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [KeyWorkerId] int NOT NULL,
        CONSTRAINT [PK_tbl_KeyWorker_StaffName] PRIMARY KEY ([KeyWorkerWorkteamId]),
        CONSTRAINT [FK_tbl_KeyWorker_StaffName_tbl_Staff_KeyWorkerVoice_KeyWorkerId] FOREIGN KEY ([KeyWorkerId]) REFERENCES [tbl_Staff_KeyWorkerVoice] ([KeyWorkerId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_KeyWorker_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_MedComp_OfficerToAct] (
        [MedCompOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [MedCompId] int NOT NULL,
        CONSTRAINT [PK_tbl_MedComp_OfficerToAct] PRIMARY KEY ([MedCompOfficerToActId]),
        CONSTRAINT [FK_tbl_MedComp_OfficerToAct_tbl_Staff_MedCompObs_MedCompId] FOREIGN KEY ([MedCompId]) REFERENCES [tbl_Staff_MedCompObs] ([MedCompId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_MedComp_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_OneToOne_OfficerToAct] (
        [OneToOneOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [OneToOneId] int NOT NULL,
        CONSTRAINT [PK_tbl_OneToOne_OfficerToAct] PRIMARY KEY ([OneToOneOfficerToActId]),
        CONSTRAINT [FK_tbl_OneToOne_OfficerToAct_tbl_Staff_OneToOne_OneToOneId] FOREIGN KEY ([OneToOneId]) REFERENCES [tbl_Staff_OneToOne] ([OneToOneId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_OneToOne_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_SpotCheck_OfficerToAct] (
        [SpotCheckOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [SpotCheckId] int NOT NULL,
        CONSTRAINT [PK_tbl_SpotCheck_OfficerToAct] PRIMARY KEY ([SpotCheckOfficerToActId]),
        CONSTRAINT [FK_tbl_SpotCheck_OfficerToAct_tbl_Staff_SpotCheck_SpotCheckId] FOREIGN KEY ([SpotCheckId]) REFERENCES [tbl_Staff_SpotCheck] ([SpotCheckId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_SpotCheck_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Supervision_OfficerToAct] (
        [SupervisionOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [StaffSupervisionAppraisalId] int NOT NULL,
        CONSTRAINT [PK_tbl_Supervision_OfficerToAct] PRIMARY KEY ([SupervisionOfficerToActId]),
        CONSTRAINT [FK_tbl_Supervision_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Supervision_OfficerToAct_tbl_Staff_SupervisionAppraisal_StaffSupervisionAppraisalId] FOREIGN KEY ([StaffSupervisionAppraisalId]) REFERENCES [tbl_Staff_SupervisionAppraisal] ([StaffSupervisionAppraisalId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Supervision_StaffName] (
        [SupervisionWorkteamId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [StaffSupervisionAppraisalId] int NOT NULL,
        CONSTRAINT [PK_tbl_Supervision_StaffName] PRIMARY KEY ([SupervisionWorkteamId]),
        CONSTRAINT [FK_tbl_Supervision_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Supervision_StaffName_tbl_Staff_SupervisionAppraisal_StaffSupervisionAppraisalId] FOREIGN KEY ([StaffSupervisionAppraisalId]) REFERENCES [tbl_Staff_SupervisionAppraisal] ([StaffSupervisionAppraisalId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Survey_OfficerToAct] (
        [SurveyOfficerToActId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [StaffSurveyId] int NOT NULL,
        CONSTRAINT [PK_tbl_Survey_OfficerToAct] PRIMARY KEY ([SurveyOfficerToActId]),
        CONSTRAINT [FK_tbl_Survey_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Survey_OfficerToAct_tbl_Staff_Survey_StaffSurveyId] FOREIGN KEY ([StaffSurveyId]) REFERENCES [tbl_Staff_Survey] ([StaffSurveyId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE TABLE [tbl_Survey_StaffName] (
        [SurveyWorkteamId] int NOT NULL IDENTITY,
        [StaffPersonalInfoId] int NOT NULL,
        [StaffSurveyId] int NOT NULL,
        CONSTRAINT [PK_tbl_Survey_StaffName] PRIMARY KEY ([SurveyWorkteamId]),
        CONSTRAINT [FK_tbl_Survey_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId] FOREIGN KEY ([StaffPersonalInfoId]) REFERENCES [tbl_StaffPersonalInfo] ([StaffPersonalInfoId]) ON DELETE CASCADE,
        CONSTRAINT [FK_tbl_Survey_StaffName_tbl_Staff_Survey_StaffSurveyId] FOREIGN KEY ([StaffSurveyId]) REFERENCES [tbl_Staff_Survey] ([StaffSurveyId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_AdlObs_OfficerToAct_ObservationId] ON [tbl_AdlObs_OfficerToAct] ([ObservationId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_AdlObs_OfficerToAct_StaffPersonalInfoId] ON [tbl_AdlObs_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BloodCoag_OfficerToAct_BloodRecordId] ON [tbl_BloodCoag_OfficerToAct] ([BloodRecordId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BloodCoag_OfficerToAct_StaffPersonalInfoId] ON [tbl_BloodCoag_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BloodCoag_Physician_BloodRecordId] ON [tbl_BloodCoag_Physician] ([BloodRecordId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BloodCoag_Physician_StaffPersonalInfoId] ON [tbl_BloodCoag_Physician] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BloodCoag_StaffName_BloodRecordId] ON [tbl_BloodCoag_StaffName] ([BloodRecordId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BloodCoag_StaffName_StaffPersonalInfoId] ON [tbl_BloodCoag_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BloodPressure_OfficerToAct_BloodPressureId] ON [tbl_BloodPressure_OfficerToAct] ([BloodPressureId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BloodPressure_OfficerToAct_StaffPersonalInfoId] ON [tbl_BloodPressure_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BloodPressure_Physician_BloodPressureId] ON [tbl_BloodPressure_Physician] ([BloodPressureId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BloodPressure_Physician_StaffPersonalInfoId] ON [tbl_BloodPressure_Physician] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BloodPressure_StaffName_BloodPressureId] ON [tbl_BloodPressure_StaffName] ([BloodPressureId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BloodPressure_StaffName_StaffPersonalInfoId] ON [tbl_BloodPressure_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BMIChart_OfficerToAct_BMIChartId] ON [tbl_BMIChart_OfficerToAct] ([BMIChartId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BMIChart_OfficerToAct_StaffPersonalInfoId] ON [tbl_BMIChart_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BMIChart_Physician_BMIChartId] ON [tbl_BMIChart_Physician] ([BMIChartId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BMIChart_Physician_StaffPersonalInfoId] ON [tbl_BMIChart_Physician] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BMIChart_StaffName_BMIChartId] ON [tbl_BMIChart_StaffName] ([BMIChartId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BMIChart_StaffName_StaffPersonalInfoId] ON [tbl_BMIChart_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BodyTemp_OfficerToAct_BodyTempId] ON [tbl_BodyTemp_OfficerToAct] ([BodyTempId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BodyTemp_OfficerToAct_StaffPersonalInfoId] ON [tbl_BodyTemp_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BodyTemp_Physician_BodyTempId] ON [tbl_BodyTemp_Physician] ([BodyTempId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BodyTemp_Physician_StaffPersonalInfoId] ON [tbl_BodyTemp_Physician] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BodyTemp_StaffName_BodyTempId] ON [tbl_BodyTemp_StaffName] ([BodyTempId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BodyTemp_StaffName_StaffPersonalInfoId] ON [tbl_BodyTemp_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BowelMovement_OfficerToAct_BowelMovementId] ON [tbl_BowelMovement_OfficerToAct] ([BowelMovementId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BowelMovement_OfficerToAct_StaffPersonalInfoId] ON [tbl_BowelMovement_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BowelMovement_Physician_BowelMovementId] ON [tbl_BowelMovement_Physician] ([BowelMovementId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BowelMovement_Physician_StaffPersonalInfoId] ON [tbl_BowelMovement_Physician] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BowelMovement_StaffName_BowelMovementId] ON [tbl_BowelMovement_StaffName] ([BowelMovementId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_BowelMovement_StaffName_StaffPersonalInfoId] ON [tbl_BowelMovement_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_BloodCoagulationRecord_ClientId] ON [tbl_Client_BloodCoagulationRecord] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_BloodPressure_ClientId] ON [tbl_Client_BloodPressure] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_BMIChart_ClientId] ON [tbl_Client_BMIChart] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_BodyTemp_ClientId] ON [tbl_Client_BodyTemp] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_BowelMovement_ClientId] ON [tbl_Client_BowelMovement] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_Cleaning_NutritionId] ON [tbl_Client_Cleaning] ([NutritionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_Cleaning_STAFFId] ON [tbl_Client_Cleaning] ([STAFFId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_ComplainRegister_ClientId] ON [tbl_Client_ComplainRegister] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_EyeHealthMonitoring_ClientId] ON [tbl_Client_EyeHealthMonitoring] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_FoodIntake_ClientId] ON [tbl_Client_FoodIntake] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_HeartRate_ClientId] ON [tbl_Client_HeartRate] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_LogAudit_ClientId] ON [tbl_Client_LogAudit] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_LogAudit_StaffPersonalInfoId] ON [tbl_Client_LogAudit] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_MealDay_ClientMealTypeId] ON [tbl_Client_MealDay] ([ClientMealTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_MealDay_MealDayofWeekId] ON [tbl_Client_MealDay] ([MealDayofWeekId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_MealDay_NutritionId] ON [tbl_Client_MealDay] ([NutritionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE UNIQUE INDEX [IX_tbl_Client_MealType_MealType] ON [tbl_Client_MealType] ([MealType]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_MedAudit_ClientId] ON [tbl_Client_MedAudit] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_MedAudit_StaffPersonalInfoId] ON [tbl_Client_MedAudit] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_MgtVisit_ClientId] ON [tbl_Client_MgtVisit] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_MgtVisit_StaffPersonalInfoId] ON [tbl_Client_MgtVisit] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_Nutrition_ClientId] ON [tbl_Client_Nutrition] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_Nutrition_StaffId] ON [tbl_Client_Nutrition] ([StaffId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_Oxygenlvl_ClientId] ON [tbl_Client_Oxygenlvl] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_PainChart_ClientId] ON [tbl_Client_PainChart] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_Program_ClientId] ON [tbl_Client_Program] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_Program_StaffPersonalInfoId] ON [tbl_Client_Program] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_PulseRate_ClientId] ON [tbl_Client_PulseRate] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_Seizure_ClientId] ON [tbl_Client_Seizure] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_ServiceWatch_ClientId] ON [tbl_Client_ServiceWatch] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_ServiceWatch_StaffPersonalInfoId] ON [tbl_Client_ServiceWatch] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_Shopping_NutritionId] ON [tbl_Client_Shopping] ([NutritionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_Shopping_STAFFId] ON [tbl_Client_Shopping] ([STAFFId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_Voice_ClientId] ON [tbl_Client_Voice] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_Voice_StaffPersonalInfoId] ON [tbl_Client_Voice] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Client_WoundCare_ClientId] ON [tbl_Client_WoundCare] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Complain_OfficerToAct_ComplainId] ON [tbl_Complain_OfficerToAct] ([ComplainId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Complain_OfficerToAct_StaffPersonalInfoId] ON [tbl_Complain_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Complain_StaffName_ComplainId] ON [tbl_Complain_StaffName] ([ComplainId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Complain_StaffName_StaffPersonalInfoId] ON [tbl_Complain_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Enotice__PublishTo] ON [tbl_Enotice_] ([PublishTo]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_EyeHealth_OfficerToAct_EyeHealthId] ON [tbl_EyeHealth_OfficerToAct] ([EyeHealthId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_EyeHealth_OfficerToAct_StaffPersonalInfoId] ON [tbl_EyeHealth_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_EyeHealth_Physician_EyeHealthId] ON [tbl_EyeHealth_Physician] ([EyeHealthId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_EyeHealth_Physician_StaffPersonalInfoId] ON [tbl_EyeHealth_Physician] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_EyeHealth_StaffName_EyeHealthId] ON [tbl_EyeHealth_StaffName] ([EyeHealthId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_EyeHealth_StaffName_StaffPersonalInfoId] ON [tbl_EyeHealth_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_FoodIntake_OfficerToAct_FoodIntakeId] ON [tbl_FoodIntake_OfficerToAct] ([FoodIntakeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_FoodIntake_OfficerToAct_StaffPersonalInfoId] ON [tbl_FoodIntake_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_FoodIntake_Physician_FoodIntakeId] ON [tbl_FoodIntake_Physician] ([FoodIntakeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_FoodIntake_Physician_StaffPersonalInfoId] ON [tbl_FoodIntake_Physician] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_FoodIntake_StaffName_FoodIntakeId] ON [tbl_FoodIntake_StaffName] ([FoodIntakeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_FoodIntake_StaffName_StaffPersonalInfoId] ON [tbl_FoodIntake_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_HeartRate_OfficerToAct_HeartRateId] ON [tbl_HeartRate_OfficerToAct] ([HeartRateId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_HeartRate_OfficerToAct_StaffPersonalInfoId] ON [tbl_HeartRate_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_HeartRate_Physician_HeartRateId] ON [tbl_HeartRate_Physician] ([HeartRateId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_HeartRate_Physician_StaffPersonalInfoId] ON [tbl_HeartRate_Physician] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_HeartRate_StaffName_HeartRateId] ON [tbl_HeartRate_StaffName] ([HeartRateId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_HeartRate_StaffName_StaffPersonalInfoId] ON [tbl_HeartRate_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Incoming_Meds_UserName] ON [tbl_Incoming_Meds] ([UserName]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_KeyWorker_OfficerToAct_KeyWorkerId] ON [tbl_KeyWorker_OfficerToAct] ([KeyWorkerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_KeyWorker_OfficerToAct_StaffPersonalInfoId] ON [tbl_KeyWorker_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_KeyWorker_StaffName_KeyWorkerId] ON [tbl_KeyWorker_StaffName] ([KeyWorkerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_KeyWorker_StaffName_StaffPersonalInfoId] ON [tbl_KeyWorker_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_LogAudit_OfficerToAct_LogAuditId] ON [tbl_LogAudit_OfficerToAct] ([LogAuditId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_LogAudit_OfficerToAct_StaffPersonalInfoId] ON [tbl_LogAudit_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_MedAudit_AuditorName_MedAuditId] ON [tbl_MedAudit_AuditorName] ([MedAuditId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_MedAudit_AuditorName_StaffPersonalInfoId] ON [tbl_MedAudit_AuditorName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_MedAudit_OfficerToAct_MedAuditId] ON [tbl_MedAudit_OfficerToAct] ([MedAuditId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_MedAudit_OfficerToAct_StaffPersonalInfoId] ON [tbl_MedAudit_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_MedComp_OfficerToAct_MedCompId] ON [tbl_MedComp_OfficerToAct] ([MedCompId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_MedComp_OfficerToAct_StaffPersonalInfoId] ON [tbl_MedComp_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_OneToOne_OfficerToAct_OneToOneId] ON [tbl_OneToOne_OfficerToAct] ([OneToOneId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_OneToOne_OfficerToAct_StaffPersonalInfoId] ON [tbl_OneToOne_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_OxygenLvl_OfficerToAct_OxygenLvlId] ON [tbl_OxygenLvl_OfficerToAct] ([OxygenLvlId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_OxygenLvl_OfficerToAct_StaffPersonalInfoId] ON [tbl_OxygenLvl_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_OxygenLvl_Physician_OxygenLvlId] ON [tbl_OxygenLvl_Physician] ([OxygenLvlId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_OxygenLvl_Physician_StaffPersonalInfoId] ON [tbl_OxygenLvl_Physician] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_OxygenLvl_StaffName_OxygenLvlId] ON [tbl_OxygenLvl_StaffName] ([OxygenLvlId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_OxygenLvl_StaffName_StaffPersonalInfoId] ON [tbl_OxygenLvl_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_PainChart_OfficerToAct_PainChartId] ON [tbl_PainChart_OfficerToAct] ([PainChartId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_PainChart_OfficerToAct_StaffPersonalInfoId] ON [tbl_PainChart_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_PainChart_Physician_PainChartId] ON [tbl_PainChart_Physician] ([PainChartId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_PainChart_Physician_StaffPersonalInfoId] ON [tbl_PainChart_Physician] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_PainChart_StaffName_PainChartId] ON [tbl_PainChart_StaffName] ([PainChartId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_PainChart_StaffName_StaffPersonalInfoId] ON [tbl_PainChart_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Program_OfficerToAct_ProgramId] ON [tbl_Program_OfficerToAct] ([ProgramId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Program_OfficerToAct_StaffPersonalInfoId] ON [tbl_Program_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_PulseRate_OfficerToAct_PulseRateId] ON [tbl_PulseRate_OfficerToAct] ([PulseRateId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_PulseRate_OfficerToAct_StaffPersonalInfoId] ON [tbl_PulseRate_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_PulseRate_Physician_PulseRateId] ON [tbl_PulseRate_Physician] ([PulseRateId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_PulseRate_Physician_StaffPersonalInfoId] ON [tbl_PulseRate_Physician] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_PulseRate_StaffName_PulseRateId] ON [tbl_PulseRate_StaffName] ([PulseRateId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_PulseRate_StaffName_StaffPersonalInfoId] ON [tbl_PulseRate_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Resources__PublishTo] ON [tbl_Resources_] ([PublishTo]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Seizure_OfficerToAct_SeizureId] ON [tbl_Seizure_OfficerToAct] ([SeizureId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Seizure_OfficerToAct_StaffPersonalInfoId] ON [tbl_Seizure_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Seizure_Physician_SeizureId] ON [tbl_Seizure_Physician] ([SeizureId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Seizure_Physician_StaffPersonalInfoId] ON [tbl_Seizure_Physician] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Seizure_StaffName_SeizureId] ON [tbl_Seizure_StaffName] ([SeizureId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Seizure_StaffName_StaffPersonalInfoId] ON [tbl_Seizure_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Service_OfficerToAct_ServiceId] ON [tbl_Service_OfficerToAct] ([ServiceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Service_OfficerToAct_StaffPersonalInfoId] ON [tbl_Service_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Service_StaffName_ServiceId] ON [tbl_Service_StaffName] ([ServiceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Service_StaffName_StaffPersonalInfoId] ON [tbl_Service_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_SpotCheck_OfficerToAct_SpotCheckId] ON [tbl_SpotCheck_OfficerToAct] ([SpotCheckId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_SpotCheck_OfficerToAct_StaffPersonalInfoId] ON [tbl_SpotCheck_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Staff_AdlObs_ClientId] ON [tbl_Staff_AdlObs] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Staff_AdlObs_StaffPersonalInfoId] ON [tbl_Staff_AdlObs] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Staff_KeyWorkerVoice_ServicesRequiresServices] ON [tbl_Staff_KeyWorkerVoice] ([ServicesRequiresServices]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Staff_KeyWorkerVoice_StaffPersonalInfoId] ON [tbl_Staff_KeyWorkerVoice] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Staff_MedCompObs_ClientId] ON [tbl_Staff_MedCompObs] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Staff_MedCompObs_StaffPersonalInfoId] ON [tbl_Staff_MedCompObs] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Staff_OneToOne_StaffPersonalInfoId] ON [tbl_Staff_OneToOne] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Staff_Reference_ApplicantRole] ON [tbl_Staff_Reference] ([ApplicantRole]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Staff_Reference_ConfirmedBy] ON [tbl_Staff_Reference] ([ConfirmedBy]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Staff_SpotCheck_ClientId] ON [tbl_Staff_SpotCheck] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Staff_SpotCheck_StaffPersonalInfoId] ON [tbl_Staff_SpotCheck] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Staff_SupervisionAppraisal_StaffPersonalInfoId] ON [tbl_Staff_SupervisionAppraisal] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Staff_Survey_StaffPersonalInfoId] ON [tbl_Staff_Survey] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Supervision_OfficerToAct_StaffPersonalInfoId] ON [tbl_Supervision_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Supervision_OfficerToAct_StaffSupervisionAppraisalId] ON [tbl_Supervision_OfficerToAct] ([StaffSupervisionAppraisalId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Supervision_StaffName_StaffPersonalInfoId] ON [tbl_Supervision_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Supervision_StaffName_StaffSupervisionAppraisalId] ON [tbl_Supervision_StaffName] ([StaffSupervisionAppraisalId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Survey_OfficerToAct_StaffPersonalInfoId] ON [tbl_Survey_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Survey_OfficerToAct_StaffSurveyId] ON [tbl_Survey_OfficerToAct] ([StaffSurveyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Survey_StaffName_StaffPersonalInfoId] ON [tbl_Survey_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Survey_StaffName_StaffSurveyId] ON [tbl_Survey_StaffName] ([StaffSurveyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Visit_OfficerToAct_StaffPersonalInfoId] ON [tbl_Visit_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Visit_OfficerToAct_VisitId] ON [tbl_Visit_OfficerToAct] ([VisitId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Visit_StaffName_StaffPersonalInfoId] ON [tbl_Visit_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Visit_StaffName_VisitId] ON [tbl_Visit_StaffName] ([VisitId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Voice_CallerName_StaffPersonalInfoId] ON [tbl_Voice_CallerName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Voice_CallerName_VoiceId] ON [tbl_Voice_CallerName] ([VoiceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Voice_GoodStaff_StaffPersonalInfoId] ON [tbl_Voice_GoodStaff] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Voice_GoodStaff_VoiceId] ON [tbl_Voice_GoodStaff] ([VoiceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Voice_OfficerToAct_StaffPersonalInfoId] ON [tbl_Voice_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Voice_OfficerToAct_VoiceId] ON [tbl_Voice_OfficerToAct] ([VoiceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Voice_PoorStaff_StaffPersonalInfoId] ON [tbl_Voice_PoorStaff] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Voice_PoorStaff_VoiceId] ON [tbl_Voice_PoorStaff] ([VoiceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_Whisttle_Blower_UserName] ON [tbl_Whisttle_Blower] ([UserName]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_WoundCare_OfficerToAct_StaffPersonalInfoId] ON [tbl_WoundCare_OfficerToAct] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_WoundCare_OfficerToAct_WoundCareId] ON [tbl_WoundCare_OfficerToAct] ([WoundCareId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_WoundCare_Physician_StaffPersonalInfoId] ON [tbl_WoundCare_Physician] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_WoundCare_Physician_WoundCareId] ON [tbl_WoundCare_Physician] ([WoundCareId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_WoundCare_StaffName_StaffPersonalInfoId] ON [tbl_WoundCare_StaffName] ([StaffPersonalInfoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    CREATE INDEX [IX_tbl_WoundCare_StaffName_WoundCareId] ON [tbl_WoundCare_StaffName] ([WoundCareId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210625163907_New_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210625163907_New_Migration', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210629105731_tbl_ClientRotaDays_weekday')
BEGIN
    ALTER TABLE [tbl_ClientRotaDays] ADD [WeekDay] nvarchar(25) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210629105731_tbl_ClientRotaDays_weekday')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210629105731_tbl_ClientRotaDays_weekday', N'3.1.3');
END;

GO

