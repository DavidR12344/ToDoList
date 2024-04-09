using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.models
{
    public class Task
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dueDate"></param>
        /// <param name="status"></param>
        /// <param name="project"></param>
        public Task(string title, DateTime dueDate, bool status, string project)
        {
            Title = title;
            DueDate = dueDate;
            Status = status;
            Project = project;
        }

        //Properties

        public string Title { get; set; } // Get and set method for Title 
        public DateTime DueDate { get; set; } // Get and set method for due date

        public bool Status { get; set; } // Get and set method for status

        public string Project { get; set; } // Get and set method for Project


    }
}
