# Analyze File with Form Recognizer Labeled Custom Model

This sample builds on [Analyze File with Labels], adding support for using your own strongly-typed class in the form results.

## Prerequisites

Please make sure you have completed the labeling and training steps from the [training sample]. You should have a `modelId` that you can use here.

You can use your own form file or pick one from the public [sample data set].

## Define a class for your labeled model

```csharp
public class Invoice
{
    [DataMember(Name = "InvoiceVatId")]
    public string VatId { get; set; }

    [DataMember(Name = "InvoiceCharges")]
    public string Charges { get; set; }

    [DataMember(Name = "InvoiceNumber")]
    public string Number { get; set; }

    [DataMember(Name = "InvoiceDueDate")]
    public string DueDate { get; set; }

    [DataMember(Name = "InvoiceDate")]
    public string Date { get; set; }
}
```

> You can omit the `DataMember` attributes if your property names match your model's predefined labels.

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
var model = client.GetModelReferenceWithLabels<Invoice>(modelId);
```

> Pass the type of your label values class as a generic parameter.

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

### Display form values

```csharp
var invoice = result.Forms[0];
Console.WriteLine($"VatId: {invoice.Form.VatId}");
Console.WriteLine($"Charges: {invoice.Form.Charges}");
Console.WriteLine($"Number: {invoice.Form.Number}");
Console.WriteLine($"DueDate: {invoice.Form.DueDate}");
Console.WriteLine($"Date: {invoice.Form.Date}");
```

### Retieve field metadata

Only the form _values_ are mapped to your custom type. In order to access the field's original text, bounding box, and confidence, use the `Fields` dictionary:

```csharp
var dateField = invoice.Fields["InvoiceDate"];
Console.WriteLine($"Date Original Text: {dateField.Text}");
Console.WriteLine($"Date Bounding Box: {string.join(',', dateField.BoundingBox)}");
Console.WriteLine($"Date Confidence: {dateField.Confidence}");
```


[Analyze File with Labels]: ./08-Analyze-File-With-Labeled-Custom-Model
[list your models]: ./05-List-Custom-Models.md