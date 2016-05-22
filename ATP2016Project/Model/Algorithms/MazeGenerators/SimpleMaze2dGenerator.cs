using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    class SimpleMaze2dGenerator : AMazeGenerator
    {
        enum Direction { LEFT, RIGHT, FORWARD, BACKWARD };

        /// <summary>
        /// genarate 2d maze
        /// </summary>
        /// <param name="x"> x size</param>
        /// <param name="y"> y size</param>
        /// <param name="z"> z size</param>
        /// <returns> maze</returns>
        public override Maze generate(int x, int y, int z)
        {
            if (x <= 2 || y <= 2)
            {
                throw new NotSupportedException("cant genarate maze this size");
            }
            byte[,] myMaze = new byte[x, y];

            Position start = new Position(0,0);
            Position end = new Position(x - 1, y - 1);
            Position current = new Position(0,0);
            

            fillArray(myMaze);//fill array with walls
            
            makePass(myMaze, current);//makes a passage in a given position

            while (!current.Equals(end))
            {
                getNextPosition(x, y, current);
                makePass(myMaze, current);
                Thread.Sleep(1);
            }
            myMaze[end.X, end.Y] = End;
            myMaze[start.X, start.Y] = Start;
            Maze maze = new Maze2d(start, end, myMaze);
            return maze;

        }


         
        /// <summary>
        /// gets the current position in the maze and move to a new legal position
        /// </summary>
        /// <param name="x"> wight</param>
        /// <param name="y"> </param>
        /// <param name="current"> current location to change</param>
        private void getNextPosition(int x, int y, Position current)
        {
            Random random = new Random();
            Direction d = (Direction)random.Next(0, 4);
            switch (d)
            {
                case Direction.LEFT:
                    if (current.Y - 1 >= 0)
                    {
                        current.Y--;
                        return;
                    }
                    break;
                case Direction.RIGHT:
                    if (current.Y + 1 < y)
                    {
                        current.Y++;
                        return;
                    }
                    break;
                case Direction.FORWARD:
                    if (current.X - 1 >= 0)
                    {
                        current.X--;
                        return;
                    }
                    break;
                case Direction.BACKWARD:
                    if (current.X + 1 < x)
                    {
                        current.X++;
                        return;
                    }
                    break;
                default:
                    break;
            }

        }
        
        /// <summary>
        ///  fill the 2d array with walls
        /// </summary>
        /// <param name="myMaze"> maze array</param>
        private static void fillArray(byte[,] myMaze)
        {
            for (int i = 0; i < myMaze.GetLength(0); i++)
            {
                for (int j = 0; j < myMaze.GetLength(1); j++)
                {
                    myMaze[i, j] = Wall;

                }
                
            }
        }
    }
}
