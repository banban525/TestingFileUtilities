using System;

namespace TestingFileUtilities
{
    public partial class Empty : INode, IPhysicalFile, IInternalNode
    {
        private string _fullPath;

        public Empty()
            :this("")
        {
        }

        public Empty(string fileName)
        {
            Name = fileName;
        }

        public string Name { get; private set; }
        string IPhysicalFile.FullPath => _fullPath;

        public IPhysicalNode CreateTo(PhysicalFolder directory)
        {
            return new PhysicalFile(Name, _fullPath);
        }

        public IPhysicalFile AsPhysicalFile()
        {
            return this;
        }

        public static bool NotMatch(INode node)
        {
            return node is Empty == false;
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
}