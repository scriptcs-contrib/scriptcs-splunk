using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ScriptCs.Contracts;
using Should;
using Xunit.Extensions;

namespace ScriptCs.Splunk.Tests
{
    public class SplunkPackTests
    {
        public class TheConstructor
        {
            [Theory, ScriptCsAutoData]
            public void SubscribesToTheCertCallback(SplunkPack pack)
            {
                ServicePointManager.ServerCertificateValidationCallback.ShouldNotBeNull();
            }
        }

        public class TheLoadConfigMethod
        {
            [Theory, ScriptCsAutoData]
            public void LoadsTheSplunkRCFile(Mock<IFileSystem> mockFileSystem)
            {
                mockFileSystem.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(fs => fs.ReadFileLines(It.IsAny<string>())).Returns(new string[]{});
                var pack = new SplunkPack(mockFileSystem.Object);
                pack.LoadConfig("");
                mockFileSystem.Verify(fs=>fs.ReadFileLines(It.IsAny<string>()));
            }
        }

        public class TheCreateServiceMethod
        {
            [Theory, ScriptCsAutoData]
            public void ReturnsANewService(SplunkPack pack)
            {
                pack.CreateService().ShouldNotBeNull();
            }
        }

        public class TheCreateServiceAndLogingMethod
        {
            [Theory, ScriptCsAutoData]
            public void CreatesANewServiceAndLogsIn(SplunkPack pack)
            {
                //var service = pack.CreateServiceAndLogin();
            }
        }
    }
}
