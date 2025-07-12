# MizeRestClient

A flexible and extensible REST client library for .NET, providing two implementations—using `HttpClient` and `HttpWebRequest`—to perform HTTP operations with support for basic authentication and custom headers.

---

## Features

- **Fluent API** for configuring REST clients and requests.
- Support for **GET** and **POST** HTTP methods.
- **Basic authentication** support.
- **Custom header** management.
- Two interchangeable implementations:
  - `HttpClient` based (`HttpClientRestClient`).
  - `HttpWebRequest` based (`WebRequestRestClient`).
- **Factory pattern** to easily instantiate clients.

---


### Usage Example

```csharp
using MizeRestClient.Core;
using MizeRestClient.Interfaces;

// Create REST client using HttpClient implementation
IRestClient client = RestClientFactory.CreateHttpClient()
    .WithBaseUrl("https://api.example.com")
    .WithBasicAuth("username", "password")
    .WithHeader("Custom-Header", "value");

// Create a request
var request = client.CreateRequest("/resource/path")
    .WithHeader("Another-Header", "header-value");

// Execute GET
string response = await request.GetAsync();

// Execute POST
string postContent = "{\"key\":\"value\"}";
string postResponse = await request.PostAsync(postContent);
