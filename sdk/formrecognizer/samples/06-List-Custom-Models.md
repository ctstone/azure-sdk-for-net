# List Custom Form Recognizer Models

This sample demonstrates how to enumerate your custom Form Recognizer models.

## Prerequisites

If you have not already created at least one custom model, please follow the guide in the [first sample].

## Create the client

```csharp
// using Azure.AI.FormRecognizer

var endpoint = new Uri("https://{your_service_name}.cognitiveservices.azure.com/");
var credential = new CognitiveKeyCredential("{your_service_key}");
var client = new FormRecognizerClient(endpoint, credential);
```

## Option 1: Enumerate All Models

If you have a large number of models, the Form Recognizer service will list your models in pages, requiring multiple service calls to fetch all models.

If you want to return all models at once, the Form Recognizer SDK exposes an `IAsyncEnumerable` that will asynchronously fetch additional pages as necessary, as long as you continue to loop over the enumerator:

```csharp
var models = client.ListModelsAsync();
await foreach (var model in models)
{
    Console.WriteLine($"{model.ModelId} - {model.Status}");
}
```

## Option 2: Enumerate Model Pages

If you need access to the model pages you can access them using `AsPages()`:

```csharp
var models = client.ListModelsAsync();
await foreach (var page in models.AsPages())
{
    Console.WriteLine($"nextLink: {page.ContinuationToken}");
    foreach (var model in page.Values)
    {
        Console.WriteLine($"{model.ModelId} - {model.Status}");
    }
}
```

## Option 3: One Page at a Time

If you need to access a single page:

```csharp
var page1 = models.AsPages().GetAsyncEnumerator().Current;

// return page1 to UI and wait for user to request next page...

var page2 = models.AsPages(page1.ContinuationToken).GetAsyncEnumerator().Current;
```


[first sample]: ./01-Train-Custom-Model.md