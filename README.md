TestingFileUtilities
====================

[![NuGet version](https://badge.fury.io/nu/TestingFileUtilities.svg)](https://badge.fury.io/nu/TestingFileUtilities)
[![NuGet Download](https://img.shields.io/nuget/dt/TestingFileUtilities.svg)](https://www.nuget.org/packages/TestingFileUtilities)
[![MIT License](https://img.shields.io/github/license/banban525/TestingFileUtilities.svg)](LICENSE)

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




## Install

You can install the package from nuget.

```
dotnet add package TestingFileUtilities
```

## Licence

[MIT](https://github.com/banban525/TestingFileUtilities/blob/main/LICENSE)

## Author

[banban525](https://github.com/banban525)