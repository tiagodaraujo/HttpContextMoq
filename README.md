# HttpContextMoq

Easy mocking for ASP.NET Core HttpContext.

HttpContextMoq is an implementation of `AspNetCore.Http.HttpContext` that stores a Mock<HttpContext> instance and works as a proxy for the real Mock.

## Getting Started

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