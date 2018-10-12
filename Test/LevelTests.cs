using _2DGame.Levels;
using _2DGame.StationaryItems;
using System;
using System.Collections.Generic;
using Xunit;

namespace Test
{
    public class LevelTests
    {
        [Fact]
        public void SetTreasureCell_FillWithTreasures_FilledGridWithTreasures()
        {
            char[,] grid = new char[10,10];
            List<Treasure> treasures = new List<Treasure>
                { new Treasure(2, 3), new Treasure(3, 5) };
            Level level = new Level(grid, null, treasures, null);

            level.SetTreasureCell(treasures, grid);

            Assert.Equal(Level.TREASURE, level.grid[2, 3]);
            Assert.Equal(Level.TREASURE, level.grid[3, 5]);
        }

        [Fact]
        public void SetTreasureCell_AddTreasureCellOutOfBounds_ThrowException()
        {
            char[,] grid = new char[10, 10];
            List<Treasure> treasures = new List<Treasure>
                { new Treasure(10, 3) };
            Level level = new Level(grid, null, null, null);

            Assert.Throws<IndexOutOfRangeException>(()=> level.SetTreasureCell(treasures, grid));

           // Assert.Equal(Level.TREASURE, level.grid[2, 3]);
        }
    }
}
