using System.Text;

namespace TestingFileUtilities
{
    public partial class PhysicalFile : IPhysicalNode, IPhysicalFile
    {
        private readonly string _fullPath;

        public PhysicalFile(string name, string filePath)
        {
            Name = name;
            _fullPath = filePath;
        }
        public string Name { get; }

        public string FullPath => _fullPath;
    }
}
