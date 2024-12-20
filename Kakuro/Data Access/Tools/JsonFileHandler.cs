﻿using Kakuro.Interfaces.Data_Access.Tools;
using System.IO;
using System.Text.Json;

namespace Kakuro.Data_Access.Tools
{
    public class JsonFileHandler<T> : IJsonFileHandler<T>
    {
        // #BAD: We should STORE filepathes here, i guess. So we could keep "Fabric method" pattern implementation.
        // I've mentioned it in IFilesHandler.cs. We shall not pass filepath straightfully

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
                throw new ArgumentException("Invalid data or filepath");

            EnsureDirectoryExists(filepath);

            var jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filepath, jsonData);
        }

        private void EnsureDirectoryExists(string filepath)
        {
            var directory = Path.GetDirectoryName(filepath);
            if (IsDirectoryNeeded(directory))
                Directory.CreateDirectory(directory);
        }

        private bool IsEmptyPath(string filepath) => string.IsNullOrEmpty(filepath);

        private bool IsInvalidFile(string filepath) => IsEmptyPath(filepath) || !File.Exists(filepath);
        private bool IsDirectoryNeeded(string directory) => !IsEmptyPath(directory) && !Directory.Exists(directory);

        private bool AreInvalidSaveParameters(IEnumerable<T> data, string filepath) => data == null || IsEmptyPath(filepath);

        public void DeleteFile(string filepath)
        {
            File.Delete(filepath);
        }
    }
}
