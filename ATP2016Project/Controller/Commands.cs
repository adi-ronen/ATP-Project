using ATP2016Project.Model;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    class DirCommand : ACommand
    {
        public DirCommand(IModel model, IView view) : base(model, view) { }
        

        public override void DoCommand(params string[] parameters)
        {
            if(parameters.Length!=2)
                throw new Exception("invalid number of parameters");
            else
            {
                if (Directory.Exists(parameters[1]))
                {
                    List<string> files = new List<string>();
                    files.AddRange(Directory.EnumerateFileSystemEntries(parameters[1])) ;
                    string print = "Files and directories in " + parameters[1] + ":";
                    m_view.Output(print);
                    foreach (string fileName in files)
                    {
                        m_view.Output(fileName);
                    }
                }
                else
                    throw new Exception("path doesn't exist");
            }


        }

        public override string GetName()
        {
            return "dir";
        }
    }
    class DisplayCommand : ACommand
    {
        public DisplayCommand(IModel model, IView view) : base(model, view) { }

        public override void DoCommand(params string[] parameters)
        {
            if (parameters.Length != 2)
                throw new Exception("invalid number of parameters");
            else
            {
                m_model.DisplayMaze(parameters[1]);
            }
        }

        public override string GetName()
        {
            return "display";
        }
    }
    class DisplaySolutionCommand : ACommand
    {
        public DisplaySolutionCommand(IModel model, IView view) : base(model, view) { }

        public override void DoCommand(params string[] parameters)
        {
            if (parameters.Length != 2)
                throw new Exception("invalid number of parameters");
            else
            {
                m_model.DisplaySolution(parameters[1]);
            }
        }

        public override string GetName()
        {
            return "displaysolution";
        }
    }
    class FileSizeCommand : ACommand
    {
        public FileSizeCommand(IModel model, IView view) : base(model, view) { }

        public override void DoCommand(params string[] parameters)
        {
            if (parameters.Length != 2)
                throw new Exception("invalid number of parameters");
            else
            {
                if (File.Exists(parameters[1]))
                {
                    long length = new System.IO.FileInfo(parameters[1]).Length;
                    string ans = "the size of " + parameters[1] + " is : " + length + " bytes ";
                    m_view.Output(ans);
                    
                }
                else
                    throw new Exception("path doesn't exist");
            }

        }

        public override string GetName()
        {
            return "filesize";
        }
    }
    class Generate3dMazeCommand : ACommand
    {
        public Generate3dMazeCommand(IModel model, IView view) : base(model, view) { }

        public override void DoCommand(params string[] parameters)
        {
            if (parameters.Length != 5)
                throw new Exception("invalid number of parameters");
            else if (isNumaric(parameters[2]) && isNumaric(parameters[3]) && isNumaric(parameters[4]))
            {
                m_model.genarateMaze(parameters[1], Convert.ToInt32(parameters[2]), Convert.ToInt32(parameters[3]), Convert.ToInt32(parameters[4]));
                
            }
            else
                throw new Exception("wrong size values");



        }
        private bool isNumaric(string str)
        {
            int i;
            return int.TryParse(str, out i);
        }

        public override string GetName()
        {
            return "generate3dmaze";
        }
    }
    class LoadMazeCommand : ACommand
    {
        public LoadMazeCommand(IModel model, IView view) : base(model, view) { }

        public override void DoCommand(params string[] parameters)
        {
            if (parameters.Length != 3)
                throw new Exception("invalid number of parameters");
            else
            {
                m_model.LoadFromDisk(parameters[1], parameters[2]);
            }
        }

        public override string GetName()
        {
            return "loadmaze";
        }
    }
    class MazeSizeCommand : ACommand
    {
        public MazeSizeCommand(IModel model, IView view) : base(model, view) { }

        public override void DoCommand(params string[] parameters)
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return "mazesize";
        }
    }
    class SaveMazeCommand : ACommand
    {
        public SaveMazeCommand(IModel model, IView view) : base(model, view) { }

        public override void DoCommand(params string[] parameters)
        {
            if (parameters.Length != 3)
                throw new Exception("invalid number of parameters");
            else
            {
                m_model.SaveToDisk(parameters[1], parameters[2]);
            }
                
        }

        public override string GetName()
        {
            return "savemaze";
        }
    }
    class SolveMazeCommand : ACommand
    {
        public SolveMazeCommand(IModel model, IView view) : base(model, view) { }

        public override void DoCommand(params string[] parameters)
        {
            if (parameters.Length != 2)
                throw new Exception("invalid number of parameters");
            else
            {
                m_model.SolveMaze(parameters[1]);
            }
        }

        public override string GetName()
        {
            return "solvemaze";
        }
    }
}
