# Authenticate Form Recognizer with Azure Active Directory

This sample demonstrates how to authenticate with the Form Recognizer service using an OAuth token obtained from your Azure Active Directory.

Please review the [Cognitive Service Azure Active Directory Authentication] documentation.

This guide will use the cross-platform [Azure CLI] to register and configure your client application credential.

## Choose an Authentication Method

You can provide the Form Recognizer Client with any [TokenCredential] implementation. This sample will demonstrate how to use the [ClientSecretCredential].

## Identify Your Cognitive Services Resource Id

Your Cognitive Services resource is the resource that holds your subscription key.

If you know its resource group and name you can look up its identifier:

```bash
az cognitiveservices account show --resource-group {resource_group} --name {name} --query id --output tsv
# /subscriptions/{subscription_id}/resourceGroups/{resource_group}/providers/Microsoft.CognitiveServices/accounts/{resource_name}
```

> You can also find the `resourceId` from the [Azure Portal] by navigating to your resource and opening the "Properties" blade.

## Register Your Client App

You will need to obtain security credentials from Azure Active Directory for your client:

```bash
az ad sp create-for-rbac --name "MyFormRecognizerApp" --role "Cognitive Services User" --scopes {resource_id}
```

> Use your Form Recognizer `resource_id` from the previous step.

When the registration is complete, make note of the client's `appId`, `password`, and `tenant` from the output JSON:

```json
{
  "appId": "{your_client_id}",
  "displayName": "MyFormRecognizerApp",
  "name": "http://MyFormRecognizerApp",
  "password": "{your_client_secret}",
  "tenant": "{your_tenant_id}"
```

> Copy these values for later use.

## Authorize the CustomFormClient with Your Credential

Now that you have an `appId` and `password`, you can create a `ClientSecretCredential` for your `tenant`:

```csharp
var endpoint = new Uri("{your_endpoint}"); // copy from Azure Portal after creating resource
var clientId = "{your_client_id}"; // aka appId
var tenantId = "{your_tenant_id}"; // aka tenant
var clientSecret = "{your_client_secret}"; // aka password
var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
var client = new CustomFormClient(new Uri(endpoint), credential);
```

> This sample demonstrates how to use AAD authorization for a `CustomFormClient`. The same process can be used for `ReceiptClient` and `LayoutClient`.

You are now ready to use your `client`!

[TokenCredential]: https://docs.microsoft.com/en-us/dotnet/api/azure.core.tokencredential?view=azure-dotnet
[ClientSecretCredential]: https://docs.microsoft.com/en-us/dotnet/api/azure.identity.clientsecretcredential?view=azure-dotnet
[EnvironmentCredential]: https://docs.microsoft.com/en-us/dotnet/api/azure.identity.environmentcredential?view=azure-dotnet
[InteractiveBrowserCredential]: https://docs.microsoft.com/en-us/dotnet/api/azure.identity.interactivebrowsercredential?view=azure-dotnet
[Azure CLI]: https://aka.ms/azcli
[Cognitive Service Azure Active Directory Authentication]: https://docs.microsoft.com/en-us/azure/cognitive-services/authentication#authenticate-with-azure-active-directory
[Azure Portal]: https://portal.azure.com/
