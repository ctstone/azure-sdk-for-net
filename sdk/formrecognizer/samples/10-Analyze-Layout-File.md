# Analyze Layout with Form Recognizer

This sample demonstrates how to analyze the table layout of a form using the Form Recognizer SDK.

## Prerequisites

If you don't have an Azure subscription, create a [free account] before you begin.

You can use your own form file or a [sample form].

## Create the client

```csharp
// using Azure.AI.FormRecognizer

var endpoint = new Uri("{your_endpoint}");
var credential = new CognitiveKeyCredential("{your_service_key}");
var client = new FormLayoutClient(endpoint, credential);
```

> Copy your `endpoint` and `credential` from the Azure Portal after you create your resource.

## Load your request file

This is the form file that you want to analyze (pdf, jpeg, png, or tiff).

```csharp
var filePath = "/path/to/local/file.jpg";
var stream = File.OpenRead(filePath);
```

> All streams types are supported, but if you supply a non-seekable stream (e.g. from an HTTP response) you will need to supply a `FormContentType`.

## Submit analysis request

Analysis may take several seconds or several minutes depending on the size and complexity of your document. When you start an analysis operation, you receive an identifier that can be used to check the status of the operation and retrieve the results when complete. The result of the analysis operation is an `LayoutAnalysis` object.

```csharp
var operation = await client.StartAnalysisAsync(stream);
Console.WriteLine($"Created request with id {operation.Id}");
```

> You can use the operation id to retrieve the analysis results for up to 48 hours.

## Wait for analysis completion

The `FormLayoutClient` can poll for the latest analysis status, asynchronously blocking the current thread.

```csharp
var response = await operation.WaitForCompletionAsync();
var result = response.Value
```

## Examine the analysis results

### Examine result information

```csharp
Console.WriteLine("Information:");
Console.WriteLine($"  Status: {result.Status}");
Console.WriteLine($"  Duration: '{result.Duration}'");
Console.WriteLine($"  Version: '{result.Version}'");
```

### Examine result tables

```csharp
Console.WriteLine("Tables:");
foreach (var table in result.Tables)
{
    table.WriteAscii(Console.Out);
}
```

```yaml
# sample program output

Information:
  Status: Succeeded
  Duration: '00:00:05'
  Version: '2.0.0'
```

```
Tables:
┌───────────────┬───────────────┬───────────────┬───────────────┬───────────────┬───────────────┐
│ Invoice Number│   Invoice Date│Invoice Due Dat│        Charges│              -│         VAT ID│
├───────────────┼───────────────┼───────────────┼───────────────┼───────────────┼───────────────┤
│        7689302│      3/09/2015│      6/29/2016│                     $22,123.24│             QR│
└───────────────┴───────────────┴───────────────┴───────────────┴───────────────┴───────────────┘
```


[free account]: https://azure.microsoft.com/free/?WT.mc_id=A261C142F
[sample form]: https://github.com/Azure-Samples/cognitive-services-REST-api-samples/blob/master/curl/form-recognizer/sample_data.zip