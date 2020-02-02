# Connect to Private Form Recognizer Container Service

This sample demonstrates how to connect the Form Recognizer client to a private container instance using an arbirary header-based credential (or no credential).

Please review the [Form Recognizer Container] documentation.

## Option 1: Authorize the FormRecognizerClient without a Credential

By default, a Form Recognizer container does not configure any authorization scheme. It's up to the container admin to setup (or not setup) authorization for clients.

> If your container endpoint is behind a private VNET, you might not need authorization.

To connect to a default Form Recognizer container, pass an empty `CognitiveHeaderCredential`:

```csharp
var endpoint = new Uri("http://my_container_host");
var client = new FormRecognizerClient(endpoint, new CognitiveHeaderCredential());
```

The client is ready to use!

## Option 2: Authorize the FormRecognizerClient with a Custom Header Credential

Advanced users may configure custom authorization for containers using a third party ingress proxy. Refer to documentation from your container orchestrator (e.g. Docker Compose or Kubernetes) to learn how to setup an ingress proxy for your container.

Configure the Form Recognizer Client to send custom headers:

```csharp
var credential = new CognitiveHeaderCredential(new HttpHeader("my-custom-api-key", "xyz"));
var endpoint = new Uri("http://my_container_host");
var client = new FormRecognizerClient(endpoint, credential);
```

The client is ready to use!

> If your authorization scheme requires additional headers, pass as many `HttpHeader` instances as you need.

If you need to refresh your credential, you can update the headers at any time. Future requests will use the new credential:

```csharp
credential.UpdateCredential(new [] { new HttpHeader("custom-api-key", "abc")});
```

> The updated credential replaces the original value.

[Form Recognizer Container]: https://docs.microsoft.com/en-us/azure/cognitive-services/form-recognizer/form-recognizer-container-howto