 docker stop sqlserver

 docker rm sqlserver
 
 docker run -d -p 1433:1433 -v d:/Projects/Data/sqlserver:/var/opt/mssql/data --restart always --name sqlserver --hostname sqlserver -e ACCEPT_EULA=Y -e SA_PASSWORD=Test123456 mcr.microsoft.com/mssql/server