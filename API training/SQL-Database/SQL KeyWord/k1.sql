-- create database, database
CREATE DATABASE DEV372_KEYWORD;

-- create table, primary key
CREATE TABLE employee(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(20),
    Age INT,
    Salary INT
);

CREATE TABLE employee2(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(20),
    Age INT,
    Salary INT
);

-- insert 
INSERT INTO employee (
	Name,
	Age,
	Salary
) VALUES(
	"Raj",
	21,
	10000
);


-- copies the data from one table to another table
INSERT INTO employee2 (
    SELECT 
        * 
    FROM 
        employee
);

-- select 
SELECT 
    Id, Name, Age, Salary
FROM
    employee;
    

-- select top
SELECT 
    Id, Name, Age, Salary
FROM
    employee
LIMIT 
	2;


DROP TABLE employee2;
TRUNCATE TABLE employee2;

-- order by
SELECT 
    Id , Name, Age, Salary, Designation
FROM
    employee    
WHERE
    Designation IS NULL
ORDER BY Name DESC;


DELETE FROM
    employee
WHERE 
    Id = 4;

-- add column into existing table
ALTER TABLE 
    employee
ADD COLUMN
    Designation2 VARCHAR(20);

-- remove column from the existing table 
ALTER TABLE 	
	employee
DROP COLUMN
	Designation2;


-- rename column into table 
ALTER TABLE 
    employee
RENAME COLUMN
    Position
to 
    Designation;


-- add constraint of primary key
ALTER TABLE 
    employee
ADD CONSTRAINT
    pk_employee
PRIMARY KEY 
    (Id);
    
    
-- create table
CREATE TABLE department(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(20) UNIQUE
);

    
SELECT 
    Id, Name, Age, Salary, Designation
FROM
    employee
WHERE
    EXISTS( SELECT 
            Id
        FROM
            employee
        WHERE
            Age < 22);
    
    
-- alter the data type of column
ALTER TABLE 
	employee	
MODIFY COLUMN
	Designation varchar(20);


-- insert the row into department table
INSERT INTO department (Name) VALUES("QA");


SELECT 
    e.Id, e.Name, e.Age, e.Designation, d.Name
FROM
    employee e
        INNER JOIN
    department d ON e.Department = d.Id;


SELECT 
    SUM(Salary) AS 'Total Salary', Designation
FROM
    employee
GROUP BY 
	Designation
HAVING
	Designation 
LIKE
	'%a%';



UPDATE 
    employee 
SET 
    Designation = 'Manager' 
WHERE 
    Id = 1; 
    

SELECT 
    *
FROM
    employee
WHERE
    Department = ANY (SELECT 
            Id
        FROM
            department
        WHERE
            Name = 'Development');
            
    
SELECT 
    Id,
    Name,
    Age,
    Salary,
    Designation,
    CASE
        WHEN Age > 25 THEN 'Age is greater than 25'
        WHEN Age < 25 THEN 'Age is less than 25'
    END AS 'Comment on Age'
FROM
    employee;
    

-- add constraint to age
ALTER TABLE 
	employee
ADD CONSTRAINT 
	check_age
CHECK
	(Age>=18);
    
-- drop constraint
ALTER TABLE
	employee
DROP CONSTRAINT
	check_age;
    
    
-- like , search
SELECT 
    Id, Name, Age, Salary, Designation
FROM
    employee
WHERE
    Name LIKE '%a%';
    

-- index
CREATE INDEX
	idx_name
ON 
	employee(Name);
    
-- unique index
CREATE UNIQUE INDEX
	idx_unq_id
ON 
	employee(Id);
    
-- drop index
DROP INDEX 
	idx_unq_id 
ON 
	employee;
    

-- view
CREATE VIEW All_Developer AS
    SELECT 
        *
    FROM
        employee
    WHERE
        Designation = 'Developer';
        
        
-- drop view
DROP VIEW 
	All_Developer;
    
    
-- set default
ALTER TABLE 
	employee
ALTER 
	Designation 
SET DEFAULT 
	'Developer';
    

-- drop default
ALTER TABLE 
	employee
ALTER 
	Designation 
DROP DEFAULT;


-- union
SELECT 
    Id, Name, Salary, Designation, Department
FROM
    employee 
UNION ALL SELECT 
    Id, Name, Salary, Designation, Department
FROM
    employee2;
    
    
    
-- outer join
SELECT 
    *
FROM
    employee e
        LEFT JOIN
    department d ON e.Department = d.Id 
UNION SELECT 
    *
FROM
    employee e
        RIGHT JOIN
    department d ON e.Department = d.Id;
    
    
ALTER TABLE
	employee
ADD FOREIGN KEY
	(Department)
REFERENCES	
	department (Id);