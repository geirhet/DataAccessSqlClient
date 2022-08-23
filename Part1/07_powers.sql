--Incerting Powers
--Demonstrate many to many

USE SuperheroesDb;


INSERT INTO Power
VALUES 
('Money', 'Use money to pay for cool gadgets'),
('Super Strength', 'Very strong'),
('Smart', 'Make cool gadgets'),
('Spidersense', 'Sense spiders');

INSERT INTO relationshipSuperheroPower
VALUES
(2,1),
(1,2),
(1,3),
(3,4),
(2,3); 


