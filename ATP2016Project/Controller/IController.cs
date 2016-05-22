using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP2016Project.Model;
using ATP2016Project.View;

namespace ATP2016Project.Controller
{
    interface IController
    {
        void SetModel(IModel model);
        void SetView(IView view);
        Dictionary<string, ACommand> GetCommands();
        void output(string v);
    }
}
