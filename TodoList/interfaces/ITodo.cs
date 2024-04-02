using System;
using System.Collections.Generic;

namespace TodoList.Interfaces // Changed namespace to match convention
{
    public interface ITodo
    {
        // Method to add a new task to the to-do list
        void Add(string title, DateTime dueDate, bool isComplete, string project);

        // Method to update an existing task in the to-do list based on its index
        void Update(int index);

        // Method to mark a task as complete/incomplete based on its index
        void Mark(int index);

        // Method to remove a task from the to-do list based on its index
        void Remove(int index);

        // Method to view all tasks in the to-do list
        void View();

        // Method to save the current state of the to-do list to a file
        void SaveToFile(string path);

        // Method to read tasks from a file and populate the to-do list
        void ReadFromFile(string path);

        // Method to sort tasks in the to-do list by due date
        void SortByDate();

        // Method to sort tasks in the to-do list by project
        void SortByProject();
    }
}
