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
  normalized_username character varying(30) NULL,
  email character varying(256) NOT NULL,
  email_confirmed boolean NOT NULL,
  normalized_email character varying(256) NULL,
  password_hash varchar NOT NULL,
  phone_number character varying(20) NULL,
  phone_number_confirmed boolean NULL,
  access_failed_count integer NOT NULL,
  concurrency_stamp varchar NULL,
  lockout_enabled boolean NOT NULL,
  lockout_end timestamp NULL,
  security_stamp varchar NULL,
  two_factor_enabled boolean not null,
  CONSTRAINT user_pk PRIMARY KEY (id)
);

CREATE TABLE account.user_tokens
(
  user_id uuid NOT NULL,
  login_provider varchar NOT NULL,
  name varchar NOT NULL,
  value varchar NULL,
  CONSTRAINT user_tokens_pk PRIMARY KEY (login_provider,name,user_id)
);

CREATE TABLE account.role
(
  id uuid NOT NULL,
  concurrency_stamp varchar NULL,
  name character varying(256) NULL,
  normalized_name character varying(256) NULL,
  CONSTRAINT role_pk PRIMARY KEY (id)
);

CREATE TABLE account.user_roles
(
  user_id uuid NOT NULL,
  role_id uuid NOT NULL,
  CONSTRAINT user_roles_pk PRIMARY KEY(user_id, role_id),
  CONSTRAINT user_roles_user_userid_fk FOREIGN KEY (user_id)
        REFERENCES account.user (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
  CONSTRAINT user_roles_role_roleid_fk FOREIGN KEY (role_id)
        REFERENCES account.role (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

CREATE TABLE account.user_login
(
  login_provider uuid NOT NULL,
  provider_key uuid NOT NULL,
  provider_display_name varchar null,
  user_id uuid not null,
  CONSTRAINT user_login_pk PRIMARY KEY(login_provider, provider_key),
  CONSTRAINT user_login_user_userid_fk FOREIGN KEY (user_id)
        REFERENCES account.user (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

CREATE TABLE account.role_claims
(
  id integer NOT NULL,
  claim_type varchar NULL,
  claim_value varchar null,
  role_id uuid not null,
  CONSTRAINT role_claims_pk PRIMARY KEY(id),
  CONSTRAINT role_claims_roleid_fk FOREIGN KEY (role_id)
        REFERENCES account.role (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

CREATE TABLE account.user_claims
(
  id integer NOT NULL,
  claim_type varchar NULL,
  claim_value varchar null,
  user_id uuid not null,
  CONSTRAINT user_claims_pk PRIMARY KEY(id),
  CONSTRAINT user_claims_userid_fk FOREIGN KEY (user_id)
        REFERENCES account.user (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

CREATE TABLE account.customer
(
  id uuid NOT NULL,
  name character varying(30) NOT NULL,
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