-- select all the employee whose salary is more then average salary

SELECT 
	P01F01,
    P01F02,
	P01F03,
    P01F04,
    P01F05 
FROM 
	EMP01 
WHERE 
	P01F05 >= (
SELECT AVG(
	P01F05
) FROM 
	EMP01
);


-- to find students with 'A' grade
SELECT 
	U01F01,
    U01F02,
    U01F03 
FROM 
	STU01 
WHERE 
	U01F01 
IN (
SELECT 
	A01F04 
FROM 
	GRA01 
WHERE 
	A01F03='A'
);
