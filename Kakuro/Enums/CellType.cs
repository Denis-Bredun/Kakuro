﻿namespace Kakuro.Enums
{
    public enum CellType
    {
        EmptyCell,  // for those that neither contain value nor any sum; a part of border
                    // for example.
        ValueCell, // cell for containing user's value
        SumCell   // cell for containing sums: only below, only right or even both
    }
}
