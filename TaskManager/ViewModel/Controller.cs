using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TaskManager.Model;
using TaskManager.View;

namespace TaskManager.ViewModel
{
    internal class Controller
    {

        public ObservableCollection<OneTask> OneTasks { get; set; }    
        public ObservableCollection<DateTime> SelectedDates { get; set; }
        public List<string> Prioritis { get; set; }
        public OneTask SelectedTaskItem { get; set; }
        public string FilterText { get; set; }

        private string Json { get; set; }
        private string path = "taskList.json";

        private CommandMain addTaskCommand;
        private CommandMain removeTaskCommand;
        private CommandMain editTaskCommand;
        private CommandMain setCompletedCommand;

        public Controller()
        {
            ReadFromJson();

            if (OneTasks == null) OneTasks = new ObservableCollection<OneTask>();

            Prioritis = new List<string> { "Высокий", "Средний", "Низкий" };
        }
                
        public CommandMain AddTaskCommand
        {
            get
            {
                return addTaskCommand ??
                (addTaskCommand = new CommandMain(async obj =>
                {
                    await TaskRan(new AddWindow(new ControllerAddWindow(OneTasks, Prioritis)));
                }));
            }
        }
                
                
        public CommandMain RemoveTaskCommand
        {
            get
            {
                return removeTaskCommand ??
                (removeTaskCommand = new CommandMain(obj =>
                {
                    if (SelectedTaskItem != null)
                        OneTasks.Remove(SelectedTaskItem);
                    else
                        MessageBox.Show("Выберите задачу для удаления", "Ошибка! Не выбрана задача", MessageBoxButton.OK, MessageBoxImage.Error);
                },
                (obj) => OneTasks.Count > 0
                ));
            }
        }

        public CommandMain EditTaskCommand
        {
            get
            {
                return editTaskCommand ??
                    (editTaskCommand = new CommandMain(async obj =>
                    {
                        if (SelectedTaskItem != null)
                            await TaskRan(new EditWindow(new ControllerEditWindow(SelectedTaskItem, Prioritis)));
                        else
                            MessageBox.Show("Выберите задачу для изменения", "Ошибка! Не выбрана задача", MessageBoxButton.OK, MessageBoxImage.Error);
                    },
                    (obj) => OneTasks.Count > 0
                    ));
            }
        }

        public CommandMain SetCompletedCommand
        {
            get
            {
                return setCompletedCommand ??
                    (setCompletedCommand = new CommandMain(Obj => 
                    {
                        if (SelectedTaskItem != null)
                        {
                            SelectedTaskItem.IsCompleted = true;
                        }
                        else
                            MessageBox.Show("Выберите задачу для изменения статуса", "Ошибка! Не выбрана задача", MessageBoxButton.OK, MessageBoxImage.Error);
                    }));
            }
        }


        private async Task TaskRan(Window window)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                window.Show();
            });
        }

        public void WriteToJson()
        {
            Json = JsonConvert.SerializeObject(OneTasks);
            File.WriteAllText(path, Json);
        }

        private void ReadFromJson()
        {
            try
            {
                Json = File.ReadAllText(path);
                OneTasks = JsonConvert.DeserializeObject<ObservableCollection<OneTask>>(Json);
            }
            catch (Exception)
            {
                OneTasks = new ObservableCollection<OneTask>();
                MessageBox.Show("Список задач пуст или не существует");
            }            
        }       
    }
}
