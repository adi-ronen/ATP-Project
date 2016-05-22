using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP2016Project.Controller;
using ATP2016Project.Model.Algorithms.MazeGenerators;
using System.Runtime.CompilerServices;
using ATP2016Project.Model.Algorithms.Search;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using ATP2016Project.Model.Algorithms.Compression;
using ATP2016Project.Model.Algorithms;

namespace ATP2016Project.Model
{
    class MyModel : IModel
    {
        private IController controller;
        private Dictionary<string, Maze3d> m_mazes = new Dictionary<string, Maze3d>();
        private Dictionary<string, Solution> m_solutions = new Dictionary<string, Solution>();

        public MyModel(IController controller)
        {
            this.controller = controller;
        }


        #region Maze Dictionary
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void AddMaze(string mazeName, Maze3d maze)
        {
            m_mazes.Add(mazeName, maze);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private Maze3d RetrieveMaze(string mazeName)
        {
            if (m_mazes.ContainsKey(mazeName))
            {
                return m_mazes[mazeName];
            }
            else
            {
                return null;
            }
        }

        private bool IsMazeExists(string name)
        {
            return m_mazes.ContainsKey(name);
        }
        #endregion
        #region Solutions Dictionary
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void AddSolution(string mazeName, Solution solution)
        {
            m_solutions.Add(mazeName, solution);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private Solution RetrieveSolution(string mazeName)
        {
            if (m_solutions.ContainsKey(mazeName))
            {
                return m_solutions[mazeName];
            }
            else
            {
                return null;
            }
        }

        private bool IsSolutionExists(string mazeName)
        {
            return (m_solutions.ContainsKey(mazeName));
        }

        #endregion

        public void SaveToDisk(string mazeName, string path)
        {
            if (IsMazeExists(mazeName))
            {
                using (Stream compressed = new FileStream(@path, FileMode.Create))
                {
                    using (Stream mycompressor = new MyCompressorStream(compressed, MyCompressorStream.Compress))
                    {
                        byte[] mazebytes = m_mazes[mazeName].toByteArray();
                        mycompressor.Write(mazebytes, 0, mazebytes.Length);
                    }
                }
            }
            else
                throw new Exception("maze name " + mazeName + " doesn't exist!");
        }

        public void LoadFromDisk(string mazeName, string path)
        {
            using (Stream compressed = new FileStream(path, FileMode.Open))
            {
                using (Stream mydecompressor = new MyCompressorStream(compressed, MyCompressorStream.Decompress))
                {
                    byte[] size = new byte[3];
                    mydecompressor.Read(size, 0, size.Length);
                    byte[] mazebytes = new byte[3+(size[0] * size[1] * size[2])];
                    for (int i = 0; i < 3; mazebytes[i] = size[i], i++) ;
                    mydecompressor.Read(mazebytes, 3, mazebytes.Length-3);
                    Maze3d newMaze = new Maze3d(mazebytes);
                    m_mazes.Add(mazeName, newMaze);
                }
            }
        }

        public void DisplayMaze(string mazeName)
        {
            if (IsMazeExists(mazeName))
                RetrieveMaze(mazeName).print();
            else
                throw new Exception("maze " + mazeName + " dosen't exist!");
        }

        public void genarateMaze(string mazeName, int x, int y, int z)
        {
            new Thread(() =>
            {
                MyMaze3dGenerator mg = new MyMaze3dGenerator();
                Maze3d maze = (Maze3d)mg.generate(x, y, z);
                AddMaze(mazeName, maze);
                controller.output("Maze " + mazeName + " genarated!");
            }).Start();

        }

        public void SolveMaze(string mazeName)
        {
            if (IsMazeExists(mazeName))
            {
                if (!IsSolutionExists(mazeName))
                {
                    new Thread(() =>
                    {
                        Maze3d maze3d = m_mazes[mazeName];
                        ISearchable maze = new SearchableMaze3d(maze3d);
                        AState startState = maze.GetStartState();
                        ASearchingAlgorithm bfs = new BreadthFirstSearch();
                        Solution solution = bfs.Solve(maze);
                        m_solutions.Add(mazeName, solution);
                        controller.output("solution for " + mazeName + " is ready");
                    }).Start();
                }
                else
                    controller.output("solution for " + mazeName + " is ready");
            }
            else
                throw new Exception("maze " + mazeName + " dosen't exist!");
        }

        public void DisplaySolution(string mazeName)
        {
            if(IsMazeExists(mazeName))
            {
                if (IsSolutionExists(mazeName))
                {
                    controller.output(m_solutions[mazeName].DisplaySolutionPath());
                    
                }
                else
                    throw new Exception("solution for " + mazeName + " doesn't exist!");
            }
            else
                throw new Exception("maze " + mazeName + " dosen't exist!");
        }
    }
}
