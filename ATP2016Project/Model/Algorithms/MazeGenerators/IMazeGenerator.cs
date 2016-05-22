using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    interface IMazeGenerator
    {
        /// <summary>
        /// genarate maze
        /// </summary>
        /// <param name="x"> x size</param>
        /// <param name="y"> y size</param>
        /// <param name="z"> z size</param>
        /// <returns> maze</returns>
        Maze generate(int x, int y, int z);
        /// <summary>
        /// measure the time that took to the algorithm to genarate a maze
        /// </summary>
        /// <param name="x"> x size</param>
        /// <param name="y"> y size</param>
        /// <param name="z"> z size</param>
        /// <returns> time </returns>
        string measureAlgorithmTime(int x, int y, int z);
    }
}
