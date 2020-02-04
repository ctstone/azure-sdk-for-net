# Analyze File with Form Recognizer Custom Model

This sample demonstrates how to analyze a new document from your local filesystem against an existing custom Form Recognizer model.

## Prerequisites

Please make sure you have completed the steps from the [first sample]. You should have a `modelId` that you can use here.

You can use your own form file or pick one from the public [sample data set].

## Create the client

```csharp
// using Azure.AI.FormRecognizer

var endpoint = new Uri("{your_endpoint}");
var credential = new CognitiveKeyCredential("{your_service_key}");
var client = new CustomFormClient(endpoint, credential);
```

> Copy your `endpoint` and `credential` from the Azure Portal after you create your resource.

## Get a model reference

You should alredy have a `modelId` from the [training sample]. You can also [list your models] if needed.

```csharp
var modelId = "{your_model_id}";
var model = client.GetModelReference(modelId);
```

## Load your request file

This is the file that you want to analyze (pdf, jpeg, png, or tiff). It should be separate from your training set.

```csharp
var filePath = "/path/to/local/file.pdf";
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

The `FormRecognizerClient` can poll for the latest analysis status, asynchronously blocking the current thread.

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

### Loop over the recognized key value pairs:

```csharp
foreach (var page in analysis.AnalyzeResult.PageResults)
{
    var clusterId = page.clusterId;
    Console.WriteLine($"Cluster({clusterId}):");
    foreach (var kvp in page.KeyValuePairs)
    {
        var keyText = kvp.Key.Text;
        var valueText = kvp.Value.Text;
        Console.WriteLine($"\t{keyText} => {valueText}");
    }
}
```

```
Cluster(0):
    Address: => 14564 Main St. Saratoga, CA 94588
    Invoice For: => First Up Consultants 1234 King St Redmond, WA 97624
    Invoice Number => 7689302
    Invoice Date => 3/09/2015
    Invoice Due Date => 6/29/2016
    Charges => $22,123.24
    VAT ID => QR
    Page => 1 of
    __Tokens__1 => Contoso Suites
    __Tokens__2 => 1
```

### Render the recognized tables:

```csharp
foreach (var table in page.Tables)
{
    table.WriteAscii(Console.Out);
    // table.WriteHtml(Console.Out);
    // table.WriteMarkdown(Console.Out);
}
```

```
┌───────────────┬───────────────┬───────────────┬───────────────┬───────────────┐
│ Invoice Number│   Invoice Date│Invoice Due Dat│        Charges│         VAT ID│
╞═══════════════╪═══════════════╪═══════════════╪═══════════════╪═══════════════╡
│        7689302│      3/09/2015│      6/29/2016│     $22,123.24│             QR│
└───────────────┴───────────────┴───────────────┴───────────────┴───────────────┘
```

> ASCII tables are rendered with unicode glyphs. If you require true ASCII tables, use `table.WriteAscii(Console.Out, unicode: false)`.

[first sample]: ./01-Train-Custom-Model.md
[training sample]: ./01-Train-Custom-Model.md
[sample data set]: https://github.com/Azure-Samples/cognitive-services-REST-api-samples/blob/master/curl/form-recognizer/sample_data.zip