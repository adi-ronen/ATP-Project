using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    class Position
    {
        private int x, y, z;

        /// <summary>
        /// defoult constractor
        /// </summary>
        public Position() : this(0, 0, 0) { }

        /// <summary>
        /// 2d constractor
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        public Position(int _x, int _y) : this(_x, _y, -1) { }

        /// <summary>
        /// 3d constractor
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_z"></param>
        public Position(int _x, int _y, int _z) { x = _x; y = _y; z = _z; }

        /// <summary>
        /// check if the position is equal to p1
        /// </summary>
        /// <param name="p1"> the position to compare</param>
        /// <returns> true if equals false otherwise</returns>
        public bool Equals(Position p1)
        {
            return (this.x ==p1.x && this.y == p1.y && this.z == p1.z);
        }
        /// <summary>
        /// print position
        /// </summary>
        public void print()
        {
            if (z >= 0)
                Console.WriteLine("({0},{1},{2})", x, y, z);
            else
                Console.WriteLine("({0},{1})", x, y);
        }

        /// <summary>
        /// x getter and setter
        /// </summary>
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        /// <summary>
        /// y getter and setter
        /// </summary>
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        internal string display()
        {
            return "(" + x + "," + y + "," + z + ")";
        }

        /// <summary>
        /// z getter and setter
        /// </summary>
        public int Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }
    }
}
