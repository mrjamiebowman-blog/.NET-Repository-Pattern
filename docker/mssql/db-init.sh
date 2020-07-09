# wait for the SQL Server to come up
sleep 25s

echo "[+] Running SQL Setup Script"

# run the setup script to create the DB and the schema in the DB
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P NyLct4D@7K{s -d master -i db-init.sql