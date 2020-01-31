# Train a Form Recognizer Model

This sample demonstrates how to train a custom Form Recognizer model without labels.

## Prerequisites

If you don't have an Azure subscription, create a [free account] before you begin.

You can use your own form data or a public [sample data set].

You will need to upload your training data into an Azure Storage blob container. You should have a minimum of five filled-in forms (PDF documents or images) of the same type/structure as your main input data.

You can use one of several cross-platform client applications to easily upload your forms to Azure Storage:

- [Azure Storage Explorer] (desktop app)
- [Azcopy] (command line)
- [az storage] (command line)
- [Azure.Storage.Blobs] (.NET SDK)

> See [Build a training data set for a custom model] for more details about structuring your training data.

## Create the client

```csharp
// using Azure.AI.FormRecognizer

var endpoint = new Uri("https://{your_service_name}.cognitiveservices.azure.com/");
var credential = new CognitiveKeyCredential("{your_service_key}");
var client = new FormRecognizerClient(endpoint, credential);
```

## Construct the training request

A training request is an Azure Storage url that points to a blob container holding your training documents. The Form Recognizer service will scan this container during the training process as your model is built. In order to limit access to your container, use a Shared Access Signature (SAS) url.

By default, the Form Recognizer service will only scan documents at the root of your container. You may optionally specify a path prefix to limit the documents that are read and a flag to recursively scan all segments matching the prefix.

> Use your Azure Storage client application to generate the SAS Url (refer to SAS instructions for [Storage Explorer], [az cli], or [.NET]).

```csharp
var trainingRequest = new TrainingRequest { Source = "{your_blob_container_sas_url}" };
```

## Submit the training request

Training may take several minutes depending on the quantity, size and complexity of your training documents. When you start a training request, you receive an identifier that can be used to check the status of the operation and retrieve the results when complete. The result of the training operation is a `Model`.

The `FormRecognizerClient` will asynchronously poll for the latest training status.

```csharp
var trainingOperation = await client.StartTrainingAsync(trainingRequest);
Console.WriteLine($"Created model with id {trainingOperation.Id}");
```

> The operation identifier is also the model identifer. You can use this value to retrieve the model information or perform analysis.

## Wait for training completion

```csharp
var trainingResponse = await trainingOperation.WaitForCompletionAsync();
if (trainingResponse.HasValue)
{
    var model = trainingResponse.Value;
    var status = model.ModelInfo.Status;
    Console.WriteLine($"Status: {status}");
}
```

## Examine the trained model

Loop through the model's keys. Each key in the cluster dictionary represents a recognized document group.

```csharp
var model = trainingResponse.Value;
foreach (var cluster in model.Keys.Clusters)
{
    var clusterId = cluster.Key;
    var numKeys = cluster.Value.Length;
    Console.WriteLine($"Cluster '{clusterId}' has {numKeys} recognized keys.");
}
```

Identify the trained document names and their status. Each document reports the original filename, number of pages, and any potential errors.

```csharp
var model = trainingResponse.Value;
foreach (var document in model.TrainResult.TrainingDocuments)
{
    var name = document.DocumentName;
    var status = document.Status;
    var numPages = document.Pages;
    var numErrors = document.Errors.Length;
    Console.WriteLine($"{name}: {status} - {numPages} page(s) - {numErrors} errors.");
}
```

## Next Steps

- Analyze file with custom model
- Analyze url with custom model
- List all custom models
- Delete a custom model


[Build a training data set for a custom model]: https://docs.microsoft.com/en-us/azure/cognitive-services/form-recognizer/build-training-data-set
[Azure Storage Explorer]: https://aka.ms/storage-explorer
[Azcopy]: https://aka.ms/azcopy
[az storage]: https://aka.ms/azcli
[Azure.Storage.Blobs]: https://www.nuget.org/packages/Azure.Storage.Blobs/
[sample data set]: https://github.com/Azure-Samples/cognitive-services-REST-api-samples/blob/master/curl/form-recognizer/sample_data.zip
[free account]: https://azure.microsoft.com/free/?WT.mc_id=A261C142F
[Storage Explorer]: https://docs.microsoft.com/en-us/azure/vs-azure-tools-storage-manage-with-storage-explorer#generate-a-shared-access-signature-in-storage-explorer
[az cli]: https://docs.microsoft.com/en-us/cli/azure/storage/container?view=azure-cli-latest#az-storage-container-generate-sas
[.NET]: https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-service-sas-create-dotnet