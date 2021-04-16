using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace TestingFileUtilities
{
    public partial class PhysicalFolder : IPhysicalNode, IDisposable, IReadOnlyDictionary<string, IPhysicalNode>
    {
        public string FullPath { get; }

        public PhysicalFolderDeleteType DeleteType { get; }

        public PhysicalFolder(string rootPath, PhysicalFolderDeleteType deleteType)
        {
            FullPath = rootPath;
            DeleteType = deleteType;
        }

        public static PhysicalFolder CreateEmptyFolder(string rootPath, PhysicalFolderDeleteType deleteType)
        {
            if (Directory.Exists(rootPath) == false)
            {
                Directory.CreateDirectory(rootPath);
                while (Directory.Exists(rootPath) == false)
                {
                    Thread.Sleep(10);
                }
            }

            RemoveSubDirectoriesAndFilesInRootDirectory(rootPath);

            return new PhysicalFolder(rootPath, deleteType);
        }

        public static PhysicalFolder Create(string rootPath, PhysicalFolderDeleteType deleteType, params INode[] nodes)
        {
            if (Directory.Exists(rootPath) == false)
            {
                Directory.CreateDirectory(rootPath);
                while (Directory.Exists(rootPath) == false)
                {
                    Thread.Sleep(10);
                }
            }

            RemoveSubDirectoriesAndFilesInRootDirectory(rootPath);

            var physicalFolder = new PhysicalFolder(rootPath, deleteType);

            foreach (var node in nodes.Where(Empty.NotMatch))
            {
                node.CreateTo(physicalFolder);
            }

            return physicalFolder;
        }

        private static void RemoveSubDirectoriesAndFilesInRootDirectory(string directoryPath)
        {
            var subDirectories = Directory.GetDirectories(directoryPath, "*", System.IO.SearchOption.TopDirectoryOnly);
            foreach (var subDirectory in subDirectories)
            {
                Directory.Delete(subDirectory, true);
                while (Directory.Exists(subDirectory))
                {
                    Thread.Sleep(10);
                }
            }

            var subFiles = Directory.GetFiles(directoryPath, "*", System.IO.SearchOption.TopDirectoryOnly);
            foreach (var subFile in subFiles)
            {
                File.Delete(subFile);
                while (File.Exists(subFile))
                {
                    Thread.Sleep(10);
                }
            }
        }

        public void Dispose()
        {
            if (DeleteType == PhysicalFolderDeleteType.NoDelete) { return; }

            if (Directory.Exists(FullPath) == false) { return; }

            if (DeleteType == PhysicalFolderDeleteType.DeleteFolder)
            {
                Directory.Delete(FullPath, true);
            }
            else
            {
                RemoveSubDirectoriesAndFilesInRootDirectory(FullPath);
            }
        }

        public IEnumerator<KeyValuePair<string, IPhysicalNode>> GetEnumerator()
        {
            var directories = Directory.GetDirectories(FullPath, "*", System.IO.SearchOption.TopDirectoryOnly);
            foreach (var directory in directories)
            {
                var key = Path.GetFileName(directory);
                yield return new KeyValuePair<string, IPhysicalNode>(key, new PhysicalFolder(directory, PhysicalFolderDeleteType.NoDelete));
            }

            var files = Directory.GetFiles(FullPath, "*", System.IO.SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                var key = Path.GetFileName(file);
                yield return new KeyValuePair<string, IPhysicalNode>(key, new PhysicalFile(key, file));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get
            {
                return Directory.GetDirectories(FullPath, "*", System.IO.SearchOption.TopDirectoryOnly).Length +
                       Directory.GetFiles(FullPath, "*", System.IO.SearchOption.TopDirectoryOnly).Length;
            }
        }

        public bool ContainsKey(string key)
        {
            var fullPath = Path.Combine(FullPath, key);
            return Directory.Exists(fullPath) || File.Exists(fullPath);
        }

        public bool TryGetValue(string key, out IPhysicalNode value)
        {
            if (ContainsKey(key))
            {
                value = this[key];
                return true;
            }

            value = null;
            return false;
        }

        public IPhysicalNode this[string key]
        {
            get
            {
                var fullPath = Path.Combine(FullPath, key);

                if (Directory.Exists(fullPath))
                {
                    return new PhysicalFolder(fullPath, PhysicalFolderDeleteType.NoDelete);
                }
                if (File.Exists(fullPath))
                {
                    return new PhysicalFile(key, fullPath);
                }
                throw new KeyNotFoundException($"{key} is not found");
            }
        }

        public IEnumerable<string> Keys =>
            Directory.GetDirectories(FullPath, "*", System.IO.SearchOption.TopDirectoryOnly).Concat(
                Directory.GetFiles(FullPath, "*", System.IO.SearchOption.TopDirectoryOnly));

        public IEnumerable<IPhysicalNode> Values => this.Select(_ => _.Value);
    }
}