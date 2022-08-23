USE SuperheroesDb;

--One Superhero, many Assist
ALTER TABLE Assistant
ADD SuperheroId INT FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id);

