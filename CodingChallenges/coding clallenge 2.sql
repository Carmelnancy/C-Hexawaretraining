create database LoanManagement;

use LoanManagement;

create table Customer (
    CustomerId int primary key,
    Name varchar(50),
    Email varchar(50),
    PhoneNumber varchar(15),
    Address VARCHAR(50),
    CreditScore int
);

create table Laon(
    LoanId int PRIMARY KEY,
    CustomerId int FOREIGN KEY REFERENCES Customer(CustomerId),
    PrincipalAmount float,
    InterestRate float,
    LoanTerm int,
    LoanType varchar(50),
    LoanStatus varchar(20)
);

create table HomeLoan (
    LoanId INT PRIMARY KEY FOREIGN KEY REFERENCES Laon(LoanId),
    PropertyAddress VARCHAR(255),
    PropertyValue INT
);

create table CarLoan (
    LoanId INT PRIMARY KEY FOREIGN KEY REFERENCES Laon(LoanId),
    CarModel varchar(100),
    CarValue int 
);

insert into Customer values
(1, 'Nancy', 'nancy@email.com', '9876543210', 'New York', 720),
(2, 'Smith', 'smith@email.com', '9123456780', 'Los Angeles', 680),
(3, 'Carmel', 'carmel@email.com', '9345678901', 'Chicago', 610),
(4, 'Danile', 'daniel@email.com', '9456123789', 'Houston', 800),
(5, 'Mancy', 'mancy@email.com', '9567894321', 'Phoenix', 640);

INSERT INTO Laon VALUES
(1, 1, 1000000, 8.5, 120, 'HomeLoan', 'Pending'),
(2, 2, 500000, 9.0, 60, 'CarLoan', 'Pending'),
(3, 3, 1500000, 7.5, 180, 'HomeLoan', 'Pending'),
(4, 4, 400000, 10.0, 48, 'CarLoan', 'Pending'),
(5, 5, 2000000, 8.0, 240, 'HomeLoan', 'Pending');

INSERT INTO HomeLoan VALUES
(1, '123  New York, NY', 1500000),
(3, '456  Chicago, IL', 1750000),
(5, '789  Phoenix, AZ', 2200000);


INSERT INTO CarLoan VALUES
(2, 'Honda 2021', 600000),
(4, 'Toyota 2022', 450000);
