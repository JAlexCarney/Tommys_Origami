USE master;
GO
DROP DATABASE IF EXISTS TripPlanner;
GO
CREATE DATABASE TripPlanner;
GO
USE TripPlanner;
GO
CREATE TABLE [User] (
 UserID UNIQUEIDENTIFIER primary key,
 Username VARCHAR(50) null,
 [Password] nvarchar(50) not null,
 Email nvarchar(50) not null,
 DateCreated date not null
)
GO
CREATE TABLE Destination (
 DestinationID int primary key identity(1,1),
 City VARCHAR(50) not null,
 StateProvince varchar(25) null,
 Country VARCHAR(57) not null
)
GO
CREATE TABLE Trip (
 TripID int primary key identity(1,1),
 UserID UNIQUEIDENTIFIER not null foreign key references [User](UserID),
 StartDate date not null,
 ProjectedEndDate date not NULL,
 ActualEndDate date NULL,
 IsBooked bit NOT NULL
)
GO
CREATE TABLE Review (
 DestinationID int not null foreign key references Destination(DestinationID),
 UserID UNIQUEIDENTIFIER not null foreign key references [User](UserID),
 [Description] VARCHAR(300) not null,
 Rating DECIMAL(2,1) not null,
 CONSTRAINT PK_Review PRIMARY KEY (DestinationID, UserID)
)
GO
CREATE TABLE DestinationTrip (
 DestinationID int not null foreign key references Destination(DestinationID),
 TripID int not null foreign key references Trip(TripID),
 [Description] VARCHAR(100) not null,
 CONSTRAINT PK_DestinationTrip PRIMARY KEY (DestinationID, TripID)
)
