using _2DGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2DGame.Enemies
{
    public enum EnemyType { Vertical, Horizontal, Random, Super };


    public class Enemy : ICombatant
    {
        public int currX = 0;
        public int currY = 0;
        public int prevX = 0;
        public int prevY = 0;
        public static bool dirVert = false;
        public static bool dirHoriz = false;
        public bool powerUnchanged = false;
        public bool treasureUnchanged = false;
        public EnemyType nemesis;
        static Random numGen = new Random();
        int pos = 0;

        public Enemy(int row, int col, EnemyType type)
        {
            nemesis = type;
            currX = row;
            currY = col;
            //Level.setEnemyCell(currX, currY, Level.grid);
            //MoveCombatant(currX, currY, enemy.ToString());
        }

        EnemyType type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public EnemyType enemyType
        {
            get
            {
                return nemesis;
            }

            set
            {
                nemesis = value;
            }
        }

        int ICombatant.row
        {
            get
            {
                return currX;
            }
            set
            {
                currX = value;
            }
        }

        int ICombatant.col
        {
            get
            {
                return currY;
            }
            set
            {
                currY = value;
            }
        }

        int ICombatant.prevRow
        {
            get
            {
                return prevX;
            }
            set
            {
                prevX = value;
            }
        }

        int ICombatant.prevCol
        {
            get
            {
                return prevY;
            }
            set
            {
                prevY = value;
            }
        }

        public void MoveCombatant(Level level, int row, int col, EnemyType enemy)
        {
            Thread current = Thread.CurrentThread;
            nemesis = enemy;
           
            switch(nemesis)
            {
                case EnemyType.Vertical:
                    verticalEnemyMove(level, row, col);
                    break;

                case EnemyType.Horizontal:
                    horizontalEnemyMove(level, row, col);
                    break;

                case EnemyType.Random:
                    randomEnemyMove(level, row, col);
                    break;

                case EnemyType.Super:
                    randomEnemyMove(level, row, col);
                    break;

                default:
                    Console.WriteLine("This is not a valid enemy");
                    break;
            }
        }

        public void randomEnemyMove(Level level, int row, int col)
        {
            currX = row;
            currY = col;
            prevX = currX;
            prevY = currY;

            pos = numGen.Next(4);

            if(pos == 0)
            {
                if (currX != level.grid.GetLength(0)-1)
                {
                    currX += 1;
                }
                else
                {
                    currX -= 1;
                }
            }
            else if(pos == 1)
            {
                if (currY != level.grid.GetLength(0)-1)
                {
                    currY += 1;
                }
                else
                {
                    currY -= 1;
                }
            }
            else if (pos == 2)
            {
                if (currX != 0)
                {
                    currX -= 1;
                }
                else
                {
                    currX += 1;
                }
            }
            else if (pos == 3)
            {
                if (currY != 0)
                {
                    currY -= 1;
                }
                else
                {
                    currY += 1;
                }
            }

            checkForNonPlayerCell(level, prevX, prevY, currX, currY);
        }

        public void verticalEnemyMove(Level level, int row, int col)
        {
            currX = row;
            prevX = currX;
            currY = col;

            if (dirVert == false)
            {
                if (currX != level.grid.GetLength(0) - 1)
                {
                    currX += 1;
                    //Level.setPrevEnemyCell(prevX, 6);
                    //Level.setEnemyCell(currX, 6);
                    checkForNonPlayerCell(level, prevX, currY, currX, currY);
                }
                else if (currX == level.grid.GetLength(0) - 1)
                {
                    dirVert = true;
                    prevX++;
                }
            }
            else if (dirVert == true)
            {
                if (currX != 0)
                {
                    currX -= 1;
                    checkForNonPlayerCell(level, prevX, currY, currX, currY);
                }
                else if (currX == 0)
                {
                    dirVert = false;
                    prevX--;
                }
            }
        }

        public void horizontalEnemyMove(Level level, int row, int col)
        {
            currY = col;
            prevY = currY;
            currX = row;

            if (dirHoriz == false)
            {
                if (currY != level.grid.GetLength(0) - 1)
                {
                    currY += 1;
                    //Level.setPrevEnemyCell(8, prevY);
                    //Level.setEnemyCell(8, currY);
                    checkForNonPlayerCell(level, currX, prevY, currX, currY);
                }
                else if (currY == level.grid.GetLength(0) - 1)
                {
                    dirHoriz = true;
                    //Level.setPrevEnemyCell(8, currY);
                    //Level.setEnemyCell(8, prevY);
                    //checkForNonPlayerCell(prevX, prevY, currX, currY);
                }
            }
            else if (dirHoriz == true)
            {
                if (currY != 0)
                {
                    currY -= 1;
                    //Level.setPrevEnemyCell(8, prevY);
                    //Level.setEnemyCell(8, currY);
                    checkForNonPlayerCell(level, currX, prevY, currX, currY);
                }
                else if (currY == 0)
                {
                    dirHoriz = false;
                    //Level.setPrevEnemyCell(8, currY);
                    //Level.setEnemyCell(8, prevY);
                    //checkForNonPlayerCell(prevX, prevY, currX, currY);
                }
            }
        }

        public void checkForNonPlayerCell(Level level, int lastX, int lastY, int presentX, int presentY)
        {
            currX = presentX;
            currY = presentY;
            prevX = lastX;
            prevY = lastY;

            if(treasureUnchanged == true)
            {
                level.setBoardCell(prevX, prevY, Level.TREASURE);
                level.setBoardCell(currX, currY, Level.ENEMY);
                treasureUnchanged = false;
            }
            else if (powerUnchanged == true)
            {
                level.setBoardCell(prevX, prevY, Level.POWER);
                level.setBoardCell(currX, currY, Level.ENEMY);
                powerUnchanged = false;
            }
            else if (level.grid[currX, currY] == Level.TREASURE)
            {
                level.setBoardCell(prevX, prevY, Level.EMPTY);
                level.setBoardCell(currX, currY, Level.ENEMY);
                treasureUnchanged = true;

            }
            else if (level.grid[currX, currY] == Level.POWER)
            {
                level.setBoardCell(prevX, prevY, Level.EMPTY);
                level.setBoardCell(currX, currY, Level.ENEMY);
                powerUnchanged = true;
            }
            else
            {
                level.setBoardCell(prevX, prevY, Level.EMPTY);
                level.setBoardCell(currX, currY, Level.ENEMY);
            }
        }
    }
}
