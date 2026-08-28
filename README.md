# fullstack-project-1
fullstack-project-1

## About the project    
This fullstack project is aiming to implement a Learning Management System website.      
It's a similar system to: Google Classroom, Moodle, Neptun (HU), Kréta (HU).    
The project's target group: Students and Teachers.    
  
With the help of this learning platform project, grades,
papers, calendar events can be written and viewed in digital form.    
The system allows you to assign different roles: Admin, Teacher, Student.    
  
    
## Used program and tech versions   
dbeaver-ce-26.1.4-windows-x86_64    

dotnet-sdk-10.0.302-win-x64    
(or at least dotnet sdk 9.0)      
   
Git-2.53.0.2-64-bit  
  
(optional)    
npp.8.0.    

postgresql-18.4-2-windows-x64   
  
(Optional)     
VS2022 (vs_Community)                  
(Version 17.14.29, March 2026)      

VSCodeUserSetup-x64-1.58.2      

(Optional)    
list of vs code extensions ...          
 
Docker Desktop Installer    
(Version 4.60.0)        
  

Docker Image: registry:latest     
Manifest Digest: @sha256:1be55279f18a2fe1a74edf2664cac61c1bea305b7b4642dab412e7affdcb3e33         

Docker Image: alpine/socat:latest  
Manifest Digest: @sha256:68b28fed1e6f5dc5cc7574315284573d89a655b0bc528720be4164f36b998680    


Docker Image: postgres:18.4-alpine     
Manifest Digest: @sha256:bbf8ecc4093dbbaacfc9d62548ca728d33d6d5a6ad236017b6c611e32b5d7cfb
    

Docker Image: dpage/pgadmin4:9.16        
Manifest Digest: @sha256:66a300a7ecdcc1f325af0c430315329bca46cd4a7067227d6899802238167c6e    
  

Jenkins     
(Version 2.547)    
Manifest Digest: @sha256:b5474f70630b148f5ebcf5c98f502f42647a6d5f6ad267fb095cdf2a43f6cfbf    


dotnet-agent version,              
alpine-agent version: jenkins/agent:alpine3.24-jdk25     
Manifest Digest: @sha256:4451714a4d5f190791689575cf2cb280ae89bd331105f30278a2a0fb54f59732
           
  
## Installation set up, steps    
Download and Install the required Programs and tech.     

Install and set up 'postgresql-18.4-2-windows' with    
database superuser (postgres).       

After installing Docker, update and repair WSL (if required).      

Install and set up the required Docker Images.     
  

### Installation steps        
Clone the repository.    

Install and set up the required Docker Images:       
- registry:latest@sha256:1be55279f18a2fe1a74edf2664cac61c1bea305b7b4642dab412e7affdcb3e33
- alpine/socat:1.8.1.3@sha256:68b28fed1e6f5dc5cc7574315284573d89a655b0bc528720be4164f36b998680
- jenkins/jenkins:2.547-jdk25@sha256:b5474f70630b148f5ebcf5c98f502f42647a6d5f6ad267fb095cdf2a43f6cfbf
- dpage/pgadmin4:9.16@sha256:66a300a7ecdcc1f325af0c430315329bca46cd4a7067227d6899802238167c6e
- postgres:18.4-alpine@sha256:bbf8ecc4093dbbaacfc9d62548ca728d33d6d5a6ad236017b6c611e32b5d7cfb
- jenkins/agent:alpine3.24-jdk25@sha256:4451714a4d5f190791689575cf2cb280ae89bd331105f30278a2a0fb54f59732
- jenkins/agent:alpine3.24-jdk25@sha256:4451714a4d5f190791689575cf2cb280ae89bd331105f30278a2a0fb54f59732
  
  
Dockerfile and docker-compose files to help with installation:   
- fullstack-project-1\docker-compose.yaml     
and fullstack-project-1\Dockerfile    
    
- fullstack-project-1\jenkins\docker-compose.yaml    
and fullstack-project-1\jenkins\Dockerfile     

- fullstack-project-1\jenkins\docker-agent-dotnet\Dockerfile   
   

Use the command     
(after using 'cd' to move into the directory):    
```
docker-compose up --build   
```
  
    
If an error occurs after using the docker-compose command     
like 'Error dependency postgres failed to start',    
do the following:    
-- open DBeaver           
-- create a Database connection     
-- open pgAdmin4     
-- connect to Database     

-- rerun the 'docker-compose up --build' command     
(don't forget to delete the previously created Docker Containers)           
  
-- If you did not create the txt files yet, then create them now:     
Put your real secret values in plain-text files here, one value per file, no     
trailing newline needed:    
``` 
  secrets/postgres_user.txt
  secrets/postgres_pass.txt
  secrets/postgres_db.txt
  secrets/pgadmin_email.txt
  secrets/pgadmin_pass.txt
``` 
  
-- rerun the 'docker-compose up --build' command     
(don't forget to delete the previously created Docker Containers)           
  
  
If you get errors like    
' exec ./entrypoint.sh: no such file or directory ' and or   
' /pgadmin-entrypoint.sh: set: line 2: illegal option - '   
do the following:    

-- convert line endings to LF (Unix) format.    
You can use Git Bash for this:       
```
file entrypoint.sh

dos2unix entrypoint.sh
dos2unix pgadmin-entrypoint.sh
```


After succesfully troubleshooting, open 
the website at:
```   
http://IPADDRESS:PORT/
```
  
  
To initialize the database with some data,    
do the following:   
  
-- Create new table in database (from cmd):  
``` 
docker exec -i DOCKER-NAME psql -U USER -d DB-NAME < scripts/sql/users_table_create.sql
``` 
  
-- Add new data or delete data in the database (from cmd):  
``` 
docker exec -i DOCKER-NAME psql -U USER -d DB-NAME < scripts/sql/users_table_data_insert.sql
``` 
  
    
Currently you can view the database contents on the 'Login' page.     
  
    
Jenkins     
Installation steps and usage:          
[jenkins/README.md](jenkins/README.md)      
  
  

## Database connection and using Environment Variables   
### Environment Variables set up   
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
  
## Database data set up   
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


## Jenkins project related description      
[jenkins/README.md](jenkins/README.md)

    
## Repository - Branches Overview  
  
( feature-1 )  
( feature-999 )  
dev-xy  
main ("develop-branch")  
staging   
production     
  
  
## Repository - Currently enabled (security) settings in repository:
  
Advanced Security -- > Private vulnerability reporting (ON)  
Advanced Security -- > Secret Protection (ON)  
Advanced Security -- > Push protection (ON)  
Profile -- > Security -- > Code Security -- > User -- > Push protection for yourself (ON)  
  
