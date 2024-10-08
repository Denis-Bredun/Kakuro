﻿using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Models;

namespace Kakuro.Interfaces.Data_Access.Data_Providers
{
    public interface IRatingRecordProvider : IReadAllRepository<RatingRecord, DifficultyLevels> { }
}
