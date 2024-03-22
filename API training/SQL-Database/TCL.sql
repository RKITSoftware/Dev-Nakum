-- Transaction control language
USE employee_dev;

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
	3, 
    'Transaction Demo 2'
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
	4, 
    'after Transaction Demo'
);

ROLLBACK;



BEGIN;
-- Inserting data into the table (after the transaction)
INSERT INTO 
	EMP03 (
    P03F01, 
    P03F02
) VALUES (
	4, 
    'before save point Transaction Demo'
);

SAVEPOINT SVE_PT;

INSERT INTO 
	EMP03 (
    P03F01, 
    P03F02
) VALUES (
	5, 
    'after save point Transaction Demo'
);

ROLLBACK TO SVE_PT;
