using System;
using System.IO;

namespace TestingFileUtilities
{
    public interface INode
    {
        string Name { get; }
        IPhysicalNode CreateTo(PhysicalFolder directory);
    }

    public interface IAttributeNode<out T>
    {
        T Attributes(FileAttributes fileAttributes);
        T CreationTime(DateTime creationDateTime);
        T LastWriteTime(DateTime lastWriteTime);
    }
}