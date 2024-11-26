CREATE DATABASE Library;
GO

use Library;

CREATE TABLE Book
(
    BookId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    ISBN NVARCHAR(13) NOT NULL,
    PublicationYear INT,
    AvailableCopies INT NOT NULL
);

-- Create Member table
CREATE TABLE Member
(
    MemberId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255),
    PhoneNumber NVARCHAR(20),
    Email NVARCHAR(255),
    RegistrationDate DATE NOT NULL
);

-- Create Loan table
CREATE TABLE Loan
(
    LoanId INT PRIMARY KEY IDENTITY(1,1),
    BookId INT FOREIGN KEY REFERENCES Book(BookId),
    MemberId INT FOREIGN KEY REFERENCES Member(MemberId),
    LoanDate DATE NOT NULL,
    DueDate DATE NOT NULL,
    ReturnDate DATE
);

-- Insert sample data into Book table  
INSERT INTO Book (Title, ISBN, PublicationYear, AvailableCopies)
VALUES ('To Kill a Mockingbird', '9780060935467', 1960, 3),
       ('1984', '9780451524935', 1949, 2),
       ('The Catcher in the Rye', '9780316769174', 1951, 4);

-- Insert sample data into Member table  
INSERT INTO Member (FirstName, LastName, Address, PhoneNumber, Email, RegistrationDate)
VALUES ('John', 'Doe', '123 Main St', '555-1234', 'john.doe@example.com', '2020-01-01'),
       ('Jane', 'Smith', '456 Elm St', '555-5678', 'jane.smith@example.com', '2021-05-15');

-- Insert sample data into Loan table  
INSERT INTO Loan (BookId, MemberId, LoanDate, DueDate)
VALUES (1, 1, '2022-01-05', '2022-01-19'),
       (2, 2, '2022-02-10', '2022-02-24'),
       (3, 1, '2022-03-01', '2022-03-15');