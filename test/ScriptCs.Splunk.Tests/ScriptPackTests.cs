using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Ploeh.AutoFixture.Xunit;
using ScriptCs.Contracts;
using Should;
using Xunit.Extensions;

namespace ScriptCs.Splunk.Tests
{
    public class ScriptPackTests
    {
        public class TheGetContextMethod
        {
            [Theory, ScriptCsAutoData]
            public void ReturnsANewSplunkPackInstance(ScriptPack scriptPack)
            {
                ((IScriptPack)scriptPack).GetContext().ShouldBeType<SplunkPack>();
            }
        }

        public class TheInitializeMethod
        {
            [Theory, ScriptCsAutoData]
            public void ImportsNamespacesToTheSession([Frozen] Mock<IScriptPackSession> session, ScriptPack scriptPack)
            {
                ((IScriptPack)scriptPack).Initialize(session.Object);
                session.Verify(s => s.ImportNamespace("Splunk.Client"));
                session.Verify(s => s.ImportNamespace("System.Reactive"));
                session.Verify(s => s.ImportNamespace("System.Reactive.Concurrency"));
                session.Verify(s => s.ImportNamespace("System.Reactive.Linq"));
            }

            [Theory, ScriptCsAutoData]
            public void AddsReferencesToTheSession([Frozen] Mock<IScriptPackSession> session, ScriptPack scriptPack)
            {
                ((IScriptPack)scriptPack).Initialize(session.Object);
                session.Verify(s => s.AddReference("System.Threading.Tasks"));
                session.Verify(s => s.AddReference("System.Runtime"));
                session.Verify(s => s.AddReference("System.Dynamic.Runtime"));
            }
        }
    }
}
