using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace TestingFileUtilities
{

    public interface ICreateArchiveFile
    {
        void Archive(string sourceDir, string destinationFilePath);
    }
    public abstract partial class ArchivedFile<T> : INode, IAttributeNode<T>, IPhysicalFile, IInternalNode
        where T : ArchivedFile<T>
    {
        protected readonly ICreateArchiveFile CreateArchiveFile;
        private string _fullPath;
        public INode[] Nodes { get; }
        protected object AnonymousTypeFolder;
        public ArchivedFile(string name, ICreateArchiveFile createArchiveFile, params INode[] nodes)
        {
            Name = name;
            CreateArchiveFile = createArchiveFile;
            Nodes = nodes.Where(Empty.NotMatch).ToArray();
        }
        public ArchivedFile(string name, ICreateArchiveFile createArchiveFile, object anonymousTypeFolder)
        {
            Name = name;
            CreateArchiveFile = createArchiveFile;
            Nodes = Array.Empty<INode>();
            AnonymousTypeFolder = anonymousTypeFolder;
        }

        public string Name { get; private set; }

        string IPhysicalFile.FullPath => _fullPath;

        public IPhysicalNode CreateTo(PhysicalFolder directory)
        {
            var destFilePath = Path.Combine(directory.FullPath, Name);
            var tempFolderPath = CreateTempFolderPath();

            if (AnonymousTypeFolder != null)
            {
                using (var dir = TypeBasePhysicalFolder.CreatePhysicalFolder(
                    tempFolderPath,
                    PhysicalFolderDeleteType.DeleteFolder,
                    AnonymousTypeFolder))
                {
                    CreateArchiveFile.Archive(tempFolderPath, destFilePath);
                }
            }
            else
            {
                using (var dir = PhysicalFolder.Create(tempFolderPath, PhysicalFolderDeleteType.DeleteFolder, Nodes))
                {
                    CreateArchiveFile.Archive(dir.FullPath, destFilePath);
                }
            }

            if (AttributesValue != null)
            {
                File.SetAttributes(destFilePath, AttributesValue.Value);
            }
            if (CreationTimeValue != null)
            {
                File.SetCreationTime(destFilePath, CreationTimeValue.Value);
            }
            if (LastWriteTimeValue != null)
            {
                File.SetLastWriteTime(destFilePath, LastWriteTimeValue.Value);
            }
            return new PhysicalFile(Name, destFilePath);
        }

        public IPhysicalFile AsPhysicalFile()
        {
            return this;
        }

        private string CreateTempFolderPath()
        {
            var tempDir = "";
            for (var i = 0; i < 10; i++)
            {
                tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                if (Directory.Exists(tempDir))
                {
                    continue;
                }
            }

            if (tempDir == "")
            {
                throw new ApplicationException("A temp directory cannot be created.");
            }

            return tempDir;
        }


        public FileAttributes? AttributesValue { get; private set; }
        public DateTime? CreationTimeValue { get; private set; }
        public DateTime? LastWriteTimeValue { get; private set; }

        public abstract T Clone();

        protected void CopyTo(T copyTo)
        {
            copyTo.LastWriteTimeValue = LastWriteTimeValue;
            copyTo.AttributesValue = AttributesValue;
            copyTo.CreationTimeValue = CreationTimeValue;
        }

        public T Attributes(FileAttributes fileAttributes)
        {
            var result = Clone();
            result.AttributesValue = fileAttributes;
            return result;
        }
        public T CreationTime(DateTime creationDateTime)
        {
            var result = Clone();
            result.CreationTimeValue = creationDateTime;
            return result;
        }
        public T LastWriteTime(DateTime lastWriteTime)
        {
            var result = Clone();
            result.LastWriteTimeValue = lastWriteTime;
            return result;
        }

        void IInternalNode.ChangeName(string newFileName)
        {
            Name = newFileName;
        }

        void IInternalNode.ChangeFilePath(string filePath)
        {
            _fullPath = filePath;
        }
    }

    class GenericArchivedFile : ArchivedFile<GenericArchivedFile>
    {
        public GenericArchivedFile(string name, ICreateArchiveFile createArchiveFile, params INode[] nodes)
            : base(name, createArchiveFile, nodes)
        {
        }

        public override GenericArchivedFile Clone()
        {
            var result = new GenericArchivedFile(Name, CreateArchiveFile, @Nodes);
            CopyTo(result);
            return result;
        }
    }
}
