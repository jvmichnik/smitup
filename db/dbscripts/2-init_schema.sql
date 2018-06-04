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
  CONSTRAINT usuario_pk PRIMARY KEY (id)
);

CREATE TABLE account.customer
(
  id uuid NOT NULL,
  name character varying(30) NOT NULL,
  email character varying(200) NOT NULL,
  gender char(1) NOT NULL,
  birthday date NOT NULL,
  marital_status integer NOT NULL,
  user_id uuid NOT NULL,
  CONSTRAINT customer_pk PRIMARY KEY (id),
  CONSTRAINT user_fk FOREIGN KEY (user_id)
        REFERENCES account.user (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);