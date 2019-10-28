#! /usr/bin/env bash

autorest \
    --input-file=../../../../../azure-rest-api-specs/specification/cognitiveservices/data-plane/FormRecognizer/preview/v2.0/FormRecognizer.json \
    --output-folder=./Generated \
    --csharp \
    --namespace=Microsoft.Azure.CognitiveServices.FormRecognizer \
    --license-header=MICROSOFT_MIT_NO_VERSION \
    --sync-methods=none \
    --use-datetimeoffset \
    --clear-output-folder \
    --azure-arm \
    --add-credentials
