using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tasks.Models
{
    public class Category
    {
        public Category()
        {
            this.Tasks = new HashSet<Task>();
        }

        [Key]
        public int CategoryId { get; set; }
        
        [Required, Display(Name = "Category name")]
        public string CategoryName { get; set; }
        public string SecondName { get; set; }

        public virtual HashSet<Task> Tasks { get; set; }
    }
}
