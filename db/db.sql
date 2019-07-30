IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Foundation] (
    [id_foundation] int NOT NULL IDENTITY,
    [name] nvarchar(75) NOT NULL,
    [address] nvarchar(150) NOT NULL,
    [association] nvarchar(145) NOT NULL,
    [email] nvarchar(150) NOT NULL,
    [web] nvarchar(145) NOT NULL,
    CONSTRAINT [PK_Foundation] PRIMARY KEY ([id_foundation])
);

GO

CREATE TABLE [User] (
    [id_user] int NOT NULL IDENTITY,
    [name] nvarchar(50) NOT NULL,
    [password] nvarchar(150) NOT NULL,
    [role] nvarchar(15) NOT NULL,
    [email] nvarchar(150) NOT NULL,
    [status] bit NOT NULL,
    [img] nvarchar(max) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([id_user])
);

GO

CREATE TABLE [Pet] (
    [id_pet] int NOT NULL IDENTITY,
    [species] nvarchar(6) NOT NULL,
    [race] nvarchar(45) NOT NULL,
    [age] int NOT NULL,
    [name] nvarchar(45) NOT NULL,
    [sex] nvarchar(15) NOT NULL,
    [img] nvarchar(500) NULL,
    [id_foundation] int NOT NULL,
    CONSTRAINT [PK_Pet] PRIMARY KEY ([id_pet]),
    CONSTRAINT [FK_Pet_Foundation_id_foundation] FOREIGN KEY ([id_foundation]) REFERENCES [Foundation] ([id_foundation]) ON DELETE CASCADE
);

GO

CREATE TABLE [Adopter] (
    [id_adopter] int NOT NULL IDENTITY,
    [name] nvarchar(45) NOT NULL,
    [identification] nvarchar(50) NOT NULL,
    [phone] nvarchar(45) NOT NULL,
    [email] nvarchar(150) NOT NULL,
    [address] nvarchar(145) NOT NULL,
    [id_pet] int NOT NULL,
    CONSTRAINT [PK_Adopter] PRIMARY KEY ([id_adopter]),
    CONSTRAINT [FK_Adopter_Pet_id_pet] FOREIGN KEY ([id_pet]) REFERENCES [Pet] ([id_pet]) ON DELETE CASCADE
);

GO

CREATE TABLE [Form] (
    [id_form] int NOT NULL IDENTITY,
    [name] nvarchar(50) NOT NULL,
    [number_adults] int NOT NULL,
    [number_children] int NOT NULL,
    [age_children] int NOT NULL,
    [pet_race] nvarchar(50) NOT NULL,
    [pets_before] nvarchar(75) NOT NULL,
    [rason_adoption] nvarchar(150) NOT NULL,
    [responsibility_pet] nvarchar(150) NOT NULL,
    [pet_status_check] bit NOT NULL,
    [report] nvarchar(max) NULL,
    [id_adopter] int NOT NULL,
    CONSTRAINT [PK_Form] PRIMARY KEY ([id_form]),
    CONSTRAINT [FK_Form_Adopter_id_adopter] FOREIGN KEY ([id_adopter]) REFERENCES [Adopter] ([id_adopter]) ON DELETE CASCADE
);

GO

CREATE UNIQUE INDEX [IX_Adopter_email] ON [Adopter] ([email]);

GO

CREATE UNIQUE INDEX [IX_Adopter_id_pet] ON [Adopter] ([id_pet]);

GO

CREATE UNIQUE INDEX [IX_Adopter_identification] ON [Adopter] ([identification]);

GO

CREATE UNIQUE INDEX [IX_Form_id_adopter] ON [Form] ([id_adopter]);

GO

CREATE UNIQUE INDEX [IX_Foundation_email] ON [Foundation] ([email]);

GO

CREATE INDEX [IX_Pet_id_foundation] ON [Pet] ([id_foundation]);

GO

CREATE UNIQUE INDEX [IX_User_email] ON [User] ([email]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190730012433_db', N'2.2.6-servicing-10079');

GO