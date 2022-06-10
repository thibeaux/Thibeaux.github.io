CREATE TABLE EmployeeCredentials
(
	[Id] INT NOT NULL PRIMARY KEY,
	username char(25) not null ,
	password char(32) not null,
	role char(15) not null,
	firstName char(20),
	lastname char(20),
)
