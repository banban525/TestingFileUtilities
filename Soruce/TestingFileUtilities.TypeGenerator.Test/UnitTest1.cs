using System.IO;
using NUnit.Framework;

namespace TestingFileUtilities.TypeGenerator.Test
{
    public class Tests
    {
        private TypeBasePhysicalFolder<TestFolder> _workDir;
        [SetUp]
        public void Setup()
        {
            var rootDir = Path.Combine(TestContext.CurrentContext.TestDirectory, TestContext.CurrentContext.Test.FullName);

            _workDir = TypeBasePhysicalFolder.CreatePhysicalFolder(rootDir, PhysicalFolderDeleteType.DeleteFolder,
                new TestFolder());
        }

        [TearDown]
        public void TearDown()
        {
            _workDir.Dispose();
        }


        [Test]
        public void Test1()
        {
            FileAssert.Exists(_workDir.Root.test_txt.FullPath);
            FileAssert.Exists(_workDir.Root.Dir.test_pdf.FullPath);
            FileAssert.Exists(_workDir.Root.test2_txt.FullPath);
            FileAssert.Exists(_workDir.Root.Dir.SubDir.text3_txt.FullPath);
        }
    }


    public partial class TestFolder
    {
        [FolderTypeGeneratorInitialValue]
        private static readonly dynamic InitValue = new
        {
            test_txt = Files.TextFile("test.txt content"),
            Dir = new
            {
                test_pdf = Files.BinaryFile(new byte[] { 0x00 }),
                Info = Files.FolderFunctions(),
                SubDir = new
                {
                    Info = Files.FolderFunctions(),
                    text3_txt = Files.TextFile("text3.txt content")
                }
            },
            test2_txt = Files.TextFile("test2.txt content"),
            binary_dat = Files.BinaryFile(new byte[] { 0x00 })
        };
    }
}