use employee_dev;

-- Create the view 
CREATE OR REPLACE VIEW
	vws_employee
AS SELECT 
	P01F01,
    P01F02,
    P01F04
FROM
	EMP01;

# Display view 
SELECT 
	P01F01,
    P01F02,
    P01F04
FROM
	vws_employee;
    
    
# update the view
UPDATE 
	vws_employee
SET
	P01F02 = CONCAT("SUPER ", P01F02)
WHERE
	P01F02 LIKE "A%";
    

# view with joins 
CREATE OR REPLACE VIEW 
	vws_emp_depart
AS SELECT 
	P02F01,
    P02F02,
    P01F02 
FROM 
	EMP02 
LEFT JOIN 
	DEP01 
ON 
	EMP02.P02F03 = DEP01.P01F01;
    
# DISPLAY VIEW WITH JOINS
SELECT 
	P02F01,
    P02F02,
    P01F02 
FROM 
	vws_emp_depart; 

# update the base table
UPDATE 
	EMP02
SET
	P02F02 = "Dev"
WHERE 
	P02F01 = 5;
    

#delete the view 
DROP VIEW 
	VIEW_EMP02_DEP01;