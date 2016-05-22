using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    class Maze3d : Maze
    {
        private Maze2d[] mazeMap;

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="_start"> start position</param>
        /// <param name="_end"> end position</param>
        /// <param name="_mazeMap"> the array of the maze</param>
        public Maze3d(Position _start, Position _end, Maze2d[] _mazeMap) : base(_start, _end) { mazeMap = _mazeMap; }

        /// <summary>
        /// build an 3d maze from compressed byte array
        /// </summary>
        /// <param name="bytes"> compressed byte array </param>
        public Maze3d(byte[] bytes) : base(new Position(), new Position())
        {
            int x = bytes[0];
            int y = bytes[1];
            int z = bytes[2];
            mazeMap = new Maze2d[z];
            int currentIndex = 3;
            for (int i = 0; i < z; i++)
            {
                byte[] bytesFor2d = new byte[x*y];
                for (int j = 0; j < bytesFor2d.Length; j++, currentIndex++)
                {
                    bytesFor2d[j] = bytes[currentIndex];
                }
                mazeMap[i] = new Maze2d(x, y, bytesFor2d);
            }
            start = mazeMap[0].getStartPosition();
            start.Z = 0;
            end= mazeMap[z - 1].getGoalPosition();
            end.Z = z - 1;

        }

        /// <summary>
        /// prints the 3d maze
        /// </summary>
        public override void print()
        {
            for (int i = 0; i < mazeMap.Length; i++)
            {
                Console.WriteLine("Layer {0}",i+1);
                mazeMap[i].print();
                Console.WriteLine();
            }
        }
        /// <summary>
        /// check if its posible to go to the current position
        /// </summary>
        /// <param name="x"> x cordinate</param>
        /// <param name="y"> y cordinate </param>
        /// <param name="z"> z cordinate </param>
        /// <returns>true if the position is available false otherwise</returns>
        public bool isAvailable(int x, int y, int z)
        {
            if (z >= 0 && z < mazeMap.Length)
                if (mazeMap[z].isAvailable(x, y))
                    return true;
            return false;
        }

        /// <summary>
        /// change the corrent position to visited 
        /// </summary>
        /// <param name="position"> the position to change</param>
        public void changeToVisited(Position position)
        {
            mazeMap[position.Z].changeToState(position,Visited);
        }

        /// <summary>
        /// recover the maze after a change 
        /// </summary>
        public void recoverMaze()
        {
            mazeMap[0].changeToState(start, Start);
            mazeMap[mazeMap.Length - 1].changeToState(end, End);

            for (int i = 1; i < mazeMap.Length-1; i++)
            {
                mazeMap[i].recoverMaze();
            }
        }

        /// <summary>
        /// arrays decompressed information
        /// </summary>
        /// <returns> array of the bytes </returns>
        public byte[] toByteArray()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(mazeMap[0].GetX()); //x size
            bytes.Add(mazeMap[0].GetY()); //y size
            bytes.Add(Convert.ToByte(mazeMap.Length)); //z size
            
            for (int i = 0; i < mazeMap.Length; i++)
            {
                foreach (byte b in mazeMap[i].toByteList())
                {
                    bytes.Add(b);
                }
            }
            return bytes.ToArray();

        }
        /// <summary>
        /// check if mazes are equal
        /// </summary>
        /// <param name="maze">maze to compre to</param>
        /// <returns>true if the mazes are equel</returns>
        public override bool equals(Maze maze)
        {
            if(maze is Maze3d)
            {
                Maze3d maze3d = (Maze3d)maze;
                if(mazeMap.Length==maze3d.mazeMap.Length)
                {
                    for (int i = 0; i < mazeMap.Length; i++)
                    {
                        if (!mazeMap[i].equals(maze3d.mazeMap[i]))
                            return false;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
