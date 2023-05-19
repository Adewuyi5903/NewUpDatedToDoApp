using System;
using System.Collections.Generic;
using System.Text;

namespace NewTodoApp
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<TaskList> Tasks { get; set; } = new List<TaskList>();

        public User()
        {

        }
    }
}
