-- Transaction control language
USE employee;

-- Creating a sample table
CREATE TABLE 
	EMP03 (
    P03F01 INT COMMENT "Id" PRIMARY KEY,
    P03F02 VARCHAR(255) COMMENT "Value"
);

-- Start a transaction
BEGIN;

-- Inserting data into the table (within the transaction)
INSERT INTO 
	EMP03 (
    P03F01, 
    P03F02
) VALUES (
	1, 
    'Transaction Demo'
);

-- Commit the transaction
COMMIT;

BEGIN;
-- Inserting data into the table (after the transaction)
INSERT INTO 
	EMP03 (
    P03F01, 
    P03F02
) VALUES (
	3, 
    'after Transaction Demo'
);

ROLLBACK;
