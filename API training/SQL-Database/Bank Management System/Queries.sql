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
	"D",
    NOW(),
    2050,
    5
);

UPDATE 
	ACC01
SET
	C01F03 = C01F03 + 2050
WHERE 
	C01F01 = 6;
    
    
-- make Withdraw of money account id 6
INSERT INTO 
	TRA01 (
    A01F02,
    A01F03,
    A01F04,
    A01F05
) VALUES (
	"W",
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
CREATE OR REPLACE VIEW vws_ACS01 AS SELECT 
	S01.S01F01, 
	S01.S01F02, 
	S01.S01F03, 
	S01.S01F04,
    C01.C01F02,
    C01.C01F03,
    A01.A01F02,
    A01.A01F03,
    A01.A01F04
FROM 
	TRA01 A01
JOIN 
	ACC01 C01
ON 
	A01.A01F05 = C01.C01F01
JOIN 
	CUS01 S01
ON 
	C01.C01F04 = S01.S01F01
LIMIT 2;
    
    
-- display the view
SELECT * FROM vws_ACS01;



-- update customer information OF Id 1
UPDATE 
	CUS01
SET
	S01F04 = 7046777357
WHERE 
	S01F01 = 1;
	

-- Retrieve accounts with no transactions
SELECT
	S01.S01F02,
    S01.S01F03,
    S01.S01F04,
	C01.C01F02,
	C01.C01F03
FROM	
	ACC01 C01
LEFT JOIN
	TRA01 A01
ON 
	A01.A01F05 = C01.C01F01
JOIN 
	CUS01 S01
ON 
	C01.C01F04 = S01.S01F01
WHERE 	
	A01.A01F01 IS NULL
ORDER BY
	C01.C01F03;


-- retrieve customer name with total balance 
SELECT 
	S01.S01F01,
	S01.S01F02,
	S01.S01F03,
	S01.S01F04,
    C01.C01F02,
    C01.C01F03
FROM
	ACC01 C01
JOIN
	CUS01 S01
ON
	C01.C01F04 = S01.S01F01;
    
-- retrieve total deposit and withdraw - group_concat
SELECT 
    (CASE
        WHEN 
			A01F02 = 'D' 
		THEN 
			'Deposit'
        ELSE 
			'Withdraw'
    END) AS TYPE,
    GROUP_CONCAT(A01F04
        SEPARATOR ' + ') AS EXPRESSION,
    SUM(A01F04) AS 'TOTAL SUM'
FROM
    TRA01
GROUP BY 
	A01F02;
    
    
    
-- display total withdraw and deposit, group by account
SELECT 
    A01F05 
AS 
	AccountID,
    SUM(CASE WHEN A01F02 = 'D' THEN A01F04 ELSE 0 END) AS TotalDeposit,
    SUM(CASE WHEN A01F02 = 'W' THEN A01F04 ELSE 0 END) AS TotalWithdrawal,
    SUM(CASE WHEN A01F02 = 'D' THEN A01F04 ELSE -A01F04 END) AS TotalMoney
FROM 
	TRA01
GROUP BY 
	A01F05;
