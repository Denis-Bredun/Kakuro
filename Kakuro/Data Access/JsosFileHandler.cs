﻿using Kakuro.Interfaces.Data_Access;
using System.IO;
using System.Text.Json;

namespace Kakuro.Data_Access
{
    public class JsosFileHandler<T> : IJsonFileHandler<T>
    {
        public IEnumerable<T> Load(string filepath)
        {
            if (IsInvalidFile(filepath))
                return new List<T>();

            var jsonData = File.ReadAllText(filepath);
            List<T>? deserializedData;

            try
            {
                deserializedData = JsonSerializer.Deserialize<List<T>>(jsonData);
            }
            catch (JsonException)
            {
                return new List<T>();
            }

            return deserializedData ?? new List<T>();
        }

        public void Save(IEnumerable<T> data, string filepath)
        {
            if (AreInvalidSaveParameters(data, filepath))
                return;

            EnsureDirectoryExists(filepath);

            var jsonData = JsonSerializer.Serialize(data);
            File.WriteAllText(filepath, jsonData);
        }

        private void EnsureDirectoryExists(string filepath)
        {
            var directory = Path.GetDirectoryName(filepath);
            if (!IsEmptyPath(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        private bool IsEmptyPath(string filepath) => string.IsNullOrEmpty(filepath);

        private bool IsInvalidFile(string filepath) => IsEmptyPath(filepath) || !File.Exists(filepath);

        private bool AreInvalidSaveParameters(IEnumerable<T> data, string filepath) => data == null || IsEmptyPath(filepath);
    }
}