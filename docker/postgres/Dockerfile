FROM postgres
COPY . /docker-entrypoint-initdb.d/
RUN chmod +x /docker-entrypoint-initdb.d/db-init.sh