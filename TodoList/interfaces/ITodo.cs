using System;
using System.Collections.Generic;
using System.IO;
namespace TodoList.interfaces
{

    public interface ITodo
    {
        void Add(string title, DateTime dueDate, bool isComplete, string project);

        void Update(int index);
        void Mark(int index);

        void Remove(int index);
        void View();
        void SaveToFile(string path);
        void ReadFromFile(string path);
        void sortByDate();
        void sortByProject();

    }
}
