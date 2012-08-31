# v2.1.0

* BUG - DirectoryScanner constructor should throw under several conditions
* BUG - IsHidden was the opposite of what was documented and so was the code that used it
* ENHANCEMENT - Get rid of IDirectoryScanner and use an IEnumerable<string> instead
* ENHANCEMENT - Option to ignore system files in DirectoryScanner (default)
* TESTS - Use log4net (NuGet) instead of Debug.Print


# v2.0.0

* Initial import of previously closed-source application code.
