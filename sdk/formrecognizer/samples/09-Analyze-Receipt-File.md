# Analyze Receipt with Form Recognizer

This sample demonstrates how to analyze a receipt using the Form Recognizer SDK.

## Prerequisites

If you don't have an Azure subscription, create a [free account] before you begin.

You can use your own receipt file or a [sample receipt].

## Create the client

```csharp
// using Azure.AI.FormRecognizer

var endpoint = new Uri("{your_endpoint}");
var credential = new CognitiveKeyCredential("{your_service_key}");
var client = new ReceiptClient(endpoint, credential);
```

> Copy your `endpoint` and `credential` from the Azure Portal after you create your resource.

## Load your request file

This is the receipt file that you want to analyze (pdf, jpeg, png, or tiff).

```csharp
var filePath = "/path/to/local/file.jpg";
var stream = File.OpenRead(filePath);
```

> All streams types are supported, but if you supply a non-seekable stream (e.g. from an HTTP response) you will need to supply a `FormContentType`.

## Submit analysis request

Analysis may take several seconds or several minutes depending on the size and complexity of your document. When you start an analysis operation, you receive an identifier that can be used to check the status of the operation and retrieve the results when complete. The result of the analysis operation is an `ReceiptAnalysis` object.

```csharp
var operation = await client.StartAnalysisAsync(stream);
Console.WriteLine($"Created request with id {operation.Id}");
```

> You can use the operation id to retrieve the analysis results for up to 48 hours.

## Wait for analysis completion

The `ReceiptClient` can poll for the latest analysis status, asynchronously blocking the current thread.

```csharp
var response = await operation.WaitForCompletionAsync();
var result = response.Value
```

## Examine the analysis results

```csharp
Console.WriteLine($"Receipts:");
foreach (var receipt in result.Receipts)
{
    Console.WriteLine($"- Type: {receipt.ReceiptType}");
    Console.WriteLine($"  Merchant:");
    Console.WriteLine($"    Name: {receipt.MerchantName}");
    Console.WriteLine($"    Address: {receipt.MerchantAddress}");
    Console.WriteLine($"    Phone: {receipt.MerchantPhoneNumber}");
    Console.WriteLine($"  Transaction:");
    Console.WriteLine($"    Date: {receipt.TransactionDate}");
    Console.WriteLine($"    Time: {receipt.TransactionTime}");
    Console.WriteLine($"  Items:");
    foreach (var item in receipt.Items.Value)
    {
        Console.WriteLine($"  - Name: {item.Name}");
        Console.WriteLine($"    Quantity: {item.Quantity}");
        Console.WriteLine($"    TotalPrice: {item.TotalPrice}");
    }
    Console.WriteLine($"  Subtotal: {receipt.Subtotal}");
    Console.WriteLine($"  Tax: {receipt.Tax}");
    Console.WriteLine($"  Tip: {receipt.Tip}");
    Console.WriteLine($"  Total: {receipt.Total}");
}
```

> The prebuilt receipt model may gain new recognition capabilities before they are available in the SDK. You can access arbitrary fields by name:
> ```csharp
> var receipt = result.Receipts[0];
> if (receipt.TryGetField("SomeNewField", out PredefinedField value))
> {
>     Console.WriteLine($"NewField: {value.Text}");
> }
>
> // also get a collection of all field names
> var fieldNames = receipt.FieldNames;
> ```

```yaml
# sample program output

Information:
  Status: Succeeded
  Duration: '00:00:09'
  Version: '2.0.0'
Receipts:
- Type: Itemized
  Merchant:
    Name: Contoso Contoso
    Address: 123 Main Street Redmond, WA 98052
    Phone: +19876543210
  Transaction:
    Date: 6/10/19 12:00:00 AM -04:00
    Time: 13:59:00
  Items:
  - Name: Cappuccino
    Quantity: 1
    TotalPrice: 2.2
  - Name: BACON & EGGS
    Quantity: 1
    TotalPrice: $9.5
  Subtotal: 11.7
  Tax: 1.17
  Tip: 1.63
  Total: 14.5
```


[free account]: https://azure.microsoft.com/free/?WT.mc_id=A261C142F
[sample receipt]: https://raw.githubusercontent.com/Azure-Samples/cognitive-services-REST-api-samples/master/curl/form-recognizer/contoso-allinone.jpg