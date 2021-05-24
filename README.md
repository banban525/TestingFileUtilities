TestingFileUtilities
====================

[![MIT License](https://img.shields.io/github/license/banban525/TestingFileUtilities.svg)](LICENSE)

[![NuGet version](https://badge.fury.io/nu/TestingFileUtilities.svg)](https://badge.fury.io/nu/TestingFileUtilities)
[![NuGet Download](https://img.shields.io/nuget/dt/TestingFileUtilities.svg)](https://www.nuget.org/packages/TestingFileUtilities)
(TestingFileUtilities)

[![NuGet version](https://badge.fury.io/nu/TestingFileUtilities.TypeGenerator.svg)](https://badge.fury.io/nu/TestingFileUtilities.TypeGenerator)
[![NuGet Download](https://img.shields.io/nuget/dt/TestingFileUtilities.TypeGenerator.svg)](https://www.nuget.org/packages/TestingFileUtilities.TypeGenerator)
(TestingFileUtilities.TypeGenerator)

This library supports the creation of test folders and files.

## Description

You can create temporary folders and files for unit testing.
This library creates these temporary files from your code.

## Usage

You can create temporary files and folders by defining a hierarchy of files and folders in your code as follows...

### Create only

```
var workDir = Path.Combine(TestContext.CurrentContext.TestDirectory, TestContext.CurrentContext.Test.Name);
var testFolder = PhysicalFolder.Create(workDir, PhysicalFolderDeleteType.NoDelete,
    new Folder("sub",
        new Folder("subsub",
            new TextFile("test.txt", "test.txt text content"),
            new BinaryFile("a.dat", new byte[] { 0x01, 0x02 }),
            new ZipFile("a.zip",
                new Folder("zip-sub",
                    new TextFile("zip.txt", "zip.txt text content"),
                    new ZipFile("b.zip",
                        new TextFile("c.txt", "c.txt text content")))))),
    new TextFile("root.txt", "root.txt text content"),
    new Folder("temp"));
```

### Type based creation and file access. (ver.1.1 or higher)

You can also create files and folders by entering an anonymous type.
With this method, you can operate the file by using properties.

```
var workDir = Path.Combine(TestContext.CurrentContext.TestDirectory, TestContext.CurrentContext.Test.FullName);
using var dir = 
    TypeBasePhysicalFolder.CreatePhysicalFolder(workDir, PhysicalFolderDeleteType.DeleteFolder,
    new
    {
        A_dat = Files.BinaryFile(new byte[] {0x01}),
        SubFolder = new
        {
            B_txt = Files.TextFile("b.txt file contents"),
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
            C_txt = Files.TextFile("c.txt file contents"),
            Folder = new
            {
                D_pdf = Files.BinaryFile(new byte[] {0x01, 0x02})
            }
        }),
    });

var fileSize = dir.Root.A_dat.FileInfo.Length;
var bTxtContents = dir.Root.SubFolder.B_txt.ReadAllText(Encoding.UTF8);
var filesInSubFolder = dir.Root.SubFolder.FolderInfo.GetFiles();

```


### Automatically created type-based creation and file access. (ver.1.1 or higher and using TestingFileUtilities.TypeGenerator)

The created folder object must be a named type, not an anonymous type, in order to be a class member.
You can use the TestingFileUtilities.TypeGenerator package to automatically generate named types from Anonymous types.

```
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
```


```
        private TypeBasePhysicalFolder<TestFolder> _workDir;
        [SetUp]
        public void Setup()
        {
            var rootDir = Path.Combine(TestContext.CurrentContext.TestDirectory, TestContext.CurrentContext.Test.FullName);

            _workDir = TypeBasePhysicalFolder.CreatePhysicalFolder(rootDir, PhysicalFolderDeleteType.DeleteFolder,
                new TestFolder());
        }

        [Test]
        public void Test1()
        {
            FileAssert.Exists(_workDir.Root.test_txt.FullPath);
            FileAssert.Exists(_workDir.Root.Dir.test_pdf.FullPath);
            FileAssert.Exists(_workDir.Root.test2_txt.FullPath);
            FileAssert.Exists(_workDir.Root.Dir.SubDir.text3_txt.FullPath);
        }
```

## Install

You can install the package from nuget.

```
dotnet add package TestingFileUtilities
```

If you use the TestingFileUtilities.TypeGenerator package, you must install it using nuget.

```
dotnet add package TestingFileUtilities.TypeGenerator
```

## Licence

[MIT](https://github.com/banban525/TestingFileUtilities/blob/main/LICENSE)

## Author

[banban525](https://github.com/banban525)