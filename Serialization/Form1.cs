using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Serialization
{
    #region FormInterface
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SerializedButton_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            ForPrintDeserializedInfo.Items.Clear();
            try
            {
                switch (TypeSelector.SelectedItem.ToString())
                {
                    case "Binary":
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            new BaseSerialization(fbd.SelectedPath, (stream, informs) => new BinaryFormatter().Serialize(stream, informs))
                                .Serialization($"result{DateTime.Now.Day}.bin");
                        }
                        break;
                    case "XML":
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            new BaseSerialization(fbd.SelectedPath, (stream, informs) =>
                                    new XmlSerializer(typeof(List<Inform>)).Serialize(stream, informs))
                                .Serialization($"result{DateTime.Now.Day}.xml");
                        }
                        break;
                    case "JSON":
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            new BaseSerialization(fbd.SelectedPath, (stream, informs) => JsonSerializer.Serialize(stream, informs))
                                .Serialization($"result{DateTime.Now.Day}.json");
                        }
                        break;
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(@"You need to select the type of serialization!");
            }
            catch
            {
                MessageBox.Show(@"Something was wrong");
            }
        }

        private void DeserializedButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ForPrintDeserializedInfo.Items.Clear();
            try
            {
                switch (TypeSelector.SelectedItem.ToString())
                {
                    case "Binary":
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            foreach (var item in new BaseDeserialization(stream => (List<Inform>)new BinaryFormatter().Deserialize(stream))
                                         .Deserialization(ofd.FileName))
                            {
                                ForPrintDeserializedInfo.Items.Add(item);
                            }
                        }
                        break;
                    case "XML":
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            foreach (var item in new BaseDeserialization(stream => 
                                             (List<Inform>)new XmlSerializer(typeof(List<Inform>)).Deserialize(stream))
                                         .Deserialization(ofd.FileName))
                            {
                                ForPrintDeserializedInfo.Items.Add(item);
                            }
                        }
                        break;
                    case "JSON":
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            foreach (var item in new BaseDeserialization(stream => JsonSerializer.Deserialize<List<Inform>>(stream))
                                         .Deserialization(ofd.FileName))
                            {
                                ForPrintDeserializedInfo.Items.Add(item);
                            }
                        }
                        break;
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(@"You need to select the type of deserialization!");
            }
            catch
            {
                MessageBox.Show(@"Something was wrong");
            }
        }
    }
    #endregion

    #region Deserialization Class

    public class BaseDeserialization
    {
        private readonly Func<Stream, List<Inform>> _deserializationFunc;

        public BaseDeserialization(Func<Stream, List<Inform>> deserializationFunc)
        {
            _deserializationFunc = deserializationFunc;
        }
        public List<Inform> Deserialization(string nameOfTheDeserializedFile)
        {
            using (Stream fs = new FileStream(nameOfTheDeserializedFile, FileMode.Open, FileAccess.Read))
            {
                return _deserializationFunc?.Invoke(fs);
            }
        }
    }
    #endregion

    #region Serialization Class
    public class BaseSerialization
    {
        private readonly DirectoryAndFileHandler _directoryAndFileHandler;
        private readonly Action<Stream, List<Inform>> _serializeFunction;

        public BaseSerialization(string path, Action<Stream, List<Inform>> serializeFunction)
        {
            _directoryAndFileHandler = new DirectoryAndFileHandler(path);
            _serializeFunction = serializeFunction;
        }

        public void Serialization(string nameOfTheSerializedFile)
        {
            using (Stream fs = new FileStream(nameOfTheSerializedFile, FileMode.Create, FileAccess.Write))
            {
                _serializeFunction?.Invoke(fs, _directoryAndFileHandler.DirectoryProcessing());
            }
        }
    }
    #endregion

    #region Serializable Class
    [Serializable]
    public class Inform
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTime ChangeTime { get; set; }
        public FileAttributes Attributes { get; set; }
        public bool IsDirectory { get; set; }

        public Inform()
        {
        }
        public Inform(FileInfo file)
        {
            Name = file.Name;
            Size = file.Length;
            ChangeTime = file.LastWriteTime;
            Attributes = file.Attributes;
        }
        public Inform(DirectoryInfo file)
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

    #region DirectoryHandler
    public class DirectoryAndFileHandler
    {
        private readonly DirectoryInfo _directory;
        public DirectoryAndFileHandler(string path)
        {
            _directory = new DirectoryInfo(path);
        }

        public List<Inform> DirectoryProcessing()
        {
            var dirs = new Stack<DirectoryInfo>();
            dirs.Push(_directory);
            var listForFileInform = new List<Inform>();
            while (dirs.Count > 0)
            {
                var currentDir = dirs.Pop();
                listForFileInform.Add(new Inform(currentDir));
                DirectoryInfo[] subDirs;
                try
                {
                    subDirs = currentDir.GetDirectories();
                    listForFileInform.AddRange(currentDir.GetFiles().Select(file => new Inform(file)));
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