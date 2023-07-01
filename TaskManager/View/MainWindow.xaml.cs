using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManager.Model;
using TaskManager.ViewModel;

namespace TaskManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<OneTask> oneTasks = new List<OneTask>();

            for (int i = 0; i < 10; i++)
                oneTasks.Add(new OneTask() {
                    Id = i + 1,
                    Title = $"Задача № {i + 1}",
                    Description = $"Это задача какаято важная №{i + 1} и вообще то это серьезно",
                    Begin = DateTime.Now,
                    End = DateTime.Now,
                    Priority = "Высокий",
                    IsCompleted = false
                });

            Controller controller = new Controller();

            taskListView.ItemsSource = controller.CreateGridTaskList(oneTasks);
        }
        
    }
}
