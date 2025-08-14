using System;
using System.Collections.Generic;

namespace TodoListApp
{
    
    class TaskItem
    {
        public string Title { get; }
        public bool IsDone { get; private set; }

        public TaskItem(string title)
        {
            Title = title.Trim();
            IsDone = false;
        }

        public void MarkDone() => IsDone = true;
    }

    class Program
    {
        
        private static readonly List<TaskItem> tasks = new List<TaskItem>();
        private const int MaxTasks = 5;

        static void Main()
        {
            Console.Title = "MY TODO LIST";
            RunMenuLoop();
        }

        private static void RunMenuLoop()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                PrintHeader("MY TODO LIST");
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. Show tasks");
                Console.WriteLine("3. Mark task as done");
                Console.WriteLine("4. Exit");
                Console.Write("\nChoose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;

                    case "2":
                        ShowTasks();
                        Pause();
                        break;

                    case "3":
                        MarkTaskDone();
                        break;

                    case "4":
                        running = false;
                        Console.WriteLine("\nClosing the program gracefully. Bye!");
                        Pause();
                        break;

                    default:
                        WriteInfo("Invalid choice. Please try again.");
                        Pause();
                        break;
                }
            }
        }

       
        private static void AddTask()
        {
            Console.Clear();
            PrintHeader("ADD TASK");

            if (tasks.Count >= MaxTasks)
            {
                WriteError($"You can have at most {MaxTasks} tasks. You cannot add more.");
                Pause();
                return;
            }

            Console.Write("Enter task title: ");
            string? title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                WriteError("Title cannot be empty.");
                Pause();
                return;
            }

            tasks.Add(new TaskItem(title));
            WriteSuccess($"Task \"{title.Trim()}\" has been added.");
            Pause();
        }


        private static void ShowTasks()
        {
            Console.Clear();
            PrintHeader("MY TASKS");

            if (tasks.Count == 0)
            {
                WriteInfo("No tasks yet.");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                var t = tasks[i];
                Console.Write($"{i + 1}. ");

                if (t.IsDone)
                {
                    SetColor(ConsoleColor.Green);
                    Console.Write("[x] ");
                }
                else
                {
                    SetColor(ConsoleColor.Red);
                    Console.Write("[ ] ");
                }

                ResetColor();
                Console.WriteLine(t.Title);
            }
        }

    
        private static void MarkTaskDone()
        {
            Console.Clear();
            PrintHeader("MARK AS DONE");

            if (tasks.Count == 0)
            {
                WriteInfo("There are no tasks to mark.");
                Pause();
                return;
            }

            ShowTasks();
            Console.Write("\nEnter the number of the task to mark as done: ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int index))
            {
                WriteError("Please enter a valid number.");
                Pause();
                return;
            }

            if (index < 1 || index > tasks.Count)
            {
                WriteError("That number does not exist.");
                Pause();
                return;
            }

            var task = tasks[index - 1];

            if (task.IsDone)
            {
                WriteInfo("That task is already marked as done.");
            }
            else
            {
                task.MarkDone();
                WriteSuccess($"\"{task.Title}\" is now marked as done.");
            }

            Pause();
        }

        
        private static void PrintHeader(string title)
        {
            Console.WriteLine("=== " + title.ToUpper() + " ===");
        }

        private static void Pause()
        {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        private static void WriteSuccess(string msg)
        {
            SetColor(ConsoleColor.Green);
            Console.WriteLine(msg);
            ResetColor();
        }

        private static void WriteError(string msg)
        {
            SetColor(ConsoleColor.Red);
            Console.WriteLine(msg);
            ResetColor();
        }

        private static void WriteInfo(string msg)
        {
            SetColor(ConsoleColor.Yellow);
            Console.WriteLine(msg);
            ResetColor();
        }

        private static void SetColor(ConsoleColor color) => Console.ForegroundColor = color;
        private static void ResetColor() => Console.ResetColor();
    }
}
