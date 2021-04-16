using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TestingFileUtilities
{
    public class TypeBasePhysicalFolder<T> : IDisposable
    {
        public TypeBasePhysicalFolder(T root, PhysicalFolder physicalFolder)
        {
            Root = root;
            _physicalFolder = physicalFolder;
        }

        public T Root { get; }

        private readonly PhysicalFolder _physicalFolder;


        public void Dispose()
        {
            _physicalFolder.Dispose();
        }


    }

    public class TypeBasePhysicalFolder
    {
        public static TypeBasePhysicalFolder<T> CreatePhysicalFolder<T>(string physicalFolder, PhysicalFolderDeleteType deleteType, T folder)
        {
            SetFileName(folder);

            var parentPhysicalFolder = PhysicalFolder.Create(physicalFolder, deleteType);
            CreateFileAndFolders(parentPhysicalFolder, folder);

            return new TypeBasePhysicalFolder<T>(folder, parentPhysicalFolder);
        }

        private static void CreateFileAndFolders(PhysicalFolder physicalFolder, object folder)
        {
            foreach (var propertyInfo in folder.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                var propertyValue = propertyInfo.GetValue(folder);

                if (propertyValue == null)
                {
                    continue;
                }
                if (propertyValue.GetType().IsClass == false)
                {
                    continue;
                }

                if (propertyValue is FolderFunctions folderFunctions)
                {
                    folderFunctions.ChangeFilePath(physicalFolder.FullPath);
                }
                else if (propertyValue is IInternalNode node)
                {
                    node.CreateTo(physicalFolder);
                    node.ChangeFilePath(Path.Combine(physicalFolder.FullPath, node.Name));
                }
                else
                {
                    var folderName = propertyInfo.Name;

                    var subPhysicalFolder = PhysicalFolder.Create(Path.Combine(physicalFolder.FullPath, folderName),
                        PhysicalFolderDeleteType.NoDelete);

                    CreateFileAndFolders(subPhysicalFolder, propertyValue);
                }
            }
        }


        private static void SetFileName(object folder)
        {
            foreach (var propertyInfo in folder.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (propertyInfo.PropertyType == typeof(FolderFunctions))
                {
                    continue;
                }

                var propertyValue = propertyInfo.GetValue(folder);
                if (propertyValue == null)
                {
                    continue;
                }

                if (propertyValue.GetType().IsClass == false)
                {
                    continue;
                }
                if (propertyValue is IInternalNode node)
                {
                    if (string.IsNullOrEmpty(node.Name))
                    {
                        var fileName = GetFileNameFromPropertyName(propertyInfo);
                        node.ChangeName(fileName);
                    }
                }
                else
                {
                    SetFileName(propertyValue);
                }
            }
        }


        private static string GetFileNameFromPropertyName(PropertyInfo propertyInfo)
        {
            var fileName = propertyInfo.Name;
            var regex = new Regex(@"^(?<fileNameWithoutExtension>.+)_(?<extension>[^_]+)$");
            if (!regex.IsMatch(fileName))
            {
                return fileName;
            }

            var match = regex.Match(fileName);
            var fileNameWithoutExtension = match.Groups["fileNameWithoutExtension"].Value;
            var extension = match.Groups["extension"].Value;

            fileName = fileNameWithoutExtension + "." + extension;

            return fileName;
        }
    }
}