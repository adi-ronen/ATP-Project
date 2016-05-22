using ATP2016Project.Model.Algorithms.MazeGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    class MazeState : AState
    {
        private Position position;

        /// <summary>
        /// get position
        /// </summary>
        /// <returns> position</returns>
        public Position GetPosition()
        {
            return position;
        }

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="pos"> position of the new state</param>
        /// <param name="parentState"> the new state parent</param>
        public MazeState(Position pos, AState parentState): base(parentState) { position = pos; }
        
        /// <summary>
        /// comper the state to other state
        /// </summary>
        /// <param name="other"> state to compare</param>
        /// <returns> 0 if equal -1 otherwise</returns>
        public override int CompareTo(AState other)
        {
            if (this.Equals(other))
                return 0;
            return -1;
        }

        /// <summary>
        /// check if the state equal to other state
        /// </summary>
        /// <param name="obj"> state to compare </param>
        /// <returns> true if equal false otherwise</returns>
        public override bool Equals(AState obj)
        {
            if (obj is MazeState)
                return this.position.Equals(((MazeState)obj).position);
            return false;
        }

        /// <summary>
        /// print the state
        /// </summary>
        public override void PrintState()
        {
            this.position.print();
        }

        internal override string display()
        {
            return position.display();
        }
    }
}
