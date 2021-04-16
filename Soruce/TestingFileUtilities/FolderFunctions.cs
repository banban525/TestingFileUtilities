using System;
using System.Collections.Generic;
using System.Text;

namespace TestingFileUtilities
{
    public partial class FolderFunctions
    {
        public string FullPath { get; private set; }

        internal void ChangeFilePath(string filePath)
        {
            FullPath = filePath;
        }
    }
}
