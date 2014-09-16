using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCs.Contracts;

namespace ScriptCs.Splunk
{
    public class ScriptPack : IScriptPack
    {
        private readonly IFileSystem _fileSystem;

        public ScriptPack(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        [ImportingConstructor]
        public ScriptPack()
        {
            _fileSystem = new FileSystem();
        }

        public void Initialize(IScriptPackSession session)
        {
            Guard.AgainstNullArgument("session", session);

            session.ImportNamespace("Splunk.Client");
            session.ImportNamespace("System.Reactive");
            session.ImportNamespace("System.Reactive.Concurrency");

            session.AddReference("System.Threading.Tasks");
            session.AddReference("System.Runtime");
            session.AddReference("System.Dynamic.Runtime");
        }

        public IScriptPackContext GetContext()
        {
            return new SplunkPack(_fileSystem);
        }

        public void Terminate()
        {
        }
    }
}
