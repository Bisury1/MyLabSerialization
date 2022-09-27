using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Serialization
{
    #region Serializable Class
    [Serializable]
    public class FileInform
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTime ChangeTime { get; set; }
        public FileAttributes Attributes { get; set; }
        public bool IsDirectory { get; set; }

        public FileInform()
        {
        }
        public FileInform(FileInfo file)
        {
            Name = file.Name;
            Size = file.Length;
            ChangeTime = file.LastWriteTime;
            Attributes = file.Attributes;
        }
        public FileInform(DirectoryInfo file)
        {
            Name = file.Name;
            Size = GetWshFolderSize(file.FullName);
            ChangeTime = file.LastWriteTime;
            Attributes = file.Attributes;
            IsDirectory = true;
        }
        public long GetWshFolderSize(string fldr)
        {
            var fso = new IWshRuntimeLibrary.FileSystemObject();
            var fldrSize = (long)fso.GetFolder(fldr).Size;
            Marshal.FinalReleaseComObject(fso);
            return fldrSize;
        }

        public override string ToString()
        {
            return string.Format(
                $"Name: {Name}, Size: {Size}, Change Time: {ChangeTime}, Attributes: {Attributes}, Is Directory: {IsDirectory}");
        }
    }
    #endregion
}