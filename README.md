TestingFileUtilities
====================

This library supports the creation of test folders and files.

## Description

You can create temporary folders and files for unit testing.
This library creates these temporary files from your code.

## Usage

You can create temporary files and folders by defining a hierarchy of files and folders in your code as follows...

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


## Install

You can install the package from nuget.

```
dotnet add package TestingFileUtilities
```

## Licence

[MIT](https://github.com/banban525/TestingFileUtilities/blob/main/LICENSE)

## Author

[banban525](https://github.com/banban525)