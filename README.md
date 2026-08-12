# fullstack-project-1
fullstack-project-1

  
### Database connection and using Environment Variables   
#### Environment Variables set up   
Put your real secret values in plain-text files here, one value per file, no
trailing newline needed:
``` 
  secrets/postgres_user.txt
  secrets/postgres_pass.txt
  secrets/postgres_db.txt
  secrets/pgadmin_email.txt
  secrets/pgadmin_pass.txt
``` 

ConnectionStrings example structure:  
```  
{
  "ConnectionStrings": {
    "PostgresDb": "Server=localhost;User Id=YOUR_USER;Password=YOUR_PASSWORD;Database=YOUR_DB;"
  }
}
```
  
#### Database data set up   
Database example structure:  
```   
YOUR_DB  
    users Table    
        id (PK) Field   
        fullname Field
```    
  
Create new table in database (from cmd):  
``` 
docker exec -i DOCKER-NAME psql -U USER -d DB-NAME < scripts/sql/users_table_create.sql
``` 

Add new data or delete data in the database (from cmd):  
``` 
docker exec -i DOCKER-NAME psql -U USER -d DB-NAME < scripts/sql/users_table_data_insert.sql

docker exec -i DOCKER-NAME psql -U USER -d DB-NAME < scripts/sql/users_table_delete.sql
``` 

View the data in the database (from cmd / you can also view the data on 'Login' page):  
``` 
$ docker exec -it DOCKER-NAME psql -U USER -d DB-NAME -c "\dt"

$ docker exec -it DOCKER-NAME psql -U USER -d DB-NAME -c "SELECT * from users"
``` 
  
  
### Repository - Branches Overview  
  
( feature-1 )  
( feature-999 )  
dev-xy  
main ("develop-branch")  
staging   
production     
  
  
### Repository - Currently enabled (security) settings in repository:
  
Advanced Security -- > Private vulnerability reporting (ON)  
Advanced Security -- > Secret Protection (ON)  
Advanced Security -- > Push protection (ON)  
Profile -- > Security -- > Code Security -- > User -- > Push protection for yourself (ON)  
  
