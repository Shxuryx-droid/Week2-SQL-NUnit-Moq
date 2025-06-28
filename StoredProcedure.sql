-- ===========================================================
-- Employee Management System - SQL Exercises
-- ===========================================================

-- =============================
-- SCHEMA: Create Tables
-- =============================
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(100)
);

CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1), -- Auto-incrementing
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    Salary DECIMAL(10,2),
    JoinDate DATE
);

-- =============================
-- SAMPLE DATA
-- =============================
-- Insert Departments
INSERT INTO Departments (DepartmentID, DepartmentName) VALUES
(1, 'HR'),
(2, 'Finance'),
(3, 'IT'),
(4, 'Marketing');

-- Insert Employees
INSERT INTO Employees (FirstName, LastName, DepartmentID, Salary, JoinDate) VALUES
('John', 'Doe', 1, 5000.00, '2020-01-15'),
('Jane', 'Smith', 2, 6000.00, '2019-03-22'),
('Michael', 'Johnson', 3, 7000.00, '2018-07-30'),
('Emily', 'Davis', 4, 5500.00, '2021-11-05');

-- ===========================================================
-- Exercise 1: Stored Procedure to Get Employees by Department
-- ===========================================================

-- Step 1: Create Stored Procedure
CREATE PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT 
        E.EmployeeID,
        E.FirstName,
        E.LastName,
        D.DepartmentName,
        E.Salary,
        E.JoinDate
    FROM Employees E
    INNER JOIN Departments D ON E.DepartmentID = D.DepartmentID
    WHERE E.DepartmentID = @DepartmentID;
END;

-- To test:
-- EXEC sp_GetEmployeesByDepartment @DepartmentID = 3;

-- ===========================================================
-- Exercise 2: Stored Procedure to Insert a New Employee
-- ===========================================================

CREATE PROCEDURE sp_InsertEmployee
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @DepartmentID INT,
    @Salary DECIMAL(10,2),
    @JoinDate DATE
AS
BEGIN
    INSERT INTO Employees (FirstName, LastName, DepartmentID, Salary, JoinDate)
    VALUES (@FirstName, @LastName, @DepartmentID, @Salary, @JoinDate);
END;

-- To test:
-- EXEC sp_InsertEmployee 
--     @FirstName = 'Laura', 
--     @LastName = 'Wilson', 
--     @DepartmentID = 2, 
--     @Salary = 6200.00, 
--     @JoinDate = '2022-05-10';


-- ===========================================================
-- Exercise 5: Return Total Employees by Department
-- ===========================================================

-- Step 1: Create Stored Procedure
CREATE PROCEDURE sp_GetEmployeeCountByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT 
        COUNT(*) AS TotalEmployees
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;

-- Step 2: Test the Stored Procedure
-- EXEC sp_GetEmployeeCountByDepartment @DepartmentID = 2;


--- ===========================================================
-- Exercise 4: Execute Stored Procedure to Get Employees by Department
-- ===========================================================

-- Step 1: Create the stored procedure (if not already created)
CREATE PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT 
        E.EmployeeID,
        E.FirstName,
        E.LastName,
        D.DepartmentName,
        E.Salary,
        E.JoinDate
    FROM Employees E
    INNER JOIN Departments D ON E.DepartmentID = D.DepartmentID
    WHERE E.DepartmentID = @DepartmentID;
END;

-- Step 2: Execute the stored procedure with DepartmentID = 2
EXEC sp_GetEmployeesByDepartment @DepartmentID = 2;
