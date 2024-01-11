-- data control language

-- create the new user
CREATE USER 	
	'dev@localhost' 
IDENTIFIED BY 
	'root';

-- grant the permission to specific user 
GRANT 
	SELECT,
    UPDATE,
    DELETE,
    INSERT
ON 
	employee.* 
TO 
	'dev@localhost';
    
-- revoke the permission
REVOKE 
	UPDATE,
    DELETE,
    INSERT
ON 
	employee.* 
FROM 
	'dev@localhost';
    





