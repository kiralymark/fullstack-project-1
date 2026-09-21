# fullstack-project-1
fullstack-project-1


Jenkins project related description  
  
-- Setup and Installation    
(+ commands)

-- -- steps, run commands:  
```
docker-compose up --build   
```

-- -- Follow the instructions on screen, set up Jenkins  

-- -- Set up a local user account   
  
  
Jenkins settings:  
01 To set all language to english:  
- Manage Jenkins -- > Plugins -- > Available plugins -- > locale plugin
- Click INSTALL
- Manage Jenkins -- > Appearance -- > Default Language
- Select ' English - en '
- Tick the checkbox ' Ignore browser preference and force this language to all users '
  
02 To set dark mode:  
- Manage Jenkins -- > Appearance
- Tick the checkbox ' Do not allow users to select a different theme '
  
  
(extra settings)  
To update an already existing docker container afterwards, and 
set it's restart parameter:  
```
docker update --restart=no <container_id> 
```
  
  
-- To create a new Freestyle project, my_job_01:  
-- -- Click on ' New Item '  
name: --    
item type:  Freestyle project 
source code management -- > none: checkbox ticked  
build environment -- > delete workspace before build starts: checkbox ticked  
build steps -- > execute shell:   
```
echo "Hello World!"
echo "Job build ID ${BUILD_ID}"
echo "Job build URL ${BUILD_URL}"
```
-- Run My Job (my_job_01):   
-- -- Click on ' Build Now '    
  

-- To create a new Freestyle project and run it on an agent:   
(+ commands)   

-- -- steps, run commands:    
01-1 Install Docker plugin (To configure a local agent):   
- Manage Jenkins -- > Plugins -- > Available plugins -- > Docker plugin   
- Click INSTALL
  
01-2 To configure a local agent (using local Cloud Platforms):   
- Manage Jenkins -- > Clouds -- > New Cloud
- New Cloud
- Cloud name: docker
- Tick the checkbox 'Docker'   
- Next, look at the section below 'Setup steps for Alpine socat connection'. 
- Docker Host URI: tcp://IPADDRESS:PORT    
- Tick the checkbox: Enabled
- Check the connection with the "Test Connection" button
- Press the "Save" button
  
01-3 To configure a local docker agent template (docker-agent-alpine):    
- Manage Jenkins -- > Clouds -- > docker
- Configure
- Docker Agent templates -- > Add Docker Template     
- labels: docker-agent-alpine 
- Tick the checkbox: Enabled
- name: docker-agent-alpine
- Docker Image: jenkins/agent:alpine3.24-jdk25@sha256:4451714a4d5f190791689575cf2cb280ae89bd331105f30278a2a0fb54f59732
- Instance Capacity: 2
- Remote File System Root: /home/jenkins
- Press the "Save" button

01-4 To create a new Freestyle project, my_job_02:
- Click on ' New Item '  
- name: --    
- item type:  Freestyle project 
- source code management -- > git -- > repository URL: --   
- source code management -- > Branches to build: */dev-mark   
- build environment -- > delete workspace before build starts: checkbox ticked  
- Press the "Save" button

01-5 To configure an agent to run a job (docker-agent-alpine):    
- Click on a job, 'MY-JOB-NAME'
- Configure
- Tick the checkbox: ' Restrict where this project can be run '
- Label Expression: docker-agent-alpine
- build steps -- > execute shell:   
```
cd FOLDER-NAME
echo "Hello World!"
echo "Job build ID ${BUILD_ID}"
echo "Job build URL ${BUILD_URL}"
ls -ltr
```
- Press the "Save" button

-- Run My Job (my_job_02):    
-- -- If you are stuck, see the section 'Setup steps for Alpine socat connection'.     
-- -- Click on ' Build Now '    


-- To create a new Docker Agent template (to run jobs on a docker agent):   

-- -- steps, run commands:     
01-1 Setup a new agent and give it a label (docker-agent-dotnet).           
To configure a local docker agent template:        
- Manage Jenkins -- > Clouds -- > docker
- Configure
- Next, look at the sections 'Setup steps for Alpine socat connection' and 'Set up a local repository'. 
- Docker Agent templates -- > Add Docker Template     
- labels: docker-agent-dotnet 
- Tick the checkbox: Enabled
- name: docker-agent-dotnet
- Docker Image: localhost:PORT/myjenkinsdotnetagent:latest
- Instance Capacity: 2
- Remote File System Root: /home/jenkins
- Pull strategy: Never pull
- Press the "Save" button

