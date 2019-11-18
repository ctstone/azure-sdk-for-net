// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    public class AnalysisContentType
    {
        public static AnalysisContentType Pdf = new AnalysisContentType("application/pdf");
        public static AnalysisContentType Jpeg = new AnalysisContentType("image/jpeg");
        public static AnalysisContentType Png = new AnalysisContentType("image/png");
        public static AnalysisContentType Tiff = new AnalysisContentType("image/tiff");

        private string _contentType;

        protected string ContentType => _contentType;

        protected AnalysisContentType(string contentType)
        {
            _contentType = contentType;
        }

        public override string ToString()
        {
            return ContentType;
        }
    }
}