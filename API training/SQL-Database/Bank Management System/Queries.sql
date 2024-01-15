-- retrieve customer information 
SELECT
	S01F01,
	S01F02,
	S01F03,
	S01F04,
	S01F05
FROM
	CUS01;
    
    
-- retrieve account information 
SELECT
	C01F01,
	C01F02,
	C01F03,
	C01F04
FROM
	ACC01;
    
-- retrive account information with customer information
SELECT 
	S01F01,
	S01F02,
	S01F03,
	S01F04,
	S01F05,
    C01F02,
	C01F03
FROM
	ACC01
JOIN
	CUS01
ON
	ACC01.C01F04 = CUS01.S01F01;
    
    
-- make deposit of money account id 1
INSERT INTO 
	TRA01 (
    A01F02,
    A01F03,
    A01F04,
    A01F05
) VALUES (
	"Deposit",
    NOW(),
    150,
    1
);

UPDATE 
	ACC01
SET
	C01F03 = C01F03 + 150
WHERE 
	C01F01 = 1;
    
    
-- make Withdraw of money account id 6
INSERT INTO 
	TRA01 (
    A01F02,
    A01F03,
    A01F04,
    A01F05
) VALUES (
	"Withdraw",
    NOW(),
    1500,
    6
);

UPDATE 
	ACC01
SET
	C01F03 = C01F03 - 1500
WHERE 
	C01F01 = 6;
    

-- retrive all transaction information with customer details

SELECT 
	C.S01F01, 
	C.S01F02, 
	C.S01F03, 
	C.S01F04,
    A.C01F02,
    A.C01F03,
    T.A01F02,
    T.A01F03,
    T.A01F04
FROM 
	TRA01 T
JOIN 
	ACC01 A
ON 
	T.A01F05 = A.C01F01
JOIN 
	CUS01 C
ON 
	A.C01F04 = C.S01F01;
    

-- display total withdraw and deposit, group by account
SELECT 
    A01F05 
AS 
	AccountID,
    SUM(CASE WHEN A01F02 = 'Deposit' THEN A01F04 ELSE 0 END) AS TotalDeposit,
    SUM(CASE WHEN A01F02 = 'Withdraw' THEN A01F04 ELSE 0 END) AS TotalWithdrawal
FROM 
	TRA01
GROUP BY 
	A01F05;


-- update customer information OF Id 1
UPDATE 
	CUS01
SET
	S01F04 = 7046777357
WHERE 
	S01F01 = 1;
	

-- Retrieve accounts with no transactions
SELECT
	C.S01F02,
    C.S01F03,
    C.S01F04,
	A.C01F02,
	A.C01F03
FROM	
	ACC01 A
LEFT JOIN
	TRA01 T
ON 
	T.A01F05 = A.C01F01
JOIN 
	CUS01 C
ON 
	A.C01F04 = C.S01F01
WHERE 	
	T.A01F01 IS NULL;


-- retrieve customer name with total balance 
SELECT 
	C.S01F01,
	C.S01F02,
	C.S01F03,
	C.S01F04,
    A.C01F02,
    A.C01F03
FROM
	ACC01 A
JOIN
	CUS01 C
ON
	A.C01F04 = C.S01F01;
    