scriptcs-splunk
===============

# What is it?
A scriptcs wrapper for [Splunk's] (http://www.splunk.com) new [C# Client] (https://github.com/splunk/splunk-sdk-csharp-pcl). You can use it interactively for working with Splunk via the REPL, or use it for automation within your scripts.

# Features
* Perform Splunk searches, send events directly to Splunk, and perform simple management tasks. 
* Supports Reactive Extensions for streaming search results.
* Supports [.splunkrc] (http://dev.splunk.com/view/csharp-sdk/SP-CAAAEPR) files for providing server and connection info. You can use profile-level or specify a specific file.

# Using the Mono engine for async/await on Windows
Splunk's Client is designed to be used with .NET 4.5's async/await keywords. To use async/await in scriptcs on Windows requires explicitly enabling the Mono engine. 
* To load a script file using the Mono engine use: `scriptcs -modules "mono" start.csx`
* To load the REPL using the Mono engine use the -r swtich: `scriptcs -modules "mono" -r`

Note: You do not have to use async/await for ScriptCs.Splunk. However you will need to access the Result object on any methods that return a Task in order to get the value.
# Install
`scriptcs -install ScriptCs.Splunk`

# Sample Usage (Mono)
