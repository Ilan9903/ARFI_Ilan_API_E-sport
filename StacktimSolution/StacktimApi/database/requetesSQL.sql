/* leaderboard players desc*/

SELECT Name, TotalScore FROM Players ORDER BY TotalScore DESC;

/* compo Team */

SELECT t.Name AS TeamName, p.Name AS PlayerName, tp.Role FROM Teams t JOIN TeamPlayers tp ON t.Id_Teams = tp.TeamId JOIN Players p ON p.Id_Players = tp.PlayerId WHERE t.Id_Teams = 1;

/* stats Team */

SELECT t.Name AS TeamName, COUNT(p.Id_Players) AS NumberOfPlayers, AVG(p.TotalScore) AS AverageScore FROM Teams t JOIN TeamPlayers tp ON t.Id_Teams = tp.TeamId JOIN Players p ON p.Id_Players = tp.PlayerId GROUP BY t.Name;

/* players sans team */

INSERT INTO Players (Name, Email, RankPlayer, TotalScore) VALUES
('Pheonix', 'phaphe@geffe.com', 'Gold', 1500),
('Plasma', 'plasma@hihou.fr', 'Master', 3500);

SELECT Name FROM Players WHERE Id_Players NOT IN (SELECT PlayerId FROM TeamPlayers);

/* nombre de players par rank */

SELECT RankPlayer, COUNT(Id_Players) AS NumberOfPlayers FROM Players GROUP BY RankPlayer ORDER BY NumberOfPlayers DESC;