USE employee_dev;

-- count the rows
SELECT
	COUNT(DISTINCT P01F03) AS "Total Count" 
FROM
	EMP01
WHERE 
	P01F03 > 21;
    

-- som of the all salary
SELECT 
	SUM(P01F05) AS "Sum"
FROM 
	EMP01;
    
 -- sum with group by 
 SELECT
	P01F04 AS "Designation",
	SUM(P01F05) AS "Total Salary"
FROM
	EMP01
GROUP BY
	P01F04;
    
    
-- sum with group by and having
 SELECT
	P01F04 AS "Designation",
	SUM(P01F05) AS "Total Salary"
FROM
	EMP01
GROUP BY
	P01F04
HAVING 
	SUM(P01F05) > 35000;
    

-- average
SELECT 
	AVG(P01F05) 
AS "Average Salary"
FROM	
	EMP01;
    
    
-- MAX Salary
SELECT 
	MAX(P01F05) 
AS "Maximum Salary"
FROM	
	EMP01;
    
    
-- MIN Salary
SELECT 
	MIN(P01F05) 
AS "Minimum Salary"
FROM	
	EMP01;