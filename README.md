# HttpContextMoq

Easy mocking for ASP.NET Core HttpContext.

HttpContextMoq is an implementation of `AspNetCore.Http.HttpContext` that stores a Mock<HttpContext> instance and works as a proxy for the real Mock.

## Instalation

## Usage

Basic GET request:
```csharp

var context = new HttpContextMock()
    .SetupUrl("http://localhost:8000/path")
    .SetupRequestMethod("GET");
```

POST request (with body):

```csharp
var data = Encoding.UTF8.GetBytes("{\"Foo\":\"Bar\"");

var context = new HttpContextMock()
    .SetupUrl("http://localhost:8000/path")
    .SetupRequestMethod("POST")
    .SetupRequestContentType("application/json")
    .SetupRequestBody(new MemoryStream(data))
    .SetupRequestContentLength(data.Length);
```

Request/Response pair, usefull for testing Action Filters:

```csharp
var data = Encoding.UTF8.GetBytes("{\"Foo\":\"Bar\"");

var context = new HttpContextMock()
    .SetupUrl("http://localhost:8000/path")
    .SetupRequestMethod("GET")
    .SetupResponseContentType("application/json")
    .SetupResponseBody(new MemoryStream(data))
    .SetupResponseContentLength(data.Length);
```

## Development

Open the solution `src\HttpContextMoq.sln` on Visual Studio.

### How to build the library?

 Open the solution file `src\HttpContextMoq.sln` with Visual Studio, and Build the Solution (Build -> Build Solution)

 or

 Execute the following make command.
 ```
 make build
 ```

 ### How to run the unit tests?

 Open the solution file `src\HttpContextMoq.sln` with Visual Studio, and run the unit tests (Test -> Run All Tests)

 or

 Execute the following make command.
 ```
 make test
 ```

 ### How to pack the library?

 Open the solution file `src\HttpContextMoq.sln` with Visual Studio, and pack the HttpContextMoq (Build -> Pack HttpContextMoq)

  ```
 make pack
 ```

 ## Contributing

Please read [contributing](CONTRIBUTING.md) for details of the code of conduct, and the process for submitting pull requests to us.

## Versioning

Uses [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository][tags].

[tags]: https://github.com/tiagodaraujo/httpcontextmoq/tags

## Authors

* **Tiago Araújo** - *Initial work* - [tiagodaraujo](https://github.com/tiagodaraujo)

See also the list of [contributors](https://github.com/tiagodaraujo/httpcontextmoq/contributors) who participated in this project.