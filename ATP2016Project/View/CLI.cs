using ATP2016Project.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.View
{
    class CLI : IView
    {
        private Stream m_input = Console.OpenStandardInput();
        private Stream m_output = Console.OpenStandardOutput();        Dictionary<string, ACommand> m_commands;
        private string m_cursor = ">>";

        private IController m_controller;

        public CLI() { }

        public CLI(IController controller)
        {
            this.m_controller = controller;

        }
       

        public void Start()
        {
            m_commands = m_controller.GetCommands();
            PrintInstructions();
            string userCommand;
            string[] splitCommand;
            while (true)
            {
                Output("");
                try
                {
                    userCommand = Input().Trim();
                    if (userCommand == "exit") { break; }

                    splitCommand = userCommand.Split(' ');

                    if (!m_commands.ContainsKey(splitCommand[0].ToLower()))
                    {
                        throw new Exception("Unrecognized command!");
                    }
                    else
                    {
                        m_commands[splitCommand[0].ToLower()].DoCommand(splitCommand);
                    }
                }
                catch (Exception e)
                {
                    Output(e.Message);
                }
                

            }
        }

        private void PrintInstructions()
        {
            Output("Available Commands:");
            Output("");
            Output("dir <path>");
            Output("generate3dmaze <maze name> <widght> <length> <depth>");
            Output("display <maze name>");
            Output("SaveMaze <maze name> <file path>");
            Output("LoadMaze <maze name> <file path>");
            Output("MazeSize <maze name>");
            Output("FileSize <file path>");
            Output("SolveMaze <maze name> <BFS/DFS>");
            Output("DisplaySolution <maze name>");
            Output("Exit");


        }

        public void Output(string output)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            StreamWriter streamWriter = new StreamWriter(m_output);
            streamWriter.AutoFlush = true;
            Console.SetCursorPosition(0, Console.CursorTop);
            streamWriter.WriteLine(output);
            streamWriter.WriteLine("");
            streamWriter.Write(m_cursor);
            Console.ResetColor();
        }

        public string Input()
        {
            StreamReader streamReader = new StreamReader(m_input);
            return streamReader.ReadLine();
        }

        public void OutputMaze(byte[] mazeInBytes)
        {
            throw new NotImplementedException();
        }
        #region Setters

        public void SetInput(Stream input)
        {
            m_input = input;
        }

        public void SetOutput(Stream output)
        {
            m_output = output;
        }

        public void SetCommands(Dictionary<string, ACommand> commands)
        {
            m_commands = commands;
        }

        #endregion
    }

}
