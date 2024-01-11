CREATE TABLE EMP01(
	P01F01 int comment "Id" primary key auto_increment,
    P01F02 varchar(50) comment "Name",
    P01F03 int comment "Age",
    P01F04 varchar(30) comment "Designation"
);

CREATE TABLE EMP02(
	P01F01 int comment "Id" primary key auto_increment,
    P01F02 varchar(50) comment "Name",
    P01F03 int comment "Age",
    P01F04 varchar(30) comment "Designation"
);


-- truncate
TRUNCATE TABLE 
	EMP02;


-- drop table 
DROP TABLE
	EMP02;

    
-- add column in table 
ALTER TABLE 
	EMP01 
ADD COLUMN 
	P01F05 INT COMMENT "Salary";


-- rename table name
ALTER TABLE 
	EMP02
RENAME TO 
	EMP01;

