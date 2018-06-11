\connect smitupdb

CREATE SCHEMA event;

CREATE TABLE event.stored_event(
	"id" uuid NOT NULL,
	"aggregate_id" uuid NOT NULL,
	"data" varchar NULL,
	"action" varchar NULL,
	"creation_date" timestamp NOT NULL,
	"user" varchar NULL,
 	CONSTRAINT stored_event_pk PRIMARY KEY (id)
);