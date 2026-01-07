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
                    dotnet tool install --global dotnet-sonarscanner || true
                    export PATH="$PATH:$HOME/.dotnet/tools"

                    dotnet sonarscanner begin \
                      /k:"sonarqube-test" \
                      /d:sonar.host.url=http://localhost:9000 \
                      /d:sonar.login=$SONAR_TOKEN

                    dotnet build

                    dotnet sonarscanner end \
                      /d:sonar.login=$SONAR_TOKEN
                    '''
                }
            }
        }
    }
}
selam