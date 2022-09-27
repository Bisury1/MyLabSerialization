using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Serialization
{
    #region DirectoryHandler
    public class FileHandler
    {
        private readonly DirectoryInfo _directory;
        public FileHandler(string path)
        {
            _directory = new DirectoryInfo(path);
        }

        public List<FileInform> DirectoryProcessing()
        {
            var dirs = new Stack<DirectoryInfo>();
            dirs.Push(_directory);
            var listForFileInform = new List<FileInform>();
            while (dirs.Count > 0)
            {
                var currentDir = dirs.Pop();
                listForFileInform.Add(new FileInform(currentDir));
                DirectoryInfo[] subDirs;
                try
                {
                    subDirs = currentDir.GetDirectories();
                    listForFileInform.AddRange(currentDir.GetFiles().Select(file => new FileInform(file)));
                    foreach (var str in subDirs)
                        dirs.Push(str);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show(@"You don't have discovery permission on a folder or file");
                }
                catch
                {
                    MessageBox.Show(@"Something was wrong. I will definitely make it");
                }
            }
            return listForFileInform;
        }
    }
    #endregion
}