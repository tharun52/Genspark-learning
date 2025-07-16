# Create Function
```bash
az functionapp create --resource-group tharun-rg --consumption-plan-location eastus2 --name azurefunction-tharun --storage-account tharunstorageaccount --runtime dotnet-isolated --functions-version 4
```

# Set function settings
```bash
az functionapp config appsettings set  --name azurefunction-tharun --resource-group tharun-rg --settings AzureStorageConnectionString="DefaultEndpointsProtocol=https;AccountName=tharunstorageaccount;AccountKey=qNIvkGP9X+xcoCZAk22qZFXvmUZdq/RuMztH/TU390QDeq1hcigTkKAYDl9R+mDdI1kD3E1YiYbl+ASt4xoRZQ==;EndpointSuffix=core.windows.net"  ContainerName="filecontainer" KeyVaultUri="https://tharun-key-vault.vault.azure.net/"
```

# List the function key
```bash
az functionapp function keys list --resource-group tharun-rg --name azurefunction-tharun --function-name Function
```
