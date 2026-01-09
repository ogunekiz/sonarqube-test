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

        stage('Quality Gate') {
            steps {
                timeout(time: 1, unit: 'MINUTES') {
                    waitForQualityGate abortPipeline: true
                }
            }
        }
    }

    post {
        success {
            slackSend(
                channel: '#ci-cd-alerts',
                color: 'good',
                message: """
✅ *BUILD SUCCESS*
*Job:* ${env.JOB_NAME}
*Build:* #${env.BUILD_NUMBER}
🔗 ${env.BUILD_URL}
"""
            )
        }

        failure {
            slackSend(
                channel: '#ci-cd-alerts',
                color: 'danger',
                message: """
❌ *QUALITY GATE FAILED*
*Job:* ${env.JOB_NAME}
*Build:* #${env.BUILD_NUMBER}
🚫 SonarQube Quality Gate ERROR
🔗 ${env.BUILD_URL}
"""
            )
        }
    }
}
