
select * from Users;

CREATE TABLE AllowedUsers (
    ApprovalID VARCHAR(100) UNIQUE NOT NULL,
);

CREATE TABLE Users (
    ApprovalID VARCHAR(100) UNIQUE NOT NULL,
    Email VARCHAR(100) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(20) NOT NULL CHECK (Role IN ('Manager', 'Receptionist')),
);

INSERT INTO AllowedUsers (ApprovalID) 
VALUES 
('ID001'), ('ID002'), ('ID003'), ('ID004'), ('ID005'), 
('ID006'), ('ID007'), ('ID008'), ('ID009'), ('ID010');

select * from AllowedUsers;