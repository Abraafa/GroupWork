CREATE DATABASE HotelBookingSystem;

USE HotelBookingSystem;

-- Kunder
CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Phone NVARCHAR(20) NOT NULL
);

-- Rumstyper (Exempel: Single, Double, Suite)
CREATE TABLE RoomTypes (
    RoomTypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(50) NOT NULL,
    PricePerNight DECIMAL(10,2) NOT NULL
);

-- Rum (Kopplat till RoomTypes)
CREATE TABLE Rooms (
    RoomID INT IDENTITY(1,1) PRIMARY KEY,
    RoomNumber NVARCHAR(10) UNIQUE NOT NULL,
    RoomTypeID INT NOT NULL,
    IsAvailable BIT DEFAULT 1,
    FOREIGN KEY (RoomTypeID) REFERENCES RoomTypes(RoomTypeID) ON DELETE CASCADE
);

-- Bokningar (Kopplat till Customers och Rooms)
CREATE TABLE Bookings (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    RoomID INT NOT NULL,
    CheckInDate DATE NOT NULL,
    CheckOutDate DATE NOT NULL,
    Status NVARCHAR(20) DEFAULT 'Active', -- Active, Canceled, Completed
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID) ON DELETE CASCADE,
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID) ON DELETE CASCADE
);

-- Lägg till kunder
INSERT INTO Customers (Name, Email, Phone) VALUES
('Anna Karlsson', 'anna@example.com', '0701234567'),
('Erik Svensson', 'erik@example.com', '0707654321');

-- Lägg till rumstyper
INSERT INTO RoomTypes (TypeName, PricePerNight) VALUES
('Single', 500.00),
('Double', 800.00),
('Suite', 1500.00);

-- Lägg till rum (Alla rum kopplas till en RoomType)
INSERT INTO Rooms (RoomNumber, RoomTypeID) VALUES
('101', 1), -- Single
('102', 1), -- Single
('201', 2), -- Double
('202', 2), -- Double
('301', 3); -- Suite

-- Lägg till bokningar (Bokningarna kopplas till Customers & Rooms)
INSERT INTO Bookings (CustomerID, RoomID, CheckInDate, CheckOutDate) VALUES
(1, 1, '2025-03-01', '2025-03-05'),
(2, 3, '2025-03-02', '2025-03-06');
