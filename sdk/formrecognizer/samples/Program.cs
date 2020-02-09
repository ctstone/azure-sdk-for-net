// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
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

                var op = args.Length > 0 ? args[0] : String.Empty;
                var endpoint = new Uri(Environment.GetEnvironmentVariable("FR_ENDPOINT"));
                var credential = new CognitiveKeyCredential(Environment.GetEnvironmentVariable("FR_KEY"));
                var options = new FormClientOptions();
                // options.Diagnostics.IsLoggingEnabled = true;
                var client = new CustomFormClient(endpoint, credential);
                var layoutClient = new FormLayoutClient(endpoint, credential);
                var receiptClient = new ReceiptClient(endpoint, credential);

                await (op switch
                {
                    "train" => TrainAsync(client, args),
                    "model" => GetModelAsync(client, args),
                    "analyze" => AnalyzeAsync(client, args),
                    "analysis" => GetAnalysisAsync(client, args),
                    "analysisResult" => GetAnalysisResultAsync(client, args),
                    "summary" => GetModelsSummaryAsync(client),
                    "delete" => DeleteModelAsync(client, args),
                    "list" => ListModelsAsync(client),
                    "receipt" => UseReceipt(receiptClient, args),
                    "layout" => UseLayout(layoutClient, args),
                    _ => throw new NotSupportedException(),
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
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
                Console.WriteLine($"Status: {op.Value.Status}");
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
                Console.WriteLine($"Status: {op.Value.Status}");
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
            Console.WriteLine(result.Value.Status);
        }

        private static async Task GetReceiptAnalysisResultAsync(ReceiptClient client, string[] args)
        {
            var modelId = args[1];
            var resultId = args[2];
            var result = await client.GetAnalysisResultAsync(resultId);
            Console.WriteLine(result.Value.Status);
            Console.WriteLine(result.Value.CreatedOn);
            Console.WriteLine(result.Value.LastUpdatedOn);
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
                PrintTables(Console.Out, op.Value.Tables);

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
            Console.WriteLine(result.Value.Status);
            Console.WriteLine(result.Value.CreatedOn);
            Console.WriteLine(result.Value.LastUpdatedOn);
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
                Console.Error.WriteLine($"{document.DocumentName}: {document.Status} - {document.Pages} page(s) - {document.Errors.Length} errors.");
            }
            PrintResponse(model);
        }

        private static async Task TrainAsync(CustomFormClient client, string[] args)
        {
            var source = args[1];
            var prefix = args.Length == 3 ? args[2] : default;
            var op = await client.StartLabeledTrainingAsync(source, new SourceFilter(prefix));

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

        private static async Task ListModelsAsync(CustomFormClient client)
        {
            var models = client.ListModelsAsync();
            await foreach (var model in models)
            {
                Console.WriteLine($"{model.ModelId} - {model.Status} ({model.LastUpdatedOn})");
            }
        }

        private static void PrintTables(TextWriter writer, DataTable[] tables)
        {
            foreach (var table in tables)
            {
                writer.WriteLine($"Page {table.PageNumber} table:");
                table.WriteAscii(writer);
            }
        }
    }
}
