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
                                    new XmlSerializer(typeof(List<FileInform>)).Serialize(stream, informs))
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
                            foreach (var item in new BaseDeserialization(stream => (List<FileInform>)new BinaryFormatter().Deserialize(stream))
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
                                             (List<FileInform>)new XmlSerializer(typeof(List<FileInform>)).Deserialize(stream))
                                         .Deserialization(ofd.FileName))
                            {
                                ForPrintDeserializedInfo.Items.Add(item);
                            }
                        }
                        break;
                    case "JSON":
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            foreach (var item in new BaseDeserialization(stream => JsonSerializer.Deserialize<List<FileInform>>(stream))
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
}