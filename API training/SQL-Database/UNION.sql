-- union - display only distinct data 
SELECT 
	P01F02 
FROM 
	EMP01
UNION
SELECT 
	P02F02 
FROM 
	EMP02;
    
    
-- unionall - display all the data 
SELECT 
	P01F02 
FROM 
	EMP01
UNION ALL
SELECT 
	P02F02 
FROM 
	EMP02;
    
    
    
-- union - display only distinct data 
SELECT 
	P01F02 
FROM 
	EMP01
UNION
SELECT 
	P02F02 
FROM 
	EMP02
UNION ALL
SELECT 
	P02F02 
FROM 
	EMP02;
    
