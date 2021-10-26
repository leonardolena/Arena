
CREATE TABLE 'Gladiators'
(
    'Id' INT PRIMARY KEY , 
    'Name' VARCHAR NOT NULL , 
    'Type' VARCHAR(16) NOT NULL,
    'Strenth' int NOT null,
);
CREATE TABLE 'Happenings'
(
    'Time' NUMERIC PRIMARY KEY,
    'Event' VARCHAR not null,
    'BearerGladiator' text,
    'RemainingHp' int,
    'TriggererGladiator' text,
);