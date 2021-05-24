using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace TestingFileUtilities.TypeGenerator.Test
{
    [TestFixture]
    public class OtherNamespaceTest
    {
        private TypeBasePhysicalFolder<OtherNamespace.TestFolder> _workDir;
        [SetUp]
        public void Setup()
        {
            var rootDir = Path.Combine(TestContext.CurrentContext.TestDirectory, TestContext.CurrentContext.Test.FullName);

            _workDir = TypeBasePhysicalFolder.CreatePhysicalFolder(rootDir, PhysicalFolderDeleteType.DeleteFolder,
                new OtherNamespace.TestFolder());
        }

        [TearDown]
        public void TearDown()
        {
            _workDir.Dispose();
        }

        [Test]
        public void CanUseSameClassNameWithOtherNamespace()
        {
            File.Exists(_workDir.Root.test2_txt.FullPath);
        }
    }

    namespace OtherNamespace
    {

        public partial class TestFolder
        {
            [FolderTypeGeneratorInitialValue]
            private static readonly dynamic Init = new
            {
                test_txt = Files.TextFile("test.txt content"),
                Dir = new
                {
                },
                test2_txt = Files.TextFile("test2.txt content"),
                binary_dat = Files.BinaryFile(new byte[] { 0x00 })
            };
        }
    }
}
