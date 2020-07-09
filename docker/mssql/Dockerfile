FROM mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-16.04
COPY . /
USER root
RUN chmod +x /db-init.sh
USER mssql
CMD /bin/bash ./entrypoint.sh