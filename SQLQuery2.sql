-- Worker Table
CREATE TABLE Workers (
    Email VARCHAR(100) PRIMARY KEY,            -- Unique email address for each worker
    Name VARCHAR(100) NOT NULL,                -- Name of the worker, cannot be NULL
    MobileNo VARCHAR(15),                      -- Mobile number (adjust length if needed)
    Gender VARCHAR(10) CHECK (Gender IN ('Male', 'Female')), -- Restrict gender values
    Salary DECIMAL(10, 2) NOT NULL CHECK (Salary > 0), -- Salary must be greater than 0
    JobTitle VARCHAR(50) NOT NULL             -- Job title of the worker
);

Select * from Workers;

select * from Users;


CREATE TABLE Rooms (
    RoomID INT IDENTITY(1,1) PRIMARY KEY,                  -- Auto-increment RoomID
    RoomType VARCHAR(10) NOT NULL CHECK (RoomType IN ('Single', 'Double', 'Triple')), -- Constraint for valid room types
    PricePerNight DECIMAL(10, 2) NOT NULL CHECK (PricePerNight > 0), -- Price must be positive
    Status VARCHAR(10) NOT NULL DEFAULT 'Available' CHECK (Status IN ('Available', 'Occupied')) -- Constraint for valid status
);

select * from Rooms;

CREATE TABLE Residents (
    Email VARCHAR(100) PRIMARY KEY,               -- Unique email address for each resident
    Name VARCHAR(100) NOT NULL,                   -- Name of the resident, cannot be NULL
    ContactInfo VARCHAR(100),                     -- Contact information (e.g., phone or email)
    CheckInDate DATE NOT NULL,                    -- Check-in date
    CheckOutDate DATE,                            -- Check-out date (nullable)
    BoardingType VARCHAR(20) NOT NULL CHECK (BoardingType IN ('Full Board', 'Half Board', 'Bed and Breakfast')), -- Boarding type constraint
     BoardingPrice DECIMAL(10, 2) NOT NULL,
    RoomID INT,                                   -- Foreign key linking to the Rooms table
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID) ON DELETE SET NULL -- RoomID set to NULL if referenced room is deleted
);






select * from Residents;
select * from Income;

CREATE TABLE Income (
    IncomeID INT IDENTITY(1,1) PRIMARY KEY,       -- Auto-increment IncomeID
    Amount DECIMAL(10, 2) NOT NULL CHECK (Amount > 0), -- Amount must be positive
    Date DATE NOT NULL                            -- Date of the income
);

