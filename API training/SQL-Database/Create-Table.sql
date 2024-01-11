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

-- insert the data into table
INSERT INTO EMP01(
	P01F02,
	P01F03,
    P01F04
) VALUES(
	"Dev",
    21,
    "SDE"
);

INSERT INTO EMP02(
	P01F02,
	P01F03,
    P01F04
) VALUES(
	"Dev",
    21,
    "SDE"
);

-- truncate
TRUNCATE TABLE 
	EMP02;

-- drop table 
DROP TABLE 
	EMP02;



-- Insert more than one data at a time
INSERT INTO EMP01(
	P01F02,
    P01F03,
    P01F04
)VALUES
	("Raj",21,"SDE"),
    ("Tushar",22,"UI/UX");
    

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


-- for sub queries and joins
CREATE TABLE 
	STU01(
    U01F01 INT COMMENT "Id" PRIMARY KEY AUTO_INCREMENT,
    U01F02 VARCHAR(20) COMMENT "Name",
    U01F03 INT COMMENT "Age"
);

CREATE TABLE 
	GRA01(
	A01F01 INT COMMENT "Id" PRIMARY KEY AUTO_INCREMENT,
    A01F02 VARCHAR(20) COMMENT "Subject Name",
    A01F03 CHAR(1) COMMENT "Grade",
    A01F04 INT COMMENT "Student Id",
    FOREIGN KEY (A01F04) REFERENCES STU01(U01F01)
);