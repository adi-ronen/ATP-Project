using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    abstract class AMazeGenerator : IMazeGenerator
    {
        protected static byte Wall = 1;
        protected static byte Passage = 0;
        protected static byte Start = 2;
        protected static byte End = 3;

        /// <summary>
        /// abstract method that genarate 
        /// </summary>
        /// <param name="x">x size</param>
        /// <param name="y"> y size</param>
        /// <param name="z">z size</param>
        /// <returns> maze</returns>
        abstract public Maze generate(int x, int y , int z);

        /// <summary>
        /// time to genarate given sized maze
        /// </summary>
        /// <param name="x"> x cordinate</param>
        /// <param name="y"> y cordinate</param>
        /// <param name="z"> z cordinate</param>
        /// <returns> string that declairs how long it took to genarate</returns>
        public string measureAlgorithmTime(int x, int y, int z)
        {
            DateTime startTime = DateTime.Now;
            this.generate(x,y,z);
            DateTime finishTime = DateTime.Now;
            return "Total genarate time: "+(finishTime - startTime).TotalSeconds+" seconds" ;
        }
        /// <summary>
        ///  make a passage in a given position
        /// </summary>
        /// <param name="myMaze"> the maze</param>
        /// <param name="current"> the position we want to make a passage in</param>
        protected void makePass(byte[,] myMaze, Position current)
        {
            myMaze[current.X, current.Y] = Passage;
        }
    }
}
