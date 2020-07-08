FROM mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-16.04
USER root
COPY . /
RUN chmod +x /db-init.sh
USER mssql
CMD /bin/bash ./entrypoint.sh