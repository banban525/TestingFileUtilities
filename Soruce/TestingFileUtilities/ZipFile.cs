using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
        private ZipFile(string name, ZipCreator createArchiveFile, params INode[] nodes)
            : base(name, createArchiveFile, nodes)
        {
            _zipCreator = (ZipCreator)createArchiveFile;
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
            var result = new ZipFile(Name, _zipCreator, @Nodes);

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