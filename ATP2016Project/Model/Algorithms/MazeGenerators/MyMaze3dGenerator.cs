using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    class MyMaze3dGenerator : AMazeGenerator
    {
        private static int ByWidth = 0;
        private static int ByLength = 1;
        /// <summary>
        ///  genarate maze with given size
        /// </summary>
        /// <param name="x"> widht </param>
        /// <param name="y"> length </param>
        /// <param name="z"> depth </param>
        /// <returns> maze</returns>
        public override Maze generate(int x, int y, int z)
        {
            if (x <= 2 || y <= 2 || z <= 2)
            {
                throw new NotSupportedException("cant genarate maze this size");
            }
            Position start = randomPosition(x, y,0);
            Thread.Sleep(4);
            Position end = randomPosition(x, y,z-1);
            
            Maze2d[] mazeMap=new Maze2d[z];
            mazeMap[0]=fillSurface(x,y,start, Start);
            mazeMap[z-1] = fillSurface(x,y,end,End);
            
            for (int i = 1; i < z-1; i++)
            {
                mazeMap[i]= make2dMaze(x, y);
            }
            Maze maze = new Maze3d(start, end, mazeMap);
            return maze;

        }

        /// <summary>
        ///  gives a random values to a point in given space
        /// </summary>
        /// <param name="x"> width </param>
        /// <param name="y"> length </param>
        /// <param name="z"> z cordinate </param>
        /// <returns> a random position</returns>
        private Position randomPosition(int x, int y,int z)
        {
            Random rnd = new Random();
            int rndX = (rnd.Next(1, x - 2) / 2) * 2 + 1;
            Thread.Sleep(1);
            int rndY = (rnd.Next(1, y - 2) / 2) * 2 + 1;
            return new Position(rndX, rndY,z);

        }

        
        /// <summary>
        /// fill the given surface and leave a one point pass 
        /// </summary>
        /// <param name="x"> width </param>
        /// <param name="y"> length </param>
        /// <param name="point"> point to leave passage</param>
        /// <param name="state"> whether the point is start or end </param>
        /// <returns> 2d maze </returns>
        private Maze2d fillSurface(int x,int y, Position point, byte state)
        {

            byte[,] myMaze = new byte[x, y];
            for (int i = 0; i < myMaze.GetLength(0); i++)
            {
                for (int j = 0; j < myMaze.GetLength(1); j++)
                {
                    myMaze[i, j] = Wall;

                }
            }
            myMaze[point.X, point.Y]= state;
            Maze2d maze = new Maze2d(myMaze);
            return maze;
        }

        /// <summary>
        ///  genarate 2d maze by recursive division algorithm
        /// </summary>
        /// <param name="x"> width </param>
        /// <param name="y"> length </param>
        /// <returns> 2d maze</returns>
        private Maze2d make2dMaze(int x, int y)
        {
            byte[,] myMaze = new byte[x, y];
            recursiveDivision(myMaze,0, y, 0, x);
            fillArrayFrame(myMaze);

            Maze2d maze = new Maze2d(myMaze);
            return maze;

        }

        /// <summary>
        ///   the recursive calling, diside if divied verticaly or horizontaly
        /// </summary>
        /// <param name="myMaze"> the maze to divide </param>
        /// <param name="left"> left corner </param>
        /// <param name="right"> right corner </param>
        /// <param name="top"> top corner </param>
        /// <param name="bottom"> bottom corner</param>
        private void recursiveDivision(byte[,] myMaze, int left, int right, int top, int bottom)
        {
            int length = bottom-top;
            int width = right - left;

            if (length > 2 && width > 2)
            {
                if (length > width)
                {
                    divide(myMaze, left, right, top, bottom, ByWidth);
                }
                else if (length < width)
                {
                    divide(myMaze, left, right, top, bottom, ByLength);
                }
                else
                {
                    Random random = new Random();
                    int direction = random.Next(0, 2);
                    switch (direction)
                    {
                        case 0:
                            divide(myMaze, left, right, top, bottom, ByWidth);
                            break;
                        case 1:
                            divide(myMaze, left, right, top, bottom, ByLength);
                            break;
                    }
                }
                Thread.Sleep(1);

            }
            else if (length >= 2 && width >= 2)
            {
                if (length > 2 && width <= 2)
                    divide(myMaze, left, right, top, bottom, ByWidth);
                else if (width > 2 && length <= 2)
                    divide(myMaze, left, right, top, bottom, ByLength);
            }
        }

        /// <summary>
        ///  divide the givem maze into two smaller one
        /// </summary>
        /// <param name="myMaze"> the maze to divide </param>
        /// <param name="left"> left corner </param>
        /// <param name="right"> right corner </param>
        /// <param name="top"> top corner </param>
        /// <param name="bottom"> bottom corner</param>
        /// <param name="side"> the diraction to divide </param>
        private void divide(byte[,] myMaze,int left, int right, int top, int bottom,int side)
        {
            int width = right - left;
            int length = bottom - top;

            Random random = new Random();
            if (side == ByWidth)
            {
                int border = (random.Next(top + 2, bottom) / 2) * 2;
                makeBorder(myMaze, border, left, right, ByWidth);
                recursiveDivision(myMaze, left, right, top, border);
                recursiveDivision(myMaze, left, right, border, bottom);
            }
            else if (side == ByLength)
            {
                int border = (random.Next(left + 2, right) / 2) * 2;
                makeBorder(myMaze, border, top, bottom, ByLength);
                recursiveDivision(myMaze, left, border, top, bottom);
                recursiveDivision(myMaze, border, right, top, bottom);
            }
            
        }


        /// <summary>
        /// choose a border and make a randomly pass on it
        /// </summary>
        /// <param name="myMaze">  maze array</param>
        /// <param name="border"> boarder cordinate</param>
        /// <param name="start"> the start of the border</param>
        /// <param name="end"> the end of the border</param>
        /// <param name="side"> the diraction to build the border </param>
        private void makeBorder(byte[,] myMaze, int border, int start, int end, int side)
        {
            if (end - start > 2)
            {
                Random random = new Random();
                int pass = (random.Next(start + 1, end - 2) / 2) * 2 + 1;
                if (side == ByWidth)
                {
                    for (int i = start; i < end; i++)
                    {
                        myMaze[border, i] = Wall;

                    }
                    makePass(myMaze, new Position(border, pass));

                }
                else if (side == ByLength)
                {
                    for (int i = start; i < end; i++)
                    {
                        myMaze[i, border] = Wall;
                    }
                    makePass(myMaze, new Position(pass, border));

                }
            }

        }
        /// <summary>
        ///  fill the frame of the array with walls
        /// </summary>
        /// <param name="myMaze"> the maze array </param>
        private static void fillArrayFrame(byte[,] myMaze)
        {
            int width = myMaze.GetLength(0);
            int length = myMaze.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                myMaze[i, 0] = Wall;
                myMaze[i, length - 1] = Wall;


            }
            for (int j = 0; j < length; j++)
            {
                myMaze[0, j] = Wall;
                myMaze[width - 1, j] = Wall;

            }
        }
    }
}
