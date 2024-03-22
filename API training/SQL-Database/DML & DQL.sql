-- DML statements are used to manage and manipulate data stored in the database.
    
    
-- INSERT 
INSERT INTO EMP01(
	P01F02,
	P01F03,
    P01F04,
    P01F05
) VALUES(
	"Kishan",
    26,
    "Engineer",
    50000
);


-- UPDATE
UPDATE
	EMP01
SET
	P01F05 = 10000
WHERE 
	P01F01 = 1;
    
    
-- DELETE
DELETE FROM
	EMP01
WHERE P01F01 = 6;


-- SELECT 
SELECT 
	P01F01,
	P01F02,
    P01F03,
    P01F04,
    P01F05
FROM 
	EMP01 
WHERE
	P01F03 > 21;
    