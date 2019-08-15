pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                echo 'Building..'
                bat "\"C:/Program Files/dotnet/dotnet.exe\" restore"
                bat "\"C:/Program Files/dotnet/dotnet.exe\" build"
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
                bat "\"C:/Program Files/dotnet/dotnet.exe\" publish"
            }
        }
        stage('Publish') {
            steps {
                echo 'Deploying....'
                bat "%SYSTEMROOT%/System32/inetsrv/appcmd stop apppool /apppool.name:\"${JOB_NAME}\""
                bat "%SYSTEMROOT%/System32/inetsrv/appcmd stop site /site.name:\"${JOB_NAME}\""
                bat "copy /y \"${WORKSPACE}\\bin\\Debug\\netcoreapp2.2\\publish\\*.*\" \"C:/PROYECTOS/${JOB_NAME}\""
                bat "%SYSTEMROOT%/System32/inetsrv/appcmd start apppool /apppool.name:\"${JOB_NAME}\""
                bat "%SYSTEMROOT%/System32/inetsrv/appcmd start site /site.name:\"${JOB_NAME}\""
            }
        }
    }
}