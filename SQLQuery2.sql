ALTER TABLE Rooms
DROP CONSTRAINT FK_Rooms_Residents;

ALTER TABLE Rooms
ADD CONSTRAINT FK_Rooms_Residents FOREIGN KEY (Email)
REFERENCES Residents (Email)
ON DELETE CASCADE;


ALTER TABLE Income
DROP CONSTRAINT FK_Income_Resident_04E4BC85;

ALTER TABLE Income
ADD CONSTRAINT FK_Income_Resident_04E4BC85
FOREIGN KEY (ResidentEmail) REFERENCES Residents(Email)
ON DELETE SET NULL;

select * from Income



-- Step 2: Drop the existing foreign key constraint
-- Replace 'FK_Income_Resident_04E4BC85' with the actual constraint name from the query above
ALTER TABLE Income DROP CONSTRAINT FK_Income_Resident_04E4BC85;

-- Step 3: Recreate the foreign key with ON UPDATE CASCADE
ALTER TABLE Income
ADD CONSTRAINT FK_Income_Resident_04E4BC85
FOREIGN KEY (ResidentEmail) REFERENCES Residents(Email)
ON DELETE CASCADE
ON UPDATE CASCADE;
