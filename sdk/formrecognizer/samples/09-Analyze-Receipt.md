# Analyze Receipt with Form Recognizer

This sample demonstrates how to analyze a receipt using the Form Recognizer SDK.

## Prerequisites

If you don't have an Azure subscription, create a [free account] before you begin.

You can use your own receipt file or a [sample receipt].

## Create the client

```csharp
// using Azure.AI.FormRecognizer

var endpoint = new Uri("https://{your_service_name}.cognitiveservices.azure.com/");
var credential = new CognitiveKeyCredential("{your_service_key}");
var client = new FormReceiptClient(endpoint, credential);
```

## Load your request file

This is the receipt file that you want to analyze (pdf, jpeg, png, or tiff).

```csharp
var filePath = "/path/to/local/file.jpg";
var stream = File.OpenRead(filePath);
```

> All streams types are supported, but if you supply a non-seekable stream (e.g. from an HTTP response) you will need to supply a `FormContentType`.

## Submit analysis request

Analysis may take several seconds or several minutes depending on the size and complexity of your document. When you start an analysis operation, you receive an identifier that can be used to check the status of the operation and retrieve the results when complete. The result of the analysis operation is an `Analysis` object.

```csharp
var analysisOperation = await model.StartAnalysisAsync(stream);
Console.WriteLine($"Created request with id {analysisOperation.Id}");
```

> You can use the operation id to retrieve the analysis results for up to 48 hours.

## Wait for analysis completion

The `FormReceiptClient` can poll for the latest analysis status, asynchronously blocking the current thread.

```csharp
var analysisResponse = await analysisOperation.WaitForCompletionAsync();
if (analysisResponse.HasValue)
{
    var analysis = analysisResponse.Value;
    var status = analysis.Status;
    Console.WriteLine($"Status: {status}");
}
```

## Examine the analysis results

```
TODO
```


[free account]: https://azure.microsoft.com/free/?WT.mc_id=A261C142F
[sample receipt]: https://raw.githubusercontent.com/Azure-Samples/cognitive-services-REST-api-samples/master/curl/form-recognizer/contoso-allinone.jpg