using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    class Maze2d : Maze
    {
        private byte[,] mazeMap;

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="_start">start position</param>
        /// <param name="_end"> end position</param>
        /// <param name="_mazeMap"> the array of the maze</param>
        public Maze2d(Position _start, Position _end, byte[,] _mazeMap) : base(_start, _end) { mazeMap = _mazeMap; }

        /// <summary>
        /// default constractor
        /// </summary>
        /// <param name="_mazeMap"> the array of the maze</param>
        public Maze2d(byte[,] _mazeMap) : base(new Position(), new Position()) { mazeMap = _mazeMap; }

        /// <summary>
        /// constractor getting the size of the maze and array byte
        /// </summary>
        /// <param name="x"> x</param>
        /// <param name="y"> y</param>
        /// <param name="bytes"> array</param>
        public Maze2d(int x, int y, byte[] bytes) : base(new Position(), new Position())
        {
            mazeMap = new byte[x, y];
            int currentIndex = 0;
            for (int i = 0; i < mazeMap.GetLength(0); i++)
            {
                for (int j = 0; j < mazeMap.GetLength(1); j++)
                {
                    if (bytes[currentIndex] == Maze.Start)
                        start= new Position(i, j);
                    if (bytes[currentIndex] == Maze.End)
                        end= new Position(i, j);

                    mazeMap[i, j] = bytes[currentIndex];
                    currentIndex++;
                   
                }
            }
        }
        

        /// <summary>
        /// prints the maze
        /// </summary>
        public override void print()
        {
            for (int i = 0; i < mazeMap.GetLength(0); i++)
            {
                for (int j = 0; j < mazeMap.GetLength(1); j++)
                {
                    
                    switch(mazeMap[i, j])
                    {
                        // passage
                        case 0:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write("  ");
                            break;
                        //wall
                        case 1:
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            Console.Write("  ");
                            break;
                        //start
                        case 2:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write("S ");
                            break;
                        //end
                        case 3:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write("E ");
                            break;

                    }
                    
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }

        /// <summary>
        /// recover the maze after a change
        /// </summary>
        internal void recoverMaze()
        {
            for (int i = 0; i < mazeMap.GetLength(0); i++)
            {
                for (int j = 0; j < mazeMap.GetLength(1); j++)
                {
                    if (mazeMap[i, j] == Maze.Visited)
                        mazeMap[i, j] = 0;

                }
            }
        }

        /// <summary>
        /// get X length
        /// </summary>
        /// <returns>X length</returns>
        internal byte GetX()
        {
            return Convert.ToByte(mazeMap.GetLength(0));
        }

        /// <summary>
        /// get X length
        /// </summary>
        /// <returns>Y length</returns>
        internal byte GetY()
        {
            return Convert.ToByte(mazeMap.GetLength(1));
        }
        /// <summary>
        /// returns the size of the maze
        /// </summary>
        /// <returns> size of the maze</returns>
        public int getLength()
        {
            return mazeMap.GetLength(0) * mazeMap.GetLength(1);
        }

        /// <summary>
        /// returs list of bytes representing the maze map
        /// </summary>
        /// <returns> list of bytes representing the maze map</returns>
        public List<byte> toByteList()
        {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < mazeMap.GetLength(0); i++)
            {
                for (int j = 0; j < mazeMap.GetLength(1); j++)
                {
                    bytes.Add(mazeMap[i, j]);
                }
            }
            return bytes;
        }

        /// <summary>
        /// changing the state
        /// </summary>
        /// <param name="position"> the position to change</param>
        /// <param name="state"> the state we want to change to</param>
        internal void changeToState(Position position,byte state)
        {
            mazeMap[position.X, position.Y] = state;
        }

        /// <summary>
        /// check if its posible to go to the current position
        /// </summary>
        /// <param name="x"> x cordinate</param>
        /// <param name="y"> y cordinate </param>
        /// <returns> true if the position is available false otherwise</returns>
        internal bool isAvailable(int x, int y)
        {
            if((x>=0 && x<mazeMap.GetLength(0))&&(y >= 0 && y < mazeMap.GetLength(1)))
            {
                if (mazeMap[x, y] == Passage || mazeMap[x, y] == End)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// check if mazes are equal
        /// </summary>
        /// <param name="maze">maze to compre to</param>
        /// <returns>true if the mazes are equel</returns>
        public override bool equals(Maze maze)
        {
            if(maze is Maze2d)
            {
                Maze2d maze2d = (Maze2d)maze;
                if (maze2d.mazeMap.GetLength(0)==mazeMap.GetLength(0)&& maze2d.mazeMap.GetLength(1) == mazeMap.GetLength(1))
                {
                    for (int i = 0; i < mazeMap.GetLength(0); i++)
                    {
                        for (int j = 0; j < mazeMap.GetLength(1); j++)
                        {
                            if (mazeMap[i, j] != maze2d.mazeMap[i, j])
                                return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
