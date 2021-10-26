
CREATE TABLE 'Gladiator'
(
    'Id' INT PRIMARY KEY , 
    'Name' VARCHAR NOT NULL , 
    'Type' VARCHAR(16) NOT NULL,
);
CREATE TABLE 'Happenings'
(
    'Time' NUMERIC PRIMARY KEY,
    'Event' VARCHAR not null,
    'BearerGladiatorId' int,
    'RemainingHp' int,
    'TriggererGladiatorId' int,
);