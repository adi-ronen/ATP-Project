using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    class DepthFirstSearch : ASearchingAlgorithm
    {
        
        private Stack<AState> stack;

        /// <summary>
        /// constractor
        /// </summary>
        public DepthFirstSearch() { stack = new Stack<AState>(); }

        /// <summary>
        /// solving searchable problem acording to dfs algorithm
        /// </summary>
        /// <param name="searchDomain"> searcheble problem </param>
        /// <returns> solution to the problem </returns>
        public override Solution Solve(ISearchable searchDomain)
        {
            StartMeasureTime();
            Solution sol = new Solution();

            AState goal = searchDomain.GetGoalState();
            AState state = searchDomain.GetStartState();
            IEnumerable<AState> stateSuccessors;

            stack.Push(state);

            while (stack.Count>0)
            {
                state = stack.Pop();

                if (state.Equals(goal))
                {
                    sol = makeSolotion(state);
                    break;
                }

                else
                {
                    stateSuccessors = searchDomain.GetAllSuccessors(state);

                    foreach (AState successor in stateSuccessors)
                    {
                        stack.Push(successor);
                        m_countGeneratedNodes++;
                    }
                }

            }
            searchDomain.recoverProblem();
            StopMeasureTime();
            return sol;
        }
    }
    
}
