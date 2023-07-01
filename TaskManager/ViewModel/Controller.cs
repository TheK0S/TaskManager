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

namespace TaskManager.ViewModel
{
    internal class Controller : INotifyPropertyChanged
    {
        public ObservableCollection<Border> GridsList { get; set; }
        public ObservableCollection<OneTask> oneTasks { get; set; }
        public ObservableCollection<OneTask> SelectedTasksList { get; set; }
        public OneTask SelectedTask { get; set; }


        private string Json { get; set; }
        private string path = "taskList.json";

        public Controller()
        {
            ReadFromJson();

            for (int i = 0; i < 10; i++)
                oneTasks.Add(new OneTask()
                {
                    Id = i + 1,
                    Title = $"Задача № {i + 1}",
                    Description = $"Это задача какаято важная №{i + 1} и вообще то это серьезно",
                    Begin = DateTime.Now,
                    End = DateTime.Now,
                    Priority = "Высокий",
                    IsCompleted = false
                });

            GridsList = CreateGridTaskList(oneTasks);
        }

        private CommandMain addTaskCommand;
        public CommandMain AddTaskCommand { get; }
        //{
        //    get {
        //        return AddTaskCommand ??
        //        (addTaskCommand = new CommandMain(obj =>
        //        {
        //            OneTask task = new OneTask();
        //            oneTasks.Insert(0, task);
        //            SelectedTask = task;

        //            GridsList = CreateGridTaskList(oneTasks);
        //        }));
        //    }
        //}

        private CommandMain removeTaskCommand;
        public CommandMain RemoveTaskCommand
        {
            get
            {
                return RemoveTaskCommand ??
                (removeTaskCommand = new CommandMain(obj =>
                {
                    if (SelectedTasksList?.Count > 0)
                    {
                        foreach (var selectedTask in SelectedTasksList)
                            oneTasks.Remove(selectedTask);

                        GridsList = CreateGridTaskList(oneTasks);
                    }
                    else
                    {
                        if(SelectedTask != null)
                        {
                            oneTasks.Remove(SelectedTask);
                            GridsList = CreateGridTaskList(oneTasks);
                        }
                        else
                            MessageBox.Show("Выберите одну или несколько задач для удаления", "Ошибка! Не выбрана задача");
                    }
                },
                (obj) => oneTasks.Count > 0
                ));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void WriteToJson()
        {
            JsonConvert.SerializeObject(oneTasks);
            File.WriteAllText(path, Json);
        }

        private void ReadFromJson()
        {
            try
            {
                Json = File.ReadAllText(path);
                oneTasks = JsonConvert.DeserializeObject<ObservableCollection<OneTask>>(Json);
            }
            catch (Exception)
            {
                oneTasks = new ObservableCollection<OneTask>();
                MessageBox.Show("Список задач пуст или не существует");
            }            
        }

        public ObservableCollection<Border> CreateGridTaskList(ObservableCollection<OneTask> tasksList)
        {
            ObservableCollection<Border> borderList = new ObservableCollection<Border>();

            foreach (var task in tasksList)
            {
                Grid grid = new Grid();
                grid.Margin = new Thickness(10);

                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });

                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());

                TextBlock title = new TextBlock();
                title.Text = task.Title;
                title.FontWeight = FontWeights.Bold;
                title.TextAlignment = TextAlignment.Center;
                Grid.SetRow(title, 0);
                Grid.SetColumnSpan(title, 2);
                grid.Children.Add(title);

                TextBlock description = new TextBlock();
                description.Text = task.Description;
                Grid.SetRow(description, 1);
                Grid.SetColumnSpan(description, 2);
                grid.Children.Add(description);

                TextBlock endDate = new TextBlock();
                endDate.Text = task.End.ToString();
                Grid.SetRow(endDate, 2);
                Grid.SetColumnSpan(endDate, 2);
                grid.Children.Add(endDate);

                TextBlock priority = new TextBlock();
                priority.Text = task.Priority;
                priority.TextAlignment = TextAlignment.Center;
                Grid.SetRow(priority, 0);
                Grid.SetColumn(priority, 2);
                grid.Children.Add(priority);

                TextBlock isComplete = new TextBlock();
                isComplete.Text = task.IsCompleted ? "Выполнена" : "Не выполнена";
                isComplete.TextAlignment = TextAlignment.Center;
                Grid.SetRow(isComplete, 2);
                Grid.SetColumn(isComplete, 2);
                grid.Children.Add(isComplete);

                // Добавляем Grid в Border и Border в окно
                Border border = new Border();
                border.BorderBrush = task.IsCompleted ? Brushes.Green : Brushes.DarkBlue;
                border.BorderThickness = new Thickness(1);
                border.Child = grid;

                borderList.Add(border);
            }

            return borderList;
        }
    }
}
