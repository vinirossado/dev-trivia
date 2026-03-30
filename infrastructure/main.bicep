param location string = resourceGroup().location
var uniqueId = uniqueString(resourceGroup().id)

@secure()
@minLength(32)
@description('JWT Secret Key for token signing')
param jwtSecretKey string

@secure()
@description('PostgreSQL connection string for the trivia database')
param postgresConnectionString string

// Reference the EXISTING App Service Plan from TravelAPI (no new machine)
var appServicePlanName = 'plan-api-2g45mzh7gjumo'
var keyVaultName = 'kv-2g45mzh7gjumo'

resource existingAppServicePlan 'Microsoft.Web/serverfarms@2023-12-01' existing = {
  name: appServicePlanName
}

module triviaApiService 'modules/compute/appservice.bicep' = {
  name: 'triviaApiDeployment'
  params: {
    appName: 'trivia-api-${uniqueId}'
    serverFarmId: existingAppServicePlan.id
    location: location
    keyVaultName: keyVaultName
    linuxFxVersion: 'DOTNETCORE|10.0'
    appSettings: [
      {
        name: 'ConnectionStrings__DefaultConnection'
        value: postgresConnectionString
      }
      {
        name: 'JwtSettings__SecretKey'
        value: jwtSecretKey
      }
      {
        name: 'JwtSettings__Issuer'
        value: 'DevTrivia'
      }
      {
        name: 'JwtSettings__Audience'
        value: 'DevTrivia.Users'
      }
      {
        name: 'JwtSettings__ExpirationInMinutes'
        value: '60'
      }
      {
        name: 'RefreshToken__ExpiryDays'
        value: '1'
      }
      {
        name: 'EnableDevToken'
        value: 'false'
      }
    ]
  }
}

// Store secrets in Key Vault
module jwtSecret 'modules/secrets/keyvault-secret.bicep' = {
  name: 'triviaJwtSecretDeployment'
  params: {
    keyVaultName: keyVaultName
    secretName: 'JwtSettings--SecretKey--Trivia'
    secretValue: jwtSecretKey
  }
}

module postgresSecret 'modules/secrets/keyvault-secret.bicep' = {
  name: 'triviaPostgresSecretDeployment'
  params: {
    keyVaultName: keyVaultName
    secretName: 'Postgres--TriviaConnectionString'
    secretValue: postgresConnectionString
  }
}

// Grant the new App Service access to the existing Key Vault (RBAC-based, matching TravelAPI)
module keyVaultRoleAssignment 'modules/secrets/key-vault-role.bicep' = {
  name: 'keyVaultRoleAssignmentDeployment'
  params: {
    keyVaultName: keyVaultName
    principalIds: [
      triviaApiService.outputs.principalId
    ]
  }
}

// Outputs
output appServiceId string = triviaApiService.outputs.appServiceId
output appServiceUrl string = triviaApiService.outputs.url
output appServicePrincipalId string = triviaApiService.outputs.principalId
