using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    public interface ISearchable
    {
        /// <summary>
        /// get the start state
        /// </summary>
        /// <returns> start state</returns>
        AState GetStartState();
        /// <summary>
        /// get the goal state
        /// </summary>
        /// <returns> goal state</returns>
        AState GetGoalState();
        /// <summary>
        /// get all the successors of the current state
        /// </summary>
        /// <param name="state"> the state</param>
        /// <returns> list of the successors states</returns>
        IEnumerable<AState> GetAllSuccessors(AState state);
        /// <summary>
        /// recover the problem
        /// </summary>
        void recoverProblem();

    }
}
