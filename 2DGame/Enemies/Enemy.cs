using _2DGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2DGame.Enemies
{
    public class Enemy : ICombatant
    {
        public enum EnemyType{Vertical, Horizontal, Random, Super};
        public int currX = 0;
        public int currY = 0;
        public int prevX = 0;
        public int prevY = 0;
        public static bool dirVert = false;
        public static bool dirHoriz = false;
        public bool powerUnchanged = false;
        public bool treasureUnchanged = false;
        public string nemesis;
        static Random numGen = new Random();
        int pos = 0;

        public Enemy(int row, int col, string type)
        {
            nemesis = type;
            currX = row;
            currY = col;
            Level.setEnemyCell(currX, currY);
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

        public string enemyType
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

        public void MoveCombatant(int row, int col, string enemy)
        {
            Thread current = Thread.CurrentThread;
            nemesis = enemy;
           
            switch(nemesis)
            {
                case "Vertical":
                    verticalEnemyMove(row);
                    break;

                case "Horizontal":
                    horizontalEnemyMove(col);
                    break;

                case "Random":
                    randomEnemyMove(row, col);
                    break;

                case "Super":
                    randomEnemyMove(row, col);
                    break;

                default:
                    Console.WriteLine("This is not a valid enemy");
                    break;
            }
        }

        public void randomEnemyMove(int row, int col)
        {
            currX = row;
            currY = col;
            prevX = currX;
            prevY = currY;

            pos = numGen.Next(4);

            if(pos == 0)
            {
                if (currX != Level.grid.GetLength(0)-1)
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
                if (currY != Level.grid.GetLength(0)-1)
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

            checkForNonPlayerCell(prevX, prevY, currX, currY);
            //Level.setBoardCell(prevX, prevY, Level.EMPTY);
            //Level.setBoardCell(currX, currY, Level.ENEMY);
            /*if(treasureUnchanged == true)
            {
                Level.setBoardCell(prevX, prevY, Level.TREASURE);
                Level.setBoardCell(currX, currY, Level.ENEMY);
                treasureUnchanged = false;
            }
            else if (powerUnchanged == true)
            {
                Level.setBoardCell(prevX, prevY, Level.POWER);
                Level.setBoardCell(currX, currY, Level.ENEMY);
                powerUnchanged = false;
            }
            else if (Level.grid[currX, currY] == Level.TREASURE)
            {
                //Level.setBoardCell(currX, currY, Level.ENEMY);
                //Level.setBoardCell(prevX, prevY, Level.EMPTY);
                Level.setBoardCell(prevX, prevY, Level.EMPTY);
                Level.setBoardCell(currX, currY, Level.ENEMY);
                treasureUnchanged = true;

            }
            else if(Level.grid[currX, currY] == Level.POWER)
            {
                //Level.setBoardCell(currX, currY, Level.ENEMY);
                //Level.setBoardCell(prevX, prevY, Level.EMPTY);
                Level.setBoardCell(prevX, prevY, Level.EMPTY);
                Level.setBoardCell(currX, currY, Level.ENEMY);
                powerUnchanged = true;
            }
            else
            {
                Level.setBoardCell(prevX, prevY, Level.EMPTY);
                Level.setBoardCell(currX, currY, Level.ENEMY);
            }*/

            //Level.setBoardCell(currX, currY, Level.ENEMY);

        }

        public void verticalEnemyMove(int row)
        {
            currX = row;
            prevX = currX;

            if (dirVert == false)
            {
                if (currX != Level.grid.GetLength(0) - 1)
                {
                    currX += 1;
                    //Level.setPrevEnemyCell(prevX, 6);
                    //Level.setEnemyCell(currX, 6);
                    checkForNonPlayerCell(prevX, 6, currX, 6);
                }
                else if (currX == Level.grid.GetLength(0) - 1)
                {
                    dirVert = true;
                    prevX++;
                    //checkForNonPlayerCell(prevX, prevY, currX, currY);
                    //Level.setPrevEnemyCell(currX, 6);
                    //Level.setEnemyCell(prevX, 6);
                }
            }
            else if (dirVert == true)
            {
                if (currX != 0)
                {
                    currX -= 1;
                    //Level.setPrevEnemyCell(prevX, 6);
                    //Level.setEnemyCell(currX, 6);
                    checkForNonPlayerCell(prevX, 6, currX, 6);
                }
                else if (currX == 0)
                {
                    dirVert = false;
                    prevX--;
                    //Level.setPrevEnemyCell(currX, 6);
                    //Level.setEnemyCell(prevX, 6);
                    //checkForNonPlayerCell(prevX, prevY, currX, currY);
                }
            }
        }

        public void horizontalEnemyMove(int col)
        {
            currY = col;
            prevY = currY;

            if (dirHoriz == false)
            {
                if (currY != Level.grid.GetLength(0) - 1)
                {
                    currY += 1;
                    //Level.setPrevEnemyCell(8, prevY);
                    //Level.setEnemyCell(8, currY);
                    checkForNonPlayerCell(8, prevY, 8, currY);
                }
                else if (currY == Level.grid.GetLength(0) - 1)
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
                    checkForNonPlayerCell(8, prevY, 8, currY);
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

        public void checkForNonPlayerCell(int lastX, int lastY, int presentX, int presentY)
        {
            currX = presentX;
            currY = presentY;
            prevX = lastX;
            prevY = lastY;

            if(treasureUnchanged == true)
            {
                Level.setBoardCell(prevX, prevY, Level.TREASURE);
                Level.setBoardCell(currX, currY, Level.ENEMY);
                treasureUnchanged = false;
            }
            else if (powerUnchanged == true)
            {
                Level.setBoardCell(prevX, prevY, Level.POWER);
                Level.setBoardCell(currX, currY, Level.ENEMY);
                powerUnchanged = false;
            }
            else if (Level.grid[currX, currY] == Level.TREASURE)
            {
                Level.setBoardCell(prevX, prevY, Level.EMPTY);
                Level.setBoardCell(currX, currY, Level.ENEMY);
                treasureUnchanged = true;

            }
            else if (Level.grid[currX, currY] == Level.POWER)
            {
                Level.setBoardCell(prevX, prevY, Level.EMPTY);
                Level.setBoardCell(currX, currY, Level.ENEMY);
                powerUnchanged = true;
            }
            else
            {
                Level.setBoardCell(prevX, prevY, Level.EMPTY);
                Level.setBoardCell(currX, currY, Level.ENEMY);
            }
        }
    }
}
