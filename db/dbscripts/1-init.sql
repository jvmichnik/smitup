CREATE USER smitup WITH
	LOGIN
	SUPERUSER
	CREATEDB
	CREATEROLE
	INHERIT
	NOREPLICATION
	CONNECTION LIMIT -1
	PASSWORD '1231234';
	
CREATE DATABASE smitupdb
    WITH 
    OWNER = smitup
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1;