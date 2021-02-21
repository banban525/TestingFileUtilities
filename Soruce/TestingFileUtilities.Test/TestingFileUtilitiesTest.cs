using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TestingFileUtilities.Test
{
    [TestFixture]
    public class TestingFileUtilitiesTest
    {
        [Test]
        public void Test()
        {
            var workDir = Path.Combine(TestContext.CurrentContext.TestDirectory, TestContext.CurrentContext.Test.Name);
            var testFolder = PhysicalFolder.Create(workDir, PhysicalFolderDeleteType.NoDelete,
                new Empty(),
                new Folder("sub",
                    new Folder("subsub",
                        new Empty(),
                        new TextFile("test.txt", "test.txt"),
                        new BinaryFile("a.dat", new byte[] { 0x01, 0x02 }),
                        new ZipFile("a.zip",
                            new Folder("zip-sub",
                                new Empty(),
                                new TextFile("zip.txt", "zip.txt"),
                                new ZipFile("b.zip",
                                    new TextFile("c.txt", "c.txt")))))),
                new TextFile("root.txt", "root.txt"),
                new Folder("temp"));


            DirectoryAssert.Exists(workDir);
            FileAssert.Exists(Path.Combine(workDir, "root.txt"));
            FileAssert.Exists(Path.Combine(workDir, "sub", "subsub", "test.txt"));
            FileAssert.Exists(Path.Combine(workDir, "sub", "subsub", "a.dat"));
            FileAssert.Exists(Path.Combine(workDir, "sub", "subsub", "a.zip"));

            Path.Combine(workDir, "root.txt").IsFileContent("root.txt");
            Path.Combine(workDir, "sub", "subsub", "test.txt").IsFileContent("test.txt");
            Path.Combine(workDir, "sub", "subsub", "a.dat").IsFileContent(new byte[] { 0x01, 0x02 });

            using (var zip = System.IO.Compression.ZipFile.Open(Path.Combine(workDir, "sub", "subsub", "a.zip"), ZipArchiveMode.Read))
            {
                var paths = zip.Entries.Select(_ => _.FullName).ToArray();
                CollectionAssert.Contains(paths, "zip-sub/zip.txt");
                CollectionAssert.Contains(paths, "zip-sub/b.zip");

                zip.Entries.First(_ => _.FullName == "zip-sub/zip.txt").ExtractToFile(Path.Combine(workDir, "temp", "zip.txt"));
                zip.Entries.First(_ => _.FullName == "zip-sub/b.zip").ExtractToFile(Path.Combine(workDir, "temp", "b.zip"));
            }

            Path.Combine(workDir, "temp", "zip.txt").IsFileContent("zip.txt");

            using (var zip = System.IO.Compression.ZipFile.Open(Path.Combine(workDir, "temp", "b.zip"), ZipArchiveMode.Read))
            {
                Assert.That(zip.Entries.First().FullName, Is.EqualTo("c.txt"));
                using (var stream = zip.Entries.First().Open())
                {
                    Assert.That(new StreamReader(stream, Encoding.UTF8).ReadToEnd(), Is.EqualTo("c.txt"));
                }
            }
        }
    }

    public static class AssertExtensions
    {
        public static void IsFileContent(this string filePath, string content)
        {
            FileAssert.Exists(filePath);

            Assert.That(File.ReadAllText(filePath), Is.EqualTo(content));
        }
        public static void IsFileContent(this string filePath, byte[] content)
        {
            FileAssert.Exists(filePath);

            CollectionAssert.AreEqual(File.ReadAllBytes(filePath), content);
        }
    }
}
