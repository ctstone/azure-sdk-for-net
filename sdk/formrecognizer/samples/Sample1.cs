// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Custom;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

                await TrainModel();


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
                Source = "<SourceUri>",
                //SourceFilter = new SourceFilter { Prefix = "" },
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
    }
}
