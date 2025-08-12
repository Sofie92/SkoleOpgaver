using System; //Provides core .NET types and console, list and so on.
using System.Collections.Generic; //Provides list <T> feks.

namespace TodoListApp //Name for the app.
{
    
    class TaskItem //One task in the list.
    {
        public string Title { get; } //Read-only for title/the text for the task.
        public bool IsDone { get; private set; } //Tracking if the task i done//True when task is completed.
    

        public TaskItem(string title) //create new task with a title.
        {
            Title = title.Trim(); //store a trimmed version of the title to remove extra spaces.
            IsDone = false; //new task are not done by default.
        }

        public void MarkDone() => IsDone = true; // Marking the task as completed/completion flag = true.
    }

    class Program // Class contains application logic and UI/Entry point container.
    {
        
        private static readonly List<TaskItem> tasks = new List<TaskItem>(); //Holds all task in order of addition.
        private const int MaxTasks = 5; //Limit on how many task to exist/max number allowed.

        static void Main() //Program starts here/App entry point.
        {
            Console.Title = "MY TODO LIST"; //Console window bar text/title.
            RunMenuLoop(); //start the menu loop.
        }

        private static void RunMenuLoop()//Main UI loop.
        {
            bool running = true; //deciding if loop should continue.

            while (running) //Continues until user decides to exit.
            {
                Console.Clear(); //clears the console.
                PrintHeader("MY TODO LIST"); //Header.
                Console.WriteLine("1. Add task"); //Menu option to add new task.
                Console.WriteLine("2. Show tasks"); //Menu option to list all task.
                Console.WriteLine("3. Mark task as done"); //Menu option to mark a task as done. 
                Console.WriteLine("4. Exit"); //Menu option to quit.
                Console.Write("\nChoose an option: "); //Asking for user input.

                string? choice = Console.ReadLine(); //Read users menu choice as strings.

                switch (choice) //waits for the input.
                {
                    case "1": //if the user is Typing 1.
                        AddTask(); //Goes to add task workflow.
                        break; //stopping other cases.

                    case "2": //If the user is typing 2.
                        ShowTasks(); //Render the list of tasks.
                        Pause(); //waiting for key so user can read output.
                        break; //Ending the case.

                    case "3": //If the user is typing 3.
                        MarkTaskDone(); //Starting to mark the done cases.
                        break; //Ending the case.

                    case "4": //if the user is typing 4.
                        running = false; //Flip the flag to exit the while-loop.
                        Console.WriteLine("\nClosing the program gracefully. CYA don't wanna be YA!"); //Say goodbye MaFriend.
                        Pause(); //Waits so the user can see the message.
                        break; //End this case.

                    default: //Other input.
                        WriteInfo("Invalid choice. Please try again."); //Info on invalid input.
                        Pause(); //Waiting for keypress
                        break; //Ends this case.
                }
            }
        }

        
        private static void AddTask() //Handling adding task if the max number of tasks hasn
        {
            Console.Clear(); //Clearing screen.
            PrintHeader("ADD TASK"); //Printing header section.

            if (tasks.Count >= MaxTasks) //Checking if the list is already maxed out.
            {
                WriteError($"You can have at most {MaxTasks} tasks. You cannot add more."); //Shows errors.
                Pause(); //waiting so message is visible.
                return; //Exit early.
            }

            Console.Write("Enter task title: "); //Ask the user for task titles.
            string? title = Console.ReadLine(); //Read the title from console.

            if (string.IsNullOrWhiteSpace(title)) //Validates that the input is not empty.
            {
                WriteError("Title cannot be empty."); //Invalid input info.
                Pause();//Waiting for keypress.
                return; //Exit without adding anything.
            }

            tasks.Add(new TaskItem(title)); //Creates a new taskitem and gives it to the list.
            WriteSuccess($"Task \"{title.Trim()}\" has been added."); //Confirmation on succes.
            Pause(); // Waiting on user can read confirmation info.
        }

        
        private static void ShowTasks() //Show all task.
        {
            Console.Clear(); //Clear the screen.
            PrintHeader("MY TASKS"); //SEction header.

            if (tasks.Count == 0) //If list is empty.
            {
                WriteInfo("No tasks yet."); //Info message.
                return; //Exit method.
            }

            for (int i = 0; i < tasks.Count; i++) //Loop through tasks.
            {
                var t = tasks[i]; //Current task.
                Console.Write($"{i + 1}. "); //Show index number.

                if (t.IsDone) //If completed.
                {
                    SetColor(ConsoleColor.Green); //Set text color to green.
                    Console.Write("[x] "); //Show checkmarks.
                }
                else //If not completed.
                {
                    SetColor(ConsoleColor.Red); //Set text color to red.
                    Console.Write("[ ] "); //Show empty box.
                }

                ResetColor(); //REset console color.
                Console.WriteLine(t.Title); //Show task title.
            }
        }

        
        private static void MarkTaskDone() //Mark task as done.
        {
            Console.Clear(); //Clear screen.
            PrintHeader("MARK AS DONE"); //Section header.

            if (tasks.Count == 0) //No task.
            {
                WriteInfo("There are no tasks to mark."); //info message.
                Pause(); //WAit for user.
                return; //Exit method.
            }

            ShowTasks(); //Show current task.
            Console.Write("\nEnter the number of the task to mark as done: "); //Ask for number.
            string? input = Console.ReadLine(); //Read input.

            if (!int.TryParse(input, out int index)) //Validate number.
            {
                WriteError("Please enter a valid number."); //Error message.
                Pause(); //Wait.    
                return; //Exit.
            }

            if (index < 1 || index > tasks.Count) //Check valid range.
            {
                WriteError("That number does not exist."); //Error message.
                Pause();//Wait.
                return; //Exit.
            }

            var task = tasks[index - 1]; //Get task from list.

            if (task.IsDone) //ALready done.
            {
                WriteInfo("That task is already marked as done."); //Info message.
            }
            else //Not done yet.
            {
                task.MarkDone(); //Mark task as done.
                WriteSuccess($"\"{task.Title}\" is now marked as done."); //Succes message.
            }

            Pause(); //Wait before running.
        }

        
        private static void PrintHeader(string title) //Print section header.
        {
            Console.WriteLine("=== " + title.ToUpper() + " ==="); //Show header.
        }

        private static void Pause() // Pause program until a key is pressed.
        {
            Console.Write("\nPress any key to continue..."); //Prompt
            Console.ReadKey(true); //Wait for key without showing it.
        }

        private static void WriteSuccess(string msg) //Succes message in green color.
        {
            SetColor(ConsoleColor.Green); //Set red text.
            Console.WriteLine(msg); //Print message.
            ResetColor(); //Reset color.
        }

        private static void WriteError(string msg) //Show error message in red.
        {
            SetColor(ConsoleColor.Red); //Set red text.
            Console.WriteLine(msg); //Print message.
            ResetColor(); //Reset color.
        }

        private static void WriteInfo(string msg) // Show info message in yellow.
        {
            SetColor(ConsoleColor.Yellow); //Set yellow text.
            Console.WriteLine(msg); //Message.
            ResetColor();//Reset color.
        }

        private static void SetColor(ConsoleColor color) => Console.ForegroundColor = color; //Apply given color.
        private static void ResetColor() => Console.ResetColor(); //Reset text color to default/Normal colors again.
    }
}
