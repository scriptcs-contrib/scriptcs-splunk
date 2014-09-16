using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Splunk.Tests
{
    public static class TestConfigLoader
    {
        public static string[] GetSplunkRC()
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ScriptCs.Splunk.Tests.splunkrc.txt");
            var reader = new StreamReader(stream);
            return reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        } 
    }
}
