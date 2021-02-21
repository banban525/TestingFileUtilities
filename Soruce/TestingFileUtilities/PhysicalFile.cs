using System.Text;

namespace TestingFileUtilities
{
    public class PhysicalFile : IPhysicalNode
    {
        public PhysicalFile(string name, string filePath)
        {
            Name = name;
            FullPath = filePath;
        }
        public string Name { get; }
        public string FullPath { get; }
    }
}
