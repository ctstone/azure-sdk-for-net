# Analyze Uri with Form Recognizer Custom Model

This sample demonstrates how to analyze a new document from any internet-addressable Uri against an existing custom Form Recognizer model.

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

## Point to your request file

```csharp
var uri = new Uri("http://myhost/myfile.pdf");
```

> ⚠️ The Uri must be internet-addressable (or if using the container service, it must be addressable from your container). If your file is an Azure Blob, you may specify a [Shared Access Signature] url.

> ⚠️ The remote endpoint must respond with a valid Form Recognizer `Content-Type`:
> - `application/pdf`
> - `image/jpeg`
> - `image/png`
> - `image/tiff`

## Submit analysis request

Analysis may take several seconds or several minutes depending on the size and complexity of your document. When you start an analysis operation, you receive an identifier that can be used to check the status of the operation and retrieve the results when complete. The result of the analysis operation is an `FormAnalysis` object.

```csharp
var operation = await model.StartAnalysisAsync(uri);
Console.WriteLine($"Created request with id {operation.Id}");
```

> You can use the operation id to retrieve the analysis results for up to 48 hours.

## Wait for analysis completion

The `CustomFormClient` can poll for the latest analysis status, asynchronously blocking the current thread.

```csharp
var response = await operation.WaitForCompletionAsync();
var result = response.Value
```

## Examine the analysis results

### Display result information

```csharp
Console.WriteLine("Information:");
Console.WriteLine($"  Status: {result.Status}");
Console.WriteLine($"  Duration: '{result.Duration}'");
Console.WriteLine($"  Version: '{result.Version}'");
```

## Display result fields

```csharp
Console.WriteLine("Fields:");
foreach (var extraction in result.Fields)
{
    Console.WriteLine($"- Field: '{extraction.Field.Text}'");
    Console.WriteLine($"  Value: '{extraction.Value.Text}'");
    Console.WriteLine($"  ClusterId: {extraction.ClusterId}");
    Console.WriteLine($"  Page: {extraction.PageNumber}");
}
```

## Display result tables

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
  Duration: '00:00:02'
  Version: '2.0.0'
Fields:
- Field: 'Address:'
  Value: '14564 Main St. Saratoga, CA 94588'
  ClusterId: 0
  Page: 1
- Field: 'Invoice For:'
  Value: 'First Up Consultants 1234 King St Redmond, WA 97624'
  ClusterId: 0
  Page: 1
- Field: 'Invoice Number'
  Value: '7689302'
  ClusterId: 0
  Page: 1
- Field: 'Invoice Date'
  Value: '3/09/2015'
  ClusterId: 0
  Page: 1
- Field: 'Invoice Due Date'
  Value: '6/29/2016'
  ClusterId: 0
  Page: 1
- Field: 'Charges'
  Value: '$22,123.24'
  ClusterId: 0
  Page: 1
- Field: 'VAT ID'
  Value: 'QR'
  ClusterId: 0
  Page: 1
- Field: 'Page'
  Value: '1 of'
  ClusterId: 0
  Page: 1
- Field: '__Tokens__1'
  Value: 'Contoso Suites'
  ClusterId: 0
  Page: 1
- Field: '__Tokens__2'
  Value: '1'
  ClusterId: 0
  Page: 1
```

```
Tables:
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
[Shared Access Signature]: https://docs.microsoft.com/en-us/azure/storage/common/storage-sas-overview