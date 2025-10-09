DROP TABLE IF EXISTS dbo.TeamPlayers;
DROP TABLE IF EXISTS dbo.Players;
DROP TABLE IF EXISTS dbo.Teams;


CREATE TABLE Players(
   Id_Players INT IDENTITY(1,1),
   Pseudo VARCHAR(50) NOT NULL UNIQUE,
   Email VARCHAR(100) NOT NULL UNIQUE,
   Rank_ VARCHAR(20),
   TotalScore INT NOT NULL DEFAULT 0,
   RegistrationDate DATETIME2 NOT NULL DEFAULT GETDATE(),
   CONSTRAINT CHK_Player_Rank CHECK (Rank_ IN ('Bronze', 'Silver', 'Gold', 'Platinum', 'Diamond', 'Master')),
   CONSTRAINT CHK_Player_TotalScore CHECK (TotalScore >= 0),
   PRIMARY KEY(Id_Players),
);

INSERT INTO Players (Pseudo, Email, Rank_, TotalScore) VALUES
('Shadow', 'shadow@email.com', 'Gold', 1500),
('Vortex', 'vortex@email.com', 'Platinum', 2200),
('Phoenix', 'phoenix@email.com', 'Gold', 1800),
('Seraph', 'seraph@email.com', 'Diamond', 3100),
('Rogue', 'rogue@email.com', 'Silver', 900),
('Zephyr', 'zephyr@email.com', 'Master', 4500);

CREATE TABLE Teams(
   Id_Teams INT IDENTITY(1,1),
   Name VARCHAR(100) NOT NULL UNIQUE,
   Tag CHAR(3) NOT NULL UNIQUE,
   CreationDate DATETIME2 NOT NULL DEFAULT GETDATE(),
   CONSTRAINT CHK_Teams_Tag CHECK (Tag LIKE '[A-Z][A-Z][A-Z]'),
   PRIMARY KEY(Id_Teams),
);

CREATE TABLE TeamPlayers(
   PlayerId INT,
   TeamId INT,
   Role INT NOT NULL,
   JoinDate DATETIME NOT NULL,
   PRIMARY KEY(PlayerId, TeamId),
   FOREIGN KEY(PlayerId) REFERENCES Players(Id_Players),
   FOREIGN KEY(TeamId) REFERENCES Teams(Id_Teams)
);