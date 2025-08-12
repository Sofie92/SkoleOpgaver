using System; //Provides core .NET types og Console, list, etc.
using System.Collections.Generic; //Provides list <T> feks.

namespace ToDoListApp // Name for the app
{
    class TaskItem //Is one task in the list
    {
        public string Title { get; } //Read-only for title/the text title for the task.

        public bool IsDone { get; private set; } //Tracking if the task is done //True when task is completed.

        public TaskItem(string title) //create new task with a title.
        {
            Title = title.Trim(); //store a trimmed version of the title to remove extra spaces.
            IsDone = false; //new task are not done by default.
        }


        public void MarkDone() => IsDone = true; //Marking the task as completed/completion flag = true.

    }

    class Program // Class that contains application logic and UI/Entry point container.
    {
        private static readonly ToDoListApp<TaskItem> task = new List<TaskItem>(); //Holds all task in order of addition.

        private const int MaxTasks = 5; //Limit on how many task to exist/max number allowed.

        static void Main() //Program starts here/App entry point.
        {
            Console.Title = "MY TODO LIST"; //Console window bar text title.
            RunMenuLoop(); // Starts the menu loop.

        }

        private static void RunMenuLoop() //Main UI loop.
        {
            bool running = true; //Deciding if loop should continue.

            while (running) //Continues until user exits.
            {
                Console.Clear(); //clears the console
                PrintHeader("MY TODO LIST"); //Header

                Console.Writeline("1. Add task"); //Menu option to add a new task.
                Console.Writeline("2. Show task"); // Menu option to list all task.
                Console.Writeline("3. Mark task as done"); //Menu option to mark a task as done.
                Console.Writeline("4. Exit"); //Menu option to quit.
                Console.write("\nChoose an option:"); //Asking for user input.

                string? choice = Console.ReadLine(); //Read users menu choice as strings.

                switch (choice) //Waits for the input.
                {
                    case "1": //If the user is typing 1
                        AddTask(); //Goes to add task workflow.
                        break; //Stopping other cases.
                    case "2": //If the user is typing 2.
                        ShowTask(); //Render the list of tasks.
                        Pause(); //Waiting for key so user can read output.
                        break; // Ending the case.
                    case "3": //If the user is typing 3.
                        MarkTaskDone(); //Starting to mark the done ones.
                        break; //Ending the case.
                    case "4": //If the user is typing 4.
                        running = false; //Flip the flag to exit the while-loop.
                        Console.WriteLine("\nClosing the program gracefully. CYA!"); //Say goodbye ma friend.
                        Pause(); // Waits so the user can see the message.
                        break; // Ends this case

                    default: //Other input.
                        WriteInfo("Invalid choice. Please try again."); //Info on invalid input.
                        Pause(); //waiting for keypress.
                        break; //Ends this case.
                }
            }
        }

        private static void AddTask() //handling adding task if the max hasn't been reached.
        {
            Console.Clear(); //Clearing screen.
            PrintHeader("ADD TASK"); //Printing section header.

            if (task.Count >= MaxTask) //Checking if the list is already maxed.
            {
                WriteError($"You can have at most {MaxTask} task. You cannot add more.") //Show errors.
                Pause(); //Waiting so message is visible
                return; //Exit early
            }

            Console.Write("Enter task title: "); //Ask user for task title
            string? title = Console.ReadLine(); //Read the title from console.

            if (string.IsNullOrWhiteSpace(title)) //Validates that the input is not empty.
            {
                WriteError("Title cannot be empty."); //Invalid input info.
                Pause(); //waiting for keypress.
                return; //Exit without adding anything.

            }

            task.Add(new TaskItem(title)); // Creates a new taskitem and gives it to the list.
            WriteSuccess($"Task \ "{ title.Trim()}\" has been added."); //Confirmation on success.
            Pause(); // waiting so user can read confirmation info.
            
        }
    }
}