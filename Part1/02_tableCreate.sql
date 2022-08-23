USE SuperheroesDb;
--Create table Superhero
CREATE TABLE Superhero (
Id int PRIMARY KEY NOT NULL IDENTITY(1,1), 
Name nvarchar(50) NOT NULL,
Alias nvarchar(50) NOT NULL,
Origin nvarchar(50) NOT NULL,
);

--Create table Assistant
CREATE TABLE Assistant (
Id int PRIMARY KEY NOT NULL IDENTITY(1,1), 
Name nvarchar(50) NOT NULL,
);

--Create table Power
CREATE TABLE Power (
Id int PRIMARY KEY NOT NULL IDENTITY(1,1), 
Name nvarchar(50) NOT NULL,
Description nvarchar(100) NOT NULL,
);
