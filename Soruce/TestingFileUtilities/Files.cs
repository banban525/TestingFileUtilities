using System;
using System.IO;
using System.Text;

namespace TestingFileUtilities
{
    public class Files
    {
        public static IPhysicalFile TextFile(string content)
        {
            return new TextFile("", content);
        }

        public static IPhysicalFile TextFile(string content, Encoding encoding)
        {
            return new TextFile("", content, encoding);
        }

        public static IPhysicalFile TextFile(Func<string> contentFactory)
        {
            return new TextFile("", contentFactory);
        }

        public static IPhysicalFile TextFile(Func<string> contentFactory, Encoding encoding)
        {
            return new TextFile("", contentFactory, encoding);
        }

        public static IPhysicalFile TextFile(Action<StreamWriter> writeCallback)
        {
            return new TextFile("", writeCallback);
        }

        public static IPhysicalFile TextFile(Action<StreamWriter> writeCallback, Encoding encoding)
        {
            return new TextFile("", writeCallback, encoding);
        }
        public static IPhysicalFile TextFile(string fileName, string content)
        {
            return new TextFile(fileName, content);
        }

        public static IPhysicalFile TextFile(string fileName, string content, Encoding encoding)
        {
            return new TextFile(fileName, content, encoding);
        }

        public static IPhysicalFile TextFile(string fileName, Func<string> contentFactory)
        {
            return new TextFile(fileName, contentFactory);
        }

        public static IPhysicalFile TextFile(string fileName, Func<string> contentFactory, Encoding encoding)
        {
            return new TextFile(fileName, contentFactory, encoding);
        }

        public static IPhysicalFile TextFile(string fileName, Action<StreamWriter> writeCallback)
        {
            return new TextFile(fileName, writeCallback);
        }

        public static IPhysicalFile TextFile(string fileName, Action<StreamWriter> writeCallback, Encoding encoding)
        {
            return new TextFile(fileName, writeCallback, encoding);
        }

        public static IPhysicalFile BinaryFile(byte[] content)
        {
            return new BinaryFile("", content);
        }

        public static IPhysicalFile BinaryFile(Func<byte[]> contentFactory)
        {
            return new BinaryFile("", contentFactory);
        }

        public static IPhysicalFile BinaryFile(Action<Stream> contentCallBack)
        {
            return new BinaryFile("", contentCallBack);
        }

        public static IPhysicalFile BinaryFile(Stream copyFromStream, bool willDisposeStream)
        {
            return new BinaryFile("", copyFromStream, willDisposeStream);
        }

        public static IPhysicalFile BinaryFile(string fileName, byte[] content)
        {
            return new BinaryFile(fileName, content);
        }

        public static IPhysicalFile BinaryFile(string fileName, Func<byte[]> contentFactory)
        {
            return new BinaryFile(fileName, contentFactory);
        }

        public static IPhysicalFile BinaryFile(string fileName, Action<Stream> contentCallBack)
        {
            return new BinaryFile(fileName, contentCallBack);
        }

        public static IPhysicalFile BinaryFile(string fileName, Stream copyFromStream, bool willDisposeStream)
        {
            return new BinaryFile(fileName, copyFromStream, willDisposeStream);
        }
        public static FolderFunctions FolderFunctions()
        {
            return new FolderFunctions();
        }

        public static IPhysicalFile ZipFile(object folder)
        {
            var zipFile = new ZipFile("", folder);
            return zipFile;
        }
        public static IPhysicalFile ZipFile(string fileName, object folder)
        {
            var zipFile = new ZipFile(fileName, folder);
            return zipFile;
        }

        public static IPhysicalFile Reserved()
        {
            return new Empty();
        }
        public static IPhysicalFile Reserved(string fileName)
        {
            return new Empty(fileName);
        }
    }
}