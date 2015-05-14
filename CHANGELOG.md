# v2.2.1
Includes v2.2.0, v2.1.3, v2.1.2, v2.1.1

* BUG - Ability to force refresh on files for busy systems
* BUG - Fixed bug with directory not found
* BUG - Fixed bugs related to recycle bin permissions
* TESTS - Added tests for dates, refactored auto-properties
* ENHANCEMENT - Added support for min and max file dates
* ENHANCEMENT - FilesNotMatching support, remove fileName
* ENHANCEMENT - Interfaces used for LineReader and FileMatcher
* ENHANCEMENT - Added nuspec
* ENHANCEMENT - Set dll version to 2.2.1
* ENHANCEMENT - Include pdb and xml files
* ENHANCEMENT - Place P/Invoke calls into SafeNativeMethods class
* ENHANCEMENT - Seal TimeDelayResultsReturner due to Dispose call (not implementing virtual Dispose(bool))
* ENHANCEMENT - Fixed xmldoc comments
* ENHANCEMENT - Enable code analysis and warnings as errors when building for Release

# v2.1.0

* BUG - DirectoryScanner constructor should throw under several conditions
* BUG - IsHidden was the opposite of what was documented and so was the code that used it
* ENHANCEMENT - Get rid of IDirectoryScanner and use an IEnumerable<string> instead
* ENHANCEMENT - Option to ignore system files in DirectoryScanner (default)
* TESTS - Use log4net (NuGet) instead of Debug.Print


# v2.0.0

* Initial import of previously closed-source application code.
