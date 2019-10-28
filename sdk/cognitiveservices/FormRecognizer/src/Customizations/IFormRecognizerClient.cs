using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
using Microsoft.Rest.Azure;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    /// <summary>
    /// Extracts information from forms and images into structured data.
    /// </summary>
    public partial interface IFormRecognizerClient : System.IDisposable
    {
        Task<AzureOperationHeaderResponse<AnalyzeWithCustomModelHeaders>> AnalyzeWithCustomModelWithHttpMessagesAsync(Guid modelId, AnalyzeRemoteFormRequest request, bool? includeTextDetails, Dictionary<string, List<string>> customHeaders, CancellationToken cancellationToken);

        Task<AzureOperationHeaderResponse<AnalyzeWithCustomModelHeaders>> AnalyzeWithCustomModelWithHttpMessagesAsync(Guid modelId, Stream fileStream, FormContentType contentType, bool? includeTextDetails = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<AzureOperationHeaderResponse<AnalyzeReceiptHeaders>> AnalyzeReceiptWithHttpMessagesAsync(AnalyzeRemoteFormRequest request, Dictionary<string, List<string>> customHeaders, CancellationToken cancellationToken);

        Task<AzureOperationHeaderResponse<AnalyzeReceiptHeaders>> AnalyzeReceiptWithHttpMessagesAsync(Stream fileStream, FormContentType contentType, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<AzureOperationHeaderResponse<AnalyzeLayoutHeaders>> AnalyzeLayoutWithHttpMessagesAsync(AnalyzeRemoteFormRequest request, Dictionary<string, List<string>> customHeaders, CancellationToken cancellationToken);

        Task<AzureOperationHeaderResponse<AnalyzeLayoutHeaders>> AnalyzeLayoutWithHttpMessagesAsync(Stream fileStream, FormContentType contentType, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}