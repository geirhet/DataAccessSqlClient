USE SuperheroesDb;

CREATE TABLE relationshipSuperheroPower(
	SuperheroId int,
	SuperpowerId int,
	PRIMARY KEY (SuperheroId, SuperpowerId)

);

ALTER TABLE relationshipSuperheroPower
ADD FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id);

ALTER TABLE relationshipSuperheroPower
ADD FOREIGN KEY (SuperpowerId) REFERENCES Power(Id);
