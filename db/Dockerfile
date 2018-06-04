FROM postgres:latest
COPY dbscripts/1-init.sql /docker-entrypoint-initdb.d/1-init.sql
COPY dbscripts/2-init_schema.sql /docker-entrypoint-initdb.d/2-init_schema.sql
EXPOSE 5432