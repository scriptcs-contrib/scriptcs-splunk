scriptcs-splunk
===============

# What is it?
A scriptcs wrapper for [Splunk's] (http://www.splunk.com) new [C# Client] (https://github.com/splunk/splunk-sdk-csharp-pcl). You can use it interactively for working with Splunk via the REPL, or use it for automation within your scripts.

# Features
* Perform Splunk searches, send events directly to Splunk, and perform simple management tasks. 
* Supports Reactive Extensions for streaming search results.
* Supports [.splunkrc] (http://dev.splunk.com/view/csharp-sdk/SP-CAAAEPR) files for providing server and connection info. You can use profile-level or specify a specific file.

# Install
`scriptcs -install ScriptCs.Splunk`
