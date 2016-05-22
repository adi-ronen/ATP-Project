using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP2016Project.Model;
using ATP2016Project.View;

namespace ATP2016Project.Controller
{
    class MyController :IController
    {

        private IModel m_model;
        private IView m_view;

        public MyController() { }

        public Dictionary<string, ACommand> GetCommands()
        {
            Dictionary<string, ACommand> commands = new Dictionary<string, ACommand>();
            ACommand dir = new DirCommand(m_model, m_view);
            ACommand generate = new Generate3dMazeCommand(m_model, m_view);
            ACommand solve = new SolveMazeCommand(m_model, m_view);
            ACommand display = new DisplayCommand(m_model, m_view);
            ACommand displaysolution = new DisplaySolutionCommand(m_model, m_view);
            ACommand filesize = new FileSizeCommand(m_model, m_view);
            ACommand mazesize = new MazeSizeCommand(m_model, m_view);
            ACommand load = new LoadMazeCommand(m_model, m_view);
            ACommand save = new SaveMazeCommand(m_model, m_view);

            commands.Add(dir.GetName(), dir);
            commands.Add(generate.GetName(), generate);
            commands.Add(solve.GetName(), solve);
            commands.Add(display.GetName(), display);
            commands.Add(displaysolution.GetName(), displaysolution);
            commands.Add(filesize.GetName(), filesize);
            commands.Add(mazesize.GetName(), mazesize);
            commands.Add(load.GetName(), load);
            commands.Add(save.GetName(), save);
            return commands;
        }

        public void output(string str)
        {
            m_view.Output(str);
        }

        public void SetModel(IModel model)
        {
            m_model = model;
        }

        public void SetView(IView view)
        {
            m_view = view;
        }
    }
}
