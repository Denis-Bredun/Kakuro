﻿using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Interfaces.Data_Access.Tools;
using Kakuro.Models;
using System.IO;

namespace Kakuro.Data_Access.Repositories
{
    public class RatingRecordRepository : IReadAllRepository<RatingRecord, DifficultyLevels>
    {
        private const int MAX_COUNT_FOR_EACH_DIFFICULTY = 10;
        private const string PART_OF_FILEPATH = ". Rating Table.json";
        private readonly string _directoryPath;

        private IJsonFileHandler<RatingRecord> _jsonFileHandler;

        public RatingRecordRepository(IJsonFileHandler<RatingRecord> jsonEnumerableFileHandler, string directoryPath = "")
        {
            _jsonFileHandler = jsonEnumerableFileHandler;
            _directoryPath = FormDirectorypath(directoryPath);
        }

        public void Add(RatingRecord entity, DifficultyLevels key)
        {
            CheckIfEntityIsEmpty(entity);

            string filepath = FormFilepath(key);
            var ratingTableConcreteDifficulty = GetAll(key).ToList();
            ratingTableConcreteDifficulty.Add(entity);

            SortAndRemoveExcess(ratingTableConcreteDifficulty);

            _jsonFileHandler.Save(ratingTableConcreteDifficulty, filepath);
        }

        public IEnumerable<RatingRecord> GetAll(DifficultyLevels key)
        {
            string filepath = FormFilepath(key);
            return _jsonFileHandler.Load(filepath);
        }

        private string FormFilepath(DifficultyLevels level) => Path.Combine(_directoryPath, level.ToString() + PART_OF_FILEPATH);

        private string FormDirectorypath(string directoryPath) => string.IsNullOrWhiteSpace(directoryPath) ? "Rating Tables\\" : directoryPath;

        private void SortAndRemoveExcess(List<RatingRecord> ratingRecords)
        {
            ratingRecords.Sort();
            if (IsExceedingMaxCount(ratingRecords.Count))
                ratingRecords.RemoveAt(MAX_COUNT_FOR_EACH_DIFFICULTY);
        }

        private bool IsExceedingMaxCount(int count) => count > MAX_COUNT_FOR_EACH_DIFFICULTY;

        private void CheckIfEntityIsEmpty(RatingRecord entity)
        {
            if (entity == null)
                throw new NullReferenceException("Entity equals null");
            else if (entity.GameCompletionTime == default || entity.GameCompletionDate == default)
                throw new ArgumentException("Entity's properties cannot be null or default.");
        }
    }
}
