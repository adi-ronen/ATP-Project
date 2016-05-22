using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP2016Project.Model.Algorithms.Search;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    class SearchableMaze3d : ISearchable
    {
        private Maze3d maze;

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="_maze"></param>
        public SearchableMaze3d(Maze3d _maze) { maze = _maze; }


        /// <summary>
        ///  gets a state and looks for possible moves
        /// </summary>
        /// <param name="state"> current state</param>
        /// <returns> list of all the possible moves from this state</returns>
        public IEnumerable<AState> GetAllSuccessors(AState state)
        {
            List<AState> successors = new List<AState>();
            MazeState mazeState = (MazeState)state;

            if(maze.isAvailable(mazeState.GetPosition().X+1, mazeState.GetPosition().Y, mazeState.GetPosition().Z))
            {
                maze.changeToVisited(mazeState.GetPosition());
                Position newPos = new Position(mazeState.GetPosition().X + 1, mazeState.GetPosition().Y, mazeState.GetPosition().Z);
                successors.Add(new MazeState(newPos, mazeState));
            }

            if (maze.isAvailable(mazeState.GetPosition().X - 1, mazeState.GetPosition().Y, mazeState.GetPosition().Z))
            {
                maze.changeToVisited(mazeState.GetPosition());
                Position newPos = new Position(mazeState.GetPosition().X - 1, mazeState.GetPosition().Y, mazeState.GetPosition().Z);
                successors.Add(new MazeState(newPos, mazeState));
            }

            if (maze.isAvailable(mazeState.GetPosition().X, mazeState.GetPosition().Y+1, mazeState.GetPosition().Z))
            {
                maze.changeToVisited(mazeState.GetPosition());
                Position newPos = new Position(mazeState.GetPosition().X, mazeState.GetPosition().Y+1, mazeState.GetPosition().Z);
                successors.Add(new MazeState(newPos, mazeState));
            }

            if (maze.isAvailable(mazeState.GetPosition().X, mazeState.GetPosition().Y-1, mazeState.GetPosition().Z))
            {
                maze.changeToVisited(mazeState.GetPosition());
                Position newPos = new Position(mazeState.GetPosition().X, mazeState.GetPosition().Y-1, mazeState.GetPosition().Z);
                successors.Add(new MazeState(newPos, mazeState));
            }

            if (maze.isAvailable(mazeState.GetPosition().X, mazeState.GetPosition().Y, mazeState.GetPosition().Z+1))
            {
                maze.changeToVisited(mazeState.GetPosition());
                Position newPos = new Position(mazeState.GetPosition().X, mazeState.GetPosition().Y, mazeState.GetPosition().Z+1);
                successors.Add(new MazeState(newPos, mazeState));
            }

            if (maze.isAvailable(mazeState.GetPosition().X, mazeState.GetPosition().Y, mazeState.GetPosition().Z-1))
            {
                maze.changeToVisited(mazeState.GetPosition());
                Position newPos = new Position(mazeState.GetPosition().X, mazeState.GetPosition().Y, mazeState.GetPosition().Z-1);
                successors.Add(new MazeState(newPos, mazeState));
            }

            return successors;
        }

        /// <summary>
        /// get the goal position
        /// </summary>
        /// <returns> goal state</returns>
        public AState GetGoalState()
        {
            return new MazeState(maze.getGoalPosition(), null);
        }

        /// <summary>
        /// get the start position
        /// </summary>
        /// <returns> start state</returns>
        public AState GetStartState()
        {
            return new MazeState(maze.getStartPosition(), null);
        }


        /// <summary>
        /// recover the maze to original 
        /// </summary>
        public void recoverProblem()
        {
            maze.recoverMaze();
        }
    }
}
