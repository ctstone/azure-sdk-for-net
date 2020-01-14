// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Core;

namespace Azure.AI.FormRecognizer.Samples
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                var op = args.Length > 0 ? args[0] : String.Empty;
                var options = new FormRecognizerClientOptions(extraHeaders: new[] { new HttpHeader("apim-subscription-id", "123") });
                var client = new FormRecognizerClient(new Uri("http://192.168.1.4:5000"), String.Empty, options);

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
                    "receipt" => UseReceipt(client, args),
                    "layout" => UseLayout(client, args),
                    _ => throw new NotSupportedException(),
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task UseLayout(FormRecognizerClient client, string[] args)
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

        private static async Task AnalyzeLayoutAsync(FormRecognizerClient client, string[] args)
        {
            var type = args[2];
            await (type switch
            {
                "file" => AnalyzeLayoutFileAsync(client, args),
                "url" => AnalyzeLayoutUrlAsync(client, args),
                _ => throw new InvalidOperationException(),
            });
        }

        private static async Task AnalyzeLayoutFileAsync(FormRecognizerClient client, string[] args)
        {
            var filePath = args[3];
            var stream = File.OpenRead(filePath);
            var op = await client.Layout.StartAnalyzeAsync(stream);
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

        private static async Task AnalyzeLayoutUrlAsync(FormRecognizerClient client, string[] args)
        {
            var url = new Uri(args[3]);
            var op = await client.Layout.StartAnalyzeAsync(url);
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

        private static async Task GetLayoutAnalysisAsync(FormRecognizerClient client, string[] args)
        {
            var modelId = args[1];
            var resultId = args[2];
            var op = client.Layout.StartAnalyze(resultId);
            var result = await op.WaitForCompletionAsync();
            Console.WriteLine(result.Value.Status);
        }

        private static async Task GetLayoutAnalysisResultAsync(FormRecognizerClient client, string[] args)
        {
            var modelId = args[1];
            var resultId = args[2];
            var result = await client.Layout.GetAnalysisResultAsync(resultId);
            Console.WriteLine(result.Value.Status);
            Console.WriteLine(result.Value.CreatedDateTime);
            Console.WriteLine(result.Value.LastUpdatedDateTime);
        }

        private static async Task UseReceipt(FormRecognizerClient client, string[] args)
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

        private static async Task AnalyzeReceiptAsync(FormRecognizerClient client, string[] args)
        {
            var type = args[2];
            await (type switch
            {
                "file" => AnalyzeReceiptFileAsync(client, args),
                "url" => AnalyzeReceiptUrlAsync(client, args),
                _ => throw new InvalidOperationException(),
            });
        }

        private static async Task AnalyzeReceiptFileAsync(FormRecognizerClient client, string[] args)
        {
            var filePath = args[3];
            var stream = File.OpenRead(filePath);
            var op = await client.Prebuilt.Receipt.StartAnalyzeAsync(stream, null, true);
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

        private static async Task AnalyzeReceiptUrlAsync(FormRecognizerClient client, string[] args)
        {
            var url = new Uri(args[3]);
            var op = await client.Prebuilt.Receipt.StartAnalyzeAsync(url);
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

        private static async Task GetReceiptAnalysisAsync(FormRecognizerClient client, string[] args)
        {
            var modelId = args[1];
            var resultId = args[2];
            var op = client.Prebuilt.Receipt.StartAnalyze(resultId);
            var result = await op.WaitForCompletionAsync();
            Console.WriteLine(result.Value.Status);
        }

        private static async Task GetReceiptAnalysisResultAsync(FormRecognizerClient client, string[] args)
        {
            var modelId = args[1];
            var resultId = args[2];
            var result = await client.Prebuilt.Receipt.GetAnalysisResultAsync(resultId);
            Console.WriteLine(result.Value.Status);
            Console.WriteLine(result.Value.CreatedDateTime);
            Console.WriteLine(result.Value.LastUpdatedDateTime);
        }

        private static async Task DeleteModelAsync(FormRecognizerClient client, string[] args)
        {
            var modelId = args[1];
            await client.Custom.UseModel(modelId).DeleteAsync();
            Console.WriteLine("Deleted!");
        }

        private static async Task AnalyzeAsync(FormRecognizerClient client, string[] args)
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

        private static async Task GetAnalysisAsync(FormRecognizerClient client, string[] args)
        {
            var modelId = args[1];
            var resultId = args[2];
            var op = client.Custom.UseModel(modelId).StartAnalyze(resultId);
            var result = await op.WaitForCompletionAsync();
            Console.WriteLine(result.Value.Status);
        }

        private static async Task GetAnalysisResultAsync(FormRecognizerClient client, string[] args)
        {
            var modelId = args[1];
            var resultId = args[2];
            var result = await client.Custom.UseModel(modelId).GetAnalysisResultAsync(resultId);
            Console.WriteLine(result.Value.Status);
            Console.WriteLine(result.Value.CreatedDateTime);
            Console.WriteLine(result.Value.LastUpdatedDateTime);
        }

        private static async Task AnalyzeFileAsync(FormRecognizerClient client, string modelId, string[] args)
        {
            var filePath = args[3];
            var stream = File.OpenRead(filePath);
            var op = await client.Custom.UseModel(modelId).StartAnalyzeAsync(stream, null, true);
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

        private static async Task AnalyzeUrlAsync(FormRecognizerClient client, string modelId, string[] args)
        {
            var url = new Uri(args[3]);
            var op = await client.Custom.UseModel(modelId).StartAnalyzeAsync(url);
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

        private static async Task GetModelAsync(FormRecognizerClient client, string[] args)
        {
            var modelId = args[1];
            var model = await client.Custom.UseModel(modelId).GetAsync();
            Console.WriteLine(model.Value.ModelInfo.Status);
        }

        private static async Task TrainAsync(FormRecognizerClient client, string[] args)
        {
            var source = args[1];
            var prefix = args[2];
            var op = await client.Custom.StartTrainAsync(new TrainingRequest
            {
                Source = source,
                SourceFilter = new SourceFilter { Prefix = prefix },
            });

            Console.WriteLine($"Created model with id {op.Id}");
            Console.WriteLine("Waiting for completion...");
            await op.WaitForCompletionAsync(TimeSpan.FromSeconds(1));
            if (op.HasValue)
            {
                Console.WriteLine($"Status: {op.Value.ModelInfo.Status}");
            }
            else
            {
                Console.WriteLine("error!");
            }
        }

        private static async Task GetModelsSummaryAsync(FormRecognizerClient client)
        {
            var resp = await client.Custom.GetSummaryAsync();
            Console.WriteLine($"Count: {resp.Value.Count}");
            Console.WriteLine($"Limit: {resp.Value.Limit}");
            Console.WriteLine($"Last Updated: {resp.Value.LastUpdatedDateTime}");
        }

        private static void ListModels(FormRecognizerClient client)
        {
            foreach (var modelInfo in client.Custom.ListModels())
            {

            }
        }

        private static async Task ListModelsAsync(FormRecognizerClient client)
        {
            await foreach (var foo in client.Custom.ListModelsAsync())
            {
                Console.WriteLine(foo.ModelId);
            }
        }
    }
}
