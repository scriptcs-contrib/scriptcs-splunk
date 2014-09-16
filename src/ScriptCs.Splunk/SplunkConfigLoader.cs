using System;
using System.Collections.Generic;
using System.IO;
using ScriptCs.Contracts;
using ScriptCs.Splunk;

namespace Splunk.Client.Helpers
{
    public class SplunkConfigLoader
    {
        private readonly IFileSystem _fileSystem;

        public string SplunkRCPath { get; private set; }

        public SplunkConfigLoader(IFileSystem fileSystem) : this(fileSystem,null)
        {
        }

        public SplunkConfigLoader(IFileSystem fileSystem, string splunkRCPath)
        {
            Guard.AgainstNullArgument("fileSystem", fileSystem);

            _fileSystem = fileSystem;

            if (splunkRCPath == null)
            {
                splunkRCPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".splunkrc");
            }
            SplunkRCPath = splunkRCPath;

        }

        public void Load(SplunkConfig config)
        {
            if (!_fileSystem.FileExists(SplunkRCPath))
            {
                throw new FileNotFoundException(".splunkrc file not found", SplunkRCPath);
            }

            var lines = _fileSystem.ReadFileLines(SplunkRCPath);
            GetSettings(lines, config);
        }

        private void GetSettings(string[] lines, SplunkConfig config)
        {
            var argList = new List<string>();

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("#", StringComparison.InvariantCulture))
                {
                    continue;
                }

                if (trimmedLine.Length == 0)
                {
                    continue;
                }

                argList.Add(trimmedLine);
            }

            foreach (string arg in argList)
            {
                string[] pair = arg.Split('=');

                switch (pair[0].ToLower().Trim())
                {
                    case "scheme":
                        config.Scheme = pair[1].Trim() == "https" ? Scheme.Https : Scheme.Http;
                        break;
                    case "host":
                        config.Host = pair[1].Trim();
                        break;
                    case "port":
                        config.Port = int.Parse(pair[1].Trim());
                        break;
                    case "username":
                        config.Username = pair[1].Trim();
                        break;
                    case "password":
                        config.Password = pair[1].Trim();
                        break;
                }
            }

        }

    }
}