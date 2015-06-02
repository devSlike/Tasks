using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tasks.Models
{
    public class Task
    {
        public Task()
        {
            this.SubTasks = new HashSet<SubTask>();
        }

        [Key]
        public int TaskId { get; set; }
        
        [Display(Name = "Date"), DataType(DataType.Date)]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "Enter task name"), StringLength(100), Display(Name = "Task name"), DataType(DataType.Text)]
        public string TaskName { get; set; }
        
        [Display(Name = "Completed")]
        public bool Completed { get; set; }
        
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual HashSet<SubTask> SubTasks { get; set; }
    }
}