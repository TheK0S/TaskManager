using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Model
{
    internal class OneTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }
    }
}
