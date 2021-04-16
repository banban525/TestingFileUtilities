using System;
using System.IO;

namespace TestingFileUtilities
{
    public interface INode
    {
        string Name { get; }
        IPhysicalNode CreateTo(PhysicalFolder directory);
    }

    public interface IInternalNode : INode
    {
        void ChangeName(string newFileName);
        void ChangeFilePath(string filePath);
    }

    public interface IAttributeNode<out T>
    {
        T Attributes(FileAttributes fileAttributes);
        T CreationTime(DateTime creationDateTime);
        T LastWriteTime(DateTime lastWriteTime);
    }
}