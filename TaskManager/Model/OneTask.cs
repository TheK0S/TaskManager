using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Model
{
    public class OneTask : INotifyPropertyChanged
    {
        private int id;
        private string title;
        private string description;
        private DateTime begin;
        private DateTime end;
        private string priority;
        private bool isCompleted;

        public int Id 
        { 
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Description 
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public DateTime Begin
        { 
            get => begin;
            set
            {
                begin = value;
                OnPropertyChanged(nameof(Begin));
            }
        }

        public DateTime End 
        {
            get => end;
            set
            {
                end = value;
                OnPropertyChanged(nameof(End));
            }
        }

        public string Priority
        {
            get => priority;
            set
            {
                priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }
        
        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
