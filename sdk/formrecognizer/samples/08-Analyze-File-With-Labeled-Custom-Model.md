# Analyze File with Form Recognizer Labeled Custom Model

This sample demonstrates how to analyze a new document from your local filesystem against an existing custom Form Recognizer model with labels.

## Prerequisites

Please make sure you have completed the labeling and training steps from the [training sample]. You should have a `modelId` that you can use here.

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
var model = client.GetModelReferenceWithLabels(modelId);
```

## Load your request file

This is the file that you want to analyze (pdf, jpeg, png, or tiff). It should be separate from your training set.

```csharp
var filePath = "/path/to/local/file.pdf";
var stream = File.OpenRead(filePath);
```

> All streams types are supported, but if you supply a non-seekable stream (e.g. from an HTTP response) you will need to supply a `FormContentType`.

## Submit analysis request

Analysis may take several seconds or several minutes depending on the size and complexity of your document. When you start an analysis operation, you receive an identifier that can be used to check the status of the operation and retrieve the results when complete. The result of the analysis operation is an `LabeledFormAnalysis` object.

```csharp
var operation = await model.StartAnalysisAsync(stream);
Console.WriteLine($"Created request with id {operation.Id}");
```

> You can use the operation id to retrieve the analysis results for up to 48 hours.

## Wait for analysis completion

The `CustomFormClient` can poll for the latest analysis status, asynchronously blocking the current thread.

```csharp
var response = await operation.WaitForCompletionAsync();
var result = response.Value
```

## Examine analysis results

### Display result information

```csharp
Console.WriteLine("Information:");
Console.WriteLine($"  Status: {result.Status}");
Console.WriteLine($"  Duration: '{result.Duration}'");
Console.WriteLine($"  Version: '{result.Version}'");
```

[previous sample]: ./07-Train-Custom-Model-With-Labels.md
[list your models]: ./05-List-Custom-Models.md