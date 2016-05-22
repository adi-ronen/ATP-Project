using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    class SimpleMaze3dGenerator : AMazeGenerator
    {
        public override Maze generate(int x, int y, int z)
        {
            Maze2d[] mazeMap = new Maze2d[z];
            for (int i = 0; i < z; i++)
            {
                mazeMap[i] = make2dMaze(x, y);
            }


        }

        private Maze2d make2dMaze(int x, int y)
        {

        }
        private static void fillArrayFrame(int[,] myMaze)
        {
            for (int i = 0; i < myMaze.GetLength(0); i++)
            {
                myMaze[i, 0] = Wall;
                myMaze[i, myMaze.GetLength(1) - 1] = Wall;


            }
            for (int j = 0; j < myMaze.GetLength(1); j++)
            {
                myMaze[0, j] = Wall;
                myMaze[myMaze.GetLength(1) - 1, j] = Wall;

            }
        }
    }
}
