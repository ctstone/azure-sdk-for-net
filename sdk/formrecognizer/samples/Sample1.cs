// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Custom;
using Azure.AI.FormRecognizer.Models;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Azure.AI.FormRecognizer.Samples
{
    public class Sample1
    {
        public static async Task Main(string[] args)
        {
            try
            {
                //string endpoint = Environment.GetEnvironmentVariable("FORM_RECOGNIZER_ENDPOINT");
                //string subscriptionKey = Environment.GetEnvironmentVariable("FORM_RECOGNIZER_SUBSCRIPTION_KEY");

                //var options = new FormRecognizerClientOptions();

                //var credential = new CognitiveHeaderCredential(new HttpHeader("apim-subscription-id", subscriptionKey));

                //var client = new FormRecognizerClient(new Uri(endpoint), credential, options);
                //var layoutClient = new FormLayoutClient(new Uri(endpoint), credential);
                //var receiptClient = new FormReceiptClient(new Uri(endpoint), credential);

                //await TrainModel();
                await Analyze();

                //await (op switch
                //{
                //    "train" => TrainAsync(client, args),
                //    "model" => GetModelAsync(client, args),
                //    "analyze" => AnalyzeAsync(client, args),
                //    "analysis" => GetAnalysisAsync(client, args),
                //    "analysisResult" => GetAnalysisResultAsync(client, args),
                //    "summary" => GetModelsSummaryAsync(client),
                //    "delete" => DeleteModelAsync(client, args),
                //    "list" => ListModelsAsync(client),
                //    "receipt" => UseReceipt(receiptClient, args),
                //    "layout" => UseLayout(layoutClient, args),
                //    _ => throw new NotSupportedException(),
                //});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task TrainModel()
        {
            string endpoint = Environment.GetEnvironmentVariable("FORM_RECOGNIZER_ENDPOINT");
            string subscriptionKey = Environment.GetEnvironmentVariable("FORM_RECOGNIZER_SUBSCRIPTION_KEY");

            var options = new FormRecognizerClientOptions();

            var credential = new CognitiveKeyCredential(subscriptionKey);
            ///var credential = new CognitiveHeaderCredential(new HttpHeader("apim-subscription-id", subscriptionKey));

            var client = new FormRecognizerClient(new Uri(endpoint), credential, options);

            //var source = args[1];
            //var prefix = args.Length == 3 ? args[2] : default;
            TrainingOperation op = await client.StartTrainingAsync(new TrainingRequest
            {
                Source = "https://annelostorage01.blob.core.windows.net/container-formreco?sp=rl&st=2020-02-01T03:54:59Z&se=2020-02-02T03:54:59Z&sv=2019-02-02&sr=c&sig=%2FlZqrmWSI%2FZ%2B9TeWdJynZfGzQmLws9zz7NB5foEjPjg%3D",
                //SourceFilter = new SourceFilter { Prefix = "" },
            });

            Console.WriteLine($"Created model with id {op.Id}");
            Console.WriteLine("Waiting for completion...");
            await op.WaitForCompletionAsync(TimeSpan.FromSeconds(1));
            if (op.HasValue)
            {
                Models.FormRecognizerCustomModel model = op.Value;
                //var modelInfo = model.ModelInfo
                //model.TrainResult.

                // TODO: How should this be used?

                Console.WriteLine($"Status: {op.Value.ModelInfo.Status}");
            }
            else
            {
                Console.WriteLine("error!");
            }
        }

        private static async Task Analyze()
        {
            string endpoint = Environment.GetEnvironmentVariable("FORM_RECOGNIZER_ENDPOINT");
            string subscriptionKey = Environment.GetEnvironmentVariable("FORM_RECOGNIZER_SUBSCRIPTION_KEY");
            var options = new FormRecognizerClientOptions();
            var credential = new CognitiveKeyCredential(subscriptionKey);
            var client = new FormRecognizerClient(new Uri(endpoint), credential, options);
            string modelId = "a36ff8a9-d7b3-4ee6-92d0-6e6eb73816c7";

            var filePath = @"C:\src\samples\cognitive\formrecognizer\sample_data\Test\Invoice_6.pdf";
            var stream = File.OpenRead(filePath);
            var op = await client.GetModelReference(modelId).StartAnalyzeAsync(stream, null, includeTextDetails: false);
            Console.Error.WriteLine($"Created request with id {op.Id}");
            Console.Error.WriteLine("Waiting for completion...");
            await op.WaitForCompletionAsync(TimeSpan.FromSeconds(1));
            if (op.HasValue)
            {
                var keyText = op.Value.AnalyzeResult.PageResults[0].KeyValuePairs[0].Key.Text;
                var valueText = op.Value.AnalyzeResult.PageResults[0].KeyValuePairs[0].Value.Text;

                var fieldName = op.Value.AnalyzeResult.DocumentResults[0].Fields.Keys.First();
                var fieldValue = op.Value.AnalyzeResult.DocumentResults[0].Fields[fieldName].Text;

                //Analysis analysis = op.Value;
                //var documentResults = analysis.AnalyzeResult.DocumentResults;
                //var pageResults = analysis.AnalyzeResult.PageResults;
                //var readResults = analysis.AnalyzeResult.ReadResults;
                ////readResults[0].
                ////pageResults[0].
                //documentResults[0].Fields["key"].

                ////foreach (var documentResult in documentResults)
                ////{
                ////}

                //// Console.WriteLine($"Status: {op.Value.Status}");
                //PrintResponse(op.GetRawResponse());
            }
            else
            {
                Console.WriteLine("error!");
            }
        }

        private static void PrintResponse(Response response)
        {
            var mem = new MemoryStream();
            response.ContentStream.Position = 0;
            response.ContentStream.CopyTo(mem);
            var body = Encoding.UTF8.GetString(mem.ToArray());
            Console.WriteLine(body);
        }
    }
}
