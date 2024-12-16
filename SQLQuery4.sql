
select * from Residents;
drop table Residents
drop table Rooms
select * from Rooms



CREATE TABLE Rooms (
     RoomID INT PRIMARY KEY,  -- Removed AUTO_INCREMENT
    RoomType VARCHAR(10) NOT NULL CHECK (RoomType IN ('Single', 'Double', 'Triple')),
    PricePerNight DECIMAL(10, 2) NOT NULL,
    Status VARCHAR(10) DEFAULT 'Available' CHECK (Status IN ('Available', 'Occupied'))
);




CREATE TABLE Residents (
    Email VARCHAR(100) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    ContactInfo VARCHAR(100),
    CheckInDate DATE NOT NULL,
    CheckOutDate DATE,
    BoardingType VARCHAR(20) NOT NULL CHECK (BoardingType IN ('Full Board', 'Half Board', 'Bed and Breakfast')),
    RoomID INT,
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID) ON DELETE SET NULL
);



SET IDENTITY_INSERT Rooms ON;

INSERT INTO Rooms (RoomID, RoomType, PricePerNight, Status) 
VALUES 
    (1, 'Single', 50.00, 'Available'),--
    (2, 'Double', 75.00, 'Available'),
    (3, 'Triple', 100.00, 'Occupied'),--
    (4, 'Single', 55.00, 'Available'),--
    (5, 'Double', 80.00, 'Available'),
    (6, 'Triple', 110.00, 'Occupied'),--
    (7, 'Single', 50.00, 'Available'),
    (8, 'Double', 70.00, 'Available'),--
    (9, 'Triple', 95.00, 'Available'),
    (10, 'Single', 60.00, 'Occupied');--

SET IDENTITY_INSERT Rooms OFF;



    INSERT INTO Residents (Email, Name, ContactInfo, CheckInDate, CheckOutDate, BoardingType, RoomID)
VALUES 
    ('john.doe@example.com', 'John Doe', '123-456-7890', '2024-12-01', '2024-12-15', 'Full Board', 1),
    ('jane.smith@example.com', 'Jane Smith', '987-654-3210', '2024-12-05', '2024-12-20', 'Half Board', 2),
    ('sam.jones@example.com', 'Sam Jones', '555-123-4567', '2024-12-10', NULL, 'Bed and Breakfast', 3),
    ('alice.white@example.com', 'Alice White', '444-555-6666', '2024-12-01', '2024-12-10', 'Full Board', 4),
    ('bob.green@example.com', 'Bob Green', '777-888-9999', '2024-12-05', '2024-12-18', 'Half Board', 5),
    ('lucy.brown@example.com', 'Lucy Brown', '111-222-3333', '2024-12-07', NULL, 'Bed and Breakfast', 6),
    ('michael.lee@example.com', 'Michael Lee', '333-444-5555', '2024-12-09', '2024-12-22', 'Full Board', 7),
    ('nancy.johnson@example.com', 'Nancy Johnson', '666-777-8888', '2024-12-11', '2024-12-25', 'Half Board', 8),
    ('chris.martin@example.com', 'Chris Martin', '222-333-4444', '2024-12-13', '2024-12-30', 'Bed and Breakfast', 9),
    ('diana.morris@example.com', 'Diana Morris', '555-666-7777', '2024-12-15', NULL, 'Full Board', 10);



    drop table Income
select * from Income;
-- Income Tracking Table
CREATE TABLE Income (
    IncomeID INT PRIMARY KEY, -- Removed AUTO_INCREMENT
    ResidentEmail VARCHAR(100), -- Added ResidentEmail field
    Amount DECIMAL(10, 2) NOT NULL,
    Date DATE NOT NULL,
    FOREIGN KEY (ResidentEmail) REFERENCES Residents(Email) -- Set foreign key constraint
);


-- Insert records into the Income table without the Description column
INSERT INTO Income (IncomeID, ResidentEmail, Amount, Date) VALUES
(1, 'john.doe@example.com', 500.00, '2024-12-01'),
(2, 'jane.smith@example.com', 300.00, '2024-12-05'),
(3, 'sam.jones@example.com', 150.00, '2024-12-10'),
(4, 'alice.white@example.com', 400.00, '2024-12-01'),
(5, 'bob.green@example.com', 350.00, '2024-12-05'),
(6, 'lucy.brown@example.com', 200.00, '2024-12-07'),
(7, 'michael.lee@example.com', 550.00, '2024-12-9'),
(8, 'nancy.johnson@example.com', 450.00, '2024-12-11'),
(9, 'chris.martin@example.com', 250.00, '2024-12-13'),
(10, 'diana.morris@example.com', 600.00, '2024-12-15');



