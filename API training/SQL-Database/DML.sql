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
	P01F05 = 35000
WHERE 
	P01F01 = 3;
    
    
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


INSERT INTO STU01 (U01F02,U01F03) VALUES
('Dev Nakum', 25),
('Raj', 25);	

INSERT INTO GRA01 (A01F04, A01F02, A01F03) VALUES
(3, 'Math', 'C'),
(3, 'English', 'A'),
(4, 'Math', 'C'),
(4, 'English', 'B'),
(5, 'Math', 'A'),
(5, 'English', 'D');
