using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TestingFileUtilities.Test
{
    [TestFixture]
    public class TypeBasedFilesCreationTest
    {
        [Test]
        public void CreateFoldersAndFilesFromType()
        {
            var workDir = Path.Combine(TestContext.CurrentContext.TestDirectory, TestContext.CurrentContext.Test.FullName);

            using var dir = TypeBasePhysicalFolder.CreatePhysicalFolder(workDir, PhysicalFolderDeleteType.DeleteFolder,
                new RootDir());

            FileAssert.Exists(Path.Combine(workDir, "SubFolder", "A.txt"));
            FileAssert.Exists(Path.Combine(workDir, "subFolder2", "B.dat"));
            
            StringAssert.AreEqualIgnoringCase(
                Path.Combine(workDir, "subFolder2"),
                dir.Root.SubFolder2.FolderInfo.FullPath
            );
        }

        class RootDir
        {
            internal FolderFunctions FolderInfo { get; } = Files.FolderFunctions();

            internal class SubFolderType
            {
                internal FolderFunctions FolderInfo { get; init; }
                // ReSharper disable once InconsistentNaming
                internal IPhysicalFile A_txt { get; init; }
            }

            internal SubFolderType SubFolder { get; } = new()
            {
                FolderInfo = Files.FolderFunctions(),
                A_txt = Files.TextFile("A_txt")
            };

            internal class SubFolder2Type
            {
                internal FolderFunctions FolderInfo { get; } = Files.FolderFunctions();
                // ReSharper disable once InconsistentNaming
                internal IPhysicalFile B_dat { get; } = Files.BinaryFile(new byte[] {0x00, 0x01});
            }

            internal SubFolder2Type SubFolder2 { get; } = new();
        }

    }


}
