// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Models;
using Azure.Core;
using Azure.Core.Diagnostics;
using Azure.Identity;

namespace Azure.AI.FormRecognizer.Samples
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                // Default user agent looks like: azsdk-net-AI.FormRecognizer/1.0.0-dev.20200114.1+e5a3b85bb6f85c29ef8e3dc47b2e89165ce5a98d,(.NET Core 4.6.28008.01; Darwin 19.2.0 Darwin Kernel Version 19.2.0: Sat Nov  9 03:47:04 PST 2019; root:xnu-6153.61.1~20/RELEASE_X86_64)
                // https://github.com/Azure/azure-sdk-for-net/blob/d99777ef4a7d75dd5f9482c84c0240a900e15f9c/sdk/core/Azure.Core/samples/Configuration.md
                // using AzureEventSourceListener listener = AzureEventSourceListener.CreateConsoleLogger();

                // var op = args.Length > 0 ? args[0] : String.Empty;
                var endpoint = new Uri(Environment.GetEnvironmentVariable("FR_ENDPOINT"));
                var credential = new CognitiveKeyCredential(Environment.GetEnvironmentVariable("FR_KEY"));
                // var options = new FormClientOptions();
                // // options.Diagnostics.IsLoggingEnabled = true;
                var client = new CustomFormClient(endpoint, credential);
                // var layoutClient = new FormLayoutClient(endpoint, credential);
                // var receiptClient = new ReceiptClient(endpoint, credential);

                // await (op switch
                // {
                //     "train" => TrainAsync(client, args),
                //     "model" => GetModelAsync(client, args),
                //     "analyze" => AnalyzeAsync(client, args),
                //     "analysis" => GetAnalysisAsync(client, args),
                //     "analysisResult" => GetAnalysisResultAsync(client, args),
                //     "summary" => GetModelsSummaryAsync(client),
                //     "delete" => DeleteModelAsync(client, args),
                //     "list" => ListModelsAsync(client),
                //     "receipt" => UseReceipt(receiptClient, args),
                //     "layout" => UseLayout(layoutClient, args),
                //     _ => throw new NotSupportedException(),
                // });

                await Sample_02_AnalyzeFileWithCustomModel(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task Sample_01_TrainAsync(CustomFormClient client)
        {
            // setup
            // var endpoint = new Uri("{your_endpoint}");
            // var credential = new CognitiveKeyCredential("{your_service_key}");
            // var client = new CustomFormClient(endpoint, credential);

            // operation
            // var source = "{your_blob_container_sas_url}";
            var source = "https://chstoneforms.blob.core.windows.net/samples?st=2020-02-09T12%3A38%3A23Z&se=2030-02-10T12%3A38%3A00Z&sp=rl&sv=2018-03-28&sr=c&sig=axqLT%2FPoLtg3BhKg6iVPlaVsp%2FVTeDqhiVleeBviogw%3D";
            var operation = client.StartTraining(source);
            Console.WriteLine($"Created model with id {operation.Id}");

            // wait for completion
            var response = await operation.WaitForCompletionAsync();

            // examine model
            var model = response.Value;
            Console.WriteLine("Information:");
            Console.WriteLine($"  Id: {model.Information.Id}");
            Console.WriteLine($"  Status: {model.Information.Status}");
            Console.WriteLine($"  Duration: '{model.Information.TrainingDuration}'");
            Console.WriteLine("Documents:");
            foreach (var document in model.Documents)
            {
                Console.WriteLine($"- Name: {document.Name}");
                Console.WriteLine($"  Status: {document.Status}");
                Console.WriteLine($"  Pages: {document.PageCount}");
                if (document.Errors.Any())
                {
                    Console.WriteLine($"  Errors:");
                    foreach (var error in document.Errors)
                    {
                        Console.WriteLine($"  - {error.Message}");
                    }
                }
            }
            Console.WriteLine("DocumentClusters:");
            foreach (var documentCluster in model.DocumentKeyClusters)
            {
                Console.WriteLine($"  ClusterId: {documentCluster.Key}");
                foreach (var key in documentCluster.Value)
                {
                    Console.WriteLine($"  - '{key}'");
                }
            }
            if (model.Errors.Any())
            {
                Console.WriteLine("Errors:");
                foreach (var error in model.Errors)
                {
                    Console.WriteLine($"- {error.Message}");
                }
            }
        }

        private static async Task Sample_02_AnalyzeFileWithCustomModel(CustomFormClient client)
        {
            // setup
            // var endpoint = new Uri("{your_endpoint}");
            // var credential = new CognitiveKeyCredential("{your_service_key}");
            // var client = new CustomFormClient(endpoint, credential);

            var modelId = "d2ab67d1-44a8-4268-90c4-cc31f6660d4d";
            var model = client.GetModelReference(modelId);
            var stream = File.OpenRead("/Users/christopherstone/Downloads/sample_data/Test/Invoice_6.pdf");

            // operation
            var operation = await model.StartAnalyzeAsync(stream);
            var response = await operation.WaitForCompletionAsync();
            var result = response.Value;

            // examine result information
            Console.WriteLine("Information:");
            Console.WriteLine($"  Status: {result.Status}");
            Console.WriteLine($"  Duration: '{result.Duration}'");
            Console.WriteLine($"  Version: '{result.Version}'");

            // examine result fields
            Console.WriteLine("Fields:");
            foreach (var extraction in result.Fields)
            {
                Console.WriteLine($"- Field: '{extraction.Field.Text}'");
                Console.WriteLine($"  Value: '{extraction.Value.Text}'");
                Console.WriteLine($"  ClusterId: {extraction.ClusterId}");
                Console.WriteLine($"  Page: {extraction.PageNumber}");
            }

            // examine result tables
            Console.WriteLine("Tables:");
            foreach (var table in result.Tables)
            {
                table.WriteAscii(Console.Out);
            }
        }

        private static async Task Sample_08_TrainWithLabelsAsync(CustomFormClient client)
        {
            // setup
            // var endpoint = new Uri("{your_endpoint}");
            // var credential = new CognitiveKeyCredential("{your_service_key}");
            // var client = new CustomFormClient(endpoint, credential);

            // operation
            // var source = "{your_blob_container_sas_url}";
            var source = "https://chstoneforms.blob.core.windows.net/samples2?st=2020-02-08T20%3A53%3A41Z&se=2030-02-09T20%3A53%3A00Z&sp=rl&sv=2018-03-28&sr=c&sig=rM43twY2fnaNugFqNMfVFCi03IhtMFqZJ2cs0ccs3w8%3D";
            var operation = await client.StartTrainingWithLabelsAsync(source);
            Console.WriteLine($"Created model with id {operation.Id}");

            // wait for completion
            var response = await operation.WaitForCompletionAsync();

            // examine model metadata
            var model = response.Value;
            Console.WriteLine("Information:");
            Console.WriteLine($"  Id: {model.Information.Id}");
            Console.WriteLine($"  Status: {model.Information.Status}");
            Console.WriteLine($"  Duration: '{model.Information.TrainingDuration}'");
            Console.WriteLine($"  Accuracy: {model.AverageAccuracy}");

            // examine model documents
            Console.WriteLine("Documents:");
            foreach (var document in model.Documents)
            {
                Console.WriteLine($"- Name: {document.Name}");
                Console.WriteLine($"  Status: {document.Status}");
                Console.WriteLine($"  Pages: {document.PageCount}");
                if (document.Errors.Any())
                {
                    Console.WriteLine($"  Errors:");
                    foreach (var error in document.Errors)
                    {
                        Console.WriteLine($"  - {error.Message}");
                    }
                }
            }

            // examine model field accuracy report
            Console.WriteLine("Fields:");
            foreach (var field in model.Fields)
            {
                Console.WriteLine($"- Name: {field.Name}");
                Console.WriteLine($"  Accuracy: {field.Accuracy}");
            }

            // examine model errors
            if (model.Errors.Any())
            {
                Console.WriteLine("Errors:");
                foreach (var error in model.Errors)
                {
                    Console.WriteLine($"- {error.Message}");
                }
            }

            // a61aba7f-98fd-49af-94f3-3e32695bb93f
        }

        private static async Task UseLayout(FormLayoutClient client, string[] args)
        {
            var op = args[1];
            await (op switch
            {
                "analyze" => AnalyzeLayoutAsync(client, args),
                "analysis" => GetLayoutAnalysisAsync(client, args),
                "analysisResult" => GetLayoutAnalysisResultAsync(client, args),
                _ => throw new NotSupportedException(),
            });
        }

        private static async Task AnalyzeLayoutAsync(FormLayoutClient client, string[] args)
        {
            var type = args[2];
            await (type switch
            {
                "file" => AnalyzeLayoutFileAsync(client, args),
                "url" => AnalyzeLayoutUrlAsync(client, args),
                _ => throw new InvalidOperationException(),
            });
        }

        private static async Task AnalyzeLayoutFileAsync(FormLayoutClient client, string[] args)
        {
            var filePath = args[3];
            var stream = File.OpenRead(filePath);
            var op = await client.StartAnalyzeAsync(stream);
            Console.WriteLine($"Created request with id {op.Id}");
            Console.WriteLine("Waiting for completion...");
            await op.WaitForCompletionAsync(TimeSpan.FromSeconds(1));
            if (op.HasValue)
            {
                Console.WriteLine($"Status: {op.Value.Status}");
            }
            else
            {
                Console.WriteLine("error!");
            }
        }

        private static async Task AnalyzeLayoutUrlAsync(FormLayoutClient client, string[] args)
        {
            var url = new Uri(args[3]);
            var op = await client.StartAnalyzeAsync(url);
            Console.WriteLine($"Created request with id {op.Id}");
            Console.WriteLine("Waiting for completion...");
            await op.WaitForCompletionAsync(TimeSpan.FromSeconds(1));
            if (op.HasValue)
            {
                Console.WriteLine($"Status: {op.Value.Status}");
            }
            else
            {
                Console.WriteLine("error!");
            }
        }

        private static async Task GetLayoutAnalysisAsync(FormLayoutClient client, string[] args)
        {
            var modelId = args[1];
            var resultId = args[2];
            var op = client.StartAnalyze(resultId);
            var result = await op.WaitForCompletionAsync();

        }

        private static async Task GetLayoutAnalysisResultAsync(FormLayoutClient client, string[] args)
        {
            var modelId = args[1];
            var resultId = args[2];
            var result = await client.GetAnalysisResultAsync(resultId);
            Console.WriteLine(result.Value.Status);
            Console.WriteLine(result.Value.CreatedOn);
            Console.WriteLine(result.Value.LastUpdatedOn);
        }

        private static async Task UseReceipt(ReceiptClient client, string[] args)
        {
            var op = args[1];
            await (op switch
            {
                "analyze" => AnalyzeReceiptAsync(client, args),
                "analysis" => GetReceiptAnalysisAsync(client, args),
                "analysisResult" => GetReceiptAnalysisResultAsync(client, args),
                _ => throw new NotSupportedException(),
            });
        }

        private static async Task AnalyzeReceiptAsync(ReceiptClient client, string[] args)
        {
            var type = args[2];
            await (type switch
            {
                "file" => AnalyzeReceiptFileAsync(client, args),
                "url" => AnalyzeReceiptUrlAsync(client, args),
                _ => throw new InvalidOperationException(),
            });
        }

        private static async Task AnalyzeReceiptFileAsync(ReceiptClient client, string[] args)
        {
            var filePath = args[3];
            var stream = File.OpenRead(filePath);
            var op = await client.StartAnalyzeAsync(stream, null, true);
            Console.WriteLine($"Created request with id {op.Id}");
            Console.WriteLine("Waiting for completion...");
            await op.WaitForCompletionAsync(TimeSpan.FromSeconds(1));
            if (op.HasValue)
            {
                foreach (var receipt in op.Value.Receipts)
                {
                    WriteReceipt(Console.Out, receipt);
                }
            }
            else
            {
                Console.WriteLine("error!");
            }
        }

        private static async Task AnalyzeReceiptUrlAsync(ReceiptClient client, string[] args)
        {
            var url = new Uri(args[3]);
            var op = await client.StartAnalyzeAsync(url);
            Console.WriteLine($"Created request with id {op.Id}");
            Console.WriteLine("Waiting for completion...");
            await op.WaitForCompletionAsync(TimeSpan.FromSeconds(1));
            if (op.HasValue)
            {
                foreach (var receipt in op.Value.Receipts)
                {
                    WriteReceipt(Console.Out, receipt);
                }
            }
            else
            {
                Console.WriteLine("error!");
            }
        }

        private static async Task GetReceiptAnalysisAsync(ReceiptClient client, string[] args)
        {
            var modelId = args[1];
            var resultId = args[2];
            var op = client.StartAnalyze(resultId);
            var result = await op.WaitForCompletionAsync();
            foreach (var receipt in result.Value.Receipts)
            {
                WriteReceipt(Console.Out, receipt);
            }
        }

        private static async Task GetReceiptAnalysisResultAsync(ReceiptClient client, string[] args)
        {
            var modelId = args[1];
            var resultId = args[2];
            var result = await client.GetAnalysisResultAsync(resultId);
            foreach (var receipt in result.Value.Receipts)
            {
                WriteReceipt(Console.Out, receipt);
            }
        }

        private static async Task DeleteModelAsync(CustomFormClient client, string[] args)
        {
            var modelId = args[1];
            await client.GetModelReference(modelId).DeleteAsync();
            Console.WriteLine("Deleted!");
        }

        private static async Task AnalyzeAsync(CustomFormClient client, string[] args)
        {
            var modelId = args[1];
            var type = args[2];
            await (type switch
            {
                "file" => AnalyzeFileAsync(client, modelId, args),
                "url" => AnalyzeUrlAsync(client, modelId, args),
                _ => throw new InvalidOperationException(),
            });
        }

        private static async Task GetAnalysisAsync(CustomFormClient client, string[] args)
        {
            var modelId = args[1];
            var resultId = args[2];
            var op = client.GetModelReference(modelId).StartAnalyze(resultId);
            var result = await op.WaitForCompletionAsync();
            if (op.HasValue)
            {
                WriteTables(Console.Out, op.Value.Tables);
                foreach (var field in op.Value.Fields)
                {
                    Console.WriteLine($"{field.Field.Text}: {field.Value.Text}");
                }
            }
            else
            {
                Console.WriteLine("error!");
            }
        }

        private static async Task GetAnalysisResultAsync(CustomFormClient client, string[] args)
        {
            var modelId = args[1];
            var resultId = args[2];
            var result = await client.GetModelReference(modelId).GetAnalysisResultAsync(resultId);

            WriteTables(Console.Out, result.Value.Tables);
            foreach (var field in result.Value.Fields)
            {
                Console.WriteLine($"{field.Field.Text}: {field.Value.Text}");
            }
        }

        private static async Task AnalyzeFileAsync(CustomFormClient client, string modelId, string[] args)
        {
            var filePath = args[3];
            var stream = File.OpenRead(filePath);
            var op = await client.GetModelReference(modelId).StartAnalyzeAsync(stream);
            Console.Error.WriteLine($"Created request with id {op.Id}");
            Console.Error.WriteLine("Waiting for completion...");
            await op.WaitForCompletionAsync(TimeSpan.FromSeconds(1));
            if (op.HasValue)
            {
                // Console.WriteLine($"Status: {op.Value.Status}");
                PrintResponse(op.GetRawResponse());
            }
            else
            {
                Console.WriteLine("error!");
            }
        }

        private static async Task AnalyzeUrlAsync(CustomFormClient client, string modelId, string[] args)
        {
            var url = new Uri(args[3]);
            var op = await client.GetModelReference(modelId).StartAnalyzeAsync(url);
            Console.Error.WriteLine($"Created request with id {op.Id}");
            Console.Error.WriteLine("Waiting for completion...");
            await op.WaitForCompletionAsync(TimeSpan.FromSeconds(1));
            if (op.HasValue)
            {
                // Console.WriteLine($"Status: {op.Value.Status}");
                PrintResponse(op.GetRawResponse());
            }
            else
            {
                Console.WriteLine("error!");
            }
        }

        private static void PrintResponse<T>(Response<T> response)
        {
            var mem = new MemoryStream();
            response.GetRawResponse().ContentStream.Position = 0;
            response.GetRawResponse().ContentStream.CopyTo(mem);
            var body = Encoding.UTF8.GetString(mem.ToArray());
            Console.WriteLine(body);
        }

        private static void PrintResponse(Response response)
        {
            var mem = new MemoryStream();
            response.ContentStream.Position = 0;
            response.ContentStream.CopyTo(mem);
            var body = Encoding.UTF8.GetString(mem.ToArray());
            Console.WriteLine(body);
        }

        private static async Task GetModelAsync(CustomFormClient client, string[] args)
        {
            var modelId = args[1];
            var model = await client.GetModelReference(modelId).GetAsync(includeKeys: true);
            foreach (var document in model.Value.Documents)
            {
                Console.Error.WriteLine($"{document.Name}: {document.Status} - {document.PageCount} page(s) - {document.Errors.Length} errors.");
            }
            PrintResponse(model);
        }

        private static async Task TrainAsync(CustomFormClient client, string[] args)
        {
            var source = args[1];
            var prefix = args.Length == 3 ? args[2] : default;
            var op = await client.StartTrainingWithLabelsAsync(source, new SourceFilter(prefix));

            Console.WriteLine("Waiting for completion...");
            await op.WaitForCompletionAsync(TimeSpan.FromSeconds(10));
            if (op.HasValue)
            {
                Console.WriteLine($"Status: {op.Value.Information.Status}");
            }
            else
            {
                var model = await client.GetModelReference(op.Id).GetAsync();
                foreach (var error in model.Value.Errors)
                {
                    Console.WriteLine(error);
                }
            }
        }

        private static async Task GetModelsSummaryAsync(CustomFormClient client)
        {
            var resp = await client.GetSummaryAsync();
            Console.WriteLine($"Count: {resp.Value.Count}");
            Console.WriteLine($"Limit: {resp.Value.Limit}");
            Console.WriteLine($"Last Updated: {resp.Value.LastUpdatedOn}");
        }

        private static void ListModels(CustomFormClient client)
        {
            foreach (var modelInfo in client.ListModels())
            {

            }
        }

        private static void WriteRequestId(TextWriter writer, Response response)
        {
            if (response.Headers.TryGetValue("apim-request-id", out string requestId))
            {
                writer.WriteLine($"Request Id: {requestId}");
            }
        }

        private static void WriteReceipt(TextWriter writer, ReceiptExtraction receipt)
        {
            writer.WriteLine($"Receipt {receipt.ReceiptType}:");
            writer.WriteLine($"\tMerchant: {receipt.MerchantName} | {receipt.MerchantAddress} | {receipt.MerchantPhoneNumber}");
            writer.WriteLine($"\tTransaction: {receipt.TransactionDate} {receipt.TransactionTime}");
            writer.WriteLine($"\tItems:");
            foreach (var item in receipt.Items.Value)
            {
                writer.WriteLine($"\t\tName: {item.Name}");
                writer.WriteLine($"\t\tQuantity: {item.Quantity}");
                writer.WriteLine($"\t\tTotal Price: {item.TotalPrice}");
            }
            writer.WriteLine($"\tSubtotal: {receipt.Subtotal}");
            writer.WriteLine($"\tTax: {receipt.Tax}");
            writer.WriteLine($"\tTip: {receipt.Tip}");
            writer.WriteLine($"\tTotal: {receipt.Total}");
        }

        private static async Task ListModelsAsync(CustomFormClient client)
        {
            var models = client.ListModelsAsync();
            await foreach (var model in models)
            {
                Console.WriteLine($"{model.Id} - {model.Status} ({model.LastUpdatedOn})");
            }
        }

        private static void WriteTables(TextWriter writer, DataTable[] tables)
        {
            foreach (var table in tables)
            {
                writer.WriteLine($"Page {table.PageNumber} table:");
                table.WriteAscii(writer);
            }
        }
    }
}
