-- Exercise 1: Ranking and Window Functions

-- 1. Using ROW_NUMBER()
WITH RankedProducts AS (
    SELECT 
        *,
        ROW_NUMBER() OVER(PARTITION BY Category ORDER BY Price DESC) AS RowNum
    FROM Products
)
SELECT * FROM RankedProducts WHERE RowNum <= 3;

-- 2. Using RANK()
WITH RankedProducts AS (
    SELECT 
        *,
        RANK() OVER(PARTITION BY Category ORDER BY Price DESC) AS RankNum
    FROM Products
)
SELECT * FROM RankedProducts WHERE RankNum <= 3;

-- 3. Using DENSE_RANK()
WITH RankedProducts AS (
    SELECT 
        *,
        DENSE_RANK() OVER(PARTITION BY Category ORDER BY Price DESC) AS DenseRankNum
    FROM Products
)
SELECT * FROM RankedProducts WHERE DenseRankNum <= 3;
