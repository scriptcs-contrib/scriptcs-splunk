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
            var lines = new List<string>();
            lines.Add("# ");
            lines.Add("host=testhost");
            lines.Add("# ");
            lines.Add("username=testuser");
            lines.Add("# ");
            lines.Add("password=testpass");
            lines.Add("# ");
            lines.Add("port=1000");
            lines.Add("# ");
            lines.Add("scheme=http");
            lines.Add("# ");
            return lines.ToArray();
        } 
    }
}


