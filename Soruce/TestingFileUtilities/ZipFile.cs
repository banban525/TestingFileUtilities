using System.IO.Compression;
using System.Text;

namespace TestingFileUtilities
{
    public class ZipFile : ArchivedFile<ZipFile>
    {
        private readonly ZipCreator _zipCreator;
        public ZipFile(string name, params INode[] nodes)
            : this(name, new ZipCreator(), nodes)
        {
        }
        public ZipFile(string name, object anonymousTypeFolder)
            : this(name, new ZipCreator(), anonymousTypeFolder)
        {
        }

        private ZipFile(string name, ZipCreator createArchiveFile, params INode[] nodes)
            : base(name, createArchiveFile, nodes)
        {
            _zipCreator = createArchiveFile;
        }
        private ZipFile(string name, ZipCreator createArchiveFile, object anonymousTypeFolder)
            : base(name, createArchiveFile, anonymousTypeFolder)
        {
            _zipCreator = createArchiveFile;
        }


        public ZipFile CompressionLevel(CompressionLevel compressionLevel)
        {
            var result = Clone();
            result._zipCreator.CompressionLevel = compressionLevel;
            return result;
        }

        public CompressionLevel CompressionLevelValue => _zipCreator.CompressionLevel;
        public Encoding EntryNameEncodingValue => _zipCreator.EntryNameEncoding;


        public ZipFile EntryNameEncoding(Encoding entryNameEncoding)
        {
            var result = Clone();
            result._zipCreator.EntryNameEncoding = entryNameEncoding;
            return result;
        }

        protected override ZipFile Clone()
        {
            var result = AnonymousTypeFolder != null ? 
                new ZipFile(Name, _zipCreator, AnonymousTypeFolder) : 
                new ZipFile(Name, _zipCreator, @Nodes);

            CopyTo(result);

            return result;
        }

    }

    public class ZipCreator : ICreateArchiveFile
    {
        public void Archive(string sourceDir, string destinationFilePath)
        {
            System.IO.Compression.ZipFile.CreateFromDirectory(sourceDir, destinationFilePath,
                CompressionLevel, false, EntryNameEncoding);
        }
        public CompressionLevel CompressionLevel { get; set; } = System.IO.Compression.CompressionLevel.Fastest;
        public Encoding EntryNameEncoding { get; set; } = Encoding.UTF8;

    }
}