-- without index

SELECT 
	P01F01,
    P01F02,
    P01F03,
    P01F04,
    P01F05
FROM 
	EMP01
WHERE 
	P01F04 = "SDE";
    
-- create index 
CREATE INDEX
	idx_P01F04
ON
	EMP01(P01F04);
    
-- after creating index
SELECT 
	P01F01,
    P01F02,
    P01F03,
    P01F04,
    P01F05
FROM 
	EMP01
WHERE 
	P01F04 = "SDE";
    
    
-- create multi-index
CREATE INDEX
	idx_P01_F01_F04
ON
	EMP01(P01F01,P01F04);
    
-- Drop index
ALTER TABLE 
	EMP01
DROP INDEX
	idx_P01_F01_F04;