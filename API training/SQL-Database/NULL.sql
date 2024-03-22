-- inserting NULL value
INSERT INTO EMP01 (
	P01F02,
    P01F03,
    P01F04
) VALUES(
    "Test",
    18,
    "Temp"
);

-- comparision with NULL
SELECT 
	P01F01,
    P01F02,
    P01F03,
    P01F04,
    P01F05
FROM 
	EMP01
WHERE
	P01F05 IS NULL;
    
    
-- Coalescing NULL
SELECT 
	P01F01,
    P01F02,
    P01F03,
    P01F04,
    COALESCE(P01F05,'N/A') AS P01F05
FROM 
	EMP01;