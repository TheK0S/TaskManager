using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    internal class Controller
    {
        public ObservableCollection<Border> GridsList { get; set; }
        public Controller()
        {

        }

        public ObservableCollection<Border> CreateGridTaskList(List<OneTask> tasksList)
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
                border.BorderBrush = Brushes.DarkBlue;
                border.BorderThickness = new Thickness(1);
                border.Child = grid;

                borderList.Add(border);
            }

            return borderList;
        }
    }
}
