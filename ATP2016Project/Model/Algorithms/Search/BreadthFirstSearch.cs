using ATP2016Project.Model.Algorithms.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms
{
    class BreadthFirstSearch : ASearchingAlgorithm
    {
        private Queue<AState> queue;

        /// <summary>
        /// constractor
        /// </summary>
        public BreadthFirstSearch() { queue = new Queue<AState>(); }


        /// <summary>
        /// solving searchable problem acording to bfs algorithm
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

            queue.Enqueue(state);

            while(queue.Count>0)
            {
                state = queue.Dequeue();

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
                        queue.Enqueue(successor);
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
