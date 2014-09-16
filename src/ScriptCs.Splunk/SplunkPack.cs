using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ScriptCs.Contracts;
using Splunk.Client;
using Splunk.Client.Helpers;

namespace ScriptCs.Splunk
{
    public class SplunkPack : IScriptPackContext
    {
        private readonly IFileSystem _fileSystem;

        public SplunkConfig Config { get; private set; }

        public string Username
        {
            get { return Config.Username; }
        }

        public string Password
        {
            get { return Config.Password; }
        }

        public SplunkPack(IFileSystem fileSystem)
        {
            Guard.AgainstNullArgument("fileSystem", fileSystem);

            Config = new SplunkConfig();
            _fileSystem = fileSystem;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };
        }

        public void LoadConfig(string splunkRCPath = null)
        {
            var loader = new SplunkConfigLoader(_fileSystem, splunkRCPath);
            loader.Load(Config);
        }

        public Service CreateService(Namespace ns = null )
        {
            var service = new Service(Config.Scheme, Config.Host, Config.Port, ns);
            return service;
        }

        public Service CreateService(Scheme scheme, string host, int port, Namespace ns = null)
        {
            var service = new Service(scheme, host, port, ns);
            return service;
        }

        public Service CreateServiceAndLogin(Namespace ns = null)
        {
            var service = new Service(Config.Scheme, Config.Host, Config.Port, ns);
            var task = service.LogOnAsync(Config.Username, Config.Password);
            task.Wait();
            return service;
        }
    }
}
