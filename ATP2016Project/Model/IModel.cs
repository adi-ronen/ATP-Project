using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model
{
    interface IModel
    {
        void SaveToDisk(string mazeName, string path);
        void DisplayMaze(string mazeName);
        void genarateMaze(string mazeName, int x, int y, int z);
        void LoadFromDisk(string mazeName, string path);
        void SolveMaze(string mazeName);
        void DisplaySolution(string v);
    }
}