01-2 To configure an agent to run a job (docker-agent-dotnet):    
- Click on a job, 'MY-JOB-NAME', (my_job_03)
- Configure
- Tick the checkbox: ' Restrict where this project can be run '
- Label Expression: docker-agent-dotnet
- source code management -- > git -- > repository URL: --   
- source code management -- > Branches to build: */dev-mark   
- build environment -- > delete workspace before build starts: checkbox ticked  
- Triggers -- > Poll SCM:   
```
*/5 * * * *
```
- build steps -- > execute shell:   
```
cd jenkins
ls -ltra
dotnet --version
dotnet new console -o App --force
cp HelloWorld.cs App/Program.cs
dotnet run --project App
```
- Press the "Save" button

-- Run My Job (my_job_03):    
-- -- If you are stuck, see the sections 'Setup steps for Alpine socat connection' and 'Set up a local repository'.         
-- -- Click on ' Build Now '     
  
  
-- To create a new Pipeline:     
(+ commands)   

01-1 To configure an agent to run a build on a Pipeline (docker-agent-dotnet):    
- Click on ' New Item '  
- name: my_build_pipeline_01    
- item type:  Pipeline   
- Pipeline -- > Pipeline script:
```
pipeline {
    agent { 
        node {
            label 'docker-agent-dotnet'
            }
      }
    stages {
        stage('Build') {
            steps {
                echo "Building.."
                sh '''
                echo "doing build operations.."
                '''
            }
        }
        stage('Test') {
            steps {
                echo "Testing...."
                sh '''
                echo "doing test operations.."
                '''
            }
        }
        stage('Deliver') {
            steps {
                echo 'Deliver......'
                sh '''
                echo "doing delivery operations.."
                '''
            }
        }
    }
}
```
- Press the "Save" button

-- Run My Build (my_build_pipeline_01):     
-- -- If you are stuck, see the sections 'Setup steps for Alpine socat connection' and 'Set up a local repository'.         
-- -- Click on ' Build Now '    
  
  
-- To create a new Pipeline:   
(+ commands)   

01-1 To configure an agent to run a build on a Pipeline (docker-agent-dotnet):    
- Click on ' New Item '    
- name: my_build_pipeline_02    
- item type:  Pipeline   
- Pipeline -- > Pipeline script from SCM
- SCM -- > Git -- > Repository URL: --
- Branches to build: */dev-mark 
- Script Path: jenkins/Jenkinsfile
- Press the "Save" button

-- Run My Build (my_build_pipeline_02):    
-- -- If you are stuck, see the sections 'Setup steps for Alpine socat connection' and 'Set up a local repository'.         
-- -- Click on ' Build Now '    
  
  
-- Modified a previously created Pipeline (my_build_pipeline_02):   
(+ commands)   

- The modified script for the 'jenkins/Jenkinsfile':   
```
pipeline {
    agent { 
        node {
            label 'docker-agent-dotnet'
            }
      }
    triggers {
        pollSCM 'H/5 * * * *'
    }
    stages {
        stage('Build') {
            steps {
                echo "Building.."
                sh '''
                cd jenkins
                ls -ltra
                dotnet --version
                dotnet new console -o App2 --force
                cp Hello.cs App2/Program.cs
                '''
            }
        }
        stage('Test') {
            steps {
                echo "Testing...."
                sh '''
                cd jenkins
                dotnet run --project App2
                dotnet run --project App2 -- Bob
                '''
            }
        }
        stage('Deliver') {
            steps {
                echo 'Deliver......'
                sh '''
                echo "doing delivery operations.."
                '''
            }
        }
    }
}
```


-- Set up a local repository (no need for DockerHub)    

-- -- Create a local repository so the Jenkins agents 
can be set up via local docker images.    

-- -- Setup steps ...    
(+ cmd commands)

-- Docker create and use local registry    
kdguntu    
https://www.youtube.com/watch?v=SJrf3R3y5cQ    
  
  
-- -- Setup steps for Alpine socat connection ...    
(+ cmd commands)

-- -- Configure it for jenkins agents.

-- Learn Jenkins! Complete Jenkins Course - Zero to Hero      
DevOps Journey    
(00:36:25)         
https://youtu.be/6YZvp2GwT0A?si=l8eUeZZPX_9IVZDX&t=2185     

