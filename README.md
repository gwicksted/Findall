# Findall

A free open source "find in files" library for .net.

## Goal of library:

* Easy to use
* Extensible (without library modifications)
* Fast
* Stable
* Minimal disk and memory usage
* Thread-safe access to results as they arrive

## Potential Uses:

* Providing text editors and IDEs with "find in files" capability
* Support tools for log files
* GREP tools

## Runtime Requirements:

* .net 4 client profile or better

## License:

* MIT (see LICENSE.md)

## Getting Started:

    SearcherFactory factory = new SearcherFactory
                                  {
                                      FileNamePattern = "*.txt",
                                      Path = @"C:\test\",
                                      Recursive = true,
                                      Hidden = false,
                                      LinePattern = "[a-z]+",
                                      LinesNotMatching = false
                                  };

    Searcher searcher = factory.ConstructSearcher();

    searcher.SearchFinished += s => Debug.Print("Finished with {0} results", s.Matches.Count);

    searcher.Begin();

## Throttled Results As They Arrive:

    SearcherFactory factory = new SearcherFactory
                                  {
                                      FileNamePattern = "*.txt",
                                      Path = @"C:\test\",
                                      Recursive = true,
                                      Hidden = false,
                                      LinePattern = "[a-z]+",
                                      LinesNotMatching = false
                                  };

    Searcher searcher = factory.ConstructSearcher();

    // 1 second throttling
    using (TimeDelayResultsReturner returner = new TimeDelayResultsReturner(searcher, 1000))
    {
        returner.NewResults += r => Debug.Print("Found {0} results...", r.Count);
        returner.Finished += r => Debug.Print("Finished with {0} results", r.Count);
        returner.Begin();
    }

## Development Requirements:

* VS2010
* Resharper (preferred)
* NuGet (NUnit for test project)

## Coding standards:

* No use of "var"
* All functions must have /// comments
* Use yield returns and LINQ to provide optimal memory/processor usage
* Must have unit tests
* Use NuGet for external libraries
* Do not allow construction/execution with invalid arguments
* No long functions or hacks
* No // comments unless it is a TODO item (anything that cannot be explained by /// or understood by code should not be written)
* Braces must always be present and must appear on the line below the statement (exceptions: auto properties, lambdas, array initializers)
* Always include access modifiers
* No third-party dependencies in library (test project is acceptable)
