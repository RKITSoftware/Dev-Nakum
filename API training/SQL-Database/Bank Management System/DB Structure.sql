CREATE DATABASE BANK;
USE BANK;

-- create customer table 
CREATE TABLE
	CUS01(
    S01F01 INT COMMENT "Id" PRIMARY KEY AUTO_INCREMENT,
    S01F02 VARCHAR(20) COMMENT "First Name"	NOT NULL,
    S01F03 VARCHAR(20) COMMENT "Last Name" NOT NULL,
    S01F04 BIGINT COMMENT "Phone" UNIQUE NOT NULL,
    S01F05 VARCHAR(50) COMMENT "Address"
);


-- create account table 
CREATE TABLE
	ACC01(
    C01F01 INT COMMENT "Id" PRIMARY KEY AUTO_INCREMENT,
    C01F02 VARCHAR(10) COMMENT "Type - Saving or Current" NOT NULL,
    C01F03 INT COMMENT "Balance" NOT NULL,
    C01F04 INT COMMENT "Customer Id" REFERENCES CUS01(S01F01) 
);


-- create transaction table
CREATE TABLE
	TRA01(
    A01F01 INT COMMENT "Id" PRIMARY KEY AUTO_INCREMENT,
    A01F02 VARCHAR(10) COMMENT "Type - deposit or withdraw" NOT NULL,
    A01F03 DATETIME COMMENT "Date and Time" NOT NULL,
	A01F04 INT COMMENT "Money" NOT NULL,
	A01F05 INT COMMENT "Account Id" REFERENCES ACC01(C01F01) 
);