# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

Function ShowDisclaimer(){
    Write-Host "Agreement"
    Write-Host "I have read all the desclaimers (https://github.com/microsoft/Machine-Learning-Patient-Risk-Analyzer-SA/README.md)" 
    Write-Host "and license agreement(https://github.com/microsoft/Machine-Learning-Patient-Risk-Analyzer-SA/license.md)."
    Write-Host "I accept and agree to proceed. (Type [Y] for Yes or [N] for No and press enter)"
    return Read-Host "[Y] Yes [N] No (default is ""N"" )"
}

$AgreementAnswer = ShowDisclaimer

while (!($AgreementAnswer).ToUpper().Equals("Y")) {
    if (($AgreementAnswer).ToUpper().Equals("N")) {
        exit 0
    }
    $AgreementAnswer = ShowDisclaimer
}

#login azure
Write-Host "Login to Azure"
az Login

$subscriptionId = Read-Host "subscription Id"
$resourcegroupName = Read-Host "resource group name"
$containerRegistryName = Read-Host "container registry name"
$kubernetesName = Read-Host "kubernetes registry name"
$azurecosmosaccountName = Read-Host "Azure CosmosDB instance name"
$azurecosmosdbDataBaseName = Read-Host "PatientHub CosmosDB DatabaseName" 
$ttsSubscriptionKey = Read-Host "Speech Service Subscription Key"
$ttsServiceRegion = Read-Host "Speech Service Region"
$mlServiceURL = Read-Host "Deployed Realtime inference service URL in Azure ML Studio"
$mlServiceBearerToken = Read-Host "Deployed Realtime inference service BearerToken in Azure ML Studio"

az account set --subscription $subscriptionId

$resourceGroup = az group exists -n $resourcegroupName
if ($resourceGroup -eq $false) {
    throw "The Resource group '$resourcegroupName' is not exist`r`n Please check resource name and try it again"
}

Write-Host "Step 1 - Setup Azure Container Registry"

az acr update --name $containerRegistryName --admin-enabled true

Write-Host "..... Retrieving Configuration Setting in Container Registry"

$acrList = az acr list | Where-Object { $_ -match $containerRegistryName + ".*" }
$loginServer = $acrList[1].Split(":")[1].Replace('"', '').Replace(',', '').Replace(' ', '')
$registryName = $acrList[2].Split(":")[1].Replace('"', '').Replace(',', '').Replace(' ', '')

$userName = $registryName
$password = ( az acr credential show --name $userName | ConvertFrom-Json).passwords.value.Split(" ")[1] 

Write-Host "..... loginServer: '$loginServer'"
Write-Host "..... registryName: '$registryName'"
Write-Host "..... userName: '$userName'"
Write-Host "..... userPassword: '$password'"

Write-Host "Step 2 - Setup Azure Kubernetes Service and Azure Container Service"

az aks update -n $kubernetesName -g $resourcegroupName --attach-acr $containerRegistryName

Write-Host "Step3 - Update Azure CosmosDB ConnectionString in App configuration...."

$connectionString = az cosmosdb keys list `
    --type connection-strings `
    --name $azurecosmosaccountName `
    --resource-group $resourcegroupName `
    --query "connectionStrings[?contains(description, 'Primary SQL Connection String')].[connectionString]" -o tsv

#Replace connectionstring in each appsettings.json files
#
# AppointmentService
#
((Get-Content -path src\Microsoft.Solutions.PatientHub.AppointmentService.Host\appsettings.json.template -Raw) -replace '{DBConnectionString}', $connectionString) | Set-Content -Path src\Microsoft.Solutions.PatientHub.AppointmentService.Host\appsettings.json
((Get-Content -path src\Microsoft.Solutions.PatientHub.AppointmentService.Host\appsettings.json -Raw) -replace '{DatabaseName}', $azurecosmosdbDataBaseName) | Set-Content -Path src\Microsoft.Solutions.PatientHub.AppointmentService.Host\appsettings.json

#
# BatchInferenceService
#
((Get-Content -path src\Microsoft.Solutions.PatientHub.BatchInferenceService.Host\appsettings.json.template -Raw) -replace '{DBConnectionString}', $connectionString) | Set-Content -Path src\Microsoft.Solutions.PatientHub.BatchInferenceService.Host\appsettings.json
((Get-Content -path src\Microsoft.Solutions.PatientHub.BatchInferenceService.Host\appsettings.json -Raw) -replace '{DatabaseName}', $azurecosmosdbDataBaseName) | Set-Content -Path src\Microsoft.Solutions.PatientHub.BatchInferenceService.Host\appsettings.json

#
# CognitiveService
#
((Get-Content -path src\Microsoft.Solutions.PatientHub.CognitiveService.Host\appsettings.json.template -Raw) -replace '{DBConnectionString}', $connectionString) | Set-Content -Path src\Microsoft.Solutions.PatientHub.CognitiveService.Host\appsettings.json
((Get-Content -path src\Microsoft.Solutions.PatientHub.CognitiveService.Host\appsettings.json -Raw) -replace '{DatabaseName}', $azurecosmosdbDataBaseName) | Set-Content -Path src\Microsoft.Solutions.PatientHub.CognitiveService.Host\appsettings.json
((Get-Content -path src\Microsoft.Solutions.PatientHub.CognitiveService.Host\appsettings.json -Raw) -replace '{TTSSubscriptionKey}', $ttsSubscriptionKey) | Set-Content -Path src\Microsoft.Solutions.PatientHub.CognitiveService.Host\appsettings.json
((Get-Content -path src\Microsoft.Solutions.PatientHub.CognitiveService.Host\appsettings.json -Raw) -replace '{TTSServiceRegion}', $ttsServiceRegion) | Set-Content -Path src\Microsoft.Solutions.PatientHub.CognitiveService.Host\appsettings.json

