using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{

    public abstract class AState : IComparable<AState>
    {
        protected AState parentState;

        /// <summary>
        /// get the parent state
        /// </summary>
        /// <returns> parent state</returns>
        public AState GetParent()
        {
            return parentState;
        }

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="_parentState"> the parent of the new state</param>
        public AState(AState _parentState)
        {
            parentState = _parentState;
        }
        /// <summary>
        /// comper to other state
        /// </summary>
        /// <param name="other"> state to compare</param>
        /// <returns> the answer to the compare</returns>
        public abstract int CompareTo(AState other);
        /// <summary>
        /// check if equal to other state
        /// </summary>
        /// <param name="obj"> state to compare</param>
        /// <returns> true if equals false otherwise</returns>
        public abstract bool Equals(AState obj);
        /// <summary>
        /// print state
        /// </summary>
        public abstract void PrintState();

        internal abstract string display();
        
    }
}
