
using ATP2016Project.Controller;
using ATP2016Project.Model;
using ATP2016Project.Model.Algorithms;
using ATP2016Project.Model.Algorithms.Compression;
using ATP2016Project.Model.Algorithms.MazeGenerators;
using ATP2016Project.Model.Algorithms.Search;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine("TEST 1: testing 2d maze genaretor");
            //testMazeGenerator(new SimpleMaze2dGenerator());
            //Console.WriteLine();
            //Console.WriteLine("TEST 2: testing 3d maze genaretor");
            //testMazeGenerator(new MyMaze3dGenerator());

            //Console.WriteLine();
            //Console.ReadKey();

            // testSearchAlgorithms();

            //testCompression();

            testCLI();

        }

        private static void testCLI()
        {
            IController controller = new MyController();
            IModel model = new MyModel(controller);
            controller.SetModel(model);
            IView view = new CLI(controller);
            controller.SetView(view);
            
            view.Start();
        }

        private static void testCompression()
        {
            MyMaze3dGenerator smg = new MyMaze3dGenerator();
            Maze maze= smg.generate(9, 6, 5);
            byte[] byets = ((Maze3d)maze).toByteArray();
            using (FileStream fileOutStream = new FileStream("1.maze", FileMode.Create))
            {
                using (MyCompressorStream outStream = new MyCompressorStream(fileOutStream,MyCompressorStream.Compress))
                {
                    outStream.Write(byets, 0, byets.Length);
                    outStream.Flush();
                }
            }
            byte[] mazeBytes;
            using (FileStream fileInStream = new FileStream("1.maze", FileMode.Open))
            {
                using (MyCompressorStream inStream = new MyCompressorStream(fileInStream,MyCompressorStream.Decompress))
                {
                    mazeBytes = new byte[byets.Length];
                    inStream.Read(mazeBytes,0,mazeBytes.Length);
                }
            }
            Maze3d loadedMaze = new Maze3d(mazeBytes);
            Console.WriteLine(loadedMaze.equals(maze)); 
            Console.WriteLine("*******************************");
            Console.WriteLine("Press any key to continue");
            Console.WriteLine();
            Console.ReadKey();
        }

        /// <summary>
        /// Part 2
        /// testing the search algorithms
        /// </summary>
        private static void testSearchAlgorithms()
        {
            MyMaze3dGenerator smg = new MyMaze3dGenerator();
            Maze maze3d = smg.generate(20, 20, 10);
            ISearchable maze = new SearchableMaze3d((Maze3d)maze3d);
            AState startState = maze.GetStartState();

            Console.WriteLine("**************************************");
            Console.WriteLine("*******TESTING SEARCH ALGORITHMS******");
            Console.WriteLine("**************************************");
            Console.WriteLine("maze to solve:");
            Console.WriteLine();
            maze3d.printMapKeys();
            maze3d.print();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("TEST 1: testing Breadth First Search algorithm");
            Console.WriteLine("**************************************");
            Console.WriteLine();

            ASearchingAlgorithm bfs = new BreadthFirstSearch();
            Solution solution = bfs.Solve(maze);

            if (solution.IsSolutionExists())
            {
                Console.WriteLine("Solution found:");
                Console.WriteLine("***************");
                Console.WriteLine();
                Console.WriteLine("Path:");

                foreach (AState state in solution.GetSolutionPath())
                {
                    state.PrintState();
                }
            }
            else
            {
                Console.WriteLine("No Solution found!");
            }

            Console.WriteLine("");
            Console.WriteLine(string.Format("Moves to goals state: {0}", solution.GetSolutionSteps()));
            Console.WriteLine(string.Format("Nodes generated: {0}", bfs.GetNumberOfGeneratedNodes()));
            Console.WriteLine(string.Format("Solving time (miliseconds): {0}", bfs.GetSolvingTimeMiliseconds()));

            Console.WriteLine("Press any key to continue");
            Console.WriteLine();
            Console.ReadKey();


            Console.WriteLine("TEST 2: testing Depth First Search algorithm");
            Console.WriteLine("**************************************");
            Console.WriteLine();
            ASearchingAlgorithm dfs = new DepthFirstSearch();
            Solution solution2 = dfs.Solve(maze);

            if (solution2.IsSolutionExists())
            {
                Console.WriteLine("Solution found:");
                Console.WriteLine("***************");
                Console.WriteLine();
                Console.WriteLine("Path:");

                foreach (AState state in solution2.GetSolutionPath())
                {
                    state.PrintState();
                }
            }
            else
            {
                Console.WriteLine("No Solution found!");
            }

            Console.WriteLine("");
            Console.WriteLine(string.Format("Moves to goals state: {0}", solution2.GetSolutionSteps()));
            Console.WriteLine(string.Format("Nodes generated: {0}", dfs.GetNumberOfGeneratedNodes()));
            Console.WriteLine(string.Format("Solving time (miliseconds): {0}", dfs.GetSolvingTimeMiliseconds()));

            Console.ReadLine();
        }

        /// <summary>
        /// test to the Maze Generator
        /// </summary>
        /// <param name="mg">Maze Generator</param>
        private static void testMazeGenerator(IMazeGenerator mg)
        {
            Console.WriteLine("**************************************");
            Console.WriteLine("*Testing algorithm time: ");
            try
            {
                Console.WriteLine(mg.measureAlgorithmTime(10, 10, 10));
                Console.WriteLine();

            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                Maze maze = mg.generate(10, 10, 10);
                Position start = maze.getStartPosition();
                Console.Write("*Start Position: ");
                start.print();
                Console.Write("*Goal Position: ");
                maze.getGoalPosition().print();
                Console.WriteLine();
                maze.printMapKeys();
                maze.print();
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();
            Console.WriteLine("**************************************");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

        }
    }

}
