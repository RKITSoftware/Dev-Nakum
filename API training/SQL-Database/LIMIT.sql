SELECT 
	P01F01,
    P01F02,
    P01F03,
    P01F04,
    P01F05
FROM 
	EMP01;
    
-- limit based on salary - desc
SELECT 
	P01F01,
    P01F02,
    P01F03,
    P01F04,
    P01F05
FROM 
	EMP01
ORDER BY 
	P01F05 
DESC
LIMIT 2;

  
-- limit ofset based on salary skip 2 rows
SELECT 
	P01F01,
    P01F02,
    P01F03,
    P01F04,
    P01F05
FROM 
	EMP01
ORDER BY 
	P01F05 
LIMIT 2,2;			# OFFSET, LIMIT
