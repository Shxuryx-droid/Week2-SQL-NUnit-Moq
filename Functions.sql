-- ===========================================================
-- Exercise 7: Scalar Function - Calculate Annual Salary
-- ===========================================================

-- Step 1: Create Scalar Function
CREATE FUNCTION fn_CalculateAnnualSalary
(
    @EmployeeID INT
)
RETURNS DECIMAL(12,2)
AS
BEGIN
    DECLARE @AnnualSalary DECIMAL(12,2);

    SELECT @AnnualSalary = Salary * 12
    FROM Employees
    WHERE EmployeeID = @EmployeeID;

    RETURN @AnnualSalary;
END;

-- Step 2: Test the Function
-- Example: Calculate annual salary of employee with ID 1
-- SELECT dbo.fn_CalculateAnnualSalary(1) AS AnnualSalary;
