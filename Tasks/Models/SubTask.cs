using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tasks.Models
{
    public class SubTask
    {
        [Key]
        public int SubTaskId { get; set; }

        public bool Completed {get; set;}
        
        [Required, Display(Name = "Text"), DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public int TaskId { get; set; }
        
        public virtual Task Task { get; set; }
    }
}