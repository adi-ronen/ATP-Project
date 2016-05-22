using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    interface IMaze
    {
        /// <summary>
        /// print the maze
        /// </summary>
        void print();
        /// <summary>
        /// get start position
        /// </summary>
        /// <returns> start position</returns>
        Position getStartPosition();
        /// <summary>
        /// get goal position
        /// </summary>
        /// <returns>goal position</returns>
        Position getGoalPosition();
    }
}
