using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace NewTodoApp
{
    public class ToDoApp
    {
        public User users = new User();
        static readonly int tableWidth = 90;

        public void ToDoMenu(User loggedInUser)
        {
            users = loggedInUser;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Login successful.");
            Console.ResetColor();

            bool looggedIn = true;
            while (looggedIn)
            {
                
                Console.WriteLine();
                Console.ForegroundColor= ConsoleColor.DarkBlue;
                Console.WriteLine("Todo List");
                Console.WriteLine(".........");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View All Tasks");
                Console.WriteLine("3. Edit Task");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Complete Task");
                Console.WriteLine("6. Logout");
                Console.ResetColor();

                Console.WriteLine(".........");
                Console.Write("Enter your choice: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string choice = Console.ReadLine();
                Console.ResetColor();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;

                    case "2":
                        ViewAllTasks();
                        break;

                    case "3":
                        EditTask();
                        break;

                    case "4":
                        DeleteTask();
                        break;

                    case "5":
                        CompleteTask();
                        break;

                    case "6":
                        SignUpSignIn signUpSignIn = new SignUpSignIn();
                        signUpSignIn.Menu();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
                Console.WriteLine();
            }
        }

        void AddTask()
        {
            Console.ForegroundColor= ConsoleColor.DarkBlue;
            Console.WriteLine("Add Task");
            Console.WriteLine("........");
            Console.ResetColor();

            while (true)
            {
                string title;
                do
                {
                    Console.WriteLine("Enter your title: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    title = Console.ReadLine();
                    Console.ResetColor();

                    if (string.IsNullOrEmpty(title))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Title cannot be empty. Please enter a valid title.");
                        Console.ResetColor();
                    }
                    else if (!Regex.IsMatch(title, @"^[A-Za-z]+$"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Title must not contain numbers or symbols. Please enter a valid title.");
                        Console.ResetColor();
                    }
                } while (string.IsNullOrEmpty(title) || !Regex.IsMatch(title, @"^[A-Za-z]+$"));

                if (users.Tasks.Any(t => t.Title == title))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A task with the same title already exists. Please enter a unique title.");
                    Console.ResetColor();
                }
                else
                {
                    string description;
                    do
                    {
                        Console.WriteLine("Enter the task description: ");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        description = Console.ReadLine();
                        Console.ResetColor();

                        if (string.IsNullOrEmpty(description))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Description cannot be empty. Please enter a valid description.");
                            Console.ResetColor();
                        }
                        else if (!Regex.IsMatch(description, @"^[A-Za-z\s]+$"))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Description must not contain numbers or symbols. Please enter a valid description.");
                            Console.ResetColor();
                        }
                    } while (string.IsNullOrEmpty(description) || !Regex.IsMatch(description, @"^[A-Za-z\s]+$"));

                    string priorityLevel;
                    do
                    {
                        Console.WriteLine("Enter your priority level (High, Medium, Low): ");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        priorityLevel = Console.ReadLine();
                        Console.ResetColor();

                        if (!Enum.TryParse(priorityLevel, true, out PriorityLevel priority) || !IsValidPriority(priorityLevel))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid priority. Kindly enter 'High', 'Medium', or 'Low'.");
                            Console.ResetColor();
                        }
                    } while (!IsValidPriority(priorityLevel));

                    static bool IsValidPriority(string priorityLevel)
                    {
                        return priorityLevel.Equals("High", StringComparison.OrdinalIgnoreCase) ||
                            priorityLevel.Equals("Medium", StringComparison.OrdinalIgnoreCase) ||
                            priorityLevel.Equals("Low", StringComparison.OrdinalIgnoreCase);
                    }

                    DateTime dueDate;
                    bool isValidDate;
                    do
                    {
                        Console.WriteLine("Enter the due date: (yyyy-MM-dd) ");
                        Console.WriteLine(DateTime.Now.ToString("hh:mm tt"));
                        string dueDateInput = Console.ReadLine();
                        isValidDate = DateTime.TryParseExact(dueDateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dueDate);
                        if (!isValidDate || dueDate < DateTime.Today)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid due date. Please enter a valid future date in the format 'yyyy-MM-dd'.");
                            Console.ResetColor();
                        }
                    } while (!isValidDate || dueDate < DateTime.Today);

                    int taskId = users.Tasks.Count + 1;
                    TaskList newTask = new TaskList
                    {
                        Title = title,
                        Description = description,
                        PriorityLevel = priorityLevel,
                        DueDate = dueDate,
                        IsComplete = false,
                        TaskId = taskId,
                        UserId = users.Id
                    };
                    users.Tasks.Add(newTask);
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Your task has been added successfully");
                    Console.ResetColor();
                    break;
                }
            }
        }

        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)

        {
            int width = (tableWidth - columns.Length + 1) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }
            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;
            return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }

        static string CentreText(string text, int width)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            int totalSpaces = width - text.Length;
            int leftSpaces = totalSpaces / 2;
            return new string(' ', leftSpaces) + text + new string(' ', totalSpaces - leftSpaces);
        }

        void ViewAllTasks()
        {
            Console.Clear();
            Console.WriteLine(CentreText("Todo Application", tableWidth));
            PrintLine();
            PrintRow("ID", "TITLE", "DESCRIPTION", "DUE DATE", "PRIORITY", "COMPLETE");
            PrintLine();

            foreach (TaskList task in users.Tasks)
            {
                PrintRow(task.TaskId.ToString(), task.Title, task.Description, task.DueDate.ToString("yyyy-MM-dd"),
                                   task.PriorityLevel, task.IsComplete ? "Yes" : "No");
            }
            PrintLine();
        }

        void EditTask()
        {
            Console.WriteLine("Enter the taskId to be edited: ");
            int taskId;
            if (!int.TryParse(Console.ReadLine(), out taskId))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid taskId. Please enter a valid integer value.");
                Console.ResetColor();
                return;
            }

            var taskToEdit = users.Tasks.Find(task => task.TaskId == taskId);

            if (taskToEdit == null)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Task with the specified taskId does not exist.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Enter the property you want to edit: ");
            Console.ForegroundColor= ConsoleColor.DarkMagenta;
            Console.WriteLine("1. Title");
            Console.WriteLine("2. Description");
            Console.WriteLine("3. Due Date");
            Console.WriteLine("4. Priority Level");
            Console.ResetColor();

            int propertyChoice;
            if (!int.TryParse(Console.ReadLine(), out propertyChoice))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid property choice. Please enter a valid integer value.");
                Console.ResetColor();
                return;
            }

            switch (propertyChoice)
            {
                case 1:
                    bool validTitle = false;
                    Console.WriteLine($"Current Title: {taskToEdit.Title}");
                    Console.WriteLine("Enter the new title: ");
                    string titlePattern = @"^(?!^\s*$)^[A-Za-z\s]+$";
                    do
                    {
                        string title = Console.ReadLine();
                        bool isValidTitle = Regex.IsMatch(title, titlePattern);
                        if (isValidTitle)
                        {
                            taskToEdit.Title = title;
                            validTitle = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error! Please enter a valid title.");
                            Console.ResetColor();
                        }
                    } while (!validTitle);
                    break;

                case 2:
                    bool validDescription = false;
                    Console.WriteLine($"Current Description: {taskToEdit.Description}");
                    Console.WriteLine("Enter the new description: ");
                    string descriptionPattern = @"^(?!^\s*$)^[A-Za-z\s]+$";
                    do
                    {
                        string description = Console.ReadLine();
                        bool isValidDescription = Regex.IsMatch(description, descriptionPattern);
                        if (isValidDescription)
                        {
                            taskToEdit.Description = description;
                            validDescription = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error! Please enter a valid description.");
                            Console.ResetColor();
                        }
                    } while (!validDescription);
                    break;

                case 3:
                    bool validDate = false;
                    Console.WriteLine($"Current Due Date: {taskToEdit.DueDate}");
                    Console.WriteLine("Enter the new Due Date: (yyyy-MM-dd) ");
                    Console.WriteLine(DateTime.Now.ToString("hh: mm tt"));
                    string pattern = @"^\d{4}-\d{2}-\d{2}$";
                    do
                    {
                        string input = Console.ReadLine();
                        DateTime dateTime = DateTime.ParseExact(input, "yyyy-MM-dd", null);
                        bool isValidDate = Regex.IsMatch(input, pattern);
                        if (isValidDate)
                        {

                            taskToEdit.DueDate = dateTime;
                            validDate = true;
                        }
                        else
                        {
                            Console.WriteLine("Error! Please enter a valid format, e.g., (yyyy-MM-dd )");
                        }
                    } while (!validDate);
                    break;

                case 4:
                    bool validPriorityLevel = false;
                    Console.WriteLine($"Current Priority Level: {taskToEdit.PriorityLevel}");
                    Console.WriteLine("Enter the new Priority Level: ");
                    taskToEdit.PriorityLevel = Console.ReadLine();
                    string priorityPattern = @"^(?!^\s*$)^[A-Za-z]+$";
                    do
                    {
                        string priorityLevel = Console.ReadLine();
                        bool isValidPriority = Regex.IsMatch(priorityLevel, priorityPattern);
                        if (isValidPriority)
                        {
                            taskToEdit.PriorityLevel = priorityLevel;
                            validPriorityLevel = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error! Please enter a valid priority level.");
                            Console.ResetColor();
                        }

                    } while (!validPriorityLevel);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid property choice. Please enter a valid choice.");
                    Console.ResetColor();
                    return;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task edited successfully!");
            Console.ResetColor();
        }

        void DeleteTask()
        {
            Console.Write("Enter the taskId to delete: ");
            int deleteTask = int.Parse(Console.ReadLine()) - 1;
            if (deleteTask >= 0 && deleteTask < users.Tasks.Count)
            {
                users.Tasks.RemoveAt(deleteTask);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Task has been deleted successfully!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid taskId.");
                Console.ResetColor();
            }
        }

        void CompleteTask()
        {
            Console.Write("Enter the taskId to mark as Completed: ");
            if (!int.TryParse(Console.ReadLine(), out int taskId) || taskId < 1 || taskId > users.Tasks.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid taskId. Please enter a valid taskId.");
                Console.ResetColor();
                return;
            }

            TaskList taskToComplete = users.Tasks.Find(task => task.TaskId == taskId);
            if (taskToComplete == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Task not found. Please enter a correct TaskId.");
                Console.ResetColor();
                return;
            }

            taskToComplete.IsComplete = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task marked as complete successfully!");
            Console.ResetColor();
        }

    }
}
