FROM postgres:latest
COPY dbscripts/1-init.sql /docker-entrypoint-initdb.d/1-init.sql
COPY dbscripts/2-account_schema.sql /docker-entrypoint-initdb.d/2-account_schema.sql
COPY dbscripts/3-storedevent_schema.sql /docker-entrypoint-initdb.d/3-storedevent_schema.sql
EXPOSE 5432