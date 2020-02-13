# Delete Custom Form Recognizer Model

This sample demonstrates how to delete a custom Form Recognizer model.

## Prerequisites

If you have not already created at least one custom model, please follow the guide in the [first sample].

You should have the `modelId` of the model you want to delete. If you're not sure, see [List Custom Models].

## Create the client

```csharp
// using Azure.AI.FormRecognizer

var endpoint = new Uri("{your_endpoint}");
var credential = new CognitiveKeyCredential("{your_service_key}");
var client = new CustomFormClient(endpoint, credential);
```

> Copy your `endpoint` and `credential` from the Azure Portal after you create your resource.

## Get a Model Reference

```csharp
var modelId = "{your_model_id}";
var model = client.GetModelReference(modelId);
```

## Delete the Model

```csharp
await model.DeleteAsync();
```

[List Custom Models]: ./06-List-Custom-Models.md