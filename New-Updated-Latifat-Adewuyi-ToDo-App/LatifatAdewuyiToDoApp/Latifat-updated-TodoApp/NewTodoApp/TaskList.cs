using System;
using System.Collections.Generic;
using System.Text;

namespace NewTodoApp
{
    public class TaskList
    {
        public Guid ID  = Guid.NewGuid();
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PriorityLevel { get; set; }
        public DateTime DueDate { get; set;}
        public bool IsComplete { get; set; }
        public Guid UserId { get; set; }
    }
    


    enum PriorityLevel
    {
        High, 
        Medium,
        Low
    }

}
