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
    public abstract class ArchivedFile<T> : INode, IAttributeNode<T>
        where T : ArchivedFile<T>
    {
        protected readonly ICreateArchiveFile _createArchiveFile;
        public INode[] Nodes { get; }
        public ArchivedFile(string name, ICreateArchiveFile createArchiveFile, params INode[] nodes)
        {
            Name = name;
            _createArchiveFile = createArchiveFile;
            Nodes = nodes.Where(Empty.NotMatch).ToArray();
        }
        public string Name { get; }

        public IPhysicalNode CreateTo(PhysicalFolder directory)
        {
            var destFilePath = Path.Combine(directory.FullPath, Name);
            var tempFolderPath = CreateTempFolderPath();
            using (var dir = PhysicalFolder.Create(tempFolderPath, PhysicalFolderDeleteType.DeleteFolder, Nodes))
            {
                _createArchiveFile.Archive(dir.FullPath, destFilePath);
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

        protected abstract T Clone();

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
    }

    class GenericArchivedFile : ArchivedFile<GenericArchivedFile>
    {
        public GenericArchivedFile(string name, ICreateArchiveFile createArchiveFile, params INode[] nodes)
            : base(name, createArchiveFile, nodes)
        {
        }

        protected override GenericArchivedFile Clone()
        {
            var result = new GenericArchivedFile(Name, _createArchiveFile, @Nodes);
            CopyTo(result);
            return result;
        }
    }
}
