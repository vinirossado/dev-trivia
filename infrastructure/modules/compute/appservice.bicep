param location string = resourceGroup().location
param appName string
param keyVaultName string
param appSettings array = []
param serverFarmId string
param linuxFxVersion string = 'DOTNETCORE|10.0'

resource webApp 'Microsoft.Web/sites@2023-12-01' = {
  name: appName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: serverFarmId
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: linuxFxVersion
      appSettings: concat(
        [
          {
            name: 'KeyVault__Vault'
            value: keyVaultName
          }
          {
            name: 'AZURE_TENANT_ID'
            value: subscription().tenantId
          }
          {
            name: 'AZURE_SUBSCRIPTION_ID'
            value: subscription().subscriptionId
          }
          {
            name: 'WEBSITE_RUN_FROM_PACKAGE'
            value: '1'
          }
          {
            name: 'ASPNETCORE_ENVIRONMENT'
            value: 'Production'
          }
        ],
        appSettings
      )
      alwaysOn: true
    }
  }
}

resource webAppConfig 'Microsoft.Web/sites/config@2023-12-01' = {
  parent: webApp
  name: 'web'
  properties: {
    scmType: 'GitHub'
  }
}

output appServiceId string = webApp.id
output principalId string = webApp.identity.principalId
output url string = 'https://${webApp.properties.defaultHostName}'
