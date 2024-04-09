using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoList.Interfaces;
using Task = TodoList.models.Task;

namespace TodoList.controllers
{
    public class Todo : ITodo
    {
        private List<Task> tasks;

        /// <summary>
        /// Todo constructor and initialize list
        /// </summary>

        public Todo()
        {
            tasks = new List<Task>();
        }


        /// <summary>
        /// Add a new task to a list
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dueDate"></param>
        /// <param name="status"></param>
        /// <param name="project"></param>
        public void Add(string title, DateTime dueDate, bool status, string project)
        {
            Task task = new Task(title, dueDate, status, project);
            tasks.Add(task);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Added new task");
            Console.ResetColor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>

        public void Update(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                Console.WriteLine("Enter new title:");
                string newTitle = Console.ReadLine();
                Console.WriteLine("Enter new due date (yyyy-MM-dd):");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime newDueDate))
                {
                    Console.WriteLine("Is task complete? (true/false):");
                    if (bool.TryParse(Console.ReadLine(), out bool newStatus))
                    {
                        Console.WriteLine("Enter new project:");
                        string newProject = Console.ReadLine();

                        tasks[index].Title = newTitle;
                        tasks[index].DueDate = newDueDate;
                        tasks[index].Status = newStatus;
                        tasks[index].Project = newProject;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Task updated successfully.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input for completion status.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date format.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid task index.");
                Console.ResetColor();
            }
        }



        /// <summary>
        /// Mark a task as completed on an index
        /// </summary>
        /// <param name="index"></param>

        public void Mark(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks[index].Status = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Task marked as completed.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Invalid task index.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>

        public void Remove(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks.RemoveAt(index);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Task removed successfully.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid task index.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Read file from a specific path
        /// </summary>
        /// <param name="path"></param>
        public void ReadFromFile(string path)
        {
            try
            {
                // Check if the file exists without the .txt extension
                if (!File.Exists(path) && File.Exists(path + ".txt"))
                {
                    path += ".txt"; // Append .txt extension to the path
                }

                if (File.Exists(path))
                {
                    tasks.Clear();
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts.Length == 4)
                            {
                                string title = parts[0];
                                bool status = (parts[1].ToLower() == "complete");
                                DateTime dueDate = DateTime.ParseExact(parts[2], "yyyy-MM-dd", null);
                                string project = parts[3];
                                Task task = new Task(title, dueDate, status, project);
                                tasks.Add(task);
                            }
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tasks loaded from file successfully!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("File not found.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error loading tasks from file: {ex.Message}");
                Console.ResetColor();
            }
        }


        /// <summary>
        /// Save task to a new file with a specific path
        /// </summary>
        /// <param name="path"></param>
        public void SaveToFile(string path)
        {
            try
            {
                // Ensure the file path has a .txt extension
                if (!path.EndsWith(".txt"))
                {
                    path += ".txt";
                }

                using (StreamWriter writer = new StreamWriter(path))
                {
                    foreach (var task in tasks)
                    {
                        writer.WriteLine($"{task.Title},{(task.Status ? "Completed" : "Pending")},{task.DueDate:yyyy-MM-dd},{task.Project}");
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Saved to file!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error saving to file: {ex.Message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Sort task by date and display it
        /// </summary>

        public void SortByDate()
        {
            tasks.Sort((t1, t2) => t1.DueDate.CompareTo(t2.DueDate));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Tasks sorted by due date.");
            Console.ResetColor();
            View();
        }

        /// <summary>
        /// Sort task by project annd display it
        /// </summary>

        public void SortByProject()
        {
            tasks.Sort((t1, t2) => t1.Project.CompareTo(t2.Project));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Tasks sorted by project.");
            Console.ResetColor();
            View();
        }

        /// <summary>
        /// View a task in a tasks list.
        /// </summary>

        public void View()
        {
            Console.WriteLine("Tasks:");
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks.");
            }
            else
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i].Title} - {(tasks[i].Status ? "Completed" : "Pending")} - Due Date: {tasks[i].DueDate.ToString("yyyy-MM-dd")} - Project: {tasks[i].Project}");
                }
            }
        }
    }
}
