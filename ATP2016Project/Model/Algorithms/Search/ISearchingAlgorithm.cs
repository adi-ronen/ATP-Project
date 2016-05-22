using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    public interface ISearchingAlgorithm
    {
        /// <summary>
        /// solve a searchable problem
        /// </summary>
        /// <param name="searchDomain"> the problem</param>
        /// <returns> solution to the problem</returns>
        Solution Solve(ISearchable searchDomain);
        /// <summary>
        /// get the number of the genarated nodes
        /// </summary>
        /// <returns> the number of the genarated nodes </returns>
        int GetNumberOfGeneratedNodes();
        /// <summary>
        /// get the time in miliseconds to solve the given problem
        /// </summary>
        /// <returns> the time in miliseconds to solve the given problem</returns>
        long GetSolvingTimeMiliseconds();
    }
}
