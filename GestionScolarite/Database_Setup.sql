-- Script de création de la BD GestionScolarite

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'GestionScolarite')
BEGIN
    CREATE DATABASE GestionScolarite;
END
GO

USE GestionScolarite;
GO

IF OBJECT_ID('dbo.Inscriptions', 'U') IS NOT NULL
    DROP TABLE dbo.Inscriptions;
GO

IF OBJECT_ID('dbo.Cours', 'U') IS NOT NULL
    DROP TABLE dbo.Cours;
GO

IF OBJECT_ID('dbo.Etudiants', 'U') IS NOT NULL
    DROP TABLE dbo.Etudiants;
GO

CREATE TABLE Etudiants
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nom NVARCHAR(100) NOT NULL,
    Prenom NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Cours
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titre NVARCHAR(200) NOT NULL,
    Code NVARCHAR(20) NOT NULL UNIQUE
);
GO

CREATE TABLE Inscriptions
(
    EtudiantId INT NOT NULL,
    CoursId INT NOT NULL,
    Session NVARCHAR(20) NOT NULL,
    Note INT NULL,
    PRIMARY KEY (EtudiantId, CoursId, Session),
    FOREIGN KEY (EtudiantId) REFERENCES Etudiants(Id) ON DELETE CASCADE,
    FOREIGN KEY (CoursId) REFERENCES Cours(Id) ON DELETE CASCADE,
    CHECK (Note IS NULL OR (Note >= 0 AND Note <= 100))
);
GO

INSERT INTO Etudiants (Nom, Prenom) VALUES 
    ('Tremblay', 'Marie'),
    ('Gagnon', 'Jean'),
    ('Roy', 'Sophie'),
    ('Côté', 'Marc'),
    ('Bouchard', 'Annie');
GO

INSERT INTO Cours (Titre, Code) VALUES 
    ('Programmation Orientée Objet I', '420-135-GG'),
    ('Programmation Orientée Objet II', '420-137-GG'),
    ('Base de données', '420-133-GG'),
    ('Développement Web', '420-136-GG'),
    ('Structures de données', '420-234-GG');
GO

INSERT INTO Inscriptions (EtudiantId, CoursId, Session, Note) VALUES 
    (1, 1, 'A2024', 85),
    (1, 2, 'H2025', NULL),
    (1, 3, 'A2024', 90),
    (2, 1, 'A2024', 78),
    (2, 2, 'H2025', NULL),
    (3, 2, 'H2025', NULL),
    (3, 3, 'A2024', 92),
    (3, 4, 'H2025', 88),
    (4, 1, 'A2024', 95),
    (4, 5, 'H2025', NULL),
    (5, 2, 'H2025', NULL),
    (5, 3, 'A2024', 87);
GO

PRINT 'BD créée avec succès !';
GO
