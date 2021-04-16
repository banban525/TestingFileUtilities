using System;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TestingFileUtilities.Test
{
    [TestFixture]
    public class AnonymousTypeBasedFilesCreationTest
    {
        [Test]
        public void CreateFoldersAndFilesFromAnonymousType()
        {
            var workDir = Path.Combine(TestContext.CurrentContext.TestDirectory, TestContext.CurrentContext.Test.FullName);

            using var dir = 
                TypeBasePhysicalFolder.CreatePhysicalFolder(workDir, PhysicalFolderDeleteType.DeleteFolder,
                new
                {
                    A_dat = Files.BinaryFile(new byte[] {0x01}),
                    SubFolder = new
                    {
                        B_txt = Files.TextFile("B_txt"),
                        Reserved_txt = Files.Reserved(),
                        FolderInfo = Files.FolderFunctions(),
                        SubSubFolder = new
                        {
                            E_dat = Files.BinaryFile(new byte[]{ 0x01, 0x02, 0x03 })
                        },
                        EmptyFolder = new
                        {
                            FolderInfo = Files.FolderFunctions(),
                        }
                    },
                    temp_zip = Files.ZipFile(new
                    {
                        C_txt = Files.TextFile("C_txt"),
                        Folder = new
                        {
                            D_pdf = Files.BinaryFile(new byte[] {0x01, 0x02})
                        }
                    }),
                });
            
            FileAssert.Exists(Path.Combine(workDir, "a.dat"));
            FileAssert.Exists(Path.Combine(workDir, "SubFolder", "b.txt"));
            FileAssert.Exists(Path.Combine(workDir, "SubFolder", "SubSubFolder", "e.dat"));
            FileAssert.Exists(Path.Combine(workDir, "temp.zip"));
            DirectoryAssert.Exists(Path.Combine(workDir, "SubFolder", "EmptyFolder"));

            using var zip = System.IO.Compression.ZipFile.OpenRead(dir.Root.temp_zip.FullPath);
            Assert.That(
                zip.Entries.Any(_ => string.Equals(_.FullName, "c.txt", StringComparison.InvariantCultureIgnoreCase)),
                Is.True);
            Assert.That(
                zip.Entries.Any(_ => string.Equals(_.FullName, "Folder/d.pdf", StringComparison.InvariantCultureIgnoreCase)),
                Is.True);


            FileAssert.DoesNotExist(Path.Combine(workDir, "SubFolder", "Reserved.txt"));

            dir.Root.SubFolder.B_txt.Copy(dir.Root.SubFolder.Reserved_txt.FullPath);

            FileAssert.Exists(Path.Combine(workDir, "SubFolder", "Reserved.txt"));
            FileAssert.AreEqual(dir.Root.SubFolder.B_txt.FullPath, 
                dir.Root.SubFolder.Reserved_txt.FullPath);

            // file operation
            var fileSize = dir.Root.A_dat.FileInfo.Length;
            var bTxtContents = dir.Root.SubFolder.B_txt.ReadAllText(Encoding.UTF8);
            var filesInSubFolder = dir.Root.SubFolder.FolderInfo.GetFiles();


        }
    }
}
