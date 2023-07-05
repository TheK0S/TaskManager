using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    public class ControllerAddWindow
    {
        public ObservableCollection<OneTask> OneTasks = new ObservableCollection<OneTask>();
        public List<string> Prioritis { get; set; }
        public string SelectedPriority { get; set; }
        public OneTask CurrentTask { get; set; }

        
        public ControllerAddWindow(ObservableCollection<OneTask> oneTasks, List<string> prioritis)
        {
            OneTasks = oneTasks;

            CurrentTask = new OneTask() { End = DateTime.Now, Title = "Задача" };

            Prioritis = prioritis;

            SelectedPriority = "Средний";
        }

        private CommandMain addCommand;
        public CommandMain AddCommand
        {
            get
            {
                return addCommand ??
                (addCommand = new CommandMain(obj =>
                {
                    CurrentTask.Begin = DateTime.Now;
                    CurrentTask.IsCompleted = false;
                    CurrentTask.Id = OneTasks.Count;
                    CurrentTask.Priority = SelectedPriority;

                    OneTasks.Insert(0, CurrentTask);
                }));
            }
        }
    }
}
