using System;
using System.IO;
using System.Linq;
using System.Threading;
using Path = System.IO.Path;

namespace TestingFileUtilities
{
    public class Folder : INode, IAttributeNode<Folder>
    {
        public INode[] Nodes { get; }
        public Folder(string name, params INode[] nodes)
        {
            Name = name;
            Nodes = nodes.Where(Empty.NotMatch).ToArray();
        }

        public static Folder CopyFrom(string name, PhysicalFolder copyFromFolder)
        {
            return new Folder(name)
            {
                CopyFromFolder = copyFromFolder
            };
        }

        public string Name { get; }

        public PhysicalFolder CopyFromFolder { get; private set; }

        public IPhysicalNode CreateTo(PhysicalFolder directory)
        {
            var dirPath = Path.Combine(directory.FullPath, Name);

            PhysicalFolder result;

            if (CopyFromFolder != null)
            {
                CopyDirectoryAndFiles(CopyFromFolder.FullPath, dirPath);
                result = new PhysicalFolder(dirPath, PhysicalFolderDeleteType.NoDelete);
            }
            else
            {
                result = PhysicalFolder.Create(dirPath, PhysicalFolderDeleteType.NoDelete, Nodes);
            }

            if (AttributesValue != null)
            {
                new DirectoryInfo(dirPath).Attributes = AttributesValue.Value;
            }
            if (CreationTimeValue != null)
            {
                Directory.SetCreationTime(dirPath, CreationTimeValue.Value);
            }

            if (LastWriteTimeValue != null)
            {
                Directory.SetLastWriteTime(dirPath, LastWriteTimeValue.Value);
            }

            return result;
        }

        private void CopyDirectoryAndFiles(string sourceDirectory, string destDirectory)
        {
            if(Directory.Exists(sourceDirectory)==false)
            {
                Directory.CreateDirectory(sourceDirectory);
                while (Directory.Exists(sourceDirectory)==false)
                {
                    Thread.Sleep(10);
                }
            }


            var files = Directory.GetFiles(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var destFilePath = Path.Combine(fileName, destDirectory);
                File.Copy(file, destFilePath, true);
            }


            var subDirectories = Directory.GetDirectories(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
            foreach (var subDirectory in subDirectories)
            {
                var directoryName = Path.GetFileName(subDirectory);
                var subDestDir = Path.Combine(destDirectory, directoryName);
                CopyDirectoryAndFiles(subDirectory, subDestDir);
            }
        }

        private Folder Clone()
        {
            return new Folder(Name, @Nodes)
            {
                AttributesValue = AttributesValue,
                LastWriteTimeValue = LastWriteTimeValue,
                CreationTimeValue = CreationTimeValue,
                CopyFromFolder = CopyFromFolder
            };
        }
        public FileAttributes? AttributesValue { get; private set; }

        public DateTime? CreationTimeValue { get; private set; }
        public DateTime? LastWriteTimeValue { get; private set; }


        public Folder Attributes(FileAttributes fileAttributes)
        {
            var result = Clone();
            result.AttributesValue = fileAttributes;
            return result;
        }

        public Folder CreationTime(DateTime creationDateTime)
        {
            var result = Clone();
            result.CreationTimeValue = creationDateTime;
            return result;
        }
        public Folder LastWriteTime(DateTime lastWriteTime)
        {
            var result = Clone();
            result.LastWriteTimeValue = lastWriteTime;
            return result;
        }
    }
}