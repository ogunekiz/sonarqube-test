pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build'
            }
        }

        stage('SonarQube Analysis') {
            steps {
                withSonarQubeEnv('SonarQube') {
                    sh '''
                    export PATH=$PATH:/var/lib/jenkins/.dotnet/tools

                    dotnet tool install --global dotnet-sonarscanner || true

                    dotnet sonarscanner begin \
                      /k:"sonarqube-test"

                    dotnet build

                    dotnet sonarscanner end
                    '''
                }
            }
        }
    }
}
