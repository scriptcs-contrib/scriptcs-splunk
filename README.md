scriptcs-splunk
===============

# What is it?
A scriptcs wrapper for [Splunk's] (http://www.splunk.com) new [C# Client] (https://github.com/splunk/splunk-sdk-csharp-pcl). You can use it interactively for working with Splunk via the REPL, or use it for automation within your scripts.

# Features
* Perform Splunk searches, send events directly to Splunk, and perform simple management tasks. 
* Supports Reactive Extensions (Rx) for streaming search results.
* Supports [.splunkrc] (http://dev.splunk.com/view/csharp-sdk/SP-CAAAEPR) files for providing server and connection info. You can use profile-level or specify a specific file.

**Note**: You do not have to use async/await for ScriptCs.Splunk. However you will need to access the Result object on any methods that return a Task in order to get the value.
# Install
`scriptcs -install ScriptCs.Splunk`

# Scripting examples
Running a search export
```csharp
var splunk = Require<SplunkPack>();
splunk.LoadConfig(); 
var service = splunk.CreateServiceAndLogin(); 
var results = service.ExportSearchResultsAsync("search index=_internal | head 10").Result;
foreach(var result in results) {
	Console.WriteLine(result.GetValue("_raw"));
}
```

Running a search export using Rx
```csharp
var splunk = Require<SplunkPack>();
splunk.LoadConfig(); 
var service = splunk.CreateServiceAndLogin(); 
var results = service.ExportSearchResultsAsync("search index=_internal | head 10").Result;
results.ToObservable()
  .Subscribe(
    Observer.Create<SearchResult>(r => Console.WriteLine(r.GetValue("_raw")))
  );
```
