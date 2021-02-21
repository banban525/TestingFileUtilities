using System;
using System.IO;
using System.Reflection;

namespace TestingFileUtilities
{
    public class BinaryFile : INode, IAttributeNode<BinaryFile>
    {
        private BinaryFile(string fileName)
        {
            Name = fileName;
        }

        public BinaryFile(string fileName, byte[] content)
        {
            Name = fileName;
            Content = content;
        }

        public BinaryFile(string fileName, Func<byte[]> contentFactory)
        {
            Name = fileName;
            ContentFactory = contentFactory;
        }

        public BinaryFile(string fileName, Action<Stream> contentCallBack)
        {
            Name = fileName;
            ContentCallBack = contentCallBack;
        }

        public BinaryFile(string fileName, Stream copyFromStream, bool willDisposeStream)
        {
            Name = fileName;
            CopyFromStream = copyFromStream;
            WillDisposeStream = willDisposeStream;
        }

        public static BinaryFile CopyFrom(string fileName, PhysicalFile copyFrom)
        {
            return new BinaryFile(fileName)
            {
                CopyFromFile = copyFrom
            };
        }



        public string Name { get; }
        public byte[] Content { get; private set; }
        public Func<byte[]> ContentFactory { get; private set; }
        public Action<Stream> ContentCallBack { get; private set; }
        public Stream CopyFromStream { get; private set; }
        public bool WillDisposeStream { get; set; }
        public PhysicalFile CopyFromFile { get; private set; }
        public IPhysicalNode CreateTo(PhysicalFolder directory)
        {
            var filePath = Path.Combine(directory.FullPath, Name);
            if (Content != null)
            {
                File.WriteAllBytes(filePath, Content);
            }
            else if (ContentFactory != null)
            {
                File.WriteAllBytes(filePath, ContentFactory());
            }
            else if (ContentCallBack != null)
            {
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    ContentCallBack(stream);
                }
            }
            else if (CopyFromStream != null)
            {
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    CopyFromStream.CopyTo(stream);
                }
                if(WillDisposeStream)
                {
                    CopyFromStream.Dispose();
                }
            }
            else if (CopyFromFile != null)
            {
                File.Copy(CopyFromFile.FullPath, filePath);
            }

            if (AttributesValue != null)
            {
                File.SetAttributes(filePath, AttributesValue.Value);
            }
            if (CreationTimeValue != null)
            {
                File.SetCreationTime(filePath, CreationTimeValue.Value);
            }
            if (LastWriteTimeValue != null)
            {
                File.SetLastWriteTime(filePath, LastWriteTimeValue.Value);
            }

            return new PhysicalFile(Name, filePath);
        }

        private BinaryFile Clone()
        {
            return new BinaryFile(Name, Content)
            {
                ContentFactory = ContentFactory,
                Content = Content,
                CopyFromStream = CopyFromStream,
                CopyFromFile = CopyFromFile,
                ContentCallBack = ContentCallBack,
                WillDisposeStream = WillDisposeStream,
                LastWriteTimeValue = LastWriteTimeValue,
                CreationTimeValue = CreationTimeValue,
                AttributesValue = AttributesValue
            };
        }


        public FileAttributes? AttributesValue { get; private set; }
        public DateTime? CreationTimeValue { get; private set; }
        public DateTime? LastWriteTimeValue { get; private set; }


        public BinaryFile Attributes(FileAttributes fileAttributes)
        {
            var result = Clone();
            result.AttributesValue = fileAttributes;
            return result;
        }
        public BinaryFile CreationTime(DateTime creationDateTime)
        {
            var result = Clone();
            result.CreationTimeValue = creationDateTime;
            return result;
        }
        public BinaryFile LastWriteTime(DateTime lastWriteTime)
        {
            var result = Clone();
            result.LastWriteTimeValue = lastWriteTime;
            return result;
        }
    }

    public class AssemblyResourceStream : Stream
    {
        private readonly Stream _baseStream;
        public AssemblyResourceStream(string resourceName, Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }

            _baseStream = assembly.GetManifestResourceStream(resourceName);
        }

        public override void Flush()
        {
            _baseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _baseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _baseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _baseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _baseStream.Write(buffer, offset, count);
        }

        public override bool CanRead => _baseStream.CanRead;
        public override bool CanSeek => _baseStream.CanSeek;
        public override bool CanWrite => _baseStream.CanWrite;
        public override long Length => _baseStream.Length;

        public override long Position
        {
            get => _baseStream.Position;
            set => _baseStream.Position = value;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _baseStream.Dispose();
            }

            base.Dispose(disposing);
        }
    }

}