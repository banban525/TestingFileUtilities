using System;
using System.IO;
using System.Text;

namespace TestingFileUtilities
{
    public class TextFile : INode, IAttributeNode<TextFile>
    {
        public TextFile(string fileName, string content)
            : this(fileName, content, Encoding.UTF8)
        {
        }

        public TextFile(string fileName, string content, Encoding encoding)
        {
            Name = fileName;
            Content = content;
            Encoding = encoding;
        }

        public TextFile(string fileName, Func<string> contentFactory)
            : this(fileName, contentFactory, Encoding.UTF8)
        {
        }

        public TextFile(string fileName, Func<string> contentFactory, Encoding encoding)
        {
            Name = fileName;
            ContentFactory = contentFactory;
            Encoding = encoding;
        }

        public TextFile(string fileName, Action<StreamWriter> writeCallback)
            : this(fileName, writeCallback, Encoding.UTF8)
        {

        }

        public TextFile(string fileName, Action<StreamWriter> writeCallback, Encoding encoding)
        {
            Name = fileName;
            WriteCallback = writeCallback;
            Encoding = encoding;
        }

        public string Name { get; }
        public string Content { get; private set; }
        public Func<string> ContentFactory { get; private set; }
        public Encoding Encoding { get; private set; }
        public Action<StreamWriter> WriteCallback { get; private set; }

        private TextFile Clone()
        {
            return new TextFile(Name, Content)
            {
                Encoding = Encoding,
                ContentFactory = ContentFactory,
                Content = Content,
                WriteCallback = WriteCallback,
                LastWriteTimeValue = LastWriteTimeValue,
                CreationTimeValue = CreationTimeValue,
                AttributesValue = AttributesValue,
            };
        }

        public IPhysicalNode CreateTo(PhysicalFolder directory)
        {
            var filePath = Path.Combine(directory.FullPath, Name);

            if (Content != null)
            {
                File.WriteAllText(filePath, Content, Encoding);
            }
            else if (ContentFactory != null)
            {
                File.WriteAllText(filePath, ContentFactory(), Encoding);
            }
            else if (WriteCallback != null)
            {
                using (var streamWriter = new StreamWriter(filePath, false, Encoding))
                {
                    WriteCallback(streamWriter);
                }
            }
            else
            {
                throw new InvalidOperationException();
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

        public FileAttributes? AttributesValue { get; private set; }
        public DateTime? CreationTimeValue { get; private set; }
        public DateTime? LastWriteTimeValue { get; private set; }


        public TextFile Attributes(FileAttributes fileAttributes)
        {
            var result = Clone();
            result.AttributesValue = fileAttributes;
            return result;
        }
        public TextFile CreationTime(DateTime creationDateTime)
        {
            var result = Clone();
            result.CreationTimeValue = creationDateTime;
            return result;
        }
        public TextFile LastWriteTime(DateTime lastWriteTime)
        {
            var result = Clone();
            result.LastWriteTimeValue = lastWriteTime;
            return result;
        }
    }
}