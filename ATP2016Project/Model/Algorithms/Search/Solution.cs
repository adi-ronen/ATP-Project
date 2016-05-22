using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    public class Solution
    {
        private ArrayList m_solutionPath;
        
        /// <summary>
        /// constractor
        /// </summary>
        public Solution()
        {
            m_solutionPath = new ArrayList();
        }

        /// <summary>
        /// add a new state
        /// </summary>
        /// <param name="state">the new state to add</param>
        public void AddState(AState state)
        {
            m_solutionPath.Add(state);
        }

        /// <summary>
        /// check if a solution exists
        /// </summary>
        /// <returns> true if exists false otherwise</returns>
        public bool IsSolutionExists()
        {
            return m_solutionPath.Count > 0;
        }

        /// <summary>
        /// get the number of states in the solution
        /// </summary>
        /// <returns> number of states in the solution</returns>
        public int GetSolutionSteps()
        {
            return m_solutionPath.Count;
        }
        /// <summary>
        /// get the solution path
        /// </summary>
        /// <returns>the solution path</returns>
        public ArrayList GetSolutionPath()
        {
            return m_solutionPath;
        }

        /// <summary>
        /// returns a string that contains the solution path
        /// </summary>
        /// <returns></returns>
        public string DisplaySolutionPath()
        {
            string str="";
            foreach (AState state in m_solutionPath)
            {
                str+=state.display();
                str += "\n";
            }
            return str;
        }

        /// <summary>
        /// reverse the solution
        /// </summary>
        public void RevereseSolution()
        {
            m_solutionPath.Reverse();
        }
    }
}
