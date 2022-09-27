using System;
using System.Collections.Generic;
using System.IO;

namespace Serialization
{
    #region Deserialization Class

    public class BaseDeserialization
    {
        private readonly Func<Stream, List<FileInform>> _deserializationFunc;

        public BaseDeserialization(Func<Stream, List<FileInform>> deserializationFunc)
        {
            _deserializationFunc = deserializationFunc;
        }
        public List<FileInform> Deserialization(string nameOfTheDeserializedFile)
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
        private readonly FileHandler _fileHandler;
        private readonly Action<Stream, List<FileInform>> _serializeFunction;

        public BaseSerialization(string path, Action<Stream, List<FileInform>> serializeFunction)
        {
            _fileHandler = new FileHandler(path);
            _serializeFunction = serializeFunction;
        }

        public void Serialization(string nameOfTheSerializedFile)
        {
            using (Stream fs = new FileStream(nameOfTheSerializedFile, FileMode.Create, FileAccess.Write))
            {
                _serializeFunction?.Invoke(fs, _fileHandler.DirectoryProcessing());
            }
        }
    }
    #endregion
}