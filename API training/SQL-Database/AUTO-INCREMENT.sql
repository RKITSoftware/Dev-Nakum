CREATE TABLE
	EMP02(
	P02F01 INT COMMENT "iD" PRIMARY KEY AUTO_INCREMENT,
    P02F02 VARCHAR(20) COMMENT "Name"
)
AUTO_INCREMENT = 101;

INSERT INTO EMP02(
	P02F02
)VALUES(
	"Pratham"
);

ALTER TABLE
	EMP02
AUTO_INCREMENT = 501;