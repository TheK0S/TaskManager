using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    public class ControllerEditWindow
    {
        public OneTask CurrentTask { get; set; }
        public List<string> Prioritis { get; set; }

        public ControllerEditWindow(OneTask task, List<string> prioritis)
        {
            CurrentTask = task;
            Prioritis = prioritis;
        }        
    }
}
