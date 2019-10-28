namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    public class FormContentType
    {
        public static readonly FormContentType Pdf = new FormContentType("application/pdf");
        public static readonly FormContentType Jpeg = new FormContentType("image/jpeg");
        public static readonly FormContentType Tiff = new FormContentType("image/tiff");
        public static readonly FormContentType Png = new FormContentType("image/png");

        private readonly string contentType;

        private FormContentType(string contentType)
        {
            this.contentType = contentType;
        }

        public string ContentType => contentType;
    }
}