#
# PatientService
#
((Get-Content -path src\Microsoft.Solutions.PatientHub.PatientService.Host\appsettings.json.template -Raw) -replace '{DBConnectionString}', $connectionString) | Set-Content -Path src\Microsoft.Solutions.PatientHub.PatientService.Host\appsettings.json
((Get-Content -path src\Microsoft.Solutions.PatientHub.PatientService.Host\appsettings.json -Raw) -replace '{DatabaseName}', $azurecosmosdbDataBaseName) | Set-Content -Path src\Microsoft.Solutions.PatientHub.PatientService.Host\appsettings.json

#
# RealtimeInferenceService
#
((Get-Content -path src\Microsoft.Solutions.PatientHub.RealtimeInferenceService.Host\appsettings.json.template -Raw) -replace '{DBConnectionString}', $connectionString) | Set-Content -Path src\Microsoft.Solutions.PatientHub.RealtimeInferenceService.Host\appsettings.json
((Get-Content -path src\Microsoft.Solutions.PatientHub.RealtimeInferenceService.Host\appsettings.json -Raw) -replace '{DatabaseName}', $azurecosmosdbDataBaseName) | Set-Content -Path src\Microsoft.Solutions.PatientHub.RealtimeInferenceService.Host\appsettings.json
((Get-Content -path src\Microsoft.Solutions.PatientHub.RealtimeInferenceService.Host\appsettings.json -Raw) -replace '{MLserviceUrl}', $mlServiceURL) | Set-Content -Path src\Microsoft.Solutions.PatientHub.RealtimeInferenceService.Host\appsettings.json
((Get-Content -path src\Microsoft.Solutions.PatientHub.RealtimeInferenceService.Host\appsettings.json -Raw) -replace '{MLServiceBearerToken}', $MLServiceBearerToken) | Set-Content -Path src\Microsoft.Solutions.PatientHub.RealtimeInferenceService.Host\appsettings.json

#
# ChangeFeedWatcherService
#
((Get-Content -path src\Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker\appsettings.json.template -Raw) -replace '{DBConnectionString}', $connectionString) | Set-Content -Path src\Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker\appsettings.json
((Get-Content -path src\Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker\appsettings.json -Raw) -replace '{DatabaseName}', $azurecosmosdbDataBaseName) | Set-Content -Path src\Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker\appsettings.json


Write-Host "Step 4 - Build Container and push Azure container registry...."

#Build and Containerizing then Push to Azure Container Registry
 Set-location ".\src"

docker build -f .\Microsoft.Solutions.PatientHub.AppointmentService.Host\Dockerfile --rm -t 'patienthub/appointment' .
docker build -f .\Microsoft.Solutions.PatientHub.BatchInferenceService.Host\Dockerfile --rm -t 'patienthub/batchinference' .
docker build -f .\Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker\Dockerfile --rm -t 'patienthub/changefeedwatcher' .
docker build -f .\Microsoft.Solutions.PatientHub.CognitiveService.Host\Dockerfile --rm -t 'patienthub/tts' .
docker build -f .\Microsoft.Solutions.PatientHub.PatientService.Host\Dockerfile --rm -t 'patienthub/patient' .
docker build -f .\Microsoft.Solutions.PatientHub.RealtimeInferenceService.Host\Dockerfile --rm -t 'patienthub/realtimeinference' .

docker tag 'patienthub/appointment' "$containerRegistryName.azurecr.io/patienthub/appointment"
docker tag 'patienthub/batchinference' "$containerRegistryName.azurecr.io/patienthub/batchinference"
docker tag 'patienthub/changefeedwatcher' "$containerRegistryName.azurecr.io/patienthub/changefeedwatcher"
docker tag 'patienthub/tts' "$containerRegistryName.azurecr.io/patienthub/tts"
docker tag 'patienthub/patient' "$containerRegistryName.azurecr.io/patienthub/patient"
docker tag 'patienthub/realtimeinference' "$containerRegistryName.azurecr.io/patienthub/realtimeinference"

Write-Host "Login to ACS `r`n"
docker login "$containerRegistryName.azurecr.io" -u $userName -p $password

Write-Host "Push Images to ACS`r`n"
docker push "$containerRegistryName.azurecr.io/patienthub/appointment"
docker push "$containerRegistryName.azurecr.io/patienthub/batchinference"
docker push "$containerRegistryName.azurecr.io/patienthub/changefeedwatcher"
docker push "$containerRegistryName.azurecr.io/patienthub/tts"
docker push "$containerRegistryName.azurecr.io/patienthub/patient"
docker push "$containerRegistryName.azurecr.io/patienthub/realtimeinference"

Set-Location ..

Write-Host "Step 5 - Set up AKS and Deploy Applications from Azure Container Registry...."
# # Set Kubernets context
az aks get-credentials -g $resourceGroupName -n $kubernetesName

Write-Host "Preparing Kubernetes Deployment.....`r`n"
# #Set up Deployment yaml file to deploy APIs
((Get-Content -path manifests\kubernetes-deployment.yaml.template -Raw) -replace '{acrname}', $containerRegistryName) | Set-Content -Path manifests\kubernetes-deployment.yaml

Write-Host "Deploy Services from ACR to Kubernetes.....`r`n"
kubectl create ns patienthub
# #Deploy Containers to Kubernetes
kubectl apply -f manifests\kubernetes-deployment.yaml --namespace patienthub

Write-Host "Application deployment has been finished.....`r`n"

Write-Host "Check the IP Address for each services`r`n"
kubectl get service -o=custom-columns=NAME:.metadata.name,EXTERNAL_IP:.status.loadBalancer.ingress[0].ip,PORT:.spec.ports[0].targetPort -n patienthub -w
