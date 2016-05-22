using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    abstract class Maze : IMaze
    {
        protected static byte Passage = 0;
        protected static byte Wall = 1;
        protected static byte Start = 2;
        protected static byte End = 3;
        protected static byte Visited = 4;

        protected Position start, end;

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="_start"> start position</param>
        /// <param name="_end"> end position</param>
        public Maze(Position _start , Position _end) { start = _start; end = _end; }
        /// <summary>
        /// print the maze
        /// </summary>
        abstract public void print();
        /// <summary>
        /// get start position 
        /// </summary>
        /// <returns> start position</returns>
        public Position getStartPosition()
        {
            return start;
        }

        
        /// <summary>
        /// get goal position
        /// </summary>
        /// <returns>goal position</returns>
        public Position getGoalPosition()
        {
            return end;
        }
        /// <summary>
        /// print the map keys
        /// </summary>
        public void printMapKeys()
        {
            Console.WriteLine("     keys: ");
            Console.WriteLine("     ***********");
            Console.Write("     *");
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" Passage ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("*");
            Console.Write("     *");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("  Wall   ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("*");
            Console.WriteLine("     ***********");



        }
        public abstract bool equals(Maze maze);


    }
}
