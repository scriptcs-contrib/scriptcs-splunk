using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Ploeh.AutoFixture.Xunit;
using ScriptCs.Contracts;
using Should;
using Splunk.Client.Helpers;
using Xunit;
using Xunit.Extensions;

namespace ScriptCs.Splunk.Tests
{
    public class SplunkConfigLoaderTests
    {
        public class TheConstructor
        {
            [Theory, ScriptCsAutoData]
            public void SetsTheSplunkRCPathToTheProvidedPath(IFileSystem fileSystem)
            {
                var loader = new SplunkConfigLoader(fileSystem, @"c:\test");
                loader.SplunkRCPath.ShouldEqual(@"c:\test");
            }

            [Theory, ScriptCsAutoData]
            public void SetsTheSplunkRCPathToTheHomeDirectoryWhenNoPathIsProvided(SplunkConfigLoader loader)
            {
                loader.SplunkRCPath.ShouldEqual(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".splunkrc"));
            }
        }

        public class TheLoadMethod
        {
            [Theory, ScriptCsAutoData]
            public void ThrowsAnExceptionIfTheFileDoesNotExist(Mock<IFileSystem> mockFileSystem, SplunkConfig config)
            {
                mockFileSystem.Setup(f => f.FileExists(It.IsAny<string>())).Returns(false);
                var loader = new SplunkConfigLoader(mockFileSystem.Object, "test");
                Assert.Throws<FileNotFoundException>(() => loader.Load(config));
            }

            [Theory, ScriptCsAutoData]
            public void LoadsTheFile(Mock<IFileSystem> mockFileSystem)
            {
                var config = LoadTestConfig(mockFileSystem);
                mockFileSystem.Verify(fs => fs.ReadFileLines(It.IsAny<string>()));
            }

            [Theory, ScriptCsAutoData]
            public void SetsTheHost(Mock<IFileSystem> mockFileSystem)
            {
                var config = LoadTestConfig(mockFileSystem);
                config.Host.ShouldEqual("testhost");
            }

            [Theory, ScriptCsAutoData]
            public void SetsThePort(Mock<IFileSystem> mockFileSystem)
            {
                var config = LoadTestConfig(mockFileSystem);
                config.Port.ShouldEqual(1000);
            }

            [Theory, ScriptCsAutoData]
            public void SetsTheUsername(Mock<IFileSystem> mockFileSystem)
            {
                var config = LoadTestConfig(mockFileSystem);
                config.Username.ShouldEqual("testuser");
            }

            [Theory, ScriptCsAutoData]
            public void SetsThePassword(Mock<IFileSystem> mockFileSystem)
            {
                var config = LoadTestConfig(mockFileSystem);
                config.Password.ShouldEqual("testpass");
            }

            private SplunkConfig LoadTestConfig(Mock<IFileSystem> mockFileSystem)
            {
                var config = new SplunkConfig();
                mockFileSystem.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(fs => fs.ReadFileLines(It.IsAny<string>())).Returns(TestConfigLoader.GetSplunkRC());
                var loader = new SplunkConfigLoader(mockFileSystem.Object);
                loader.Load(config);
                return config;
            }

        }
    }
}
