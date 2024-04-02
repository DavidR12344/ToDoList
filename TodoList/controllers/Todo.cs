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
        /// <param name="isComplete"></param>
        /// <param name="project"></param>
        public void Add(string title, DateTime dueDate, bool isComplete, string project)
        {
            Task task = new Task(title, dueDate, isComplete, project);
            tasks.Add(task);
            Console.WriteLine("Added new task");
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
                    if (bool.TryParse(Console.ReadLine(), out bool newIsComplete))
                    {
                        Console.WriteLine("Enter new project:");
                        string newProject = Console.ReadLine();

                        tasks[index].Title = newTitle;
                        tasks[index].DueDate = newDueDate;
                        tasks[index].IsComplete = newIsComplete;
                        tasks[index].Project = newProject;

                        Console.WriteLine("Task updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for completion status.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
            }
            else
            {
                Console.WriteLine("Invalid task index.");
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
                tasks[index].IsComplete = true;
                Console.WriteLine("Task marked as completed.");
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
                Console.WriteLine("Task removed successfully.");
            }
            else
            {
                Console.WriteLine("Invalid task index.");
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
                                bool isCompleted = (parts[1].ToLower() == "complete");
                                DateTime dueDate = DateTime.ParseExact(parts[2], "yyyy-MM-dd", null);
                                string project = parts[3];
                                Task task = new Task(title, dueDate, isCompleted, project);
                                tasks.Add(task);
                            }
                        }
                    }
                    Console.WriteLine("Tasks loaded from file successfully!");
                }
                else
                {
                    Console.WriteLine("File not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tasks from file: {ex.Message}");
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
                        writer.WriteLine($"{task.Title},{(task.IsComplete ? "Completed" : "Pending")},{task.DueDate:yyyy-MM-dd},{task.Project}");
                    }
                }
                Console.WriteLine("Saved to file!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        /// <summary>
        /// Sort task by date and display it
        /// </summary>

        public void SortByDate()
        {
            tasks.Sort((t1, t2) => t1.DueDate.CompareTo(t2.DueDate));
            Console.WriteLine("Tasks sorted by due date.");
            View();
        }

        /// <summary>
        /// Sort task by project annd display it
        /// </summary>

        public void SortByProject()
        {
            tasks.Sort((t1, t2) => t1.Project.CompareTo(t2.Project));
            Console.WriteLine("Tasks sorted by project.");
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
                    Console.WriteLine($"{i + 1}. {tasks[i].Title} - {(tasks[i].IsComplete ? "Completed" : "Pending")} - Due Date: {tasks[i].DueDate.ToString("yyyy-MM-dd")} - Project: {tasks[i].Project}");
                }
            }
        }
    }
}
