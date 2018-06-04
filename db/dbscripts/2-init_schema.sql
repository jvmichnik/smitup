CREATE DATABASE smitupdb
    WITH 
    OWNER = smitup
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1;
    
\connect smitupdb

CREATE SCHEMA account;

CREATE TABLE account.user
(
  id uuid NOT NULL,
  username character varying(30) NOT NULL,
  password character varying(70) NOT NULL,
  CONSTRAINT pk_usuario PRIMARY KEY (id)
);

CREATE TABLE account.customer
(
  id uuid NOT NULL,
  name character varying(30) NOT NULL,
  email character varying(200) NOT NULL,
  gender char(1) NOT NULL,
  birthday date NOT NULL,
  maritalstatus integer NOT NULL,
  userid uuid NOT NULL,
  CONSTRAINT pk_customer PRIMARY KEY (id),
  CONSTRAINT fk_user FOREIGN KEY (userid)
        REFERENCES account.user (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);