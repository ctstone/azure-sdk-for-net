# Microsoft Azure Form Recognizer SDK

The Microsoft Azure Form Recognizer SDK for .NET allows you to use custom or prebuilt models to analyze the semmantic meaning of form documents.

This directory contains the open source subset of the .NET SDK. For documentation of the complete Azure SDK, please see the [Microsoft Azure .NET Developer Center].

## Features

- Custom Models
    - Train new models from documents stored in an Azure Storage blob container.
    - Extract key-value pairs, tables, and semantic values from a given document.
    - Retrieve, Delete, and List custom models.
- Prebuilt Models
    - Extract field text and semantic values from a given receipt document.
- Layout
    - Extract text and layout information from a given document.

The Form Recognizer client can process documents in any of the supported formats:

- `PDF`
- `JPEG`
- `PNG`
- `TIFF`

Documents are provided either as a local `Stream` object or using a `Uri` reference to an http resource.

The Form Recognizer client supports all generally-available Form Recognizer service APIs:

- `v2.0`

## Getting started

The complete Microsoft Azure SDK can be downloaded from the Microsoft Azure Downloads Page and ships with support for building deployment packages, integrating with tooling, rich command line tooling, and more.

Please review [What is Form Recognizer] if you are not familiar with Azure Form Recognizer.

For the best development experience, developers should use the official Microsoft NuGet packages for libraries. NuGet packages are regularly updated with new functionality and hotfixes.

## Requirements

Microsoft Azure Subscription: To call Microsoft Azure services, you need to first create an account. Sign up for a free trial or use your MSDN subscriber benefits.

## Download Package

See [Azure.AI.FormRecognizer] on NuGet.

## Versioning Information

The Form Recognizer SDK uses the [semantic versioning scheme].

## Prerequisites

The Form Recognizer Client Library shares the same [prerequisites] as the Microsoft Azure SDK for .NET.

## To Build

For information on building the Azure Form Recognizer SDK, please see [Building the Microsoft Azure SDK for .NET].

## Running Tests

Tests for the Azure Form Recognizer SDK are run in the same manner as the rest of the tests for the Azure SDK for .NET. For information please see [How to Run Tests].

## Samples

Code samples for the Azure Form Recognizer SDK are available on [Azure Code Samples].

<!-- Links -->

[Microsoft Azure .NET Developer Center]: http://azure.microsoft.com/en-us/develop/net/
[Microsoft Azure Downloads Page]: http://azure.microsoft.com/en-us/downloads/?sdk=net
[What is Form Recognizer]: https://docs.microsoft.com/en-us/azure/cognitive-services/form-recognizer/overview
[create an account]: https://account.windowsazure.com/Home/Index
[Azure.AI.FormRecognizer]: https://www.nuget.org/packages/Azure.AI.FormRecognizer
[semantic versioning scheme]: http://semver.org/
[prerequisites]: https://github.com/azure/azure-sdk-for-net#prerequisites
[Building the Microsoft Azure SDK for .NET]: https://github.com/azure/azure-sdk-for-net#to-build
[How to Run Tests]: https://github.com/azure/azure-sdk-for-net#to-run-the-tests
[Azure Code Samples]: https://azure.microsoft.com/en-us/resources/samples/?sort=0&service=form-recognizer&platform=dotnet