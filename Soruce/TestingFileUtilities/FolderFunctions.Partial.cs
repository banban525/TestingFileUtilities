﻿// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY T4. DO NOT CHANGE IT. CHANGE THE .tt FILE INSTEAD.
// </auto-generated>
 
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace TestingFileUtilities
{
    public partial class FolderFunctions
    {

        public DirectoryInfo GetParent()
        {
            return Directory.GetParent(FullPath);
        }

        public DirectoryInfo CreateDirectory()
        {
            return Directory.CreateDirectory(FullPath);
        }

        public Boolean Exists()
        {
            return Directory.Exists(FullPath);
        }

        public void SetCreationTime(DateTime creationTime)
        {
            Directory.SetCreationTime(FullPath, creationTime);
        }

        public void SetCreationTimeUtc(DateTime creationTimeUtc)
        {
            Directory.SetCreationTimeUtc(FullPath, creationTimeUtc);
        }

        public DateTime GetCreationTime()
        {
            return Directory.GetCreationTime(FullPath);
        }

        public DateTime GetCreationTimeUtc()
        {
            return Directory.GetCreationTimeUtc(FullPath);
        }

        public void SetLastWriteTime(DateTime lastWriteTime)
        {
            Directory.SetLastWriteTime(FullPath, lastWriteTime);
        }

        public void SetLastWriteTimeUtc(DateTime lastWriteTimeUtc)
        {
            Directory.SetLastWriteTimeUtc(FullPath, lastWriteTimeUtc);
        }

        public DateTime GetLastWriteTime()
        {
            return Directory.GetLastWriteTime(FullPath);
        }

        public DateTime GetLastWriteTimeUtc()
        {
            return Directory.GetLastWriteTimeUtc(FullPath);
        }

        public void SetLastAccessTime(DateTime lastAccessTime)
        {
            Directory.SetLastAccessTime(FullPath, lastAccessTime);
        }

        public void SetLastAccessTimeUtc(DateTime lastAccessTimeUtc)
        {
            Directory.SetLastAccessTimeUtc(FullPath, lastAccessTimeUtc);
        }

        public DateTime GetLastAccessTime()
        {
            return Directory.GetLastAccessTime(FullPath);
        }

        public DateTime GetLastAccessTimeUtc()
        {
            return Directory.GetLastAccessTimeUtc(FullPath);
        }

        public String[] GetFiles(String searchPattern)
        {
            return Directory.GetFiles(FullPath, searchPattern);
        }

        public String[] GetFiles(String searchPattern,SearchOption searchOption)
        {
            return Directory.GetFiles(FullPath, searchPattern, searchOption);
        }

        public String[] GetDirectories(String searchPattern)
        {
            return Directory.GetDirectories(FullPath, searchPattern);
        }

        public String[] GetDirectories(String searchPattern,SearchOption searchOption)
        {
            return Directory.GetDirectories(FullPath, searchPattern, searchOption);
        }

        public String[] GetFileSystemEntries(String searchPattern)
        {
            return Directory.GetFileSystemEntries(FullPath, searchPattern);
        }

        public String[] GetFileSystemEntries(String searchPattern,SearchOption searchOption)
        {
            return Directory.GetFileSystemEntries(FullPath, searchPattern, searchOption);
        }

        public IEnumerable<String> EnumerateDirectories(String searchPattern)
        {
            return Directory.EnumerateDirectories(FullPath, searchPattern);
        }

        public IEnumerable<String> EnumerateDirectories(String searchPattern,SearchOption searchOption)
        {
            return Directory.EnumerateDirectories(FullPath, searchPattern, searchOption);
        }

        public IEnumerable<String> EnumerateFiles(String searchPattern)
        {
            return Directory.EnumerateFiles(FullPath, searchPattern);
        }

        public IEnumerable<String> EnumerateFiles(String searchPattern,SearchOption searchOption)
        {
            return Directory.EnumerateFiles(FullPath, searchPattern, searchOption);
        }

        public IEnumerable<String> EnumerateFileSystemEntries(String searchPattern)
        {
            return Directory.EnumerateFileSystemEntries(FullPath, searchPattern);
        }

        public IEnumerable<String> EnumerateFileSystemEntries(String searchPattern,SearchOption searchOption)
        {
            return Directory.EnumerateFileSystemEntries(FullPath, searchPattern, searchOption);
        }

        public String GetDirectoryRoot()
        {
            return Directory.GetDirectoryRoot(FullPath);
        }

        public void SetCurrentDirectory()
        {
            Directory.SetCurrentDirectory(FullPath);
        }

        public void Move(String destDirName)
        {
            Directory.Move(FullPath, destDirName);
        }

        public void Delete()
        {
            Directory.Delete(FullPath);
        }

        public void Delete(Boolean recursive)
        {
            Directory.Delete(FullPath, recursive);
        }

        public String[] GetFileSystemEntries()
        {
            return Directory.GetFileSystemEntries(FullPath);
        }

        public IEnumerable<String> EnumerateFiles()
        {
            return Directory.EnumerateFiles(FullPath);
        }

        public IEnumerable<String> EnumerateFileSystemEntries()
        {
            return Directory.EnumerateFileSystemEntries(FullPath);
        }

        public String[] GetFiles()
        {
            return Directory.GetFiles(FullPath);
        }

        public String[] GetDirectories()
        {
            return Directory.GetDirectories(FullPath);
        }

        public IEnumerable<String> EnumerateDirectories()
        {
            return Directory.EnumerateDirectories(FullPath);
        }
    }


    public partial class PhysicalFolder
    {

        public DirectoryInfo GetParent()
        {
            return Directory.GetParent(FullPath);
        }

        public DirectoryInfo CreateDirectory()
        {
            return Directory.CreateDirectory(FullPath);
        }

        public Boolean Exists()
        {
            return Directory.Exists(FullPath);
        }

        public void SetCreationTime(DateTime creationTime)
        {
            Directory.SetCreationTime(FullPath, creationTime);
        }

        public void SetCreationTimeUtc(DateTime creationTimeUtc)
        {
            Directory.SetCreationTimeUtc(FullPath, creationTimeUtc);
        }

        public DateTime GetCreationTime()
        {
            return Directory.GetCreationTime(FullPath);
        }

        public DateTime GetCreationTimeUtc()
        {
            return Directory.GetCreationTimeUtc(FullPath);
        }

        public void SetLastWriteTime(DateTime lastWriteTime)
        {
            Directory.SetLastWriteTime(FullPath, lastWriteTime);
        }

        public void SetLastWriteTimeUtc(DateTime lastWriteTimeUtc)
        {
            Directory.SetLastWriteTimeUtc(FullPath, lastWriteTimeUtc);
        }

        public DateTime GetLastWriteTime()
        {
            return Directory.GetLastWriteTime(FullPath);
        }

        public DateTime GetLastWriteTimeUtc()
        {
            return Directory.GetLastWriteTimeUtc(FullPath);
        }

        public void SetLastAccessTime(DateTime lastAccessTime)
        {
            Directory.SetLastAccessTime(FullPath, lastAccessTime);
        }

        public void SetLastAccessTimeUtc(DateTime lastAccessTimeUtc)
        {
            Directory.SetLastAccessTimeUtc(FullPath, lastAccessTimeUtc);
        }

        public DateTime GetLastAccessTime()
        {
            return Directory.GetLastAccessTime(FullPath);
        }

        public DateTime GetLastAccessTimeUtc()
        {
            return Directory.GetLastAccessTimeUtc(FullPath);
        }

        public String[] GetFiles(String searchPattern)
        {
            return Directory.GetFiles(FullPath, searchPattern);
        }

        public String[] GetFiles(String searchPattern,SearchOption searchOption)
        {
            return Directory.GetFiles(FullPath, searchPattern, searchOption);
        }

        public String[] GetDirectories(String searchPattern)
        {
            return Directory.GetDirectories(FullPath, searchPattern);
        }

        public String[] GetDirectories(String searchPattern,SearchOption searchOption)
        {
            return Directory.GetDirectories(FullPath, searchPattern, searchOption);
        }

        public String[] GetFileSystemEntries(String searchPattern)
        {
            return Directory.GetFileSystemEntries(FullPath, searchPattern);
        }

        public String[] GetFileSystemEntries(String searchPattern,SearchOption searchOption)
        {
            return Directory.GetFileSystemEntries(FullPath, searchPattern, searchOption);
        }

        public IEnumerable<String> EnumerateDirectories(String searchPattern)
        {
            return Directory.EnumerateDirectories(FullPath, searchPattern);
        }

        public IEnumerable<String> EnumerateDirectories(String searchPattern,SearchOption searchOption)
        {
            return Directory.EnumerateDirectories(FullPath, searchPattern, searchOption);
        }

        public IEnumerable<String> EnumerateFiles(String searchPattern)
        {
            return Directory.EnumerateFiles(FullPath, searchPattern);
        }

        public IEnumerable<String> EnumerateFiles(String searchPattern,SearchOption searchOption)
        {
            return Directory.EnumerateFiles(FullPath, searchPattern, searchOption);
        }

        public IEnumerable<String> EnumerateFileSystemEntries(String searchPattern)
        {
            return Directory.EnumerateFileSystemEntries(FullPath, searchPattern);
        }

        public IEnumerable<String> EnumerateFileSystemEntries(String searchPattern,SearchOption searchOption)
        {
            return Directory.EnumerateFileSystemEntries(FullPath, searchPattern, searchOption);
        }

        public String GetDirectoryRoot()
        {
            return Directory.GetDirectoryRoot(FullPath);
        }

        public void SetCurrentDirectory()
        {
            Directory.SetCurrentDirectory(FullPath);
        }

        public void Move(String destDirName)
        {
            Directory.Move(FullPath, destDirName);
        }

        public void Delete()
        {
            Directory.Delete(FullPath);
        }

        public void Delete(Boolean recursive)
        {
            Directory.Delete(FullPath, recursive);
        }

        public String[] GetFileSystemEntries()
        {
            return Directory.GetFileSystemEntries(FullPath);
        }

        public IEnumerable<String> EnumerateFiles()
        {
            return Directory.EnumerateFiles(FullPath);
        }

        public IEnumerable<String> EnumerateFileSystemEntries()
        {
            return Directory.EnumerateFileSystemEntries(FullPath);
        }

        public String[] GetFiles()
        {
            return Directory.GetFiles(FullPath);
        }

        public String[] GetDirectories()
        {
            return Directory.GetDirectories(FullPath);
        }

        public IEnumerable<String> EnumerateDirectories()
        {
            return Directory.EnumerateDirectories(FullPath);
        }
    }
}
