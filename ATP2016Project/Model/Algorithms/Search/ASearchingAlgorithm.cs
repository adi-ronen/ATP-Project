using ATP2016Project.Model.Algorithms.Search;
using System.Diagnostics;

namespace ATP2016Project.Model.Algorithms
{
    public abstract class ASearchingAlgorithm : ISearchingAlgorithm
    {
        private Stopwatch m_stopWatch;
        protected int m_countGeneratedNodes = 1; 


       /// <summary>
       /// defoult constarctor 
       /// </summary>
        public ASearchingAlgorithm()
        {
            m_stopWatch = new Stopwatch();
        }

        /// <summary>
        /// get the number of the nodes we genaretad
        /// </summary>
        /// <returns> the number of the nodes we genaretad</returns>
        public int GetNumberOfGeneratedNodes()
        {
            return m_countGeneratedNodes;
        }

        /// <summary>
        /// solve the searcheble problem
        /// </summary>
        /// <param name="searchDomain"></param>
        /// <returns> solution</returns>
        public abstract Solution Solve(ISearchable searchDomain);


        /// <summary>
        /// start the running time
        /// </summary>
        public void StartMeasureTime()
        {
            m_stopWatch.Restart();
        }

        /// <summary>
        /// end the running time
        /// </summary>
        public void StopMeasureTime()
        {
            m_stopWatch.Stop();
        }

        /// <summary>
        /// get the solving time in miliseconds 
        /// </summary>
        /// <returns>time in miliseconds </returns>
        public long GetSolvingTimeMiliseconds()
        {
            return m_stopWatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// reconstruction all the moves from the end position to the start and reverce it 
        /// </summary>
        /// <param name="state"> the end position</param>
        /// <returns> the solution to the problem </returns>
        protected Solution makeSolotion(AState state)
        {
            Solution result = new Solution();
            while (state != null)
            {
                result.AddState(state);
                state = state.GetParent();
            }

            result.RevereseSolution();

            return result;
        }
    }
}