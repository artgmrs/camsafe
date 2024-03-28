# wait for the SQL Server to come up
sleep 15s

echo "Creating Database"
/opt/mssql-tools/bin/sqlcmd -S sqldata -U sa -P Password@1 -d master -i /tmp/01-DDL_Create_Database.sql

echo "Configuring Tables"
/opt/mssql-tools/bin/sqlcmd -S sqldata -U sa -P Password@1 -d master -i /tmp/02-DDL_Config_Tables_CamSafeDB.sql

echo "Inserting Mock Data"
/opt/mssql-tools/bin/sqlcmd -S sqldata -U sa -P Password@1 -d master -i /tmp/03-Insert_Mock_data.sql
